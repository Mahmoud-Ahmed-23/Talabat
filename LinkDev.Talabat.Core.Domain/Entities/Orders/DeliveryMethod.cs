using LinkDev.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
	public class DeliveryMethod : BaseAuditableEntity<int>
	{
		public required string ShortName { get; set; }
		public required string Descryption { get; set; }
		public decimal Cost { get; set; }
		public required string DeliveryTime { get; set; }
	}
}
