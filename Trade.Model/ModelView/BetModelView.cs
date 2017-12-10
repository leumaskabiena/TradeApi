using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Model.ModelView
{
    public class BetModelView
    {
        [DisplayName("Reference")]
        public string itemref { get; set; }
        [DisplayName("Current Price")]
        public double Currentprice { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Bet Price")]
        public double Newprice { get; set; }
        public int IsAccept { get; set; }
        public DateTime date { get; set; }
        [DisplayName("Better Name")]
        public string BetterName { get; set; }
        [DisplayName("Item Name")]
        public string ItemName { get; set; }
        [DisplayName("Description")]
        public string ItemDescription { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
    }
}
