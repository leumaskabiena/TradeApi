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
    public class BetService : IBetService
    {
        private ApplicationDbContext _ApplicationDbContext = null;
        private readonly IRepository<Bet> _BetService;
        public BetService()
        {
            _ApplicationDbContext = new ApplicationDbContext();
            _BetService = new RepositoryService<Bet>(_ApplicationDbContext);
        }
        public void Delete(Bet model)
        {
            _BetService.Delete(model);
        }

        public void Dispose()
        {
            _ApplicationDbContext.Dispose();
            _ApplicationDbContext = null;
        }

        public IEnumerable<Bet> Find(Func<Bet, bool> predicate)
        {
            return _BetService.Find(predicate).ToList();
        }

        public List<Bet> GetAll()
        {
            return _BetService.GetAll().ToList();
        }

        public Bet GetById(string id)
        {
            return _BetService.GetById(id);
        }

        public void Insert(Bet model)
        {
            _BetService.Insert(model);
        }

        public void Update(Bet model)
        {
            _BetService.Update(model);
        }
    }
}
