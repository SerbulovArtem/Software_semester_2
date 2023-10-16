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
        private ImdbContext _context;
        public ActionManagerActionsRepository(ImdbContext context) 
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
            return _context.TblActions.ToList();
        }

        public void Update(TblAction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
