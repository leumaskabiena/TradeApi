using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Data.Tables;

namespace Trade.Service.IRepository
{
    public interface IBetService : IDisposable
    {
        Bet GetById(string id);
        List<Bet> GetAll();
        void Insert(Bet model);
        void Update(Bet model);
        void Delete(Bet model);
        IEnumerable<Bet> Find(Func<Bet, bool> predicate);
    }
}
