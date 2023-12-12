using AtlassianManager.Data.Models;
using BH_Library.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AtlassianManager.Data
{
	public class CategoryHelper
	{
		private static CategoryHelper _instance = null;

		public static CategoryHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new CategoryHelper();
					_instance.InitCategory();
				}
				return _instance;
			}
		}

		public List<CategoryModel> ListAll = new List<CategoryModel>();

		public List<CategoryModel> FormulaList
		{
			get
			{
				return ListAll.Where(x => x.Type == Types.Formula).ToList();
			}
		}

		public List<CategoryModel> PackageList
		{
			get
			{
				return ListAll.Where(x => x.Type == Types.Package).ToList();
			}
		}

		private void InitCategory()
		{
			ListAll = new List<CategoryModel>();
			ListAll.Add(new CategoryModel(Types.Formula, 42, 2392305, 0, "Formula", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Formula"));
			ListAll.Add(new CategoryModel(Types.Formula, 46, 2392351, 2392305, "Skincare", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Skincare"));
			ListAll.Add(new CategoryModel(Types.Formula, 67, 3113411, 2392351, "Cream / Emulsion", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=3113411"));
			ListAll.Add(new CategoryModel(Types.Formula, 68, 2392353, 2392351, "Serum / Oil", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392353"));
			ListAll.Add(new CategoryModel(Types.Formula, 69, 2392354, 2392351, "Mist / Toner", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392354"));
			ListAll.Add(new CategoryModel(Types.Formula, 82, 2392355, 2392351, "Mask", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Mask"));
			ListAll.Add(new CategoryModel(Types.Formula, 83, 2392356, 2392351, "EyeCare", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/EyeCare"));
			ListAll.Add(new CategoryModel(Types.Formula, 89, 2392357, 2392351, "Cleansing", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Cleansing"));
			ListAll.Add(new CategoryModel(Types.Formula, 91, 2392358, 2392351, "Suncare", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Suncare"));
			ListAll.Add(new CategoryModel(Types.Formula, 47, 2392359, 2392305, "BaseMakeup", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/BaseMakeup"));
			ListAll.Add(new CategoryModel(Types.Formula, 70, 2392360, 2392359, "Foundations", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Foundations"));
			ListAll.Add(new CategoryModel(Types.Formula, 84, 2392361, 2392359, "TintedMoisturizer / BB / CC", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392361"));
			ListAll.Add(new CategoryModel(Types.Formula, 71, 2392362, 2392359, "Primer", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Primer"));
			ListAll.Add(new CategoryModel(Types.Formula, 72, 2392363, 2392359, "Concealers", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Concealers"));
			ListAll.Add(new CategoryModel(Types.Formula, 73, 2392364, 2392359, "Countouring / Highlighters", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392364"));
			ListAll.Add(new CategoryModel(Types.Formula, 74, 2392365, 2392359, "Blushes / Bronzers", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392365"));
			ListAll.Add(new CategoryModel(Types.Formula, 85, 2392366, 2392359, "Powder / SettingSpray", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392366"));
			ListAll.Add(new CategoryModel(Types.Formula, 48, 2392367, 2392305, "EyeMakeup", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/EyeMakeup"));
			ListAll.Add(new CategoryModel(Types.Formula, 75, 2392368, 2392367, "Eyeshadow", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Eyeshadow"));
			ListAll.Add(new CategoryModel(Types.Formula, 76, 2392369, 2392367, "Eyebrow", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Eyebrow"));
			ListAll.Add(new CategoryModel(Types.Formula, 77, 2392370, 2392367, "Eyeliner", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Eyeliner"));
			ListAll.Add(new CategoryModel(Types.Formula, 78, 2392371, 2392367, "Mascara", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Mascara"));
			ListAll.Add(new CategoryModel(Types.Formula, 49, 2392372, 2392305, "LipMakeup", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/LipMakeup"));
			ListAll.Add(new CategoryModel(Types.Formula, 79, 2392373, 2392372, "Lipstick / LipLiner", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392373"));
			ListAll.Add(new CategoryModel(Types.Formula, 80, 2392374, 2392372, "LiquidLipstick", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/LiquidLipstick"));
			ListAll.Add(new CategoryModel(Types.Formula, 86, 2392375, 2392372, "LipCare", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/LipCare"));
			ListAll.Add(new CategoryModel(Types.Formula, 92, 2392376, 2392305, "Body", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/Body"));
			ListAll.Add(new CategoryModel(Types.Formula, 93, 2392377, 2392376, "BodyCare", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/BodyCare"));
			ListAll.Add(new CategoryModel(Types.Formula, 94, 2392378, 2392376, "HairCare", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/HairCare"));
			ListAll.Add(new CategoryModel(Types.Formula, 95, 2392379, 2392376, "BodyWash", "https://wiki.ctkcosmetics.biz/display/FORMULALIB/BodyWash"));
			ListAll.Add(new CategoryModel(Types.Package, 122, 5046509, 2392305, "Launched", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=5046509"));

			ListAll.Add(new CategoryModel(Types.Package, 43, 2392312, -1, "Package", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392312"));

			ListAll.Add(new CategoryModel(Types.Package, 125, 12746760, 2392312, "E-CATALOG", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746760"));
			ListAll.Add(new CategoryModel(Types.Package, 127, 12747856, 2392312, "EXHIBITION", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747856"));
			ListAll.Add(new CategoryModel(Types.Package, 129, 12747842, 2392312, "CLEAN BEAUTY", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747842"));
			ListAll.Add(new CategoryModel(Types.Package, 131, 12747846, 2392312, "CTK PROPERTY", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747846"));
			ListAll.Add(new CategoryModel(Types.Package, 133, 12747850, 2392312, "TAIWAN", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747850"));
			ListAll.Add(new CategoryModel(Types.Package, 147, 12747854, 2392312, "CHINA", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747854"));

			ListAll.Add(new CategoryModel(Types.Package, 50, 2392412, 2392312, "BLOW", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392412"));
			ListAll.Add(new CategoryModel(Types.Package, 96, 12746882, 2392412, "Blow(Injection, Direct)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746882"));
			ListAll.Add(new CategoryModel(Types.Package, 98, 12746883, 2392412, "Heavy Blow", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746883"));
			ListAll.Add(new CategoryModel(Types.Package, 65, 2392429, 2392412, "TOTTLE", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392429"));

			ListAll.Add(new CategoryModel(Types.Package, 52, 2392414, 2392312, "BRUSH", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392414"));
			ListAll.Add(new CategoryModel(Types.Package, 136, 12747859, 2392414, "All(Brush)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747859"));

			ListAll.Add(new CategoryModel(Types.Package, 87, 2392415, 2392312, "EYEBROW", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392415"));
			ListAll.Add(new CategoryModel(Types.Package, 99, 12747815, 2392415, "Pencil type(eyebrow)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747815"));
			ListAll.Add(new CategoryModel(Types.Package, 103, 12746888, 2392415, "Mascara type(eyebrow)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746888"));
			ListAll.Add(new CategoryModel(Types.Package, 104, 12746889, 2392415, "Dual ended type(eyebrow)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746889"));
			ListAll.Add(new CategoryModel(Types.Package, 137, 12747819, 2392415, "Liquid type(eyebrow)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747819"));

			ListAll.Add(new CategoryModel(Types.Package, 88, 2392416, 2392312, "EYELINER", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392416"));
			ListAll.Add(new CategoryModel(Types.Package, 105, 12746890, 2392416, "Pencil type(eyeliner)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746890"));
			ListAll.Add(new CategoryModel(Types.Package, 106, 12746891, 2392416, "Dual ended type(eyeliner)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746891"));
			ListAll.Add(new CategoryModel(Types.Package, 138, 12747824, 2392416, "Liquid type(eyeliner)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747824"));

			ListAll.Add(new CategoryModel(Types.Package, 53, 2392417, 2392312, "CREAMJAR", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392417"));
			ListAll.Add(new CategoryModel(Types.Package, 107, 12746892, 2392417, "Single wall", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746892"));
			ListAll.Add(new CategoryModel(Types.Package, 108, 12746893, 2392417, "Double wall", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746893"));
			ListAll.Add(new CategoryModel(Types.Package, 109, 12746894, 2392417, "Airless jar", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746894"));

			ListAll.Add(new CategoryModel(Types.Package, 54, 2392418, 2392312, "CUSHION/COMPACT", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392418"));
			ListAll.Add(new CategoryModel(Types.Package, 139, 12747827, 2392418, "Cushion", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747827"));
			ListAll.Add(new CategoryModel(Types.Package, 57, 2392421, 2392418, "COMPACT", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392421"));

			ListAll.Add(new CategoryModel(Types.Package, 55, 2392419, 2392312, "LIPPRODUCTS", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392419"));
			ListAll.Add(new CategoryModel(Types.Package, 110, 12746895, 2392419, "lipstick", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746895"));
			ListAll.Add(new CategoryModel(Types.Package, 111, 12746896, 2392419, "Slim lipstick", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746896"));
			ListAll.Add(new CategoryModel(Types.Package, 112, 12746897, 2392419, "Lip Tint/Gloss", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746897"));
			ListAll.Add(new CategoryModel(Types.Package, 113, 12746898, 2392419, "Back fill type(lip)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746898"));
			ListAll.Add(new CategoryModel(Types.Package, 114, 12746899, 2392419, "Front fill type(lip)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746899"));
			ListAll.Add(new CategoryModel(Types.Package, 115, 12746900, 2392419, "Molding type(lip)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746900"));

			ListAll.Add(new CategoryModel(Types.Package, 56, 2392420, 2392312, "MASCARA", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392420"));
			ListAll.Add(new CategoryModel(Types.Package, 140, 12747866, 2392420, "All(Mascara)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747866"));

			ListAll.Add(new CategoryModel(Types.Package, 58, 2392422, 2392312, "PALLETTE", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392422"));
			ListAll.Add(new CategoryModel(Types.Package, 141, 12747868, 2392422, "All(Palette)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747868"));

			ListAll.Add(new CategoryModel(Types.Package, 60, 2392424, 2392312, "POWDER", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392424"));
			ListAll.Add(new CategoryModel(Types.Package, 142, 12747870, 2392424, "All(Powder)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747870"));

			ListAll.Add(new CategoryModel(Types.Package, 61, 2392425, 2392312, "PUMP", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392425"));
			ListAll.Add(new CategoryModel(Types.Package, 116, 12746901, 2392425, "Airless pump", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746901"));
			ListAll.Add(new CategoryModel(Types.Package, 117, 12746902, 2392425, "Dip tube pump", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746902"));
			ListAll.Add(new CategoryModel(Types.Package, 118, 12746903, 2392425, "Other", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746903"));

			ListAll.Add(new CategoryModel(Types.Package, 63, 2392427, 2392312, "SPOID/AMPULE", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392427"));
			ListAll.Add(new CategoryModel(Types.Package, 143, 12747872, 2392427, "All(Spoid,Ampule)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747872"));

			ListAll.Add(new CategoryModel(Types.Package, 64, 2392428, 2392312, "STICK", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=2392428"));
			ListAll.Add(new CategoryModel(Types.Package, 144, 12747874, 2392428, "Single Stick", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747874"));
			ListAll.Add(new CategoryModel(Types.Package, 145, 12747876, 2392428, "Dual ended type(stick)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747876"));
			ListAll.Add(new CategoryModel(Types.Package, 119, 12746904, 2392428, "Back fill type(stick)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746904"));
			ListAll.Add(new CategoryModel(Types.Package, 120, 12746905, 2392428, "Front fill type(stick)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746905"));
			ListAll.Add(new CategoryModel(Types.Package, 121, 12746906, 2392428, "Molding type(stick)", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12746906"));

			ListAll.Add(new CategoryModel(Types.Package, 66, 3118497, 2392312, "TUBE", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=3118497"));
			ListAll.Add(new CategoryModel(Types.Package, 146, 12747878, 3118497, "TUBE", "https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId=12747878"));
		}

		public string GetAncestorLableFromString(string str)
		{
			string result = "";

			List<int> labels = new List<int>();
			var s = BH_Library.Utils.StringUtil.SplitByString(str.ToStringEx().Replace("|", ","), ",");
			foreach (string ss in s)
			{
				if (string.IsNullOrEmpty(ss)) continue;
				//labels.AddRange(GetAncestorLable(ss.ToIntEx()));
				int label = ss.ToIntEx();
				List<int> result2 = new List<int>();

				CategoryModel cm = ListAll.First(x => x.SalesLibraryID == label);
				result2.Add(cm.SalesLibraryID);

				while (true)
				{
					cm = ListAll.FirstOrDefault(x => x.WikiID == cm.WikiParentID);
					if (cm == null)
						break;
					else
					{
						result2.Add(cm.SalesLibraryID);
						if (cm.WikiParentID == -1)
							break;
					}
				}
				labels.AddRange(result2);
			}
			labels = labels.Distinct().ToList();
			labels.Sort();

			foreach (int lb in labels)
			{
				result += string.Format("{0},", lb);
			}
			if (result.EndsWith(",")) result = result.Substring(0, result.Length - 1);
			return result;
		}

		public List<int> GetAncestorLable(int label)
		{
			List<int> result = new List<int>();

			CategoryModel cm = ListAll.First(x => x.SalesLibraryID == label);
			result.Add(cm.SalesLibraryID);

			while (true)
			{
				cm = ListAll.First(x => x.WikiID == cm.WikiParentID);
				result.Add(cm.SalesLibraryID);
				if (cm.WikiParentID == -1)
					break;
			}

			return result;
		}

		public string GetAncestorLableEx(int label)
		{
			List<int> lst = GetAncestorLable(label);

			string result = "";

			foreach (int idx in lst)
			{
				result += string.Format("{0},", idx);
			}

			if (result.EndsWith(","))
				result = result.Substring(0, result.Length - 1);
			return result;
		}
	}
}