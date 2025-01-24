
using System.Text.Json.Serialization;


namespace MobileKoisk.Models
{
    public class SaleItem
    {
		public string sale_name { get; set; }
		public string sale_description { get; set; }
		public string sale_date { get; set; }
	}

	[JsonSerializable(typeof(List<SaleItem>))]
	internal sealed partial class SaleItemContext : JsonSerializerContext
	{
	}
}
