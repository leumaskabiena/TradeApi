using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Data.Tables
{
    public class Bet
    {
        [Key]

        public string ItemRef { get; set; }
        public string BetterName { get; set; }
        public double NewPrice { get; set; }
        //0 bet is created
        //1 bet is refused
        //2 bet is accepted
        public int IsAccept { get; set; }
        public DateTime date { get; set; }
        public bool IsRead { get; set; }
    }
}
