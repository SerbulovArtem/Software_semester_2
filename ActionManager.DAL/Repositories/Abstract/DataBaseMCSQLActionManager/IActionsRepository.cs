using ActionManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager
{
    public interface IActionsRepository : IRepository<TblAction>
    {
        public List<TblAction> GetPastList();

        public List<TblAction> GetPresentList();

        public List<TblAction> GetFutureList();

        public bool CheckDiscountPercentage(decimal discountpercentage);
    }
}
