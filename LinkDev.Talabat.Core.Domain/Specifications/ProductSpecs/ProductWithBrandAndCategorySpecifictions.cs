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
		public ProductWithBrandAndCategorySpecifictions() : base()
		{
			Includes.Add(p => p.Brand!);

			Includes.Add(p => p.Category!);

		}

		public ProductWithBrandAndCategorySpecifictions(int Id) : base(Id)
		{

			Includes.Add(p => p.Brand!);

			Includes.Add(p => p.Category!);
		}
	}
}
