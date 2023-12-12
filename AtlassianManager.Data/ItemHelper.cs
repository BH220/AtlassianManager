using AtlassianManager.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace AtlassianManager.Data
{
	public class ItemHelper
	{
		private static ItemHelper _instance = null;

		public static ItemHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ItemHelper();
					_instance.InitCategory();
				}
				return _instance;
			}
		}

		public List<ItemModel> ListAll = new List<ItemModel>();

		public List<ItemModel> FormulaList
		{
			get
			{
				return ListAll.Where(x => x.Type == Types.Formula).ToList();
			}
		}

		public List<ItemModel> PackageList
		{
			get
			{
				return ListAll.Where(x => x.Type == Types.Package).ToList();
			}
		}

		private void InitCategory()
		{
			ListAll = new List<ItemModel>();
		}

		public bool IsExistsItem(Types Type, string name)
		{
			return ListAll.Any(x => x.Type == Type && x.Name == name);
		}

		public string AddItem(Types Type, string ProduceCode, string Manufacturer, string Keyword, string Category, string Name, string Labno)
		{
			if (ListAll.Any(x => x.Type == Type && x.Name == Name))
			{
				return string.Format("[{0}] {1} 카테고리에 {2} 제품이 이미 있습니다.", ProduceCode, Type, Name);
			}
			else
			{
				ListAll.Add(new ItemModel(Type, ProduceCode, Manufacturer, Keyword, Category, Name, Labno));
			}
			return string.Empty;
		}
	}
}