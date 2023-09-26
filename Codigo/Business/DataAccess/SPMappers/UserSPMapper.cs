using System.Data.Common;
using System.Data;
using Business.Entities;
using Microsoft.Data.SqlClient;

namespace Business.DataAccess.SPMappers
{
    public static class UserSPMapper
    {
        /// <summary>
        /// Crea comando para ejecutar SP SAVE_USER
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DbCommand RegisterUser(DbCommand cmd, User entity)
        {
            if (entity == null)
                return null;

            cmd.CommandText = "SAVE_USER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("ID", SqlDbType.VarChar) { Value = entity.Id });
            cmd.Parameters.Add(new SqlParameter("NAME", SqlDbType.VarChar) { Value = entity.Name });
            cmd.Parameters.Add(new SqlParameter("BIRTH_DATE", SqlDbType.DateTime) { Value = entity.BirthDate });
            cmd.Parameters.Add(new SqlParameter("SEX", SqlDbType.VarChar) { Value = entity.Sex });

            return cmd;

        }

        /// <summary>
        /// Crea comando para ejecutar SP EDIT_USER
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DbCommand EditUser(DbCommand cmd, User entity)
        {
            if (entity == null)
                return null;

            cmd.CommandText = "EDIT_USER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("ID", SqlDbType.VarChar) { Value = entity.Id });
            cmd.Parameters.Add(new SqlParameter("NAME", SqlDbType.VarChar) { Value = entity.Name });
            cmd.Parameters.Add(new SqlParameter("BIRTH_DATE", SqlDbType.DateTime) { Value = entity.BirthDate });
            cmd.Parameters.Add(new SqlParameter("SEX", SqlDbType.VarChar) { Value = entity.Sex });

            return cmd;

        }

        /// <summary>
        /// Crea comando para ejecutar SP DELETE_USER
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DbCommand DeleteUser(DbCommand cmd, string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            cmd.CommandText = "DELETE_USER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("ID", SqlDbType.VarChar) { Value = id });
            return cmd;

        }

    }
}
