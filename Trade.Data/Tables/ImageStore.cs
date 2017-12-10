using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.Tables
{
    public class ImageStore
    {
        [Key]
        public string imgId { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public DateTime date { get; set; }
        [Column(TypeName = "image")]
        public byte[] imgByte { get; set; }
    }
}
