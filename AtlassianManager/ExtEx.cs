using BH_Library.Utils;
using DevExpress.Spreadsheet;

namespace AtlassianManager
{
	public static class ExtEx
	{
		public static object GetParseValue(this CellValue cell)
		{
			if (cell.IsNumeric)
				return cell.NumericValue;
			else
				return cell.TextValue;
		}

		public static string GetStr(this CellValue cell)
		{
			if (cell.IsNumeric)
				return cell.NumericValue.ToStringEx();
			else
				return cell.TextValue.ToStringEx();
		}

		public static int GetInt(this CellValue cell)
		{
			if (cell.IsNumeric)
				return cell.NumericValue.ToIntEx();
			else
				return cell.TextValue.ToIntEx();
		}
	}
}