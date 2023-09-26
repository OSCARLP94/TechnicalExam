using Business.DataAccess.ContextDB;
using Business.DataAccess.Factory;
using Business.DataAccess.SPMappers;
using Business.Entities;
using Business.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Business.BusinessLogic
{
    public class UserBL : Repository<User>
    {
        private readonly IContextDBFactory _contextDBfactory;

        public UserBL(IContextDBFactory contextDBFactory) : base(contextDBFactory)
        { 
            _contextDBfactory = contextDBFactory;
        }
        public async Task<List<User>> GetAll()
        {
            try
            {
                return await this.List();
            }
            catch (Exception ex)
            {
                throw ;
            }

        }

        public async Task<User> Register(User newUser)
        {
            try
            {
                #region Solo dejé esto para validar mi conocimiento con LINQ y uso de EF
                //verificamos si existe un usuario identico alparecer (porque no usamos DNI)
                var exists = await Task.Run(() =>
                {
                    using (var context = this._contextDBfactory.Process())
                    {
                        var result = (from user in context.DbSet<User>()
                                      where user.Name == newUser.Name && user.BirthDate == newUser.BirthDate && user.Sex == newUser.Sex
                                      select user).FirstOrDefault();

                        return result;
                    }
                });

                if (exists != null)
                    throw new ArgumentException("User already exists");

                #endregion

                //generamos id unico
                newUser.Id = Guid.NewGuid().ToString();

                //registramos en este caso usando SP como pide la prueba
                var result = -1; //the sp return id of entity
                DbCommand cmd = this._contextDBfactory.Process().Database.GetDbConnection().CreateCommand();
                var cmd2 = UserSPMapper.RegisterUser(cmd, newUser);
                result = await ExecuteSP(cmd2);

                if (result <= 0)
                    throw new ArgumentException("Error registering user");

                return newUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> Update(User editUser)
        {
            try
            {
                User current = await this.Get(editUser.Id);

                if (current == null)
                    throw new ArgumentException($"User {editUser.Id} not exists");

                //registramos en este caso usando SP como pide la prueba
                var result = -1; //the sp return id of entity
                DbCommand cmd = this._contextDBfactory.Process().Database.GetDbConnection().CreateCommand();
                var cmd2 = UserSPMapper.EditUser(cmd, editUser);
                result = await ExecuteSP(cmd2);

                if (result <= 0)
                    throw new ArgumentException("Error updating user");

                return editUser;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(String id)
        {
            try
            {
                //se procede a borrar usando SP como pide la prueba

                var result = -1; //the sp return id of entity
                DbCommand cmd = this._contextDBfactory.Process().Database.GetDbConnection().CreateCommand();
                var cmd2 = UserSPMapper.DeleteUser(cmd, id);
                result = await ExecuteSP(cmd2);

                if (result <= 0)
                    throw new ArgumentException("Error deleting user");

                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
