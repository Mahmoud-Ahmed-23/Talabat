using LinkDev.Talabat.Core.Application.Abstraction.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Order.Models
{
	public class OrderToRetunDto
	{
		public required int Id { get; set; }
		public required string BuyerEmail { get; set; }
		public DateTime OrderDate { get; set; }
		public required string Status { get; set; }
		public required AddressDto ShippingAddress { get; set; }
		public int? DeliveryhMethodId { get; set; }
		public string? DeliveryMethod { get; set; }
		public virtual required ICollection<OrderItemDto> Items { get; set; }
		public decimal Subtotal { get; set; }
		public decimal Total { get; set; }
	}
}
