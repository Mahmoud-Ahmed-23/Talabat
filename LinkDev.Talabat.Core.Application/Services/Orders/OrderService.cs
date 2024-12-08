﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infratructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
	public class OrderService(UnitOfWork unitOfWork, IMapper mapper, IBasketService basketService) : IOrderService
	{
		public async Task<OrderToRetunDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
		{
			//1-get basket fom basket repo
			var basket = await basketService.GetCustomerBasketAsync(order.BasketId);
			//2- get selected items basket from products repo
			var orderItems = new List<OrderItem>();
			if (basket.Items.Count() > 0)
			{
				var productRepo = unitOfWork.GetRepsitory<Product, int>();
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

			var orderToCreate = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = address,
				Subtotal = subTotal,
				Items = orderItems,
				DeliveryhMethodId = order.DeliveryMethodId,
				CreatedBy = "",
				LastModifiedBy = "",
			};
			await unitOfWork.GetRepsitory<Order, int>().AddAsync(orderToCreate);
			//6-save to database
			var created = await unitOfWork.CompleteAsync() > 0;
			if (!created) throw new BadRequestException("an error has been occured during creating order");
			return mapper.Map<OrderToRetunDto>(orderToCreate);
		}
		public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
		{
			throw new NotImplementedException();
		}
		public Task<OrderToRetunDto> GetOrderByIdAsync(string buyerEmail, int orderId)
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<OrderToRetunDto>> GetOrdersForUserAsync(string buyerEmail)
		{
			throw new NotImplementedException();
		}
	}
}