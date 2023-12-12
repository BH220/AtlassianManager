using Newtonsoft.Json;
using System.ComponentModel;

namespace AtlassianManager.Data.Models
{
	[JsonObject]
	public class WikiAttachFileModel
	{
		[JsonProperty]
		[DefaultValue("")]
		public string Title { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public double Id { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public double ParentId { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public decimal FileSize { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string FileName { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string Url { get; set; }
	}
}