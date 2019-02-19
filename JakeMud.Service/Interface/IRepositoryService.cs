using JakeMud.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JakeMud.Service.Interface
{
    interface IRepositoryService<TEntity>
        where TEntity : class
    {
        IResult Create(TEntity instance);

        IResult Update(TEntity instance);

        IResult Delete(Expression<Func<TEntity, bool>> predicate);

        bool IsExists(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();
    }
}
