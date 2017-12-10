using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.BusinessLogic.IBusiness;
using Trade.Data.Tables;
using Trade.Model.ModelView;
using Trade.Service.Repository;

namespace Trade.BusinessLogic.Business
{
    public class ImageStoreBusiness : IImageStoreBusiness
    {
        public void CreateImageForItem(ItemModelview model)
        {
            using (var ImageStorerepo = new ImageStoreService())
            {
                int count = 0;
                // from the website
                if (model.ImageFile != null)
                {

                    foreach (var file in model.ImageFile)
                    {
                        byte[] imgbyte = null;
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        imgbyte = reader.ReadBytes(file.ContentLength);
                        ImageStore img = new ImageStore
                        {
                            imgId = model.ItemRef + count,
                            UserName = model.UserName,
                            ItemName = model.ItemName,
                            date = DateTime.Now,
                            imgByte = imgbyte
                        };
                        ImageStorerepo.Insert(img);
                        count++;
                    }
                }
                // from app
                else
                {
                    ImageStore img = new ImageStore
                    {
                        imgId = model.ItemRef + count,
                        UserName = model.UserName,
                        ItemName = model.ItemName,
                        date = DateTime.Now,
                        imgByte = model.src
                    };
                    ImageStorerepo.Insert(img);
                }
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
        //Find the newly item added in the database
        public List<SliderModelView> GetImageForEachUser()
        {
            //Get the UserName from Imagestore table who have an item
            var users = GetAllImage().Select(x => x.UserName).Distinct();
            List<SliderModelView> lst = new List<SliderModelView>();
            string itemref = "";
            string status = "";
            var Itemrepo = new ItemService();
            foreach (var user in users)
            {

                if (user != null)
                {
                    //  find all item for a particular user
                    var items = GetAllImage().Where(x => x.UserName.Equals(user)).ToList();
                    // find all item that are not sold
                    foreach (var x in items)
                    {
                        status = Itemrepo.GetById(x.imgId.Substring(0, x.imgId.Length - 1)).status;
                        if (status == "Sold")
                        {
                            items.Remove(x);
                        }
                    }
                    double[] post = new double[items.Count()];
                    int count = 0;
                    //check the date of the item
                    foreach (var x in items)
                    {
                        TimeSpan diff = DateTime.Now.Subtract(x.date);
                        post[count] = Math.Round(Double.Parse(diff.TotalHours.ToString()), 1);
                        count++;
                    }
                    //find the smallest
                    double small = post[0];
                    int position = 0;
                    for (int i = 1; i < post.Count(); i++)
                    {
                        if (post[i] < small)
                        {
                            small = post[i];
                            position = i;
                        }
                    }
                    var item = items[position].imgByte;
                    var base64 = Convert.ToBase64String(item);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    SliderModelView sl = new SliderModelView();
                    sl.src = imgSrc;
                    sl.title = items[position].ItemName;
                    //take the description from item table via itemref
                    sl.id = itemref = items[position].imgId.Substring(0, items[position].imgId.Length - 1);
                    sl.description = Itemrepo.GetAll().Find(model => model.ItemRef.Equals(itemref)).ItemDescription;
                    sl.Name = user;
                    lst.Add(sl);
                }
            }
            if (lst.Count <= 5)
            {
                return lst;
            }
            else
            {
                return lst.Take(5).ToList();
            }
        }
        public List<SliderModelView> GetAllImageForEachUser()
        {
            var Itemrepo = new ItemService();
            List<SliderModelView> ListItem = new List<SliderModelView>();
            string id = "";
            string status = "";
            foreach (var item in GetAllImage())
            {
                id = item.imgId.Substring(0, item.imgId.Length - 1);
                status = Itemrepo.GetById(id).status;
                if (!ListItem.Select(x => x.id).Contains(id) && status != "Sold")
                {
                    var _SliderModelView = new SliderModelView();
                    _SliderModelView.id = id;
                    _SliderModelView.title = item.ItemName;
                    _SliderModelView.description = Itemrepo.GetById(id).ItemDescription;
                    _SliderModelView.Name = item.UserName;
                    _SliderModelView.price = Itemrepo.GetById(id).ItemPrice;
                    var base64 = Convert.ToBase64String(item.imgByte);
                    _SliderModelView.src = String.Format("data:image/gif;base64,{0}", base64);
                    ListItem.Add(_SliderModelView);
                }
            }
            return ListItem;
        }
        public List<SliderModelViewApp> GetImageForEachUserApp()
        {
            //Get the UserName from Imagestore table who have an item
            var users = GetAllImage().Select(x => x.UserName).Distinct();
            List<SliderModelViewApp> lst = new List<SliderModelViewApp>();
            string itemref = "";
            string status = "";
            var Itemrepo = new ItemService();
            foreach (var user in users)
            {
                if (user != null)
                {
                    var items = GetAllImage().Where(x => x.UserName.Equals(user)).ToList();
                    // find all item that are not sold
                    foreach (var x in items)
                    {
                        status = Itemrepo.GetById(x.imgId.Substring(0, x.imgId.Length - 1)).status;
                        if (status == "Sold")
                        {
                            items.Remove(x);
                        }
                    }
                    double[] post = new double[items.Count()];
                    int count = 0;
                    //check the date of the item
                    foreach (var x in items)
                    {
                        TimeSpan diff = DateTime.Now.Subtract(x.date);
                        post[count] = Math.Round(Double.Parse(diff.TotalHours.ToString()), 1);
                        count++;
                    }
                    //find the smallest
                    double small = post[0];
                    int position = 0;
                    for (int i = 1; i < post.Count(); i++)
                    {
                        if (post[i] < small)
                        {
                            small = post[i];
                            position = i;
                        }
                    }
                    var item = items[position].imgByte;
                    //var base64 = Convert.ToBase64String(item);
                    //var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    SliderModelViewApp sl = new SliderModelViewApp();
                    sl.src = items[position].imgByte;
                    sl.title = items[position].ItemName;
                    //take the description from item table via itemref
                    sl.id = itemref = items[position].imgId.Substring(0, items[position].imgId.Length - 1);
                    sl.description = Itemrepo.GetAll().Find(model => model.ItemRef.Equals(itemref)).ItemDescription;
                    sl.Name = user;
                    sl.price = Itemrepo.GetAll().Find(model => model.ItemRef.Equals(itemref)).ItemPrice;
                    lst.Add(sl);
                }
            }
            return lst;
        }
        public List<ImageStoreModelView> GetAllImageForEachUserApp()
        {
            throw new NotImplementedException();
        }
        public ImageStoreModelView DetailsImage(int? id)
        {
            throw new NotImplementedException();
        }
        public void DeleteAllImage(string id)
        {

            using (var repo = new ImageStoreService())
            {
                var items = repo.GetAll();
                foreach (var item in items)
                {
                    string newId = item.imgId.Substring(0, item.imgId.Length - 1);
                    if (newId.Equals(id))
                    {
                        var itemToDelete = repo.GetById(item.imgId);
                        repo.Delete(itemToDelete);
                    }
                }
            }
        }
        public void PostEdit(ItemModelview model)
        {
            using (var repo = new ImageStoreService())
            {
                // Find all item with the same reference number
                var ImageStore = repo.GetAll().Where(x => x.imgId.Substring(0, x.imgId.Length - 1).Equals(model.ItemRef)).ToList();
                //from website
                if (model.ImageFile != null)
                {
                    //check the number of picture
                    int NumberOfItem = model.ImageFile.Count();
                    if (ImageStore.Count() > NumberOfItem)
                    {
                        //will delete the remaining of ImageStore
                        int count = 0;

                        byte[] imgbyte = null;
                        //update item with new details
                        foreach (var item in ImageStore)
                        {
                            BinaryReader reader = new BinaryReader(model.ImageFile[count].InputStream);
                            imgbyte = reader.ReadBytes(model.ImageFile[count].ContentLength);
                            item.ItemName = model.ItemName;
                            item.imgByte = imgbyte;
                            repo.Update(item);
                            count++;
                        }

                        //delete the remainig
                        for (int i = model.ImageFile.Count() + 1; i < ImageStore.Count(); i++)
                        {
                            var ItemToDelete = ImageStore[i];
                            repo.Delete(ItemToDelete);
                        }
                    }
                    else
                    {
                        // will add the remaing of model.ImageFile
                    }
                }
                //from app
                else
                {
                    for (int i = 0; i < model.Lstsrc.Count(); i++)
                    {
                        var itemToUpdate = ImageStore[i];
                        itemToUpdate.ItemName = model.ItemName;
                        itemToUpdate.imgByte = model.Lstsrc[i];
                        repo.Update(itemToUpdate);
                    }

                    // Delete the remaining item
                    //1 bcz they will be only one pic from app and 
                    //the number of img in imageStore must not be one
                    if (model.Lstsrc.Count() == 1 && ImageStore.Count() > 1)
                    {
                        for (int i = 1; i < ImageStore.Count(); i++)
                        {
                            var ItemToDelete = ImageStore[i];
                            repo.Delete(ItemToDelete);
                        }

                    }
                }
            }
        }


    }
}
