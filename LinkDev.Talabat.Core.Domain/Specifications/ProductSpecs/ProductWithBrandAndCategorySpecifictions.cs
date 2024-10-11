using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifictions : BaseSpecifications<Product, int>
	{
		public ProductWithBrandAndCategorySpecifictions(string? sort, int? brandId, int? categoryId) :
			base(

			p =>
			(!brandId.HasValue || p.BrandId == brandId.Value)
			&&
			(!categoryId.HasValue || p.CategoryId == categoryId.Value)
		)

		{
			AddInclude();

			switch (sort)
			{
				case "nameDesc":
					AddOrderDesc(p => p.Name);
					break;
				case "priceAsc":
					AddOrder(p => p.Price);
					break;
				case "priceDesc":
					AddOrderDesc(p => p.Price);
					break;
				default:
					AddOrder(p => p.Name);
					break;

			}
		}

		public ProductWithBrandAndCategorySpecifictions(int Id) : base(Id)
		{
			AddInclude();
		}

		private protected override void AddInclude()
		{
			base.AddInclude();

			Includes.Add(p => p.Brand!);

			Includes.Add(p => p.Category!);
		}

	}
}
