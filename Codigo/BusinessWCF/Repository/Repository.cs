using System.Data.Common;
using System.Data;
using System.Diagnostics;
using BusinessWCF.DataAccess.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessWCF.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        private readonly TechnicalExamDBContext _dbContext;

        public Repository(TechnicalExamDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

      
        public async Task<TEntity> Get(object id)
        {
            if (_dbContext != null)
            {
                try
                {
                    return await _dbContext.FindAsync<TEntity>(id);
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
                if (_dbContext != null)
                    return _dbContext.Set<TEntity>().ToList();
                else
                    throw new ArgumentException("Error. Context DB cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Create(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Add(entity);
                    return true;
                }
                else
                    throw new ArgumentException("Error. Context DB or entity cannot be null.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Update(entity);
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
                if (_dbContext != null && entity != null)
                {
                    _dbContext.Set<TEntity>().Remove(entity);
                    return true;
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

        public DbCommand GetDbCommand()
        {
           return this._dbContext.Database.GetDbConnection().CreateCommand();
        }
    }

}
