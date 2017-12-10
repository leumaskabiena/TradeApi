using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.Tables
{
    public class Item
    {
        [Key]
        public string ItemRef { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public string status { get; set; }
    }
}
