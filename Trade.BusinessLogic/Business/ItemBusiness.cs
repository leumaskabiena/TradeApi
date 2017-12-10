using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.BusinessLogic.IBusiness;
using Trade.Data.Tables;
using Trade.Model.ModelView;
using Trade.Service.Repository;

namespace Trade.BusinessLogic.Business
{
    public class ItemBusiness : IItemBusiness
    {
        ImageStoreBusiness _ImageStoreBusiness = new ImageStoreBusiness();
        public void CreateItem(ItemModelview model)
        {
            #region Create a ref number of the item
            string word = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rd = new Random();
            int num1 = rd.Next(-1, 24);
            string StampDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString();
            model.ItemRef = word.Substring(num1, 3) + model.ItemName.ToUpper().Substring(0, 1) + StampDate;
            #endregion
            using (var Itemrepo = new ItemService())
            {
                _ImageStoreBusiness.CreateImageForItem(model);

                Item _Item = new Item
                {
                    ItemName = model.ItemName,
                    ItemPrice = model.ItemPrice,
                    ItemDescription = model.ItemDescription,
                    ItemRef = model.ItemRef
                };
                Itemrepo.Insert(_Item);

            }
        }
        public List<ImageStoreModelView> GetAllImage()
        {
            using (var ImageStorerepo = new ImageStoreService())
            {
                return ImageStorerepo.GetAll().Select(model => new ImageStoreModelView()
                {
                    imgId = model.imgId,
                    imgByte = model.imgByte,
                    UserName = model.UserName,
                    date = model.date,
                    ItemName = model.ItemName
                }).ToList();
            }
        }
        public List<ItemModelview> GetAllItems()
        {
            using (var Itemrepo = new ItemService())
            {
                return Itemrepo.GetAll().Select(model => new ItemModelview()
                {
                    ItemName = model.ItemName,
                    ItemPrice = model.ItemPrice,
                    ItemDescription = model.ItemDescription,
                    ItemRef = model.ItemRef
                }).ToList();
            }
        }
        public List<ItemWithImageModelView> GetAllItemWithImage()
        {

            List<ItemWithImageModelView> lst = new List<ItemWithImageModelView>();
            ItemWithImageModelView _ItemWithImageModelView = new ItemWithImageModelView();
            string Itemref;

            using (var ImageStorerepo = new ImageStoreService())
            {
                foreach (var item in GetAllItems())
                {
                    //find item ref of the item in imagestore table
                    Itemref = item.ItemRef + "0";

                    //  _ItemWithImageModelView.imgByte = ImageStorerepo.GetAll().Find(img => img.imgId.Equals(Itemref)).imgByte;
                    _ItemWithImageModelView.ItemName = item.ItemName;
                    _ItemWithImageModelView.ItemPrice = item.ItemPrice;
                    _ItemWithImageModelView.ItemDescription = item.ItemDescription;
                    _ItemWithImageModelView.ItemRef = item.ItemRef;

                    //add to the list
                    lst.Add(_ItemWithImageModelView);
                }
                return lst;
            }
        }
        public ItemModelview DetailsItem(string id)
        {
            var ImageStorerepo = new ImageStoreService();
            using (var Itemrepo = new ItemService())
            {
                string itemid = id + "0";
                ItemModelview _ItemModelview = new ItemModelview();
                if (!id.Equals(""))
                {
                    //find the item in item table
                    var item = Itemrepo.GetAll().Find(x => x.ItemRef.Equals(id));
                    _ItemModelview.ItemName = item.ItemName;
                    _ItemModelview.ItemPrice = item.ItemPrice;
                    //find client username in the storeImage table
                    _ItemModelview.UserName = ImageStorerepo.GetById(itemid).UserName;
                    _ItemModelview.ItemDescription = item.ItemDescription;
                    _ItemModelview.ItemRef = id;
                }
                return _ItemModelview;
            }
        }
        public ItemModelview DetailsItemApp(string id)
        {
            var ImageStorerepo = new ImageStoreService();
            List<byte[]> lst = new List<byte[]>();
            using (var Itemrepo = new ItemService())
            {
                string itemid = id + "0";
                ItemModelview _ItemModelview = new ItemModelview();

                if (!id.Equals(string.Empty))
                {
                    //find the item in item table
                    var item = Itemrepo.GetAll().Find(x => x.ItemRef.Equals(id));
                    _ItemModelview.ItemName = item.ItemName;
                    _ItemModelview.ItemPrice = item.ItemPrice;
                    //find client username in the storeImage table
                    _ItemModelview.UserName = ImageStorerepo.GetById(itemid).UserName;
                    _ItemModelview.ItemDescription = item.ItemDescription;
                    _ItemModelview.ItemRef = id;
                    _ItemModelview.date = ImageStorerepo.GetById(itemid).date;
                    foreach (var img in GetAllImage())
                    {
                        if (img.imgId.Substring(0, img.imgId.Length - 1).Equals(id))
                        {
                            lst.Add(img.imgByte);
                        }
                    }
                    _ItemModelview.Lstsrc = lst;
                }
                return _ItemModelview;
            }
        }
        public ItemModelview FindItem(string id)
        {
            ItemModelview _ItemModelview = new ItemModelview();
            using (var repo = new ItemService())
            {
                var item = repo.GetById(id);
                _ItemModelview.ItemName = item.ItemName;
                _ItemModelview.ItemPrice = item.ItemPrice;
                _ItemModelview.ItemRef = item.ItemRef;
                _ItemModelview.ItemDescription = item.ItemDescription;

                return _ItemModelview;
            }
        }
        public bool DeleteItem(string id)
        {
            ItemModelview _ItemModelview = new ItemModelview();
            using (var repo = new ItemService())
            {
                try
                {
                    var item = repo.GetById(id);
                    _ImageStoreBusiness.DeleteAllImage(id);
                    repo.Delete(item);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        // this is method is the same with the find
        public ItemModelview GetEditItem(string id)
        {
            ItemModelview _ItemModelview = new ItemModelview();
            using (var repo = new ItemService())
            {
                var item = repo.GetById(id);
                _ItemModelview.ItemName = item.ItemName;
                _ItemModelview.ItemPrice = item.ItemPrice;
                _ItemModelview.ItemRef = item.ItemRef;
                _ItemModelview.ItemDescription = item.ItemDescription;

                return _ItemModelview;
            }
        }
        public void PostEditItem(ItemModelview model)
        {
            using (var repo = new ItemService())
            {
                var Itemtopdate = repo.GetById(model.ItemRef);
                Itemtopdate = new Item
                {
                    ItemName = model.ItemName,
                    ItemPrice = model.ItemPrice,
                    ItemDescription = model.ItemDescription
                };


                _ImageStoreBusiness.PostEdit(model);
                repo.Update(Itemtopdate);
            }
        }
        public List<ItemModelview> GetMyTrade(string username)
        {
            var ImageStorerepo = new ImageStoreService();
            var Itemrepo = new ItemService();
            List<ItemModelview> lstItemModelview = new List<ItemModelview>();
            var lstimgId = ImageStorerepo.GetAll().FindAll(x => x.UserName.Equals(username)).Select(w => w.imgId);

            List<string> lstItemref = new List<string>();
            foreach (var item in lstimgId)
            {
                string x = item.Substring(0, item.Length - 1);
                if (!lstItemref.Contains(x))
                {
                    lstItemref.Add(x);
                }
            }
            foreach (var item in lstItemref)
            {
                var _item = Itemrepo.GetById(item);
                ItemModelview _ItemModelview = new ItemModelview
                {
                    ItemName = _item.ItemName,
                    ItemPrice = _item.ItemPrice,
                    ItemDescription = _item.ItemDescription,
                    ItemRef = _item.ItemRef,
                    src = ImageStorerepo.GetById(item + "0").imgByte

                };
                lstItemModelview.Add(_ItemModelview);
            }
            return lstItemModelview;
        }

    }
}
