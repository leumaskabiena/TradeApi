using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Model.ModelView;

namespace Trade.BusinessLogic.IBusiness
{
    public interface IItemBusiness
    {
        void CreateItem(ItemModelview model);
        List<ImageStoreModelView> GetAllImage();
        List<ItemModelview> GetAllItems();
        List<ItemWithImageModelView> GetAllItemWithImage();
        ItemModelview DetailsItem(string id);
        ItemModelview DetailsItemApp(string id);
        ItemModelview FindItem(string id);
        bool DeleteItem(string id);
        List<ItemModelview> GetMyTrade(string username);
    }
}
