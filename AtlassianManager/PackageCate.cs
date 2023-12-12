using System.Collections.Generic;

namespace AtlassianManager
{
	public class PackageCate
	{
		private static PackageCate _instance = null;

		public static PackageCate Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new PackageCate();
					_instance.InitLoad();
				}
				return _instance;
			}
		}

		public List<P_Model> CateList = new List<P_Model>();

		private void InitLoad()
		{
			CateList = new List<P_Model>();
			CateList.Add(new P_Model(96, 50));
			CateList.Add(new P_Model(98, 50));
			CateList.Add(new P_Model(65, 50));
			CateList.Add(new P_Model(136, 52));
			CateList.Add(new P_Model(99, 87));
			CateList.Add(new P_Model(103, 87));
			CateList.Add(new P_Model(104, 87));
			CateList.Add(new P_Model(137, 87));
			CateList.Add(new P_Model(105, 88));
			CateList.Add(new P_Model(106, 88));
			CateList.Add(new P_Model(138, 88));
			CateList.Add(new P_Model(107, 53));
			CateList.Add(new P_Model(108, 53));
			CateList.Add(new P_Model(109, 53));
			CateList.Add(new P_Model(139, 54));
			CateList.Add(new P_Model(57, 54));
			CateList.Add(new P_Model(110, 55));
			CateList.Add(new P_Model(111, 55));
			CateList.Add(new P_Model(112, 55));
			CateList.Add(new P_Model(113, 55));
			CateList.Add(new P_Model(114, 55));
			CateList.Add(new P_Model(115, 55));
			CateList.Add(new P_Model(140, 56));
			CateList.Add(new P_Model(141, 58));
			CateList.Add(new P_Model(142, 60));
			CateList.Add(new P_Model(116, 61));
			CateList.Add(new P_Model(117, 61));
			CateList.Add(new P_Model(118, 61));
			CateList.Add(new P_Model(143, 63));
			CateList.Add(new P_Model(144, 64));
			CateList.Add(new P_Model(145, 64));
			CateList.Add(new P_Model(119, 64));
			CateList.Add(new P_Model(120, 64));
			CateList.Add(new P_Model(121, 64));
			CateList.Add(new P_Model(146, 64));
			CateList.Add(new P_Model(125, 43));
			CateList.Add(new P_Model(127, 43));
			CateList.Add(new P_Model(129, 43));
			CateList.Add(new P_Model(131, 43));
			CateList.Add(new P_Model(133, 43));
			CateList.Add(new P_Model(135, 43));
			CateList.Add(new P_Model(50, 43));
			CateList.Add(new P_Model(50, 43));
			CateList.Add(new P_Model(52, 43));
			CateList.Add(new P_Model(87, 43));
			CateList.Add(new P_Model(88, 43));
			CateList.Add(new P_Model(53, 43));
			CateList.Add(new P_Model(54, 43));
			CateList.Add(new P_Model(55, 43));
			CateList.Add(new P_Model(56, 43));
			CateList.Add(new P_Model(58, 43));
			CateList.Add(new P_Model(60, 43));
			CateList.Add(new P_Model(61, 43));
			CateList.Add(new P_Model(63, 43));
			CateList.Add(new P_Model(64, 43));
		}
	}

	public class P_Model
	{
		public int Parent { get; set; }
		public int Child { get; set; }

		public P_Model(int c, int p)
		{
			Parent = p;
			Child = c;
		}
	}
}