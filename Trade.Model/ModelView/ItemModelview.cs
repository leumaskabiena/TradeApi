using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Trade.Model.ModelView
{
    public class ItemModelview
    {
        [DisplayName("Ref Number")]
        public string ItemRef { get; set; }
        [DisplayName("Name")]
        [Required]
        public string ItemName { get; set; }
        [DisplayName("Seller Name")]
        public string UserName { get; set; }
        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        [Required]
        public double ItemPrice { get; set; }
        [DisplayName("Description")]
        [Required]
        public string ItemDescription { get; set; }
        public List<HttpPostedFileWrapper> ImageFile { get; set; }
        public string status { get; set; }
        public byte[] src { get; set; }
        public List<byte[]> Lstsrc { get; set; }
        public DateTime date { get; set; }
    }
}
