using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model.ModelView
{
    public class ItemWithImageModelView
    {
        public int ItemId { get; set; }
        [DisplayName("Name")]
        public string ItemName { get; set; }
        public string UserName { get; set; }
        [DisplayName("Price")]
        public double ItemPrice { get; set; }
        [DisplayName("Description")]
        public string ItemDescription { get; set; }
        public string ItemRef { get; set; }
        public byte[] imgByte { get; set; }
        public string imageUrl { get; set; }
    }
}
