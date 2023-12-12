using BH_Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlassianManager.Interface.Models
{
    public class ItemModel
    {
        public Types  Type         { get; set; }
        public string ProduceCode   { get; set; }
        public string Manufacturer  { get; set; }
        public string Keyword       { get; set; }
        public string Category      { get; set; }
        public string Name          { get; set; }
        public string Labno         { get; set; }

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
                string fat = "--action copyPage--space \"{0}\"--title \"{1}\"--newTitle \"{2}\"--descendents--parent \"{3}\"--findReplace \"{4}\"--special \" #\" --labels \"{5}\" --replace--noConvert";

                string space = Type == Types.Formula ? "FORMULALIB" : "PACKAGELIB";
                string title = Type == Types.Formula ? "TF" : "PF";
                string newTitle = Name;
                var cats = Category.Split(new char[] { '|' }).ToList().Select(x => x.Trim()).ToList();
                string parent = CategoryHelper
                return "";
            }
        }
    }
}
