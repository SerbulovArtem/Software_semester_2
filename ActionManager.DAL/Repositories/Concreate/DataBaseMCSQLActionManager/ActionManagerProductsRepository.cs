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
    public class ActionManagerProductsRepository : IProductsRepository
    {
        private ActionManagerContext _context;
        public ActionManagerProductsRepository(ActionManagerContext context) 
        {
            _context = context;
        }

        public void Create(TblProduct entity)
        {
            _context.TblProducts.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TblProduct entity)
        {
            _context.TblProducts.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<TblProduct> GetDbSet()
        {
            return _context.TblProducts;
        }

        public List<TblProduct> GetList()
        {
            return GetDbSet().ToList();
        }

        public void Update(TblProduct entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
