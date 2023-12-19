using ActionManager.DAL.Data;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionManager.DAL.Repositories.Concreate.DataBaseMCSQLActionManager
{
    public class ActionManagerActionsRepository : IActionsRepository
    {
        private ActionManagerContext _context;
        public ActionManagerActionsRepository(ActionManagerContext context) 
        {
            _context = context;
        }

        public void Create(TblAction entity)
        {
            _context.TblActions.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TblAction entity)
        {
            _context.TblActions.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<TblAction> GetDbSet()
        {
            return _context.TblActions;
        }

        public List<TblAction> GetList()
        {
            return GetDbSet().Include(a => a.Product).Include(a => a.TypeAction).ToList();
        }

        public List<TblAction> GetPastList()
        {
            return GetDbSet().Include(a => a.Product).Include(a => a.TypeAction).Where(a => a.TypeAction.TypeActionId == 1).ToList();
        }

        public List<TblAction> GetPresentList()
        {
            return GetDbSet().Include(a => a.Product).Include(a => a.TypeAction).Where(a => a.TypeAction.TypeActionId == 2).ToList();
        }

        public List<TblAction> GetFutureList()
        {
            return GetDbSet().Include(a => a.Product).Include(a => a.TypeAction).Where(a => a.TypeAction.TypeActionId == 3).ToList();
        }

        public bool CheckDiscountPercentage(decimal discountpercentage)
        {
            return discountpercentage > 0.00m && discountpercentage < 100.00m;
        }

        public void Update(TblAction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
