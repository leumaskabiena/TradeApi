using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Data.DbContext;
using Trade.Data.IRepository;
using Trade.Data.Repository;
using Trade.Data.Tables;
using Trade.Service.IRepository;

namespace Trade.Service.Repository
{
    public class ItemService : IItemService
    {
        private ApplicationDbContext _ApplicationDbContext = null;
        private readonly IRepository<Item> _ItemService;
        public ItemService()
        {
            _ApplicationDbContext = new ApplicationDbContext();
            _ItemService = new RepositoryService<Item>(_ApplicationDbContext);
        }
        public void Delete(Item model)
        {
            _ItemService.Delete(model);
        }

        public IEnumerable<Item> Find(Func<Item, bool> predicate)
        {
            return _ItemService.Find(predicate).ToList();
        }

        public List<Item> GetAll()
        {
            return _ItemService.GetAll().ToList();
        }

        public Item GetById(string id)
        {
            return _ItemService.GetById(id);

        }

        public void Insert(Item model)
        {
            _ItemService.Insert(model);
        }

        public void Update(Item model)
        {
            _ItemService.Update(model);
        }
        public void Dispose()
        {
            _ApplicationDbContext.Dispose();
            _ApplicationDbContext = null;
        }
    }
}
