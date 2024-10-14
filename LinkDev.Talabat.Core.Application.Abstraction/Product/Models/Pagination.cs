namespace LinkDev.Talabat.Core.Application.Abstraction.Product.Models
{
	public class Pagination<T>
	{
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 5;
		public int Count { get; set; }
		public required IEnumerable<T> Data { get; set; }

		public Pagination(int pageIndex, int pageSize, int count)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Count = count;
		}
	}
}
