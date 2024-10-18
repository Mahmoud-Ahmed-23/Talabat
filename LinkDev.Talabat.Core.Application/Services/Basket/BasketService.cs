using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Models;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LinkDev.Talabat.Core.Application.Services.Basket
{
	internal class BasketService(IBasketRepository _basketRepository, IMapper _mapper, IConfiguration _configuration) : IBasketService
	{


		public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)

		{
			var basket = await _basketRepository.GetAsync(basketId);

			//if (basket == null)
			//{
			//	throw new NotFoundException
			//}

			return _mapper.Map<CustomerBasketDto>(basket);
		}
		
		
		public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
		{
			var basket = _mapper.Map<CustomerBasket>(basketDto);

			var timeToLive = TimeSpan.FromDays(double.Parse(_configuration.GetSection("RedisSettings")["DaysToLive"]!));

			var updatedBasket = await _basketRepository.UpdateAsync(basket, timeToLive);

			//if(updatedBasket == null)
			//{
			//	throw new BadRequestException("")
			//}

			return basketDto;
		}


		public async Task DeleteCustomerBasketAsync(string basketId)
		{
			var deleted = await _basketRepository.DeleteAsync(basketId);

			//if(!deleted)
			//{
			//	throw new BadRequestException("")
			//}
		}
	}
}
