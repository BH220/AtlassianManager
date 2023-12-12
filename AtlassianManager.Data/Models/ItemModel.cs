using BH_Library.Utils;
using System.Linq;

namespace AtlassianManager.Data.Models
{
	public class ItemModel
	{
		public Types Type { get; set; }
		public string ProduceCode { get; set; }
		public string Manufacturer { get; set; }
		public string Keyword { get; set; }
		public string Category { get; set; }
		public string Name { get; set; }
		public string Labno { get; set; }

		public ItemModel(Types Type, string ProduceCode, string Manufacturer, string Keyword, string Category, string Name, string Labno)
		{
			this.Type = Type;
			this.ProduceCode = ProduceCode;
			this.Manufacturer = Manufacturer;
			this.Keyword = Keyword;
			this.Category = Category;
			this.Name = Name;
			this.Labno = Labno;
		}

		public string Argument
		{
			get
			{//string fat = "--action copyPage--space \"FORMULALIB\"--title \"TF\"--newTitle \"newA112\"--descendents--parent \"Cream_Emuilsion\"--findReplace \"%wha%#qwersdfqwerasdf\"--special \" #\" --labels \"data1,data2\"--replace--noConvert";
				string fat = " --action copyPage  --space \"{0}\"  --title \"{1}\" --newTitle \"{2}\" --descendents --parent \"{3}\" --findReplace \"{4}\" --special \" #\"  --labels \"{5}\" --encoding \"utf-8\" --replace --noConvert ";

				string space = Type == Types.Formula ? "FORMULALIB" : "PACKAGELIB";
				string title = Type == Types.Formula ? "TF" : "TP";
				string newTitle = Name;
				var ccat = Category.Replace("|44", "").Replace("44", "").Replace("|45", "").Replace("45", "");
				var lastCategoty = ccat.Split(new char[] { '|' }).ToList().Select(x => x.Trim()).ToList().Last().ToIntEx();
				int parent = -1;
				CategoryModel cm = null;

				if (Type == Types.Formula)
					cm = CategoryHelper.Instance.FormulaList.Where(x => x.SalesLibraryID == lastCategoty).FirstOrDefault();
				else
					cm = CategoryHelper.Instance.PackageList.Where(x => x.SalesLibraryID == lastCategoty).FirstOrDefault();
				if (cm != null)
					parent = cm.WikiID;
				string replace = "(%producecode%)#" + ProduceCode + ",";
				if (string.IsNullOrEmpty(Manufacturer) == false)
					replace += string.Format("(%manufacturer%)#{0},", Manufacturer);
				if (string.IsNullOrEmpty(Labno) == false)
					replace += string.Format("(%labno%)#{0},", Labno);

				if (replace.EndsWith(",")) replace = replace.Substring(0, replace.Length - 1);

				string labels = string.Format("{0},", cm.Name);
				int currentId = CategoryHelper.Instance.ListAll.FirstOrDefault(x => x.SalesLibraryID == lastCategoty).WikiParentID;
				while (currentId > 0)
				{
					var parentcate = CategoryHelper.Instance.ListAll.FirstOrDefault(x => x.WikiID == currentId);
					if (parentcate != null)
					{
						labels += string.Format("{0},", parentcate.Name);
						currentId = parentcate.WikiParentID;
					}
					else
						break;
				}

				return string.Format(fat, space, title, newTitle, parent, replace, labels);
			}
		}
	}
}