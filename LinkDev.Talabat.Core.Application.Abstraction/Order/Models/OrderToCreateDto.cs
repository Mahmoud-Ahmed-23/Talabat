using LinkDev.Talabat.Core.Application.Abstraction.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order.Models
{
	public class OrderToCreateDto
	{
		public required int BasketId { get; set; }
		public int DeliveryMethodId { get; set; }
		public required AddressDto ShippingAddress { get; set; }

	}
}
