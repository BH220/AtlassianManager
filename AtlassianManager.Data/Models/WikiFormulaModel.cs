using Newtonsoft.Json;
using System.ComponentModel;

namespace AtlassianManager.Data.Models
{
	[JsonObject]
	public class WikiFormulaModel
	{
		[JsonProperty]
		[DefaultValue(0)]
		public int Id { get; set; }

		//[JsonProperty]
		//[DefaultValue(0)]
		//public int ParentId { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 제목 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 제조사 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 제조사연락처 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 키워드 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 랩넘버 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 내용물단위 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 내용물단가 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 임가공단가 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 완제품단가 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 추천용량 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 실제용량 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 규제 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string CTK내부개발담당자 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 제안브랜드 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string 관련제품개발프로젝트 { get; set; }

		[JsonProperty]
		[DefaultValue(0)]
		public string SalesLibraryLinkCode { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string WhatItIs { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string Benefit { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Labels { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 분류코드 { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string Launching { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string LabelEx { get; set; }

		[JsonProperty]
		[DefaultValue(1)]
		public int FactsheetCount { get; set; }

		[JsonProperty]
		[DefaultValue("")]
		public string 패키지옵션 { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_ProductCode{ get; set; }

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
		//public string Pre_Price { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Benefits { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_Size { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_SaleLibrary { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_RelatedProject { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_ViewerLink { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_WhatItIs { get; set; }

		//[JsonProperty]
		//[DefaultValue("")]
		//public string Pre_HowToUse { get; set; }
	}
}