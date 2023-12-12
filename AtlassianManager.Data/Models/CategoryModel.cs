namespace AtlassianManager.Data.Models
{
	public class CategoryModel
	{
		public Types Type { get; set; }
		public int SalesLibraryID { get; set; }
		public string Name { get; set; }
		public int WikiID { get; set; }
		public int WikiParentID { get; set; }
		public string Link { get; set; }

		public CategoryModel(Types type, int SalesLibraryID, int WikiID, int WikiParentID, string Name, string Link)
		{
			this.Type = type;
			this.Name = Name;
			this.SalesLibraryID = SalesLibraryID;
			this.WikiID = WikiID;
			this.WikiParentID = WikiParentID;
			this.Link = Link;
		}
	}
}