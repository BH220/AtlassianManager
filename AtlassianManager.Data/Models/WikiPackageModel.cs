using Newtonsoft.Json;
using System.ComponentModel;

namespace AtlassianManager.Data.Models
{
	[JsonObject]
	public class WikiPackageModel
	{
		[JsonProperty]
		[DefaultValue(0)]
		public int Id { get; set; }

		//[JsonProperty]
		//[DefaultValue(0)]
		//public int ParentId { get; set; }

		/// <summary>
		/// Title,
		/// </summary>
		[JsonProperty]
		[DefaultValue("")]
		public string 제목 { get; set; }

		/// <summary>
		/// MANUFACTURER
		/// </summary>
		[JsonProperty]
		[DefaultValue("")]
		public string 제조사 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 연락처 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 키워드 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string MOQ { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 통화 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 가격 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 가격업데이트날짜 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 용량 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 용량단위 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 포뮬라타입 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string Material { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 런칭제품 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string SalesLibraryLinkCode { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string Description { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Labels { get; set; }
		[JsonProperty]
		[DefaultValue("")]
		public string 분류코드 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 용량구분 { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string LabelEx { get; set; }

		[JsonProperty]
		[DefaultValue(1)]
		public int FactsheetCount { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 포뮬라옵션 { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_ProductCode { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_LabNo { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Manufacturer { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_LaunchType { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_LaunchStatus { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Volume { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Price { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Benefits { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Size { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_FormulaType { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_SalesLibrary { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_RelatedProject { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Material { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_ViewerLink { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Description { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_WhatItIs { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_HowToUse { get; set; }

		//[JsonProperty]
		//[DefaultValue(true)]
		//public bool Visible { get; set; }

		//[JsonProperty]
		//[DefaultValue(false)]
		//public bool NowUpdate { get; set; }
	}
}