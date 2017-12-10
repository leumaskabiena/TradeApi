using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade.Model.ModelView;

namespace Trade.BusinessLogic.IBusiness
{
    public interface IBetBusiness
    {
        List<string> GetItemRef(string UserName);
        List<BetModelView> GetAllBet();
        BetModelView GetBet(string id);
        List<BetModelView> GetAllNotUpdateBet();
        BetModelView GetItemToBet(string id);
        void CreateBet(BetModelView model);
        void CreateBetApp(BetModelView model);
        int NumberOfBet(string Username);
        List<BetModelView> MyNotUpdateBet(string Username);
        bool UpdateBet(UpdateBetModelView model);
        List<BetModelView> GetUpdatedBet(string username);
        void UpdateRead(string itemref);
    }
}
