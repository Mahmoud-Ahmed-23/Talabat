using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using Shared.Exception;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Stripe.V2;
using Product = LinkDev.Talabat.Core.Domain.Entities.Products.Product;
namespace LinkDev.Talabat.Infratructure.Payment
{
	public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IOptions<RedisSettings> redisSettings) : IPaymentService
	{
		private readonly RedisSettings _redisSettings = redisSettings.Value;
		public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await basketRepository.GetAsync(basketId);
			if (basket is null) throw new NotFoundException(nameof(CustomerBasket), basketId);

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
				if (deliveryMethod is null) throw new NotFoundException(nameof(DeliveryMethod), basket.DeliveryMethodId.Value);
				basket.ShippingPrice = deliveryMethod.Cost;

			}

			if (basket.Items.Count() > 0)
			{
				var productRepo = unitOfWork.GetRepository<Product, int>();
				foreach (var item in basket.Items)
				{
					var product = await productRepo.GetAsync(item.Id);

					if (product is null) throw new NotFoundException(nameof(Product), item.Id);

					if (item.Price != product.Price)
						item.Price = product.Price;
				}

			}

			PaymentIntent? paymentIntent = null;
			PaymentIntentService paymentIntentService = new PaymentIntentService();
			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
					Currency = "USD",
					PaymentMethodTypes = new List<string>() { "Card" }

				};
				paymentIntent = await paymentIntentService.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;


			}
			else
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)basket.ShippingPrice * 100,
				};
				await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);

			}

			await basketRepository.UpdateAsync(basket, TimeSpan.FromDays(_redisSettings.TimeToLiveInDays));
			return basket;
		}
	}
}
