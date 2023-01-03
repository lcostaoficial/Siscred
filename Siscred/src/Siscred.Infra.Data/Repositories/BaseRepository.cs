using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace Siscred.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected MainContext Context;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(MainContext mainContext)
        {
            Context = mainContext;
            DbSet = Context.Set<TEntity>();
        }

        public virtual TEntity Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual TEntity Atualizar(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public virtual TEntity Remover(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
            return entity;
        }

        public virtual TEntity ObterUnico(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include)).FirstOrDefault(expression);
        }

        public IEnumerable<TEntity> ObterTodos(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            if (expression == null)
            {
                return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include));
            }
            else
            {
                return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include)).Where(expression);
            }
        }

        public IEnumerable<TEntity> ObterTodosFilter(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            if (expression == null)
            {
                return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.IncludeFilter(include));
            }
            else
            {
                return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.IncludeFilter(include)).Where(expression);
            }
        }

        public virtual IEnumerable<TEntity> ObterTodosPaginado(Expression<Func<TEntity, bool>> expressionWhere, Expression<Func<TEntity, object>> expressionOrder, int start, int length, out int recordsTotal, params Expression<Func<TEntity, object>>[] includes)
        {
            recordsTotal = DbSet.Count(expressionWhere);
            return includes.Aggregate(DbSet.AsQueryable(), (current, include) => current.Include(include)).Where(expressionWhere).OrderBy(expressionOrder).Skip(start).Take(length);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet;
        }

        public virtual bool Existe(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Any(expression);
        }

        public int Contar(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null) return DbSet.Count();
            return DbSet.Count(expression);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Context.Dispose();
        }
    }
}