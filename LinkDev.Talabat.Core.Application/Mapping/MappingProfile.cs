using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Basket.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Order.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Product.Models;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Mapping
{
	internal class MappingProfile : Profile
	{
		public MappingProfile()
		{
			#region Product Mapping
			CreateMap<ProductBrand, BrandDto>();

			CreateMap<ProductCategory, CategoryDto>();

			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand!.Name))
				.ForMember(d => d.Category, o => o.MapFrom(s => s.Category!.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureURLResolver>());

			#endregion

			#region Basket Mapping

			CreateMap<BasketItem, BasketItemDto>().ReverseMap();

			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

			#endregion

			#region Order Mapping

			CreateMap<Order, OrderToReturnDto>()
				.ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod!.ShortName));
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
				.ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
				.ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureURLResolver>());
			CreateMap<Address, AddressDto>().ReverseMap();
			CreateMap<DeliveryMethod, DeliveryMethodDto>();

			#endregion
		}
	}
}
