using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.OrderSpecs;
using LinkDev.Talabat.Infratructure.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
	public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketService basketService) : IOrderService
	{
		public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
		{
			//1-get basket fom basket repo
			var basket = await basketService.GetCustomerBasketAsync(order.BasketId);
			//2- get selected items basket from products repo
			var orderItems = new List<OrderItem>();
			if (basket.Items.Count() > 0)
			{
				var productRepo = unitOfWork.GetRepository<Product, int>();
				foreach (var item in basket.Items)
				{
					var product = await productRepo.GetAsync(item.Id);
					if (product != null)
					{
						var productItemOrdered = new ProductItemOrdered()
						{
							ProductId = product.Id,
							ProductName = product.Name,
							PictureUrl = product.PictureUrl ?? ""
						};
						var orderItem = new OrderItem()
						{
							Product = productItemOrdered,
							Price = item.Price,
							Quantity = item.Quantity,
							CreatedBy = "",
							LastModifiedBy = "",
						};
						orderItems.Add(orderItem);
					}
				}
			}
			var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

			var address = mapper.Map<Address>(order.ShippingAddress);

			var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(order.DeliveryMethodId);

			var orderToCreate = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = address,
				Subtotal = subTotal,
				Items = orderItems,
				DeliveryMethod = deliveryMethod,
				CreatedBy = "",
				LastModifiedBy = "",
			};
			await unitOfWork.GetRepository<Order, int>().AddAsync(orderToCreate);
			//6-save to database
			var created = await unitOfWork.CompleteAsync() > 0;
			if (!created) throw new BadRequestException("an error has been occured during creating order");
			return mapper.Map<OrderToReturnDto>(orderToCreate);
		}
		public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
		{
			var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

			return mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
		}
		public async Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
		{
			var orderSpecs = new OrderSpecifications(buyerEmail, orderId);

			var order = await unitOfWork.GetRepository<Order, int>().GetWithSpecAsync(orderSpecs);

			if (order is null) throw new NotFoundException(nameof(Order), orderId);

			return mapper.Map<OrderToReturnDto>(order);
		}
		public async Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
		{
			var orderSpecs = new OrderSpecifications(buyerEmail);

			var orders = await unitOfWork.GetRepository<Order, int>().GetAllWithSpecAsync(orderSpecs);

			return mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
		}
	}
}