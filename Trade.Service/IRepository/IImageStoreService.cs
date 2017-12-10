using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Data.Tables;

namespace Trade.Service.IRepository
{
    public interface IImageStoreService : IDisposable
    {
        ImageStore GetById(string id);
        List<ImageStore> GetAll();
        void Insert(ImageStore model);
        void Update(ImageStore model);
        void Delete(ImageStore model);
        IEnumerable<ImageStore> Find(Func<ImageStore, bool> predicate);
    }
}
