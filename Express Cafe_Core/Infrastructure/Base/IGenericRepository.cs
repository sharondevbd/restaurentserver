using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Infrastructure.Base
{
    public interface IGenericRepository<T> where T : class
    {
		//Task<IEnumerable<T>> getDATA();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();

      Task<IEnumerable<T> >GetAll(Expression<Func<T, bool>>? predicate, string? includeProperties);
        //IEnumerable<T> GetAllbyParent(Expression<Func<T, bool>> predicate);
        T GetT(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Add(List<T> entity);
        void Delete(T entity);
        void DeletebyID(Expression<Func<T, bool>> predicate);
        void DeleteRange(IEnumerable<T> entitylist);
        void Update(T entity);
    }
}
