using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager
{
    public interface IRepository<T>
    {
        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);

        List<T> GetAll();
    }
}
