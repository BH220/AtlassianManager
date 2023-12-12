using Atlassian.Jira;
using AtlassianManager.Data;
using AtlassianManager.Data.Models;
using BH_Library.Utils;
using BH_Library.Utils.HangulJaso;
using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpreadsheet;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlassianManager
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			Task t = ReturnsIssuesUsingRest();
		}

		public async Task ReturnsIssuesUsingRest()
		{
			//Jira jiraConn = Jira.CreateRestClient("https://pms.ctkcosmetics.biz/", txtId.Text, txtPw.Text);
			//jiraConn.MaxIssuesPerRequest = 500;

			////var issues = await jiraConn.Issues.GetIssuesFromJqlAsync("updated >= 2019-04-07 AND updated <= 2019-04-13 AND (assignee in (bhjin) OR description ~ 진병호 OR comment ~ 진병호 OR reporter in (bhjin)) ORDER BY status DESC");
			//var issues = await jiraConn.Issues.GetIssuesFromJqlAsync("project = EUPDM AND issuetype = Package");
			//var issuesList = issues.ToList();
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			Jira jiraConn = Jira.CreateRestClient("https://pms.ctkcosmetics.biz/", txtId.Text, txtPw.Text);
			jiraConn.Issues.MaxIssuesPerRequest = 10000;
			IEnumerable<Issue> jiraIssues = (from data in jiraConn.Issues.Queryable
											 where data.Project == "ITPDM"
												   || data.Project == "UEPDM"
												   || data.Project == "UWPDM"
												   || data.Project == "ASPDM"
												   || data.Project == "EUPDM"
											 select data).ToList();

			foreach (Issue iss in jiraIssues)
			{
			}
		}

		private void simpleButton3_Click(object sender, EventArgs e)
		{
			Jira jiraConn = Jira.CreateRestClient("https://pms.ctkcosmetics.biz/", txtId.Text, txtPw.Text);
		}

		private async void simpleButton4_Click(object sender, EventArgs e)
		{
			//var confluenceClient = ConfluenceClient.Create(new Uri("https://wiki.ctkcosmetics.biz"));
			//confluenceClient.SetBasicAuthentication("bhjin", "123qwe");
			//var query = Where.And(Where.Type.IsPage, Where.Title.Contains("tt"));
			//var searchResult = await confluenceClient.Content.SearchAsync(query);
			//foreach (var content in searchResult.Results)
			//{
			//    Debug.WriteLine(content.Body);
			//}
			//Dapplo.Confluence.Entities.Content x = await confluenceClient.Content.CreateAsync(ContentTypes.Page, "asdf", "Cream_Emuilsion","<html><body>test</body></html>");
		}

		private IWebElement GetEelment(string xPath)
		{
			//Thread.Sleep(1000);
			IWebElement result = null;

			for (int idx = 0; idx < 60; idx++)
			{
				try
				{
					//result = driver.FindElement(By.XPath(xPath));
					//if (result != null)
					//{
					//    //result.Click();
					//    break;
					//}
				}
				catch { }
			}
			return result;
		}

		private void simpleButton5_Click(object sender, EventArgs e)
		{
			//if (driver == null)
			//    driver = new ChromeDriver();
			//driver.Url = "https://wiki.ctkcosmetics.biz/pages/templates2/listpagetemplates.action?key=FORMULALIB";
		}

		private void simpleButton6_Click(object sender, EventArgs e)
		{
			IWebElement el = GetEelment("//*[@id=\"content\"]/div/div[2]/div/div/div[1]/div[2]/div/div/table/tbody/tr[1]/td/div/p/input");
			el.Clear();
			el.SendKeys("a");

			el = GetEelment("//*[@id=\"content\"]/div/div[2]/div/div/div[1]/div[2]/div/div/table/tbody/tr[2]/td/div/p/input");
			el.Clear();
			el.SendKeys("b");

			el = GetEelment("//*[@id=\"content\"]/div/div[2]/div/div/div[1]/div[2]/div/div/table/tbody/tr[3]/td/div/p/input");
			el.Clear();
			el.SendKeys("b");
		}

		private void simpleButton7_Click(object sender, EventArgs e)
		{
			SpreadsheetControl sc = new SpreadsheetControl();
			sc.LoadDocument(@"C:\sl\new\list.xlsx");

			DevExpress.Spreadsheet.Worksheet ws = (DevExpress.Spreadsheet.Worksheet)sc.Document.Sheets[0];
			Types Type = Types.Formula;
			string ProduceCode = "";
			string Manufacturer = "";
			string Keyword = "";
			string Category = "";
			string Name = "";
			string Labno = "";

			int cnt = 716;

			for (int idx = 1; idx <= cnt; idx++)
			{
				var cat = ws.Cells[idx, 6].Value.ToString().Split(new char[] { '|' });
				Type = cat.Contains("42") ? Types.Formula : Types.Package;
				ProduceCode = ws.Cells[idx, 1].Value.ToStringEx();
				Manufacturer = ws.Cells[idx, 2].Value.ToStringEx();
				Keyword = ws.Cells[idx, 4].Value.ToStringEx();
				Category = ws.Cells[idx, 6].Value.ToStringEx();
				Name = ws.Cells[idx, 3].Value.ToStringEx();
				Labno = ws.Cells[idx, 5].Value.ToStringEx();
				if (ItemHelper.Instance.IsExistsItem(Type, Name) == false)
				{
					ItemHelper.Instance.AddItem(Type, ProduceCode, Manufacturer, Keyword, Category, Name, Labno);
				}
				else
				{
					if (ItemHelper.Instance.IsExistsItem(Type, Name + "_1"))
					{
						if (ItemHelper.Instance.IsExistsItem(Type, Name + "_2"))
						{
							if (ItemHelper.Instance.IsExistsItem(Type, Name + "_3"))
							{
								if (ItemHelper.Instance.IsExistsItem(Type, Name + "_4"))
								{
									txtLog.Text += string.Format("[{0}] {1} 카테고리에 {2} 제품이 이미 있습니다.", ProduceCode, Type, Name) + Environment.NewLine;
								}
								else
								{
									ItemHelper.Instance.AddItem(Type, ProduceCode, Manufacturer, Keyword, Category, Name + "_4", Labno);
								}
							}
							else
							{
								ItemHelper.Instance.AddItem(Type, ProduceCode, Manufacturer, Keyword, Category, Name + "_3", Labno);
							}
						}
						else
						{
							ItemHelper.Instance.AddItem(Type, ProduceCode, Manufacturer, Keyword, Category, Name + "_2", Labno);
						}
					}
					else
					{
						ItemHelper.Instance.AddItem(Type, ProduceCode, Manufacturer, Keyword, Category, Name + "_1", Labno);
					}
				}
			}
		}

		private void simpleButton8_Click(object sender, EventArgs e)
		{
			Console.WriteLine("asdf");
			ProcessStartInfo psi = new ProcessStartInfo();

			Process proc = new Process();
			proc.StartInfo = new ProcessStartInfo();
			proc.StartInfo.FileName = @"C:\cli\confluence.bat";

			string result = "";
			StringBuilder sb = new StringBuilder();
			foreach (ItemModel im in ItemHelper.Instance.ListAll)
			{
				sb.AppendLine(im.Argument);
				//proc = new Process();
				//psi = new ProcessStartInfo
				//{
				//    FileName = @"C:\cli\confluence.bat --action run --file g.txt":,
				//    Arguments = ,
				//    UseShellExecute = false,
				//    RedirectStandardOutput = true,
				//};
				//proc.StartInfo = psi;
				//proc.Start();
				//while (!proc.StandardOutput.EndOfStream)
				//{
				//    result = proc.StandardOutput.ReadLine();
				//}
			}
			File.WriteAllText(@"C:\cli\aa.txt", sb.ToString(), Encoding.UTF8);
		}

		private void simpleButton9_Click(object sender, EventArgs e)
		{
			string s = File.ReadAllText(@"c:\aaa.txt");
			List<string> lst = StringUtil.SplitByEnterLine(s).Where(x => string.IsNullOrEmpty(x.Trim()) == false).ToList().Where(x => x.EndsWith(".pptx\"") == false).ToList();
			StringBuilder sb = new StringBuilder();
			string fat = "--action addAttachment --id {0} --file \"{1}\"";
			foreach (string ss in lst)
			{
				sb.AppendLine(ss);
				//var sa = ss.Split(new char[] { ',' });
				//var files = Directory.GetFiles(@"C:\sl\new\upload target group\" + sa[0] + "\\");
				//foreach (string f in files)
				//{
				//    sb.AppendLine(string.Format(fat, sa[1], f));
				//}
			}
			s = sb.ToString();
			lst = StringUtil.SplitByString(s, "Run: --action getpagesource").ToList();
			sb = new StringBuilder();
			foreach (string ss in lst)
			{
				sb.AppendLine(ss.Replace("\r\n", ""));
			}
			s = sb.ToString();
		}

		private bool GetConfluence(string argement, out string result)
		{
			int exitCode;
			ProcessStartInfo processInfo;
			Process process;
			processInfo = new ProcessStartInfo();
			processInfo.WorkingDirectory = @"C:\cli";
			processInfo.CreateNoWindow = true;
			processInfo.UseShellExecute = false;
			// *** Redirect the output ***
			processInfo.RedirectStandardError = true;
			processInfo.RedirectStandardOutput = true;
			processInfo.FileName = @"C:\cli\confluence.bat";
			processInfo.Arguments = argement;

			process = Process.Start(processInfo);
			process.WaitForExit(700000);
			string output = process.StandardOutput.ReadToEnd();
			string error = process.StandardError.ReadToEnd();
			exitCode = process.ExitCode;
			//Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
			//Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
			//Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
			process.Close();
			result = output;
			return string.IsNullOrEmpty(error);
		}

		private void simpleButton10_Click(object sender, EventArgs e)
		{
			string resultMsg = string.Empty;
			string argument = " --action getPage  --space \"packagelib\" --title \"pompom stick\" --outputFormat 4";
			bool result = GetConfluence(argument, out resultMsg);
		}

		private void simpleButton11_Click(object sender, EventArgs e)
		{
			string resultMsg = string.Empty;
			string argument = " --action getPageList  --cql \"space = packagelib\" --outputFormat 998  --columns \"id,title\"";
			bool result = GetConfluence(argument, out resultMsg);
			txtLog.Text = resultMsg;
		}

		private void simpleButton12_Click(object sender, EventArgs e)
		{
			var fs = Directory.GetFiles(@"C:\CLI\confluenctData_2019-09-25\");
			foreach (string f in fs)
			{
				var fn = Path.GetFileName(f).Replace("pacekage", "package");
				if (fn.StartsWith("package"))
				{
					File.Move(f, @"C:\CLI\confluenctData_2019-09-25\" + fn);
				}
			}

			List<string> list = StringUtil.SplitByEnterLine(txtLog.Text).ToList().Where(x => string.IsNullOrEmpty(x) == false).ToList();

			string dicPath = string.Format(@"c:\cli\confluenctData_{0}\", DateTime.Now.ToString("yyyy-MM-dd"));
			if (Directory.Exists(dicPath) == false)
				Directory.CreateDirectory(dicPath);
			string id = string.Empty;
			string title = string.Empty;
			string type = string.Empty;
			string resultMsg = string.Empty;
			string resultMsg1 = string.Empty;
			int idx = 1;
			int tot = list.Count;
			bool result = false;
			string resultMsg2 = string.Empty;
			StringBuilder errids = new StringBuilder();
			int idz = 1;
			foreach (string s in list)
			{
				try
				{
					var sr = StringUtil.SplitByString(s, "!@#!@");
					resultMsg = resultMsg1 = resultMsg2 = string.Empty;
					Console.WriteLine("{0}/{1}..Start", idx, tot);
					//type = sr[0].ToStringEx().Trim().Replace("\"", "");
					//id = sr[1].ToStringEx().Trim().Replace("\"", "");
					//title = sr[2].ToStringEx().Trim().Replace("\"", "");
					id = s;
					var sf = dicPath + type + "_" + id + ".txt";
					if (File.Exists(sf))
					{
						continue;
					}
					else
					{
					}

					resultMsg = string.Empty;
					result = GetConfluence(" --action getPage --id " + id + " --outputFormat 4", out resultMsg1);
					if (result == false)
					{
					}
					resultMsg += resultMsg1;
					resultMsg += Environment.NewLine;
					result = GetConfluence(" --action getLabelList  --id " + id, out resultMsg2);
					resultMsg += resultMsg2;
					File.WriteAllText(dicPath + type + "_" + id + ".txt", resultMsg);
					Console.WriteLine("{0}/{1}..{2}", idx, tot, result ? "Sucess" : "fail");
				}
				catch (Exception ex)
				{
					errids.AppendLine(string.Format("{0}     {1}", s, ex.Message));
				}
				finally
				{
					idx++;
				}
			}
			File.WriteAllText(dicPath + "err.txt", errids.ToString());
		}

		private void simpleButton13_Click(object sender, EventArgs e)
		{
			string path = @"C:\cdt";
			var files = Directory.GetFiles(path);
			List<WikiFormulaModel> fList = new List<WikiFormulaModel>();
			List<WikiPackageModel> pList = new List<WikiPackageModel>();
			WikiFormulaModel fm = null;
			WikiPackageModel pm = null;

			string resultMsg = "", resultMsg1 = "", resultMsg2 = "";
			bool result = false;

			#region 포뮬라

			var fs = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\flist.txt"));

			int tot = files.Count();
			string fat = @"c:\cdt\f_{0}.txt";

			string ori = "";

			bool skip = false;
			var fFiles = files.Where(x => Path.GetFileName(x).StartsWith("f_"));
			foreach (string file in fs)
			{
				try
				{
					resultMsg = resultMsg1 = resultMsg2 = "";
					result = GetConfluence(" --action getPage --id " + file + " --outputFormat 4", out resultMsg1);
					if (result == false)
					{
					}
					resultMsg += resultMsg1;
					resultMsg += Environment.NewLine;
					result = GetConfluence(" --action getLabelList  --id " + file, out resultMsg2);
					resultMsg += resultMsg2;

					File.WriteAllText(string.Format(@"c:\cli\confluenctData_2019-09-25\formula_{0}.txt", file.Trim()), resultMsg, Encoding.UTF8);

					var rs = StringUtil.SplitByString(resultMsg, "\n").ToList();
					string origin = rs[20].Substring(31, rs[20].Length - 31);
					if (string.IsNullOrEmpty(origin.Trim()))
					{
						throw new Exception(file);
					}
					else
					{
						fm = new WikiFormulaModel();
						HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
						doc.LoadHtml(origin);
						for (int idx = 1; idx < 20; idx++)
						{
							HtmlAgilityPack.HtmlNode n1_t = doc.DocumentNode.SelectSingleNode("//tbody/tr[" + idx.ToString() + "]/th[1]");
							HtmlAgilityPack.HtmlNode n1_v = doc.DocumentNode.SelectSingleNode("//tbody/tr[" + idx.ToString() + "]/td[1]");

							if (n1_t == null || n1_v == null)
							{
								if (idx == 1)
								{
									skip = true;
								}
								break;
							}
							skip = false;

							switch (n1_t.InnerText)
							{
								case "PRODUCT CODE":
								case "Sales Library Link (code)":
									fm.SalesLibraryLinkCode = ParseTxt(n1_v.InnerText);
									//fm.Pre_ProductCode= ParseTxt(n1_v.InnerText);
									break;

								case "LAB NO.":
								case "랩넘버":
									fm.랩넘버 = ParseTxt(n1_v.InnerText);
									//fm.Pre_LabNo = ParseTxt(n1_v.InnerText);
									break;

								case "MANUFACTURER":
								case "제조사":
									fm.제조사 = ParseTxt(n1_v.InnerText);
									//fm.Pre_Manufacturer = ParseTxt(n1_v.InnerText);
									break;

								case "제조사 연락처":
									fm.제조사연락처 = ParseTxt(n1_v.InnerText);
									break;

								case "키워드":
									fm.키워드 = ParseTxt(n1_v.InnerText);
									break;

								case "LAUNCH TYPE":
									//fm.Pre_LaunchType = ParseTxt(n1_v.InnerText);
									break;

								case "LAUNCH STATUS":
									//fm.Pre_LaunchStatus = ParseTxt(n1_v.InnerText);
									break;

								case "PRICE":
								case "임가공 단가":
									fm.임가공단가 = ParseTxt(n1_v.InnerText);
									//fm.Pre_Price = ParseTxt(n1_v.InnerText);
									break;

								case "BENEFITS":
									//fm.Pre_Benefits = ParseTxt(n1_v.InnerText);
									break;

								case "SIZE":
									//fm.Pre_Size = ParseTxt(n1_v.InnerText);
									break;

								case "VIEWER LINK":
									//fm.Pre_ViewerLink = ParseTxt(n1_v.InnerHtml);
									break;

								case "SALES LIBRARY":
									if (n1_v.InnerText.Contains("바로가기"))
									{
										//fm.Pre_SaleLibrary = ParseTxt(n1_v.InnerHtml);
									}
									else
									{
										//fm.Pre_SaleLibrary = ParseTxt(n1_v.InnerText);
									}
									break;

								case "내용물 단위":
									fm.내용물단위 = ParseTxt(n1_v.InnerText);
									break;

								case "내용물 단가":
									fm.내용물단가 = ParseTxt(n1_v.InnerText);
									break;

								case "추천 용량":
									fm.추천용량 = ParseTxt(n1_v.InnerText);
									break;

								case "실제 용량":
									fm.실제용량 = ParseTxt(n1_v.InnerText);
									break;

								case "규제":
									fm.규제 = ParseTxt(n1_v.InnerText);
									break;

								case "CTK 내부 개발 담당자":
									fm.CTK내부개발담당자 = ParseTxt(n1_v.InnerText);
									break;

								case "제안브랜드":
									fm.제안브랜드 = ParseTxt(n1_v.InnerText);
									break;

								case "관련 제품개발 프로젝트":
									fm.관련제품개발프로젝트 = ParseTxt(n1_v.InnerText);
									break;

								case "RELATED PROJECT":
									//fm.Pre_RelatedProject = ParseTxt(n1_v.InnerText);
									break;

								default:
									throw new Exception(file);
							}
						}
						if (skip == false)
						{
							fm.제목 = rs[3].Substring(31, rs[3].Length - 31).Trim();
							fm.Id = rs[2].Substring(31, rs[2].Length - 31).ToIntEx();
							//fm.ParentId = rs[8].Substring(31, rs[8].Length - 31).ToIntEx();
							HtmlAgilityPack.HtmlNode h21 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]/h2[1]");
							if (h21 != null && h21.InnerText.ToLower() == "what it is")
							{
								HtmlAgilityPack.HtmlNode ul1 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]/ul[1]");// "/ac:layout[1]/ac:layout-section[1]/ac:layout-cell[2]/ul[1]");
								if (ul1 != null)
								{
									fm.WhatItIs = ParseTxt(ul1.InnerText);
									//fm.Pre_WhatItIs = ParseTxt(ul1.InnerText);
								}
							}
							HtmlAgilityPack.HtmlNode h22 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]/h2[2]");
							if (h22 != null)
							{
								if (h22.InnerText.ToLower() == "how to use")
								{
									HtmlAgilityPack.HtmlNode ul2 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]/ul[2]");
									//if (ul2 != null) fm.Pre_HowToUse = ParseTxt(ul2.InnerText);
								}
								else if (h22.InnerText.ToLower() == "Benefit")
								{
									HtmlAgilityPack.HtmlNode ul2 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]/ul[2]");
									if (ul2 != null) fm.Benefit = ParseTxt(ul2.InnerText);
								}
							}

							StringBuilder sb = new StringBuilder();

							string lb = string.Empty;
							for (int idx = 24; idx < 100; idx++)
							{
								if (rs.Count > idx)
								{
									lb = rs[idx];
									if (lb.Length > 2)
									{
										lb = lb.Substring(1, lb.Length - 2);
									}
									else
										break;
									sb.AppendFormat("{0},", lb.Trim().Replace("\"", ""));
								}
							}
							string label = sb.ToString();
							if (label.EndsWith(","))
								label = label.Substring(0, label.Length - 1);
							fm.키워드 = label;

							fList.Add(fm);
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(fList);
			File.WriteAllText(@"c:\fList.json", json);

			#endregion 포뮬라

			#region 패키지

			var ps = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\plist.txt"));

			tot = files.Count();
			fat = @"C:\CLI\confluenctData_2019-09-25\package_{0}.txt";

			ori = "";

			StringBuilder esb = new StringBuilder();
			skip = false;
			fFiles = files.Where(x => Path.GetFileName(x).StartsWith("p_"));
			foreach (string file in ps)
			{
				try
				{
					ori = string.Format(fat, file.Trim());
					var rs = StringUtil.SplitByString(File.ReadAllText(ori), "\n").ToList();
					string origin = rs[20].Substring(31, rs[20].Length - 31);
					if (string.IsNullOrEmpty(origin.Trim()))
					{
						throw new Exception(file);
					}
					else
					{
						pm = new WikiPackageModel();
						HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
						doc.LoadHtml(origin);
						for (int idx = 1; idx < 20; idx++)
						{
							HtmlAgilityPack.HtmlNode n1_t = doc.DocumentNode.SelectSingleNode("//tbody/tr[" + idx.ToString() + "]/th[1]");
							HtmlAgilityPack.HtmlNode n1_v = doc.DocumentNode.SelectSingleNode("//tbody/tr[" + idx.ToString() + "]/td[1]");

							if (n1_t == null || n1_v == null)
							{
								if (idx == 1)
								{
									skip = true;
								}
								break;
							}
							skip = false;

							switch (n1_t.InnerText)
							{
								case "PRODUCT CODE":
								case "Sales Library Link (code)":
									pm.SalesLibraryLinkCode = ParseTxt(n1_v.InnerText);
									//pm.Pre_ProductCode = ParseTxt(n1_v.InnerText);
									break;
								//case "LAB NO.":
								//    pm.Pre_LabNo = ParseTxt(n1_v.InnerText);
								//    break;
								//case "LAUNCH TYPE":
								//    pm.Pre_LaunchType = ParseTxt(n1_v.InnerText);
								//    break;
								case "MOQ":
									pm.MOQ = ParseTxt(n1_v.InnerText);
									break;
								//case "LAUNCH STATUS":
								//    pm.Pre_LaunchStatus = ParseTxt(n1_v.InnerText);
								//    break;
								case "MANUFACTURER":
								case "제조사":
									pm.제조사 = ParseTxt(n1_v.InnerText);
									//pm.Pre_Manufacturer = ParseTxt(n1_v.InnerText);
									break;
								//case "VOLUME":
								//    pm.Pre_Volume = ParseTxt(n1_v.InnerText);
								//    break;
								case "연락처":
									pm.연락처 = ParseTxt(n1_v.InnerText);
									break;

								case "키워드":
									pm.키워드 = ParseTxt(n1_v.InnerText);
									break;

								case "통화":
									pm.통화 = ParseTxt(n1_v.InnerText);
									break;

								case "PRICE":
								case "가격":
									pm.가격 = ParseTxt(n1_v.InnerText);
									//pm.Pre_Price = ParseTxt(n1_v.InnerText);
									break;

								case "가격 업데이트 날짜":
									pm.가격업데이트날짜 = ParseTxt(n1_v.InnerText);
									break;

								//case "SIZE":
								//    pm.Pre_Size = ParseTxt(n1_v.InnerText);
								//    break;

								case "용량":
									pm.용량 = ParseTxt(n1_v.InnerText);
									break;

								case "용량 단위":
									pm.용량단위 = ParseTxt(n1_v.InnerText);
									break;

								case "포뮬라 타입":
									pm.포뮬라타입 = ParseTxt(n1_v.InnerText);
									break;

								case "관련 제품개발 프로젝트":
								case "런칭제품":
									pm.런칭제품 = ParseTxt(n1_v.InnerText);
									//pm.관련제품개발프로젝트 = ParseTxt(n1_v.InnerText);
									break;

								//case "BENEFITS":
								//    pm.Pre_Benefits = ParseTxt(n1_v.InnerText);
								//    break;
								//case "FORMULA TYPE":
								//    pm.Pre_FormulaType = ParseTxt(n1_v.InnerText);
								//    break;
								case "MATERIAL":
								case "Material":
									//pm.Pre_Material = pm.Pre_Material;
									pm.Material = ParseTxt(n1_v.InnerText);
									break;
								//case "VIEWER LINK":
								//    pm.Pre_ViewerLink = ParseTxt(n1_v.InnerHtml);
								//    break;
								//case "SALES LIBRARY":
								//    if (n1_v.InnerText.Contains("바로가기"))
								//    {
								//        pm.Pre_SalesLibrary = ParseTxt(n1_v.InnerHtml);
								//    }
								//    else
								//    {
								//        pm.Pre_SalesLibrary = ParseTxt(n1_v.InnerText);
								//    }
								//    break;
								//case "RELATED PROJECT":
								//    pm.Pre_RelatedProject = ParseTxt(n1_v.InnerText);
								//    break;
								default:
									throw new Exception(file);
							}
						}
						if (skip == false)
						{
							pm.제목 = rs[3].Substring(31, rs[3].Length - 31).Trim();
							pm.Id = rs[2].Substring(31, rs[2].Length - 31).ToIntEx();
							//pm.ParentId = rs[8].Substring(31, rs[8].Length - 31).ToIntEx();

							HtmlAgilityPack.HtmlNode h21 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]//h2[1]");
							if (h21 != null)
							{
								if (h21.InnerText.ToLower() == "description")
								{
									HtmlAgilityPack.HtmlNode ul1 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]//ul[1]");
									if (ul1 != null)
									{
										//pm.Pre_Description = ParseTxt(ul1.InnerText);
									}
								}
								else if (h21.InnerText.ToLower() == "what it is")
								{
									HtmlAgilityPack.HtmlNode ul1 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]//ul[1]");
									if (ul1 != null)
									{
										//pm.Pre_WhatItIs = ParseTxt(ul1.InnerText);
									}
								}
							}

							HtmlAgilityPack.HtmlNode h22 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]//h2[2]");
							if (h22 != null)
							{
								if (h22.InnerText.ToLower() == "how to use")
								{
									HtmlAgilityPack.HtmlNode ul2 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]//ul[2]");
									if (ul2 != null)
									{
										//pm.Pre_HowToUse = ul2.InnerText.Trim();
									}
								}
								else if (h22.InnerText.ToLower() == "Benefit")
								{
									HtmlAgilityPack.HtmlNode ul2 = doc.DocumentNode.SelectSingleNode("//*[name()='ac:layout-cell'][2]//ul[2]");
									if (ul2 != null)
									{
										//pm.Pre_Benefits = ul2.InnerText.Trim();
									}
								}
							}

							StringBuilder sb = new StringBuilder();

							string lb = string.Empty;
							for (int idx = 24; idx < 100; idx++)
							{
								if (rs.Count > idx)
								{
									lb = rs[idx];
									if (lb.Length > 2)
									{
										lb = lb.Substring(1, lb.Length - 2);
									}
									else
										break;
									sb.AppendFormat("{0},", lb.Trim());
								}
							}
							string label = sb.ToString();
							if (label.EndsWith(","))
								label = label.Substring(0, label.Length - 1);
							pm.키워드 = label;
							pList.Add(pm);
						}
					}
				}
				catch (Exception ex)
				{
					esb.AppendLine(file.Trim());
				}
			}

			var asese = esb.ToString();
			json = Newtonsoft.Json.JsonConvert.SerializeObject(pList);
			File.WriteAllText(@"c:\pList.json", json);

			#endregion 패키지
		}

		private string ParseTxt(string origin)
		{
			string result = origin
				.Replace("<a href=\"", "")
				.Replace("\" target=\"_blank\"", "")
				.Replace("<p>", "")
				.Replace(">Sales Library 바로가기</a>", "")
				.Replace("\">Sales Library 바로가기</a>", "")
				.Replace("</p>", "")
				.Replace("</div>", "")
				.Replace("<br>", "")
				.Replace("\\", "")
				.Replace("<div class=\"content-wrapper\">", "")
				.Replace("&nbsp;", "").Trim();
			return result;
		}

		private void simpleButton14_Click(object sender, EventArgs e)
		{
			DevExpress.Spreadsheet.Worksheet ws = spreadsheetControl1.ActiveWorksheet;
			//string json = File.ReadAllText(@"c:\WikiList.json");
			//List<WikiModel> lst = JsonConvert.DeserializeObject<List<WikiModel>>(json);
			List<WikiAttachFileModel> lst = new List<WikiAttachFileModel>();
			WikiAttachFileModel wm = null;

			for (int idx = 1; idx < 1725; idx++)
			{
				wm = new WikiAttachFileModel();
				wm.Title = ws.GetCellValue(0, idx).TextValue;
				wm.Id = ws.GetCellValue(1, idx).NumericValue;
				wm.ParentId = ws.GetCellValue(2, idx).NumericValue;
				wm.FileName = ws.GetCellValue(3, idx).TextValue;
				wm.FileSize = ws.GetCellValue(4, idx).NumericValue.ToDecimalEx();
				wm.Url = ws.GetCellValue(5, idx).TextValue;
				lst.Add(wm);
			}
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
			File.WriteAllText(@"c:\wikifileList.json", json);
		}

		private List<WikiAttachFileModel> WikiAttachFileModelList = null;
		private int idxComplete = 1;

		private void simpleButton15_Click(object sender, EventArgs e)
		{
			string json = File.ReadAllText(@"c:\wikifileList.json");
			WikiAttachFileModelList = JsonConvert.DeserializeObject<List<WikiAttachFileModel>>(json);
			int idx = 1;
			idxComplete = 1;
			string path = @"c:\WIKI\Files\{0}\";
			WebClient myWebClient = new WebClient();
			myWebClient.DownloadFileCompleted += MyWebClient_DownloadFileCompleted;
			List<double> errlst = new List<double>();

			string resultMsg = string.Empty;
			bool result = false;
			string argument = string.Empty;
			string fpath = string.Empty;
			List<double> parentList = WikiAttachFileModelList.Select(x => x.ParentId).Distinct().ToList();
			int tot = parentList.Count;
			foreach (double pid in parentList)
			{
				try
				{
					fpath = string.Format(@"WikiFiles\{0}", pid);
					if (Directory.Exists(@"c:\cli\" + fpath) == false) Directory.CreateDirectory(@"c:\cli\" + fpath);
					resultMsg = string.Empty;
					Console.WriteLine("{0}/{1}..Start", idx, tot);
					resultMsg = string.Empty;
					argument = " --action runFromAttachmentList --id " + pid.ToString()
						+ " --common \"--action getAttachment --id @pageId@ --name \"@attachment@\" --file " + fpath + "\" --continue";
					result = GetConfluence(argument, out resultMsg);
					Console.WriteLine("{0}/{1}..{2}", idx, tot, result ? "Sucess" : "fail");
					if (result == false)
						throw new Exception();
				}
				catch (Exception ex)
				{
					errlst.Add(pid);
				}
				finally
				{
					idx++;
				}
			}
			string s = string.Empty;
			errlst.ForEach(x =>
			{
				s += string.Format("{0},", x);
			});
			string errpath = string.Format(path, "error");
			if (Directory.Exists(errpath) == false) Directory.CreateDirectory(errpath);
			File.WriteAllText(errpath + "error.txt", s);
		}

		private void MyWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			int id = e.UserState.ToIntEx();
			//WikiAttachFileModel wm = WikiAttachFileModelList.First(x => x.Id == id);
			//Console.WriteLine(string.Format("[{0}/{1}] {2} Download Complete..", idxComplete, WikiAttachFileModelList.Count, wm.FileName));
			idxComplete++;
		}

		private void simpleButton16_Click(object sender, EventArgs e)
		{
			string path = @"C:\WIKI\pages\";
			string fPath = @"C:\WIKI\pages\{0}\Files\";
			string tp = string.Empty;
			string[] dics = Directory.GetDirectories(path);
			string file = string.Empty;
			List<string> fNames = new List<string>();
			foreach (string dic in dics)
			{
				fNames = new List<string>();
				file = dic + @"\" + Path.GetFileName(dic) + ".txt";
				var rs = StringUtil.SplitByString(File.ReadAllText(file), "\n").ToList();

				HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
				doc.LoadHtml(rs[20]);
				HtmlAgilityPack.HtmlNodeCollection atts = doc.DocumentNode.SelectNodes("//*[name()='ri:attachment']");
				foreach (var att in atts)
				{
					HtmlAgilityPack.HtmlAttribute attribute = att.Attributes["ri:filename"];
					fNames.Add(attribute.Value.ToStringEx());
				}
				string fp = string.Format(fPath, Path.GetFileName(dic));
				string[] files = Directory.GetFiles(fp);

				foreach (string fn in files)
				{
					if (fNames.Any(x => x.ToLower() == Path.GetFileName(fn).ToLower()) == false)
					{
						var ext = Path.GetExtension(fn);
						switch (ext.ToLower())
						{
							case ".png":
							case ".gif":
							case ".jpg":
							case ".jpeg":
								File.Delete(fn);
								break;

							case ".pptx":
							case ".pdf":
								break;

							default:
								if (true)
								{
								}
								break;
						}
					}
				}
				//tp = string.Format(fPath, Path.GetFileName(dic));
				//Directory.CreateDirectory(tp);
				//var files = Directory.GetFiles(dic);
				//foreach(string file in files)
				//{
				//    File.Move(file, tp + Path.GetFileName(file));
				//}
			}
		}

		private void simpleButton17_Click(object sender, EventArgs e)
		{
			string path = @"C:\WIKI\origin\files\";
			string page = @"C:\WIKI\pages\{0}\Files\{1}";
			string id = string.Empty;
			string tp = string.Empty;
			string[] dics = Directory.GetDirectories(path);
			foreach (string dic in dics)
			{
				id = Path.GetFileName(dic);
				string[] files = Directory.GetFiles(dic);

				foreach (string file in files)
				{
					string nf = string.Format(page, id, Path.GetFileName(file));
					File.Copy(file, nf, true);
				}
			}
		}

		private void simpleButton18_Click(object sender, EventArgs e)
		{
			string path = @"C:\WIKI\pages\";
			string fPath = @"C:\WIKI\pages\{0}\Files\";
			string rfp = @"C:\WIKI\files\{0}_{1}{2}";
			string tp = string.Empty;
			string[] dics = Directory.GetDirectories(path);
			string file = string.Empty;
			List<string> fNames = new List<string>();
			int png = 0;
			int jpg = 0;
			int jpeg = 0;
			int pptx = 0;
			int pdf = 0;
			int gif = 0;
			foreach (string dic in dics)
			{
				png = jpg = jpeg = pptx = pdf = gif = 0;
				string id = Path.GetFileName(dic);
				string fp = string.Format(fPath, id);
				string[] files = Directory.GetFiles(fp);
				foreach (string ff in files)
				{
					string f = ff.ToLower();
					string ext = Path.GetExtension(f);
					switch (ext)
					{
						case ".png":
							png++;
							File.Move(ff, string.Format(rfp, id, png, ext));
							break;

						case ".jpg":
							jpg++;
							break;

						case ".jpeg":
							jpeg++;
							break;

						case ".pptx":
							pptx++;
							break;

						case ".pdf":
							pdf++;
							break;

						case ".gif":
							gif++;
							break;

						default:
							if (true)
							{
							}
							break;
					}
				}
			}
		}

		private void simpleButton19_Click(object sender, EventArgs e)
		{
			string spath = @"C:\WIKI\pages\";
			string ssp = @"C:\WIKI\pages\";
			string[] dics = Directory.GetDirectories(spath);
			string file = "";
			string id = "";
			List<string> fNames = new List<string>();
			foreach (string d in dics)
			{
				fNames = new List<string>();
				id = Path.GetFileName(d);
				file = spath + id + "\\" + id + ".txt";
				var rs = StringUtil.SplitByString(File.ReadAllText(file), "\n").ToList();

				HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
				doc.LoadHtml(rs[20]);
				HtmlAgilityPack.HtmlNodeCollection atts = doc.DocumentNode.SelectNodes("//*[name()='ri:attachment']");
				foreach (var att in atts)
				{
					HtmlAgilityPack.HtmlAttribute attribute = att.Attributes["ri:filename"];
					fNames.Add(attribute.Value.ToStringEx().ToLower());
				}

				string[] files = Directory.GetFiles(d + "\\files\\");

				foreach (string f in files)
				{
					if (fNames.Any(x => x == Path.GetFileName(f).ToLower()) == false)
					{
						string ext = Path.GetExtension(f).ToLower();
						if (ext == ".jpg" ||
							ext == ".jpeg" ||
							ext == ".gif" ||
							ext == ".png")
							File.Delete(f);
						else
						{
						}
					}
				}
			}
		}

		private void simpleButton20_Click(object sender, EventArgs e)
		{
			string spath = @"C:\WIKI\pages\";
			string sf = @"C:\WIKI\pages\{0}\files\";
			string ssp = @"C:\WIKI\pages\";
			string[] dics = Directory.GetDirectories(spath);
			string file = "";
			string id = "";
			List<string> pngs = new List<string>();
			List<string> jpgs = new List<string>();
			List<string> jpegs = new List<string>();
			List<string> gifs = new List<string>();
			List<string> pptxs = new List<string>();
			List<string> ppts = new List<string>();
			List<string> pdfs = new List<string>();

			foreach (string d in dics)
			{
				pngs = new List<string>();
				jpgs = new List<string>();
				jpegs = new List<string>();
				gifs = new List<string>();
				pptxs = new List<string>();
				ppts = new List<string>();
				pdfs = new List<string>();
				id = Path.GetFileName(d);
				string sff = string.Format(sf, id);
				string[] files = Directory.GetFiles(sff);

				foreach (string s in files)
				{
					string ext = Path.GetExtension(s).ToLower();
					switch (ext)
					{
						case ".png": pngs.Add(s); break;
						case ".jpg": jpgs.Add(s); break;
						case ".jpeg": jpegs.Add(s); break;
						case ".gif": gifs.Add(s); break;
						case ".pptx": pptxs.Add(s); break;
						case ".ppt": ppts.Add(s); break;
						case ".pdf": pdfs.Add(s); break;
						default:
							if (true)
							{
							}
							break;
					}
				}

				for (int idx = 0; idx < pngs.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.png";
					string nf = string.Format(fa, id, idx);
					File.Move(pngs[idx], nf);
				}
				for (int idx = 0; idx < jpgs.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.jpg";
					string nf = string.Format(fa, id, idx);
					File.Move(jpgs[idx], nf);
				}
				for (int idx = 0; idx < jpegs.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.jpeg";
					string nf = string.Format(fa, id, idx);
					File.Move(jpegs[idx], nf);
				}
				for (int idx = 0; idx < gifs.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.gif";
					string nf = string.Format(fa, id, idx);
					File.Move(gifs[idx], nf);
				}
				for (int idx = 0; idx < pptxs.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.pptx";
					string nf = string.Format(fa, id, idx);
					File.Move(pptxs[idx], nf);
				}
				for (int idx = 0; idx < ppts.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.ppt";
					string nf = string.Format(fa, id, idx);
					File.Move(ppts[idx], nf);
				}
				for (int idx = 0; idx < pdfs.Count; idx++)
				{
					string fa = @"C:\WIKI\fs\{0}_{1}.pdf";
					string nf = string.Format(fa, id, idx);
					File.Move(pdfs[idx], nf);
				}
			}
		}

		private void simpleButton21_Click(object sender, EventArgs e)
		{
			string path = @"C:\WIKI\fs\pptx\";
			string[] files = Directory.GetFiles(path);
			foreach (string file in files)
			{
				File.Move(file, path + Path.GetFileName(file).Replace("_0", ""));
			}
		}

		private void simpleButton22_Click(object sender, EventArgs e)
		{
			string ppp = @"C:\WIKI\fs\comple\jpg\";
			string[] pp = Directory.GetFiles(ppp);
			foreach (string p in pp)
			{
				if (p.Contains("_1"))
				{
				}
				string fn = Path.GetFileName(p);
				fn = fn.Replace("_0", "_1");
				File.Move(p, ppp + fn);
			}
		}

		private void simpleButton23_Click(object sender, EventArgs e)
		{
			string ppp = @"C:\WIKI\pages\";
			string fff = @"C:\WIKI\pages\{0}\files\";
			string ffss = @"C:\WIKI\fs\";
			string id = "";
			string[] di = Directory.GetDirectories(ppp);
			string[] ffs = Directory.GetFiles(ffss);
			foreach (string d in di)
			{
				id = Path.GetFileName(d);
				string ff = string.Format(fff, id);

				var files = ffs.Where(x => Path.GetFileName(x).StartsWith(id));
				foreach (string f in files)
				{
					string fa = string.Format(fff, id) + Path.GetFileName(f);
					File.Move(f, fa);
				}
			}
		}

		private string wikiRoot = @"C:\Wiki Metadata\";

		//string wikiRoot = @"D:\OneDrive\OneDrive - 씨티케이코스메틱스\SalesLibrary\";
		private void simpleButton24_Click(object sender, EventArgs e)
		{
			wikiRoot = @"c:\";
			List<WikiFormulaModel> fList = new List<WikiFormulaModel>();
			string json = File.ReadAllText(wikiRoot + "fList.json");
			fList = JsonConvert.DeserializeObject<List<WikiFormulaModel>>(json);

			List<WikiPackageModel> pList = new List<WikiPackageModel>();
			string pjson = File.ReadAllText(wikiRoot + "pList.json");
			pList = JsonConvert.DeserializeObject<List<WikiPackageModel>>(pjson);

			List<int> errList = new List<int>();
			int total = fList.Count;
			int now = 1;
			int nowID = 0;
			string labels = "";
			string strJson = "";
			if (true)
			{
				string resultMsg, resultMsg1;
				string fat = "";
				StringBuilder sb = new StringBuilder();
				foreach (WikiFormulaModel fm in fList)
				{
					if (fm.Id == 16810083)
					{
					}
					//else
					//{
					//    continue;
					//}
					fat = " --action storePage --id {0} --content \"{1}\" --noConvert --encoding \"euc-kr\"";
					resultMsg = resultMsg1 = string.Empty;
					string re = GetParsePage(fm, pList);
					sb.AppendLine(string.Format(fat, fm.Id, re));
					sb.AppendLine(" --action removeLabels --id " + fm.Id + " --labels \"@all\"");
					labels = CategoryHelper.Instance.GetAncestorLableFromString(fm.분류코드);
					if (string.IsNullOrEmpty(fm.키워드) == false)
                    {
                        if(fm.키워드.Trim().EndsWith(","))
                        {
                            labels = fm.키워드 +  labels;
                        }
                        else
                        {
						    labels = fm.키워드 + "," + labels;
                        }
                    }
					if (string.IsNullOrEmpty(fm.Launching) == false)
					{
						labels += "," + fm.Launching.Replace("|", ",");
					}
					labels = labels.Replace(".", "．")
					.Replace("!", "！")
					.Replace("#", "＃")
					.Replace("&", "＆")
					.Replace("(", "（")
					.Replace(")", "）")
					.Replace("*", "＊")
					.Replace(":", "：")
					.Replace(";", "；")
					.Replace("<", "〈")
					.Replace(">", "〉")
					.Replace("?", "？")
					.Replace("@", "＠")
					.Replace("[", "［")
					.Replace("]", "］")
					.Replace("^", "＾")
					.Replace(",,", ",");

					sb.AppendLine(" --action addLabels --id " + fm.Id + " --labels \"" + labels + "\"");
				}
				File.WriteAllText(@"c:\cli\wf.txt", sb.ToString(), Encoding.UTF8);
				resultMsg = resultMsg1 = string.Empty;
				//bool result = GetConfluence(" --action run --file wp.txt --encoding utf-8", out resultMsg1);
			}

			if (false)
			{
				foreach (WikiFormulaModel fm in fList)
				{
					nowID = fm.Id;
					try
					{
						Console.WriteLine("{0}/{1} Start", now, total);
						string resultMsg, resultMsg1;
						string fat = "--action storePage --id {0} --content \"{1}\" --noConvert  --encoding \"euc-kr\"";

						resultMsg = resultMsg1 = string.Empty;
						var re = "";
						//var re = GetParsePage(fm);
						bool result = GetConfluence(string.Format(fat, fm.Id, re), out resultMsg1);
						if (result == false)
							throw new Exception();
						else
							Console.WriteLine("{0}/{1} {2} Success", now, total, nowID);
					}
					catch (Exception ex)
					{
						Console.WriteLine("{0}/{1} {2} Fail", now, total, nowID);
						Console.WriteLine(ex.Message);
						errList.Add(fm.Id);
					}
					finally
					{
						now++;
					}
				}
			}
		}

		private string GetParsePage(WikiFormulaModel fm, List<WikiPackageModel> packlist)
		{
			string 섬네일이미지 = fm.SalesLibraryLinkCode + ".png";
			string 제조사 = fm.제조사.Replace("\n", "<br/>").Replace("\t", "    ");
			//string 제조사연락처 = fm.제조사연락처.Replace("\n", "<br/>").Replace("\t", "    ");
			string 키워드 = fm.키워드.Replace("\n", "<br/>").Replace("\t", "    ");
			string 랩넘버 = fm.랩넘버.Replace("\n", "<br/>").Replace("\t", "    ");
			string 내용물단위 = fm.내용물단위.Replace("\n", "<br/>").Replace("\t", "    ");
			string 내용물단가 = fm.내용물단가.Replace("\n", "<br/>").Replace("\t", "    ");
			string 임가공단가 = fm.임가공단가.Replace("\n", "<br/>").Replace("\t", "    ");
			//string 완제품단가 = fm.완제품단가.Replace("\n", "<br/>").Replace("\t", "    ");
			string 추천용량 = fm.추천용량.Replace("\n", "<br/>").Replace("\t", "    ");
			string 실제용량 = fm.실제용량.Replace("\n", "<br/>").Replace("\t", "    ");
			string 규제 = fm.규제.Replace("\n", "<br/>").Replace("\t", "    ");
			string CTK내부개발담당자 = fm.CTK내부개발담당자.Replace("\n", "<br/>").Replace("\t", "    ");
			string 제안브랜드 = fm.제안브랜드.Replace("\n", "<br/>").Replace("\t", "    ");
			string 관련제품개발프로젝트 = fm.관련제품개발프로젝트.Replace("\n", "<br/>").Replace("\t", "    ");
			string saleslibrarylinkcode = fm.SalesLibraryLinkCode.Replace("\n", "<br/>").Replace("\t", "    ");
			string whatitis = fm.WhatItIs.Replace("\n", "<br/>").Replace("\t", "    ");
			string Launching = fm.Launching.Replace("\n", "<br/>").Replace("\t", "    ").Replace("|", ", ");
			string 패키지옵션 = fm.패키지옵션.Replace("\n", "<br/>").Replace("\t", "    ");

			var po = StringUtil.SplitByString(패키지옵션, "|");

			string poresult = "";
			if (po.Count() > 1)
			{
			}
			foreach (string s in po)
			{
				if (string.IsNullOrEmpty(s))
					continue;
				poresult += string.Format("<a href=\"https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId={0}\">{1}</a>,", s, GetPKGname(s, packlist));
			}
			if (poresult.EndsWith(","))
				poresult = poresult.Substring(0, poresult.Length - 1);
			패키지옵션 = poresult;
			if (Launching != "")
			{
			}
			string benefit = fm.Benefit.Replace("\n", "<br/>").Replace("\t", "    ");
			string 팩싯 = "";
			string JSON원본 = JsonConvert.SerializeObject(fm).Replace("\"", "&quot;");

			string 팩싯f = @"<p style='text-align: center;'><ac:image ac:border='true'><ri:attachment ri:filename='{0}_{1}.jpg' /></ac:image></p>";
			StringBuilder 팩싯s = new StringBuilder();
			for (int idx = 1; idx <= fm.FactsheetCount; idx++)
			{
				팩싯s.AppendFormat(팩싯f, saleslibrarylinkcode, idx);
			}
			팩싯 = 팩싯s.ToString();
			//string html =
			//    "<ac:layout><ac:layout-section ac:type='two_left_sidebar'><ac:layout-cell><p><br /></p><p><br /></p><p><br /></p><p>" +
			//    "<ac:image ac:align='center' ac:width='200'><ri:attachment ri:filename='#섬네일이미지#' /></ac:image></p></ac:layout-cell><ac:layout-cell>" +
			//    "<ac:structured-macro ac:name='details' ac:schema-version='1' ac:macro-id='formula-pageInfo-#macroid#'><ac:parameter ac:name='id'>f_info</ac:parameter><ac:rich-text-body>" +
			//    "<p class='auto-cursor-target'><br /></p><table class='relative-table wrapped' style='width: 99.9173%;'><colgroup><col style='width: 15.9859%;' /><col style='width: 84.0251%;' /></colgroup><tbody>" +
			//    "<tr><th>Filler (제조사)</th><td><div class='content-wrapper'><p>#제조사#</p></div></td></tr>" +
			//    //"<tr><th>Filler Contacts (제조사 연락처)</th><td><div class='content-wrapper'><p>#제조사연락처#</p></div></td></tr>" +
			//    "<tr><th>Keyword (키워드)</th><td><div class='content-wrapper'><p>#키워드#</p></div></td></tr>" +
			//    "<tr><th>Lab Number (랩넘버)</th><td><div class='content-wrapper'><p>#랩넘버#</p></div></td></tr>" +
			//    "<tr><th>ml, g (내용물 단위)</th><td><div class='content-wrapper'><p>#내용물단위#</p></div></td></tr>" +
			//    "<tr><th>bulk cost (내용물 단가)</th><td><div class='content-wrapper'><p>#내용물단가#</p></div></td></tr>" +
			//    "<tr><th>labor cost (임가공 단가)</th><td><div class='content-wrapper'><p>#임가공단가#</p></div></td></tr>" +
			//    //"<tr><th>완제품 단가</th><td><div class='content-wrapper'><p>#완제품단가#</p></div></td></tr>" +
			//    "<tr><th>Suggested Volume (추천 용량)</th><td><div class='content-wrapper'><p>#추천용량#</p></div></td></tr>" +
			//    "<tr><th>Actual Volume (실제 용량)</th><td><div class='content-wrapper'><p>#실제용량#</p></div></td></tr>" +
			//    "<tr><th>Compliance (규제)</th><td><div class='content-wrapper'><p>#규제#</p></div></td></tr>" +
			//    "<tr><th>Person in charge (CTK 내부 개발담당자)</th><td><div class='content-wrapper'><p>#CTK내부개발담당자#</p></div></td></tr>" +
			//    "<tr><th>Brand (제안브랜드 - 지속적인 update)</th><td><div class='content-wrapper'><p>#제안브랜드#</p></div></td></tr>" +
			//    "<tr><th>Related Projects (관련 제품 개발 프로젝트 - 지속적인 update)</th><td><div class='content-wrapper'><p>#관련제품개발프로젝트#</p></div></td></tr>" +
			//    "<tr><th>Launching (런칭)</th><td><div class='content-wrapper'><p>#Launching#</p></div></td></tr>" +
			//    //"<tr><th>Sales Library Link</th><td><div class='content-wrapper'><p>#saleslibrarylinkcode#</p></div></td></tr>" +
			//    "<tr><th>Package Options</th><td><div class='content-wrapper'><p>#packageoptions#</p></div></td></tr>" +
			//    "</tbody></table>" +
			//    "</ac:rich-text-body></ac:structured-macro>" +
			//    "<h2>What it is</h2><p>#whatitis#</p>" +
			//    "<h2>Benefit</h2><p>#benefit#</p>" +
			//    "</ac:layout-cell></ac:layout-section>" +
			//    "<ac:layout-section ac:type='single'><ac:layout-cell>" +
			//    "<h1><strong><em>FACTSHEET</em></strong></h1><hr />#팩싯#</ac:layout-cell></ac:layout-section>"+
			//    "<ac:layout-section ac:type='single'><ac:layout-cell><p class='auto-cursor-target'><br /></p>" +
			//    "<ac:structured-macro ac:name='details' ac:schema-version='1' ac:macro-id='formula-pageInfo-#macroid#-2'>" +
			//    "<ac:parameter ac:name='id'>f_json</ac:parameter><ac:parameter ac:name='hidden'>true</ac:parameter><ac:rich-text-body>" +
			//    "<p><br /></p><table><colgroup><col /><col /></colgroup><tbody><tr><td>JSON</td><td><div><p>#JSON원본#</p></div></td></tr></tbody></table><p><br /></p>" +
			//    "</ac:rich-text-body></ac:structured-macro><p class='auto-cursor-target'><br /></p></ac:layout-cell></ac:layout-section></ac:layout>";

			string html =
	"<ac:layout><ac:layout-section ac:type='two_left_sidebar'><ac:layout-cell><p><br /></p><p><br /></p><p><br /></p><p>" +
	"<ac:image ac:align='center' ac:width='200'><ri:attachment ri:filename='#섬네일이미지#' /></ac:image></p></ac:layout-cell><ac:layout-cell>" +
	"<ac:structured-macro ac:name='details' ac:schema-version='1' ac:macro-id='formula-pageInfo-#macroid#'><ac:parameter ac:name='id'>f_info</ac:parameter><ac:rich-text-body>" +
	"<p class='auto-cursor-target'><br /></p><table class='relative-table wrapped' style='width: 99.9173%;'><colgroup><col style='width: 15.9859%;' /><col style='width: 84.0251%;' /></colgroup><tbody>" +
	"<tr><th>Filler</th><td><div class='content-wrapper'><p>#제조사#</p></div></td></tr>" +
	"<tr><th>Keyword</th><td><div class='content-wrapper'><p>#키워드#</p></div></td></tr>" +
	"<tr><th>Lab Number</th><td><div class='content-wrapper'><p>#랩넘버#</p></div></td></tr>" +
	"<tr><th>ml, g</th><td><div class='content-wrapper'><p>#내용물단위#</p></div></td></tr>" +
	"<tr><th>bulk cost</th><td><div class='content-wrapper'><p>#내용물단가#</p></div></td></tr>" +
	"<tr><th>labor cost</th><td><div class='content-wrapper'><p>#임가공단가#</p></div></td></tr>" +
	"<tr><th>Suggested Volume</th><td><div class='content-wrapper'><p>#추천용량#</p></div></td></tr>" +
	"<tr><th>Actual Volume</th><td><div class='content-wrapper'><p>#실제용량#</p></div></td></tr>" +
	"<tr><th>Compliance</th><td><div class='content-wrapper'><p>#규제#</p></div></td></tr>" +
	"<tr><th>Person in charge</th><td><div class='content-wrapper'><p>#CTK내부개발담당자#</p></div></td></tr>" +
	"<tr><th>Brand</th><td><div class='content-wrapper'><p>#제안브랜드#</p></div></td></tr>" +
	"<tr><th>Related Projects</th><td><div class='content-wrapper'><p>#관련제품개발프로젝트#</p></div></td></tr>" +
	"<tr><th>Launching</th><td><div class='content-wrapper'><p>#Launching#</p></div></td></tr>" +
	"<tr><th>Package Options</th><td><div class='content-wrapper'><p>#packageoptions#</p></div></td></tr>" +
	"</tbody></table>" +
	"</ac:rich-text-body></ac:structured-macro>" +
	"<h2>What it is</h2><p>#whatitis#</p>" +
	"<h2>Benefit</h2><p>#benefit#</p>" +
	"</ac:layout-cell></ac:layout-section>" +
	"<ac:layout-section ac:type='single'><ac:layout-cell>" +
	"<h1><strong><em>FACTSHEET</em></strong></h1><hr />#팩싯#</ac:layout-cell></ac:layout-section>" +
	"<ac:layout-section ac:type='single'><ac:layout-cell><p class='auto-cursor-target'><br /></p>" +
	"<ac:structured-macro ac:name='details' ac:schema-version='1' ac:macro-id='formula-pageInfo-#macroid#-2'>" +
	"<ac:parameter ac:name='id'>f_json</ac:parameter><ac:parameter ac:name='hidden'>true</ac:parameter><ac:rich-text-body>" +
	"<p><br /></p><table><colgroup><col /><col /></colgroup><tbody><tr><td>JSON</td><td><div><p>#JSON원본#</p></div></td></tr></tbody></table><p><br /></p>" +
	"</ac:rich-text-body></ac:structured-macro><p class='auto-cursor-target'><br /></p></ac:layout-cell></ac:layout-section></ac:layout>";

			;
			html = html
					.Replace("#섬네일이미지#", 섬네일이미지)
					.Replace("#제조사#", 제조사)
					//.Replace("#제조사연락처#", 제조사연락처)
					.Replace("#키워드#", 키워드)
					.Replace("#랩넘버#", 랩넘버)
					.Replace("#내용물단위#", 내용물단위)
					.Replace("#내용물단가#", 내용물단가)
					.Replace("#임가공단가#", 임가공단가)
					//.Replace("#완제품단가#", 완제품단가)
					.Replace("#추천용량#", 추천용량)
					.Replace("#실제용량#", 실제용량)
					.Replace("#규제#", 규제)
					.Replace("#macroid#", fm.Id.ToString().Trim())
					.Replace("#CTK내부개발담당자#", CTK내부개발담당자)
					.Replace("#제안브랜드#", 제안브랜드)
					.Replace("#관련제품개발프로젝트#", 관련제품개발프로젝트)
					.Replace("#Launching#", Launching)
					.Replace("#saleslibrarylinkcode#", saleslibrarylinkcode)
					.Replace("#whatitis#", whatitis)
					.Replace("#benefit#", benefit)
					.Replace("#팩싯#", 팩싯)
					.Replace("#packageoptions#", 패키지옵션)
					.Replace("#JSON원본#", JSON원본);

			return html;
		}

		private string GetPKGname(string packageid, List<WikiPackageModel> packlist)
		{
			var obj = packlist.FirstOrDefault(x => x.Id == packageid.ToIntEx());
			if (obj != null)
			{
				return obj.제목;
			}
			else
				return string.Empty;
		}

		private string GetFORname(string formula, List<WikiFormulaModel> forlist)
		{
			var obj = forlist.FirstOrDefault(x => x.Id == formula.ToIntEx());
			if (obj != null)
			{
				return obj.제목;
			}
			else
				return string.Empty;
		}

		private string GetParsePage(WikiPackageModel pm, List<WikiFormulaModel> flist)
		{
			string 제조사 = pm.제조사.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 연락처 = pm.연락처.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 키워드 = pm.키워드.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 통화 = pm.통화.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string MOQ = pm.MOQ.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 가격 = pm.가격.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 가격업데이트날짜 = pm.가격업데이트날짜.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 용량 = pm.용량.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 용량단위 = pm.용량단위.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 포뮬라타입 = pm.포뮬라타입.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string Material = pm.Material.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 런칭제품 = pm.런칭제품.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string saleslibrarylinkcode = pm.SalesLibraryLinkCode.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			saleslibrarylinkcode = saleslibrarylinkcode.Trim();
			string description = pm.Description.ToStringEx().Replace("\n", "<br/>").Replace("\t", "    ");
			string 섬네일이미지 = saleslibrarylinkcode + ".png";
			string JSON원본 = JsonConvert.SerializeObject(pm).Replace("\"", "&quot;");
			string 팩싯 = "";
			string 팩싯f = @"<p style='text-align: center;'><ac:image ac:border='true'><ri:attachment ri:filename='{0}_{1}.jpg' /></ac:image></p>";

			string 포뮬라옵션 = pm.포뮬라옵션.Replace("\n", "<br/>").Replace("\t", "    ");

			var po = StringUtil.SplitByString(포뮬라옵션, "|");

			string poresult = "";
			if (po.Count() > 1)
			{
			}
			foreach (string s in po)
			{
				if (string.IsNullOrEmpty(s))
					continue;
				poresult += string.Format("<a href=\"https://wiki.ctkcosmetics.biz/pages/viewpage.action?pageId={0}\">{1}</a>,", s, GetFORname(s, flist));
			}
			if (poresult.EndsWith(","))
				poresult = poresult.Substring(0, poresult.Length - 1);
			포뮬라옵션 = poresult;

			StringBuilder 팩싯s = new StringBuilder();
			for (int idx = 1; idx <= pm.FactsheetCount; idx++)
			{
				팩싯s.AppendFormat(팩싯f, saleslibrarylinkcode, idx);
			}
			팩싯 = 팩싯s.ToString();
			string html =
				"<ac:layout><ac:layout-section ac:type='two_left_sidebar'><ac:layout-cell><p><br /></p><p><br /></p><p><br /></p><p>" +
				"<ac:image ac:align='center' ac:width='200'><ri:attachment ri:filename='#섬네일이미지#' /></ac:image></p></ac:layout-cell><ac:layout-cell>" +
				"<ac:structured-macro ac:name='details' ac:schema-version='1' ac:macro-id='package-pageInfo-#macroid#'><ac:parameter ac:name='id'>p_info</ac:parameter><ac:rich-text-body>" +
				"<p class='auto-cursor-target'><br /></p><table class='relative-table wrapped' style='width: 99.9173%;'><colgroup><col style='width: 15.9859%;' /><col style='width: 84.0251%;' /></colgroup><tbody>" +
				"<tr><th>Manufacturer</th><td><div class='content-wrapper'><p><span>#제조사#</span></p></div></td></tr>" +
				"<tr><th>Contact Person</th><td><div class='content-wrapper'><p><span>#연락처#</span></p></div></td></tr>" +
				"<tr><th>Keyword</th><td><div class='content-wrapper'><p><span>#키워드#</span></p></div></td></tr>" +
				"<tr><th>MOQ</th><td><div class='content-wrapper'><p><span>#MOQ#</span></p></div></td></tr>" +
				"<tr><th>Currency</th><td><div class='content-wrapper'><p><span>#통화#</span></p></div></td></tr>" +
				"<tr><th>Price</th><td><div class='content-wrapper'><p><span>#가격#</span></p></div></td></tr>" +
				"<tr><th>Issued date for the price</th><td><div class='content-wrapper'><p><span>#가격업데이트날짜#</span></p></div></td></tr>" +
				"<tr><th>Volume</th><td><div class='content-wrapper'><p><span>#용량#</span></p></div></td></tr>" +
				"<tr><th>Unit</th><td><div class='content-wrapper'><p><span>#용량단위#</span></p></div></td></tr>" +
				"<tr><th>Formula</th><td><div class='content-wrapper'><p><span>#포뮬라타입#</span></p></div></td></tr>" +
				"<tr><th>Material</th><td><div class='content-wrapper'><p><span>#Material#</span></p></div></td></tr>" +
				"<tr><th>Launched Product</th><td><div class='content-wrapper'><p><span>#런칭제품#</span></p></div></td></tr>" +
				"<tr><th>Sales Library Link</th><td><div class='content-wrapper'><p><span>#saleslibrarylinkcode#</span></p></div></td></tr>" +
				"<tr><th>Formula Options</th><td><div class='content-wrapper'><p><span>#formulaoptions#</span></p></div></td></tr>" +
				"</tbody></table>" +
				"</ac:rich-text-body></ac:structured-macro>" +
				"<h3>Description</h3><p>#description#</p></ac:layout-cell></ac:layout-section>" +
				"<ac:layout-section ac:type='single'><ac:layout-cell>" +
				"<h1><strong><em>FACTSHEET</em></strong></h1><hr />#팩싯#</ac:layout-cell></ac:layout-section>" +
				"<ac:layout-section ac:type='single'><ac:layout-cell><p class='auto-cursor-target'><br /></p>" +
				"<ac:structured-macro ac:name='details' ac:schema-version='1' ac:macro-id='package-pageInfo-#macroid#-2'>" +
				"<ac:parameter ac:name='id'>p_json</ac:parameter><ac:parameter ac:name='hidden'>true</ac:parameter><ac:rich-text-body>" +
				"<p><br /></p><table><colgroup><col /><col /></colgroup><tbody><tr><td>JSON</td><td><div><p>#JSON원본#</p></div></td></tr></tbody></table><p><br /></p>" +
				"</ac:rich-text-body></ac:structured-macro><p class='auto-cursor-target'><br /></p></ac:layout-cell></ac:layout-section></ac:layout>";
			;
			html = html
					.Replace("#섬네일이미지#", 섬네일이미지)
					.Replace("#제조사#", 제조사)
					.Replace("#연락처#", 연락처)
					.Replace("#키워드#", 키워드)
					.Replace("#MOQ#", MOQ)
					.Replace("#통화#", 통화)
					.Replace("#가격#", 가격)
					.Replace("#가격업데이트날짜#", 가격업데이트날짜)
					.Replace("#용량#", 용량)
					.Replace("#용량단위#", 용량단위)
					.Replace("#포뮬라타입#", 포뮬라타입)
					.Replace("#Material#", Material)
					.Replace("#런칭제품#", 런칭제품)
					.Replace("#saleslibrarylinkcode#", saleslibrarylinkcode)
					.Replace("#description#", description)
					.Replace("#formulaoptions#", 포뮬라옵션)
					.Replace("#팩싯#", 팩싯)
					.Replace("#macroid#", pm.Id.ToString().Trim())
					.Replace("#JSON원본#", JSON원본);

			return html;
		}

		private void simpleButton25_Click(object sender, EventArgs e)
		{
			wikiRoot = @"c:\";
			List<WikiPackageModel> pList = new List<WikiPackageModel>();
			string json = File.ReadAllText(wikiRoot + "pList.json");
			//string json = File.ReadAllText(wikiRoot + "pList2.json");
			pList = JsonConvert.DeserializeObject<List<WikiPackageModel>>(json);

			List<WikiFormulaModel> fList = new List<WikiFormulaModel>();
			string fjson = File.ReadAllText(wikiRoot + "fList.json");
			fList = JsonConvert.DeserializeObject<List<WikiFormulaModel>>(fjson);

			List<int> errList = new List<int>();
			int total = pList.Count;
			int now = 1;
			int nowID = 0;

			string resultMsg, resultMsg1;
			string fat = "";
			StringBuilder sb = new StringBuilder();
			foreach (WikiPackageModel pm in pList)
			{
				fat = " --action storePage --id {0} --content \"{1}\" --noConvert --encoding \"euc-kr\"";
				resultMsg = resultMsg1 = string.Empty;
				string re = GetParsePage(pm, fList);
				sb.AppendLine(string.Format(fat, pm.Id, re));
				//sb.AppendLine(" --action renamePage --id " + pm.Id + " --newTitle \"" + pm.제목 + "\" --encoding \"utf-8\"");
				sb.AppendLine(" --action removeLabels --id " + pm.Id + " --labels \"@all\"");
				var lb = CategoryHelper.Instance.GetAncestorLableFromString(pm.분류코드).Replace("135", "147");
				sb.AppendLine(" --action addLabels --id " + pm.Id + " --labels \"" + (pm.키워드 + "," + lb) + "\"");
			}
			File.WriteAllText(@"c:\cli\wp.txt", sb.ToString(), Encoding.UTF8);
			resultMsg = resultMsg1 = string.Empty;
			//bool result = GetConfluence(" --action run --file wp.txt --encoding utf-8", out resultMsg1);
		}

		private void simpleButton26_Click(object sender, EventArgs e)
		{
			simpleButton24_Click(null, null);
			simpleButton25_Click(null, null);

			string so = File.ReadAllText(@"c:\cli\wp.txt", Encoding.UTF8);
			so += Environment.NewLine;
			so += File.ReadAllText(@"c:\cli\wf.txt", Encoding.UTF8);

			File.WriteAllText(@"c:\cli\wi.txt", so, Encoding.UTF8);
			//List<WikiPackageModel> pList = new List<WikiPackageModel>();
			//string json = File.ReadAllText(wikiRoot + "pList.json");
			//pList = JsonConvert.DeserializeObject<List<WikiPackageModel>>(json);
			//foreach (WikiPackageModel v in pList)
			//{
			//    if (string.IsNullOrEmpty(v.MOQ) == false)
			//        v.MOQ = v.MOQ.Replace("\n", "<br/>");
			//    if (string.IsNullOrEmpty(v.Material) == false)
			//        v.Material = v.Material.Replace("\n", "<br/>");
			//}

			//File.WriteAllText(wikiRoot + "pList.json", JsonConvert.SerializeObject(pList), Encoding.UTF8);
		}

		private void simpleButton27_Click(object sender, EventArgs e)
		{
			spreadsheetControl1.LoadDocument(@"C:\Users\bhjin\Downloads\2차 누락데이터 수정 (1).xlsx");
			DevExpress.Spreadsheet.Worksheet ws = spreadsheetControl1.ActiveWorksheet;

			List<WikiPackageModel> pList = new List<WikiPackageModel>();
			string json = File.ReadAllText(wikiRoot + "pList.json");
			pList = JsonConvert.DeserializeObject<List<WikiPackageModel>>(json);
			foreach (WikiPackageModel vpm in pList)
			{
				//vpm.NowUpdate = false;
			}

			WikiPackageModel pm = null;
			int cnt = ws.Rows.LastUsedIndex.ToIntEx();
			int id = -1;
			for (int idx = 1; idx < cnt; idx++)
			{
				try
				{
					id = GetValue(ws.GetCellValue(0, idx)).ToIntEx();
					pm = pList.First(x => x.Id == id);
					//pm.ParentId = GetValue(ws.GetCellValue(1, idx)).ToIntEx();
					pm.제목 = GetValue(ws.GetCellValue(3, idx));
					pm.제조사 = GetValue(ws.GetCellValue(4, idx));
					pm.연락처 = GetValue(ws.GetCellValue(5, idx));
					pm.키워드 = GetValue(ws.GetCellValue(6, idx));
					pm.MOQ = GetValue(ws.GetCellValue(7, idx));
					pm.통화 = GetValue(ws.GetCellValue(8, idx));
					pm.가격 = GetValue(ws.GetCellValue(9, idx));
					pm.가격업데이트날짜 = GetValue(ws.GetCellValue(10, idx));
					pm.용량 = GetValue(ws.GetCellValue(11, idx));
					pm.용량단위 = GetValue(ws.GetCellValue(12, idx));
					pm.포뮬라타입 = GetValue(ws.GetCellValue(13, idx));
					pm.Material = GetValue(ws.GetCellValue(14, idx));
					pm.런칭제품 = GetValue(ws.GetCellValue(15, idx));
					//pm.Visible = true;
					//pm.NowUpdate = true;
				}
				catch (Exception ex)
				{
				}
			}
			string st = JsonConvert.SerializeObject(pList);
			File.WriteAllText(wikiRoot + "pList.json", st, Encoding.UTF8);
			if (true)
			{
			}
		}

		private string GetValue(CellValue cvel)
		{
			var a = cvel.ToString();
			if (cvel.TextValue == null && cvel.NumericValue != 0)
				return cvel.NumericValue.ToString();
			return cvel.TextValue;
		}

		private void simpleButton28_Click(object sender, EventArgs e)
		{
			List<WikiPackageModel> pList = new List<WikiPackageModel>();
			string json = File.ReadAllText(wikiRoot + "pList.json");
			pList = JsonConvert.DeserializeObject<List<WikiPackageModel>>(json);
			string resultMsg = "";
			List<int> errlist = new List<int>();
			bool result = false;
			foreach (WikiPackageModel v in pList)
			{
				//if (v.NowUpdate == false) continue;
				//try
				//{
				//    resultMsg = string.Empty;
				//    result = GetConfluence(" --action renamePage --id " + v.Id + " --newTitle " + v.제목 , out resultMsg);
				//    if (result == false)
				//        throw new Exception();
				//}
				//catch(Exception ex)
				//{
				//    errlist.Add(v.Id);
				//}
			}
		}

		private void simpleButton29_Click(object sender, EventArgs e)
		{
			var sd = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\aa.txt", Encoding.UTF8));
			StringBuilder sb = new StringBuilder();
			foreach (string s in sd)
			{
				sb.AppendLine(s.GetInitialConsonantStringEx());
			}
			var aaes = sb.ToString();
		}

		private void simpleButton30_Click(object sender, EventArgs e)
		{
			spreadsheetControl1.LoadDocument(@"C:\Users\bhjin\Desktop\2019-09-24 신규등록 목록.xlsx");
			DevExpress.Spreadsheet.Worksheet ws = spreadsheetControl1.ActiveWorksheet;

			//List<WikiPackageModel> pList = new List<WikiPackageModel>();
			//string json = File.ReadAllText(wikiRoot + "pList.json");
			//pList = JsonConvert.DeserializeObject<List<WikiPackageModel>>(json);
			//foreach (WikiPackageModel vpm in pList)
			//{
			//    vpm.NowUpdate = false;
			//}

			string resultMsg = "";
			bool result = false;
			int cnt = ws.Rows.LastUsedIndex.ToIntEx();
			int id = -1;

			string title = "";
			string parent = "";
			string ids = "";
			string name = "";
			string file = "";
			string seq = "";
			for (int idx = 8; idx <= cnt; idx++)
			{
				try
				{
					Console.WriteLine(idx);
					//--action addPage  --space "zclipermissions"  --title "child1"  --parent "children"
					seq = GetValue(ws.GetCellValue(0, idx)).ToStringEx();

					ids = GetValue(ws.GetCellValue(1, idx)).ToStringEx();
					parent = GetValue(ws.GetCellValue(2, idx)).ToStringEx();
					resultMsg = string.Empty;
					//
					//result = GetConfluence(" --action addAttachment  --id " + ids + "  --name " + seq + ".png --file \"C:\\Users\\bhjin\\Downloads\\parent 누락건\\새 폴더\\" + seq + ".png\"", out resultMsg);
					//result = GetConfluence(" --action addAttachment  --id " + ids + "  --name " + seq + "_1.jpg --file \"C:\\Users\\bhjin\\Downloads\\parent 누락건\\새 폴더\\" + seq + "_1.jpg\"", out resultMsg);
					//result = GetConfluence(" --action addAttachment  --id " + ids + "  --name " + seq + ".pptx --file \"C:\\Users\\bhjin\\Downloads\\parent 누락건\\새 폴더\\" + seq + ".pptx\"", out resultMsg);

					////string fat = " --action storePage --id {0} --content \"{1}\" --noConvert --encoding \"euc-kr\"";

					////string 섬네일이미지 = seq + ".png";
					////string 제조사 = GetValue(ws.GetCellValue(4, idx)).ToStringEx();
					////string 연락처                = GetValue(ws.GetCellValue(5, idx)).ToStringEx();
					////string 키워드                = GetValue(ws.GetCellValue(6, idx)).ToStringEx();
					////string 통화                  = GetValue(ws.GetCellValue(8, idx)).ToStringEx();
					////string MOQ                   = GetValue(ws.GetCellValue(7, idx)).ToStringEx();
					////string 가격                  = GetValue(ws.GetCellValue(9, idx)).ToStringEx();
					////string 가격업데이트날짜      = GetValue(ws.GetCellValue(10, idx)).ToStringEx();
					////string 용량                  = GetValue(ws.GetCellValue(11, idx)).ToStringEx();
					////string 용량단위              = GetValue(ws.GetCellValue(12, idx)).ToStringEx();
					////string 포뮬라타입            = GetValue(ws.GetCellValue(13, idx)).ToStringEx();
					////string Material              = GetValue(ws.GetCellValue(14, idx)).ToStringEx();
					////string 런칭제품              = GetValue(ws.GetCellValue(15, idx)).ToStringEx();
					////string saleslibrarylinkcode  = GetValue(ws.GetCellValue(17, idx)).ToStringEx();
					////string description           = GetValue(ws.GetCellValue(17, idx)).ToStringEx();
					////string 팩싯                   = "";
					////string 팩싯f = @"<p style='text-align: center;'><ac:image ac:border='true'><ri:attachment ri:filename='{0}' /></ac:image></p>";
					////팩싯 = string.Format(팩싯f, seq + "_1.jpg");
					////string html = "<ac:layout><ac:layout-section ac:type='two_left_sidebar'><ac:layout-cell><p><br /></p><p><br /></p><p><br /></p><p><ac:image ac:thumbnail='true' ac:align='center'><ri:attachment ri:filename='#섬네일이미지#' /></ac:image></p></ac:layout-cell><ac:layout-cell><p class='auto-cursor-target'><br /></p><table class='relative-table wrapped' style='width: 99.9173%;'><colgroup><col style='width: 15.9859%;' /><col style='width: 84.0251%;' /></colgroup><tbody>" +
					////    "<tr><th>제조사</th><td><div class='content-wrapper'><p><span>#제조사#</span></p></div></td></tr>" +
					////    "<tr><th>연락처</th><td><div class='content-wrapper'><p><span>#연락처#</span></p></div></td></tr>" +
					////    "<tr><th>키워드</th><td><div class='content-wrapper'><p><span>#키워드#</span></p></div></td></tr>" +
					////    "<tr><th>MOQ</th><td><div class='content-wrapper'><p><span>#MOQ#</span></p></div></td></tr>" +
					////    "<tr><th>통화</th><td><div class='content-wrapper'><p><span>#통화#</span></p></div></td></tr>" +
					////    "<tr><th>가격</th><td><div class='content-wrapper'><p><span>#가격#</span></p></div></td></tr>" +
					////    "<tr><th>가격 업데이트 날짜</th><td><div class='content-wrapper'><p><span>#가격업데이트날짜#</span></p></div></td></tr>" +
					////    "<tr><th>용량</th><td><div class='content-wrapper'><p><span>#용량#</span></p></div></td></tr>" +
					////    "<tr><th>용량 단위</th><td><div class='content-wrapper'><p><span>#용량단위#</span></p></div></td></tr>" +
					////    "<tr><th>포뮬라 타입</th><td><div class='content-wrapper'><p><span>#포뮬라타입#</span></p></div></td></tr>" +
					////    "<tr><th>Material</th><td><div class='content-wrapper'><p><span>#Material#</span></p></div></td></tr>" +
					////    "<tr><th>런칭제품</th><td><div class='content-wrapper'><p><span>#런칭제품#</span></p></div></td></tr>" +
					////    "<tr><th>Sales Library Link (code)</th><td><div class='content-wrapper'><p><span>#saleslibrarylinkcode#</span></p></div></td></tr>" +
					////    "</tbody></table>" +
					////    "<h3>Description</h3><p>#description#</p></ac:layout-cell></ac:layout-section>" +
					////    "<ac:layout-section ac:type='single'><ac:layout-cell><h1><strong>" +
					////    "<em>FACTSHEET</em></strong></h1><hr />#팩싯#</ac:layout-cell></ac:layout-section></ac:layout>";
					////html = html
					////        .Replace("#섬네일이미지#", 섬네일이미지)
					////        .Replace("#제조사#", 제조사)
					////        .Replace("#연락처#", 연락처)
					////        .Replace("#키워드#", 키워드)
					////        .Replace("#MOQ#", MOQ)
					////        .Replace("#통화#", 통화)
					////        .Replace("#가격#", 가격)
					////        .Replace("#가격업데이트날짜#", 가격업데이트날짜)
					////        .Replace("#용량#", 용량)
					////        .Replace("#용량단위#", 용량단위)
					////        .Replace("#포뮬라타입#", 포뮬라타입)
					////        .Replace("#Material#", Material)
					////        .Replace("#런칭제품#", 런칭제품)
					////        .Replace("#saleslibrarylinkcode#", saleslibrarylinkcode)
					////        .Replace("#description#", description)
					////        .Replace("#팩싯#", 팩싯)
					////        .Replace("\n", "<br/>");

					//string resultMsg1 = "";
					//result = GetConfluence(string.Format(fat, ids, html), out resultMsg1);
					//if (result == false)
					//    throw new Exception();

					string fat = " --action addLabels  --labels \"package,{0}\"  --id {1}";
					string labels = GetValue(ws.GetCellValue(18, idx)).ToStringEx();
					string resultMsg1 = "";
					result = GetConfluence(string.Format(fat, labels, ids), out resultMsg1);
					if (result == false)
						throw new Exception();

					//result = GetConfluence(" --action addPage  --space packagelib  --title \"" + title.Trim() + "\"  --parent " + parent, out resultMsg);
					//if (result == false)
					//    throw new Exception();
					//else
					//{
					//    resultMsg = resultMsg.Replace("\r\n", "");
					//    var idstr = StringUtil.SplitByString(resultMsg, "Page has id ").Last();
					//    ws.Cells["B" + (idx + 1).ToString()].Value = idstr.Replace(".", "").Trim();
					//}
					//try
					//{
					//    id = GetValue(ws.GetCellValue(0, idx)).ToIntEx();
					//    pm = pList.First(x => x.Id == id);
					//    pm.ParentId = GetValue(ws.GetCellValue(1, idx)).ToIntEx();
					//    pm.제목 = GetValue(ws.GetCellValue(3, idx));
					//}
				}
				catch (Exception ex)
				{
					Console.WriteLine(idx.ToString() + ex.Message);
				}
			}

			spreadsheetControl1.SaveDocument();
		}

		private void simpleButton31_Click(object sender, EventArgs e)
		{
			var files = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\flist.txt"));
			int idx = 1;
			int tot = files.Count();
			string fat = @"c:\cdt\f_{0}.txt";

			string origin = "";
			string fo = "";
			foreach (string f in files)
			{
				fo = string.Format(fat, f);
				if (File.Exists(fo))
				{
					origin = File.ReadAllText(fo);
				}
				else
				{
				}
			}
		}

		private void simpleButton32_Click(object sender, EventArgs e)
		{
			formulaload();
			packageload();
		}

		private void packageload()
		{
			//spreadsheetControl1.LoadDocument(@"\\192.168.0.40\nict\22.WIKI\메타데이타\04.Wiki_package.xlsx");
			spreadsheetControl1.LoadDocument(@"c:\wiki.xlsx");
			DevExpress.Spreadsheet.Worksheet ws = spreadsheetControl1.ActiveWorksheet;
			wikiRoot = @"c:\";
			int cnt = ws.Rows.LastUsedIndex.ToIntEx();

			List<WikiPackageModel> list = new List<WikiPackageModel>();

			WikiPackageModel pm = null;
			for (int idx = 1; idx <= cnt; idx++)
			{
				pm = new WikiPackageModel();
				pm.Id = ws.GetCellValue(0, idx).GetInt();
				pm.제목 = ws.GetCellValue(1, idx).GetStr();
				pm.제조사 = ws.GetCellValue(2, idx).GetStr();
				pm.연락처 = ws.GetCellValue(3, idx).GetStr();
				pm.키워드 = ws.GetCellValue(4, idx).GetStr();
				pm.MOQ = ws.GetCellValue(5, idx).GetStr();
				pm.통화 = ws.GetCellValue(6, idx).GetStr();
				pm.가격 = ws.GetCellValue(7, idx).GetStr();
				pm.가격업데이트날짜 = ws.GetCellValue(8, idx).GetStr();
				pm.용량 = ws.GetCellValue(9, idx).GetStr();
				pm.용량단위 = ws.GetCellValue(10, idx).GetStr();
				pm.포뮬라타입 = ws.GetCellValue(11, idx).GetStr();
				pm.Material = ws.GetCellValue(12, idx).GetStr();
				pm.런칭제품 = ws.GetCellValue(13, idx).GetStr();
				pm.SalesLibraryLinkCode = ws.GetCellValue(14, idx).GetStr();
				pm.Description = ws.GetCellValue(15, idx).GetStr();
				pm.분류코드 = ws.GetCellValue(16, idx).GetStr();
				pm.용량구분 = ws.GetCellValue(17, idx).GetStr();
				pm.FactsheetCount = ws.GetCellValue(18, idx).GetInt();
				pm.포뮬라옵션 = ws.GetCellValue(19, idx).GetStr();

				list.Add(pm);
			}
			string strJson = JsonConvert.SerializeObject(list);
			File.WriteAllText(wikiRoot + @"\pList.json", strJson, Encoding.UTF8);
			//File.WriteAllText(@"\\192.168.0.40\ict\22.WIKI\메타데이타\04.Wiki_package.json", strJson, Encoding.UTF8);
			//File.WriteAllText(wikiRoot + @"\pList2.json", strJson, Encoding.UTF8);
		}

		private void formulaload()
		{
			spreadsheetControl1.LoadDocument(@"\\192.168.0.40\nict\22.WIKI\메타데이타\03.Wiki_formula.xlsx");
            //spreadsheetControl1.LoadDocument(@"c:\wiki.xlsx");
            DevExpress.Spreadsheet.Worksheet ws = spreadsheetControl1.ActiveWorksheet;
			wikiRoot = @"c:\";
			int cnt = ws.Rows.LastUsedIndex.ToIntEx();

			List<WikiFormulaModel> list = new List<WikiFormulaModel>();

			WikiFormulaModel fm = null;
			for (int idx = 1; idx <= cnt; idx++)
			{
				fm = new WikiFormulaModel();

				fm.Id = ws.GetCellValue(0, idx).GetInt();
				fm.제목 = ws.GetCellValue(1, idx).GetStr();
				fm.제조사 = ws.GetCellValue(2, idx).GetStr();
				fm.키워드 = ws.GetCellValue(3, idx).GetStr();
				fm.랩넘버 = ws.GetCellValue(4, idx).GetStr();
				fm.내용물단위 = ws.GetCellValue(5, idx).GetStr();
				fm.내용물단가 = ws.GetCellValue(6, idx).GetStr();
				fm.임가공단가 = ws.GetCellValue(7, idx).GetStr();
				fm.추천용량 = ws.GetCellValue(8, idx).GetStr();
				fm.실제용량 = ws.GetCellValue(9, idx).GetStr();
				fm.규제 = ws.GetCellValue(10, idx).GetStr();
				fm.CTK내부개발담당자 = ws.GetCellValue(11, idx).GetStr();
				fm.제안브랜드 = ws.GetCellValue(12, idx).GetStr();
				fm.관련제품개발프로젝트 = ws.GetCellValue(13, idx).GetStr();
				fm.SalesLibraryLinkCode = ws.GetCellValue(14, idx).GetStr();
				fm.WhatItIs = ws.GetCellValue(15, idx).GetStr().Replace("\v", "\n"); ;
				fm.Benefit = ws.GetCellValue(16, idx).GetStr().Replace("\v", "\n"); ;
				fm.분류코드 = ws.GetCellValue(17, idx).GetStr();
				fm.FactsheetCount = ws.GetCellValue(18, idx).GetInt();
				fm.Launching = ws.GetCellValue(19, idx).GetStr();
				fm.패키지옵션 = ws.GetCellValue(20, idx).GetStr();
				list.Add(fm);
			}
			string strJson = JsonConvert.SerializeObject(list);
			File.WriteAllText(wikiRoot + @"\fList.json", strJson, Encoding.UTF8);
			File.WriteAllText(@"\\192.168.0.40\nict\22.WIKI\메타데이타\03.Wiki_formula.json", strJson, Encoding.UTF8);
		}

		private string GetLableEx(string 분류코드, string Labels)
		{
			List<int> salesCategory = new List<int>();
			var labels = StringUtil.SplitByString(분류코드, ",");
			foreach (string s in labels)
			{
				salesCategory.AddRange(CategoryHelper.Instance.GetAncestorLable(s.ToIntEx()));
			}
			string result = "";
			salesCategory = salesCategory.Distinct().ToList();
			salesCategory.Sort();
			foreach (int c in salesCategory)
			{
				result += string.Format("{0},", c);
			}

			if (string.IsNullOrEmpty(Labels))
			{
				if (result.EndsWith(","))
					result = result.Substring(0, result.Length - 1);
			}
			else
			{
				if (result.EndsWith(",") == false)
					result = result + "," + Labels;
				else
					result = result + Labels;
			}
			return result;
		}

		private void simpleButton33_Click(object sender, EventArgs e)
		{
			var files = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\aa.txt"));
			foreach (string id in files)
			{
				string txt = File.ReadAllText(string.Format(@"{0}{1}\{1}.txt", wikiRoot, id));
				var rs = StringUtil.SplitByString(txt, "\n");
				string origin = rs[20].Substring(31, rs[20].Length - 31);

				HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
				doc.LoadHtml(origin);
				HtmlAgilityPack.HtmlNodeCollection ncs = doc.DocumentNode.SelectNodes(("//*[name()='ac:layout-section']"));
				StringBuilder 팩싯s = new StringBuilder();

				int oridx = origin.IndexOf("<em>FACTSHEET</em>");
				if (oridx == -1)
					oridx = origin.IndexOf(">FACTSHEET</em>");
				string fats = origin.Substring(oridx, origin.Length - oridx);
				if (fats.StartsWith("<em") == false) fats = "<em" + fats;
				fats = fats.Replace("</strong>", "").Replace("<strong>", "");

				doc.LoadHtml(fats);
				ncs = doc.DocumentNode.SelectNodes(("//*[name()='ri:attachment']"));

				if (ncs.Count > 1)
				{
				}
			}
		}

		private void simpleButton34_Click(object sender, EventArgs e)
		{
			var eer = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\aeae.txt", Encoding.UTF8));

			StringBuilder sb = new StringBuilder();
			foreach (string ee in eer)
			{
				if (ee.StartsWith("\"") && ee.StartsWith("\"T") == false)
				{
					string fs = StringUtil.SplitByString(ee, "\",\"")[0];
					if (fs.ToLower().Trim().EndsWith(".png") || fs.ToLower().Trim().EndsWith(".jpg"))
						sb.AppendLine(ee);
				}
			}
			var s = sb.ToString();
		}

		private void simpleButton35_Click(object sender, EventArgs e)
		{
			string fat = " --action storePage --id {0} --content \"{1}\" --noConvert --encoding \"utf-8\"";

			string id = "";
			string cname = "";
			string label = "";

			var lst = StringUtil.SplitByEnterLine(File.ReadAllText(@"C:\wikimacro.txt"));
			StringBuilder sb = new StringBuilder();
			foreach (string ls in lst)
			{
				var aa = StringUtil.SplitByString(ls, "|;|");
				id = aa[0].Trim();
				cname = aa[1].Trim();
				label = aa[2].Trim();
				string html = "" +
				"<p>" +
				"<ac:structured-macro ac:name='livesearch' ac:schema-version='1' ac:macro-id='livesearch-macro-#pageid#'>" +
				"<ac:parameter ac:name='spaceKey'>" +
				"<ri:space ri:space-key='PACKAGELIB' />" +
				"</ac:parameter>" +
				"<ac:parameter ac:name='placeholder'>#카테고리명# 검색</ac:parameter>" +
				"<ac:parameter ac:name='labels'>#레이블#</ac:parameter>" +
				"</ac:structured-macro></p>" +
				"<p>" +
				"<ac:structured-macro ac:name='rw-content-layout' ac:schema-version='1' ac:macro-id='content-macro-#pageid#' />" +
				"</p>";

				html = html.Replace("#pageid#", id)
					.Replace("#카테고리명#", cname)
					.Replace("#레이블#", label);

				sb.AppendLine(string.Format(fat, id, html));
			}

			File.WriteAllText(@"c:\cli\wm.txt", sb.ToString());
		}

		private void simpleButton36_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			var a = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\a.txt", Encoding.UTF8));
			StringBuilder ss = new StringBuilder();
			string sa = "";
			List<string> lst = new List<string>();
			foreach (string s in a)
			{
				string sss3 = s.Replace(" ", ",")
					.Replace("!", "_")
					.Replace("#", "_")
					.Replace("&", "_")
					.Replace("(", "_")
					.Replace(")", "_")
					.Replace("*", "_")
					.Replace(".", "_")
					.Replace(":", "_")
					.Replace(";", "_")
					.Replace("<", "_")
					.Replace(">", "_")
					.Replace("?", "_")
					.Replace("@@", "_")
					.Replace("[", "_")
					.Replace("]", "_")
					.Replace("^", "_");

				var z = StringUtil.SplitByString(sss3, ",");

				lst = new List<string>();
				sa = "";
				foreach (string zz in z)
				{
					if (string.IsNullOrEmpty(zz)) continue;
					if (lst.Any(x => x == zz.ToLower().Trim()) == false)
					{
						lst.Add(zz.ToLower().Trim());
						sa += string.Format("{0},", zz.ToLower().Trim());
					}
				}
				if (sa.EndsWith(",")) sa = sa.Substring(0, sa.Length - 1);
				sb.AppendLine(sa);
			}
			var sss = sb.ToString();
		}

		private void simpleButton37_Click(object sender, EventArgs e)
		{
			string serv50 = @"\\192.168.50.249\MailBackup\201909\";
			string serv40 = @"\\192.168.0.40\997.MailBackup\201909\";

			List<string> lst40 = new List<string>();//= Directory.GetFiles(serv40).ToList();
			List<string> lst50 = new List<string>();//= Directory.GetFiles(serv50).ToList();
			var l40 = Directory.GetFiles(serv40).ToList();
			foreach (string s in l40)
			{
				lst40.Add(Path.GetFileName(s));
			}
			var l50 = Directory.GetFiles(serv50).ToList();
			foreach (string s in l50)
			{
				lst50.Add(Path.GetFileName(s));
			}
			lst40.Sort();
			lst50.Sort();
			for (int idx = 0; idx < lst50.Count; idx++)
			{
				if (lst40[idx] != lst50[idx])
				{
				}
			}
		}

		private void simpleButton38_Click(object sender, EventArgs e)
		{
			var ls = StringUtil.SplitByEnterLine(txtLog.Text);
			StringBuilder sb = new StringBuilder();
			string tmps = "";
			string tmpa = "";
			List<int> lst = new List<int>();
			foreach (string s in ls)
			{
				lst = new List<int>();
				tmps = "";
				tmpa = "";
				var lls = StringUtil.SplitByString(s, ",");
				foreach (string a in lls)
				{
					if (string.IsNullOrEmpty(a) == false)
					{
						lst.Add(a.ToIntEx());
						var a1 = PackageCate.Instance.CateList.FirstOrDefault(x => x.Child == a.ToIntEx());
						if (a1 != null)
						{
							lst.Add(a1.Parent);
							var a2 = PackageCate.Instance.CateList.FirstOrDefault(x => x.Child == a1.Parent);
							if (a2 != null)
								lst.Add(a2.Parent);
						}
					}
				}

				lst = lst.Distinct().ToList();
				lst.Sort();

				foreach (int i in lst)
				{
					tmps += i.ToString() + "|";
					tmpa += "N|";
				}

				if (tmps.EndsWith("|"))
				{
					tmps = tmps.Substring(0, tmps.Length - 1);
					tmpa = tmpa.Substring(0, tmpa.Length - 1);
				}
				sb.AppendLine(tmps + "\t" + tmpa);
			}

			if (true)
			{
				var asad = sb.ToString();
			}
		}

		private void simpleButton39_Click(object sender, EventArgs e)
		{
			string ss = @"C:\CLI\pa01\";
			string[] va = StringUtil.SplitByEnterLine(File.ReadAllText(@"c:\cli\azaz.txt"));
			foreach (string s in va)
			{
				string[] vs = StringUtil.SplitByString(s, ",");

				try
				{
					File.Move(ss + vs[0] + "_1.jpg", ss + vs[1] + "_1.jpg");
				}
				catch (Exception ex)
				{
				}
				if (true)
				{
				}
			}
		}

		private void simpleButton40_Click(object sender, EventArgs e)
		{
			spreadsheetControl1.LoadDocument(@"d:\as.xlsx");
			DevExpress.Spreadsheet.Worksheet ws = spreadsheetControl1.ActiveWorksheet;
			int row = ws.Rows.LastUsedIndex;

			string root = @"C:\CLI\atf\{0}_{1}.jpg";
			string fat = " --action getattachment --id {0} --file \"at{1}\\{2}_{3}.jpg\"";
			string fat2 = " --action getattachment --id {0} --file \"at{1}\\{2}.png\"";
			StringBuilder sb = new StringBuilder();
			int id = -1;
			string fn = "";
			string na = "";
			int cnt = -1;
			string flag = "";
			for (int idx = 1; idx <= row; idx++)
			{
				id = ws.GetCellValue(0, idx).NumericValue.ToIntEx();
				na = ws.GetCellValue(1, idx).TextValue;
				fn = ws.GetCellValue(2, idx).TextValue;
				cnt = ws.GetCellValue(3, idx).NumericValue.ToIntEx();
				flag = ws.GetCellValue(4, idx).TextValue;
				sb.AppendLine(string.Format(fat2, id, flag, fn));
				for (int idy = 1; idy <= cnt; idy++)
				{
					sb.AppendLine(string.Format(fat, id, flag, fn, idy));
				}
			}

			File.WriteAllText(@"c:\cli\c20.txt", sb.ToString(), Encoding.UTF8);
		}

		private void simpleButton41_Click(object sender, EventArgs e)
		{
			List<string> fs = new List<string>();
			StringBuilder sb = new StringBuilder();
			Dictionary<string, string> id = new Dictionary<string, string>();
			var s = StringUtil.SplitByEnterLine(File.ReadAllText(@"d:\aa.txt", Encoding.UTF8));

			string r = @"D:\절대지우지마세요\Downloads\deri&lpi_filter\";
			var files = Directory.GetFiles(r);
			// --action addAttachment  --id 16089179 --file "nf/P0000BKN.png"  --name "P0000BKN.png"
			string rf = @"C:\CLI\atp\{0}.png";
			string rf2 = @"C:\CLI\atp\{0}_{1}.jpg";
			int cnt = -1;
			foreach (string a in s)
			{
				var sa = StringUtil.SplitByString(a, ",");
				File.Move(string.Format("{0}{1}.png", r, sa[0]), string.Format("{0}{1}.png", r, sa[1]));
				File.Move(string.Format("{0}{1}.pptx", r, sa[0]), string.Format("{0}{1}.pptx", r, sa[1]));
				File.Move(string.Format("{0}{1}_1.JPG", r, sa[0]), string.Format("{0}{1}_1.jpg", r, sa[1]));
				continue;
				cnt = sa[1].ToIntEx();
				if (File.Exists(string.Format(rf, sa[0])) == false)
				{
					sb.AppendLine(sa[0]);
				}
				for (int idx = 1; idx <= cnt; idx++)
				{
					if (File.Exists(string.Format(rf2, sa[0], idx)) == false)
					{
						sb.AppendLine(sa[0]);
					}
				}
				id.Add(sa[0], sa[1]);
			}

			if (true)
			{
			}
			foreach (string a in s)
			{
				var sa = StringUtil.SplitByString(a, ",");
				cnt = sa[1].ToIntEx();
				fs.Add(sa[0] + ".png");
				for (int idx = 1; idx <= cnt; idx++)
				{
					fs.Add(sa[0] + "_" + idx.ToString() + ".jpg");
				}
			}

			if (true)
			{
				var aesw = sb.ToString();
			}
			foreach (string f in files)
			{
				var fc = Path.GetFileNameWithoutExtension(f);
				fc = fc.Replace("_1", "").Replace("_2", "").Replace("_3", "").Replace("_4", "");
				string fn = Path.GetFileName(f);
				if (fs.Exists(x => x == fn) == false)
				{
					File.Delete(f);
				}
			}
			string asas = sb.ToString();
		}

		private void simpleButton42_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			var sss = StringUtil.SplitByEnterLine(File.ReadAllText(@"d:\aa.txt", Encoding.UTF8));
			foreach (string ss in sss)
			{
				var re = CategoryHelper.Instance.GetAncestorLableFromString(ss);
				sb.AppendLine(re);
			}
			if (true)
			{
			}
			var res = sb.ToString();
		}

		private void simpleButton43_Click(object sender, EventArgs e)
		{
			string rp = @"D:\절대지우지마세요\Downloads\Formula_wiki 등재_191113\2019_5_HK\";
			StringBuilder sb1 = new StringBuilder();
			StringBuilder sb2 = new StringBuilder();
			string sf0 = " --action addAttachment  --id {0} --file \"nf191114/{1}.png\"  --name \"{1}.png\"";
			string sf1 = " --action addAttachment  --id {0} --file \"nf191114/{1}_{2}.jpg\"  --name \"{1}_{2}.jpg\"";
			string sf2 = " --action addAttachment  --id {0} --file \"nf191114/{1}.pptx\"  --name \"{1}.pptx\"";

			var sss = StringUtil.SplitByEnterLine(File.ReadAllText(@"d:\aa.txt", Encoding.UTF8));
			var fs = Directory.GetFiles(rp);
			foreach (string ss in sss)
			{
				var sa = StringUtil.SplitByString(ss, ",");
				var cnt = sa[2].ToIntEx();
				sb1.AppendLine(string.Format(sf0, sa[1], sa[0]));
				sb1.AppendLine(string.Format(sf2, sa[1], sa[0]));
				string ts = "";
				for (int idx = 1; idx <= cnt; idx++)
				{
					sb1.AppendLine(string.Format(sf1, sa[1], sa[0], idx));
				}
				continue;
				var ffs = fs.Where(x => x.StartsWith(rp + sa[0]));
				foreach (string f in ffs)
				{
					if (f.EndsWith(".png"))
					{
						File.Move(f, rp + sa[1] + ".png");
					}
					else if (f.EndsWith("_1.jpg"))
					{
						File.Move(f, rp + sa[1] + "_1.jpg");
					}
					else if (f.EndsWith("_2.jpg"))
					{
						File.Move(f, rp + sa[1] + "_2.jpg");
					}
					else if (f.EndsWith("_3.jpg"))
					{
						File.Move(f, rp + sa[1] + "_3.jpg");
					}
					else if (f.EndsWith(".pptx"))
					{
						File.Move(f, rp + sa[1] + ".pptx");
					}
					else
					{
					}
				}
			}

			var sa1 = sb1.ToString();
			var sa2 = sb2.ToString();
		}

		private void simpleButton44_Click(object sender, EventArgs e)
		{
            string path = @"D:\절대지우지마세요\Downloads\이미지PNG\";
            var files = Directory.GetFiles(path);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("200110001", "P0000BYR");
            dic.Add("200110002", "P0000BYS");
            dic.Add("200110003", "P0000BYT");
            dic.Add("200110004", "P0000BYU");
            dic.Add("200110005", "P0000BYV");
            dic.Add("200110006", "P0000BYW");
            dic.Add("200110007", "P0000BYX");


            foreach (string f in files)
            {
                var n = Path.GetFileName(f);
                var ext = Path.GetExtension(f).ToLower();
                var fn = Path.GetFileNameWithoutExtension(f).Replace("_1", "");
                try
                {

                switch (ext)
                {
                    case ".png": File.Move(f, path + dic[fn] + ".png"); break;
                    case ".pptx": File.Move(f, path + dic[fn] + ".pptx"); break;
                    case ".jpg": File.Move(f, path + dic[fn] + "_1.jpg"); break;
                }
                }
                catch (Exception ex)
                {

                }
            }
        }

		private void simpleButton45_Click(object sender, EventArgs e)
		{
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180514.docx", "22577393");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180515.docx", "22577394");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180518.docx", "22577395");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180530.docx", "22577396");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180615.docx", "22577397");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180619.docx", "22577398");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180620.docx", "22577399");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180629.docx", "22577400");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180702.docx", "22577401");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180704.docx", "22577402");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180716.docx", "22577403");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180720.docx", "22577404");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180730.docx", "22577405");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180806.docx", "22577406");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180810.docx", "22577407");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180821.docx", "22577408");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180831.docx", "22577409");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180907.docx", "22577410");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180912.docx", "22577411");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\180917.docx", "22577412");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\181106.docx", "22577413");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\181116.docx", "22577414");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190111.docx", "22577415");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190402.docx", "22577416");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190404.docx", "22577417");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190408.docx", "22577418");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190409.docx", "22577419");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190725.docx", "22577420");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\190819.docx", "22577421");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\eBranch\200211.docx", "22577422");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\Patuah\200205.docx", "22577423");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\Patuah\200207.docx", "22577424");
            dic.Add(@"\\192.168.0.40\ict\24.협력업체 원격 보고서\Patuah\200211.docx", "22577425");
            string fat = " --action storePage --id {0} --content \"{1}\" --noConvert --encoding \"euc-kr\"";
            string ft = "<table class='relative-table wrapped' style='width: 100.0%;'> <colgroup> <col style='width: 10.0%;' /> <col style='width: 40.0%;' /> <col style='width: 10.0%;' /> <col style='width: 40.0%;' /> </colgroup> <tbody> <tr> <td><strong>작업일</strong></td> <td>#작업일#</td> <td><strong>작업시간</strong></td> <td>#작업시간#</td> </tr> <tr> <td><strong>작업자</strong></td> <td>#작업자#</td> <td><strong>검수자</strong></td> <td>#검수자#</td> </tr> <tr> <td colspan='4'>#내용#</td> </tr> </tbody> </table>";
            StringBuilder sb = new StringBuilder();
            int idx = 0;
            string plainText = "";
            string str = "";
            string desc = "";
            RichEditDocumentServer server = null;
            foreach (var a in dic)
            {
                server = new RichEditDocumentServer();
                server.LoadDocument(a.Key);
                plainText = server.Text;
                str = "";
                var as3 = StringUtil.SplitByEnterLine(plainText);
                try
                {
                    if (as3[0] != "작업일") throw new Exception("작업일이 아니래");
                    if (as3[2] != "작업시간") throw new Exception("작업시간이 아니래");
                    if (as3[4] != "작업자") throw new Exception("작업자가 아니래");
                    if (as3[6] != "검수자") throw new Exception("검수자가 아니래");
                    if (as3[8] != "내용") throw new Exception("내용이 아니래");
                    desc = "";

                    for(int x = 9; x < as3.Count(); x++)
                    {
                        desc += (as3[x] + "<br />");
                    }
                    str = ft.Replace("#작업일#", as3[1])
                            .Replace("#작업시간#", as3[3])
                            .Replace("#작업자#", as3[5])
                            .Replace("#검수자#", as3[7])
                            .Replace("#내용#", desc).Replace("\"", "'")
                            .Replace(".", "．")
                    .Replace("!", "！")
                    .Replace("#", "＃")
                    .Replace("&", "＆")
                    .Replace("(", "（")
                    .Replace(")", "）")
                    .Replace("*", "＊")
                    .Replace(":", "：")
                    .Replace(";", "；")
                    .Replace("?", "？")
                    .Replace("@", "＠")
                    .Replace("[", "［")
                    .Replace("]", "］")
                    .Replace("^", "＾")
                    .Replace(",,", ",");
                    sb.AppendLine(string.Format(fat, a.Value, str));
                }
                catch(Exception ex)
                {

                }
            }
            var ssss = sb.ToString();

        }

        private void simpleButton46_Click(object sender, EventArgs e)
		{
		}

		private void simpleButton47_Click(object sender, EventArgs e)
		{
		}

		private void simpleButton48_Click(object sender, EventArgs e)
		{
		}

		private void simpleButton49_Click(object sender, EventArgs e)
		{
		}

		private void simpleButton50_Click(object sender, EventArgs e)
		{
		}
	}
}




































