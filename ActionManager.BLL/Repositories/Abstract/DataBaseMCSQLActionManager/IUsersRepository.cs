using ActionManager.DAL.Data;
using ActionManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionManager.BLL.Repositories.Concreate.DataBaseMCSQLActionManager
{
    public interface IUsersRepository
    {
        public void Create(TblUser entity);

        public string GenerateSalt(string username, string password);

        public string EncryptPassword(string password, string salt);

        public bool VerifyPassword(string password, string hashpassword);

        public bool AuthUser(string username, string password);

        public void Delete(TblUser entity);

        public DbSet<TblUser> GetDbSet();

        public List<TblUser> GetList();

        public void Update(TblUser entity);
    }
}
