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
    public class ActionManagerTypeActionsRepository : ITypeActionsRepository
    {
        private ActionManagerContext _context;
        public ActionManagerTypeActionsRepository(ActionManagerContext context) 
        {
            _context = context;
        }

        public void Create(TblTypeAction entity)
        {
            _context.TblTypeActions.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TblTypeAction entity)
        {
            _context.TblTypeActions.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<TblTypeAction> GetDbSet()
        {
            return _context.TblTypeActions;
        }

        public List<TblTypeAction> GetList()
        {
            return GetDbSet().ToList();
        }

        public void Update(TblTypeAction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
