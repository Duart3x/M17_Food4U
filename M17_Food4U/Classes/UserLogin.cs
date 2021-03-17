using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Classes
{
    public class UserLogin
    {
        BaseDados bd;

        public UserLogin()
        {
            this.bd = new BaseDados();
        }

        static public DataTable verificaLogin(string email, string password)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT * FROM users WHERE 
                     email=@email AND password=HASHBYTES('SHA2_512',@password)
                     AND estado=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=SqlDbType.VarChar,
                    Value=email
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=SqlDbType.VarChar,
                    Value=password
                }
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return null;

            return dados;
        }

        public static bool CheckUserPassword(int id_user, string password)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT * FROM users WHERE 
                     id=@id AND password=HASHBYTES('SHA2_512',@password)
                     AND estado=1";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@id",
                    SqlDbType=SqlDbType.Int,
                    Value=id_user
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=SqlDbType.VarChar,
                    Value=password
                }
            };
            DataTable dados = bd.devolveSQL(sql, parametros);
            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return false;

            return true;
        }
    }
}