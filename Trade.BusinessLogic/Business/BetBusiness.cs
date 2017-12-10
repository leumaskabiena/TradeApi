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
    public class BetBusiness : IBetBusiness
    {
        public void CreateBet(BetModelView model)
        {
            var Itemrepo = new ItemService();
            string word = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rd = new Random();
            int num1 = rd.Next(-1, 24);
            string StampDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString();
            string BetRef = word.Substring(num1, 3) + StampDate + "-" + model.itemref;

            using (var Betrepo = new BetService())
            {
                Bet _Bet = new Bet
                {
                    ItemRef = BetRef,
                    BetterName = model.BetterName,
                    NewPrice = model.Newprice,
                    IsAccept = 0,
                    date = DateTime.Now
                };
                Betrepo.Insert(_Bet);
            }
        }
        public void CreateBetApp(BetModelView model)
        {
            var Itemrepo = new ItemService();
            string word = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rd = new Random();
            int num1 = rd.Next(-1, 24);
            string StampDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString();
            string BetRef = word.Substring(num1, 3) + StampDate + "-" + model.itemref;
            using (var Betrepo = new BetService())
            {

                Bet _Bet = new Bet
                {
                    ItemRef = BetRef,
                    BetterName = model.BetterName,
                    NewPrice = model.Newprice,
                    IsAccept = 0,
                    date = DateTime.Now
                };
                Betrepo.Insert(_Bet);
            }
        }
        public List<string> GetItemRef(string UserName)
        {
            var ImageStorerepo = new ImageStoreService();
            var Itemrepo = new ItemService();
            List<ItemModelview> lstItemModelview = new List<ItemModelview>();
            var lstimgId = ImageStorerepo.GetAll().FindAll(x => x.UserName.Equals(UserName)).Select(w => w.imgId);

            List<string> lstItemref = new List<string>();
            foreach (var item in lstimgId)
            {
                string x = item.Substring(0, item.Length - 1);
                if (!lstItemref.Contains(x))
                {
                    lstItemref.Add(x);
                }
            }
            return lstItemref;
        }
        public List<BetModelView> GetAllBet()
        {
            var Itemrepo = new ItemService();
            using (var Betrepo = new BetService())
            {
                return Betrepo.GetAll().Select(model => new BetModelView()
                {
                    itemref = model.ItemRef,
                    //find Item id by using substring
                    Currentprice = Itemrepo.GetById(model.ItemRef.Substring(model.ItemRef.IndexOf('-') + 1)).ItemPrice,
                    Newprice = model.NewPrice,
                    IsAccept = model.IsAccept,
                    date = model.date,
                    BetterName = model.BetterName
                }).ToList();
            }
        }
        public BetModelView GetBet(string id)
        {
            var Itemrepo = new ItemService();
            using (var Betrepo = new BetService())
            {
                var model = Betrepo.GetById(id);
                var betModelView = new BetModelView
                {
                    itemref = model.ItemRef,
                    Currentprice = Itemrepo.GetById(model.ItemRef).ItemPrice,
                    Newprice = model.NewPrice,
                    IsAccept = model.IsAccept,
                    date = model.date,
                    BetterName = model.BetterName
                };
                return betModelView;
            }
        }
        public List<BetModelView> GetAllNotUpdateBet()
        {
            var Itemrepo = new ItemService();
            using (var Betrepo = new BetService())
            {
                return Betrepo.GetAll().Where(x => x.IsAccept == 0).Select(model => new BetModelView()
                {
                    itemref = model.ItemRef,
                    Currentprice = Itemrepo.GetById(model.ItemRef.Substring(model.ItemRef.IndexOf('-') + 1)).ItemPrice,
                    Newprice = model.NewPrice,
                    IsAccept = model.IsAccept,
                    date = model.date,
                    BetterName = model.BetterName,
                    ItemName = Itemrepo.GetById(model.ItemRef.Substring(model.ItemRef.IndexOf('-') + 1)).ItemName,
                    ItemDescription = Itemrepo.GetById(model.ItemRef.Substring(model.ItemRef.IndexOf('-') + 1)).ItemDescription
                }).ToList();
            }
        }
        public BetModelView GetItemToBet(string id)
        {
            var Itemrepo = new ItemService();
            var item = Itemrepo.GetById(id);
            var ItemToBet = new BetModelView();
            try
            {
                ItemToBet.itemref = item.ItemRef;
                ItemToBet.Currentprice = item.ItemPrice;
                ItemToBet.Newprice = 0;

            }
            catch (Exception)
            {
                ItemToBet = new BetModelView();
            }
            return ItemToBet;
        }
        public int NumberOfBet(string Username)
        {
            int count = 0;
            var repo = new ImageStoreService();
            foreach (var item in GetAllNotUpdateBet())
            {
                // find the owener of the item

                string refe = item.itemref.Substring(item.itemref.IndexOf('-') + 1) + "0";
                string username = repo.GetById(refe).UserName;
                if (username.Equals(Username))
                {
                    count++;
                }
            }
            return count;
        }
        public List<BetModelView> MyNotUpdateBet(string Username)
        {
            List<BetModelView> lst = new List<BetModelView>();
            var repo = new ImageStoreService();
            foreach (var item in GetAllNotUpdateBet())
            {
                // find the owner of the item

                string refe = item.itemref.Substring(item.itemref.IndexOf('-') + 1) + "0";
                string username = repo.GetById(refe).UserName;
                if (username.Equals(Username))
                {
                    lst.Add(item);
                }
            }
            return lst;
        }
        public bool UpdateBet(UpdateBetModelView Updatemodel)
        {
            var Itemrepo = new ItemService();
            string id = Itemrepo.GetById(Updatemodel.id.Substring(Updatemodel.id.IndexOf('-') + 1)).ItemRef;
            using (var Betrepo = new BetService())
            {
                var model = Betrepo.GetById(Updatemodel.id);
                try
                {
                    model.IsAccept = Updatemodel.ans;
                    if (Updatemodel.ans == 2)
                    {
                        var item = Itemrepo.GetById(id);
                        item.status = "Sold";
                        Itemrepo.Update(item);
                    }
                    Betrepo.Update(model);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        string GetMessage(int num)
        {

            if (num == 1)
            {
                return "Bet Was declined";
            }
            else if (num == 2)
            {
                return "Bet Was Accept";
            }
            else
            {
                return "Pending";
            }
        }
        public List<BetModelView> GetUpdatedBet(string username)
        {
            var Itemrepo = new ItemService();

            using (var Betrepo = new BetService())
            {
                return Betrepo.GetAll().Where(x => x.IsAccept != 0 && x.BetterName == username && x.IsRead == false).Select(model => new BetModelView()
                {
                    itemref = model.ItemRef,
                    Newprice = model.NewPrice,
                    IsAccept = model.IsAccept,
                    date = model.date,
                    BetterName = model.BetterName,
                    ItemName = Itemrepo.GetById(model.ItemRef.Substring(model.ItemRef.IndexOf('-') + 1)).ItemName,
                    ItemDescription = Itemrepo.GetById(model.ItemRef.Substring(model.ItemRef.IndexOf('-') + 1)).ItemDescription,
                    Message = GetMessage(model.IsAccept)
                }).ToList();
            }

        }
        public void UpdateRead(string itemref)
        {
            var Betrepo = new BetService();
            var item = Betrepo.GetById(itemref);
            item.IsRead = true;
            Betrepo.Update(item);
        }
    }
}
