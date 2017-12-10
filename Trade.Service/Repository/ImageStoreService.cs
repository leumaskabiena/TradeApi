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
    public class ImageStoreService : IImageStoreService
    {
        private ApplicationDbContext _ApplicationDbContext = null;
        private readonly IRepository<ImageStore> _ImageStoreService;
        public ImageStoreService()
        {
            _ApplicationDbContext = new ApplicationDbContext();
            _ImageStoreService = new RepositoryService<ImageStore>(_ApplicationDbContext);
        }
        public void Delete(ImageStore model)
        {
            _ImageStoreService.Delete(model);
        }

        public void Dispose()
        {
            _ApplicationDbContext.Dispose();
            _ApplicationDbContext = null;
        }

        public IEnumerable<ImageStore> Find(Func<ImageStore, bool> predicate)
        {
            return _ImageStoreService.Find(predicate).ToList();
        }

        public List<ImageStore> GetAll()
        {
            return _ImageStoreService.GetAll().ToList();
        }

        public ImageStore GetById(string id)
        {
            return _ImageStoreService.GetById(id);
        }

        public void Insert(ImageStore model)
        {
            _ImageStoreService.Insert(model);
        }

        public void Update(ImageStore model)
        {
            _ImageStoreService.Update(model);
        }
    }
}
