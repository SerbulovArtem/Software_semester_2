using ActionManager.DAL.Data;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionManager.BLL.Repositories.Concreate.DataBaseMCSQLActionManager
{
    public class ActionManagerUserRepository : IUsersRepository
    {
        private ActionManagerContext _context;
        public ActionManagerUserRepository(ActionManagerContext context)
        {
            _context = context;
        }

        public void Create(TblUser entity)
        {
            _context.TblUsers.Add(entity);
            _context.SaveChanges();
        }

        public string GenerateSalt(string username, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(username + password);
        }

        public string EncryptPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password + salt);
        }

        public bool VerifyPassword(string password, string hashpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashpassword);
        }

        public bool AuthUser(string username, string password)
        {
            var user = _context.TblUsers.SingleOrDefault(u => u.Username == username);
            return BCrypt.Net.BCrypt.Verify(password + user.Salt, user.Password);
        }

        public void Delete(TblUser entity)
        {
            _context.TblUsers.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<TblUser> GetDbSet()
        {
            return _context.TblUsers;
        }

        public List<TblUser> GetList()
        {
            return _context.TblUsers.ToList();
        }

        public void Update(TblUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
