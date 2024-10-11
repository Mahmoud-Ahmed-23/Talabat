using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.ProductSpecs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int PageSize, int PageIndex) :
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

			ApplyPagination((PageIndex - 1) * PageSize, PageSize);


		}

		public ProductWithBrandAndCategorySpecifications(int Id) : base(Id)
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
