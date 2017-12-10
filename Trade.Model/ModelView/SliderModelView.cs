using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model.ModelView
{
    public class SliderModelView
    {
        public string id { get; set; }
        public string src { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
    }
    public class SliderModelViewApp
    {
        public string id { get; set; }
        public byte[] src { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
    }
}