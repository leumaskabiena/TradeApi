using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model.ModelView
{
    public class ImageStoreModelView
    {
        public string imgId { get; set; }
        public string UserName { get; set; }
        public DateTime date { get; set; }
        public string ItemName { get; set; }
        public byte[] imgByte { get; set; }

    }
}