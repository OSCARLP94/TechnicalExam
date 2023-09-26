using Business.DataAccess.Factory;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using Business.DataAccess.ContextDB;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        private readonly IContextDBFactory dbContextFactory;

        public Repository(IContextDBFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

      
        public async Task<TEntity> Get(object id)
        {
            if (dbContextFactory != null)
            {
                try
                {
                    using (var context = dbContextFactory.Process())
                    {
                        return await context.DbSet<TEntity>().FindAsync(id);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("Error. Context DB cannot be null.");
            }
            return null;
        }

        public async Task<List<TEntity>> List()
        {
            try
            {
                if (dbContextFactory != null)
                {
                    using (var context = dbContextFactory.Process())
                    {
                        return await context.DbSet<TEntity>().ToListAsync();
                    }
                }
                else
                {
                    throw new ArgumentException("Error. Context DB cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Create(TEntity entity)
        {
            bool result = false;
            try
            {
                if (dbContextFactory != null && entity != null)
                {
                    using (var context = this.dbContextFactory.Process())
                    {
                        await context.DbSet<TEntity>().AddAsync(entity);
                        await context.SaveChangesAsync();
                    }
                    result = true;
                }
                else
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                if (dbContextFactory != null && entity != null)
                {
                    //this.EntitySet.Attach(entity);                  
                    using (var context = this.dbContextFactory.Process())
                    {
                        context.DbSet<TEntity>().Update(entity);
                        await context.SaveChangesAsync();
                    }
                    return entity;
                }
                else
                {
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

     
        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                if (dbContextFactory != null && entity != null)
                {
                    using (var context = this.dbContextFactory.Process())
                    {
                        context.Remove(entity);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                else
                {
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public async Task<int> ExecuteSP(DbCommand cmd)
        {
            int result = -1;
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                    var resp = await cmd.ExecuteScalarAsync();
                    result = (resp != null) ? Convert.ToInt32(resp) : -1;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return result;
        }
    }

}
