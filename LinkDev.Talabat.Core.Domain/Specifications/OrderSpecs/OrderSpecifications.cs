using LinkDev.Talabat.Core.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.OrderSpecs
{
	public class OrderSpecifications : BaseSpecifications<Order, int>
	{
		public OrderSpecifications(string buyerEmail, int orderId) : base(order => order.Id == orderId && order.BuyerEmail == buyerEmail)
		{
			AddInclude();
		}
		public OrderSpecifications(string buyerEmail) : base(order => order.BuyerEmail == buyerEmail)
		{
			AddInclude();
			AddOrderDesc(order => order.OrderDate);
		}
		private protected override void AddInclude()
		{
			base.AddInclude();
			Includes.Add(order => order.Items);
			Includes.Add(order => order.DeliveryMethod!);
		}
	}
}
