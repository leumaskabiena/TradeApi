using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Data.Tables;

namespace Trade.Service.IRepository
{
    public interface IItemService : IDisposable
    {
        Item GetById(string id);
        List<Item> GetAll();
        void Insert(Item model);
        void Update(Item model);
        void Delete(Item model);
        IEnumerable<Item> Find(Func<Item, bool> predicate);

    }
}