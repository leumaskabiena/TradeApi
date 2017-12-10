using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Model.ModelView;

namespace Trade.BusinessLogic.IBusiness
{
    public interface IImageStoreBusiness
    {
        void CreateImageForItem(ItemModelview model);
        List<ImageStoreModelView> GetAllImage();
        List<SliderModelView> GetImageForEachUser();
        List<SliderModelView> GetAllImageForEachUser();
        List<SliderModelViewApp> GetImageForEachUserApp();
        List<ImageStoreModelView> GetAllImageForEachUserApp();
        ImageStoreModelView DetailsImage(int? id);
        void DeleteAllImage(string id);
        void PostEdit(ItemModelview model);
    }
}