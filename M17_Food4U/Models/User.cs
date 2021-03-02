using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string nif { get; set; }
        public string password { get; set; }
        public int perfil { get; set; }
        public DateTime data_nasc { get; set; }
        public double saldo { get; set; }
        public string driving_license { get; set; }
        public DateTime driving_licenseValidity { get; set; }

        BaseDados bd;

        public User()
        {
            bd = new BaseDados();
        }

        public void Adicionar()
        {
            string sql = @"INSERT INTO users(email,name,nif,password,estado,perfil,data_nasc,saldo,createDate)
                            VALUES (@email,@name,@nif,HASHBYTES('SHA2_512',@password),@estado,@perfil,@data_nasc,@saldo,@createDate)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter()
                {
                    ParameterName="@name",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },
                new SqlParameter()
                {
                    ParameterName="@nif",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nif
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.password
                },
                new SqlParameter()
                {
                    ParameterName="@estado",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=1
                },
                new SqlParameter()
                {
                    ParameterName="@perfil",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.perfil
                },
                new SqlParameter()
                {
                    ParameterName="@data_nasc",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_nasc
                },
                new SqlParameter()
                {
                    ParameterName="@saldo",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=0
                },
                 new SqlParameter()
                {
                    ParameterName="@createDate",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=DateTime.Now
                },
            };
            bd.executaSQL(sql, parametros);
        }
        public void AdicionarEstafeta()
        {
            string sql = @"INSERT INTO users(email,name,nif,password,estado,perfil,data_nasc,saldo,drivingLicense,drivingLicenseValidity,createDate)
                            VALUES (@email,@name,@nif,HASHBYTES('SHA2_512',@password),@estado,@perfil,@data_nasc,@saldo,@drivingLicense,@drivingLicenseValidity,@createDate)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter()
                {
                    ParameterName="@name",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },
                new SqlParameter()
                {
                    ParameterName="@nif",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nif
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.password
                },
                new SqlParameter()
                {
                    ParameterName="@estado",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=1
                },
                new SqlParameter()
                {
                    ParameterName="@perfil",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.perfil
                },
                new SqlParameter()
                {
                    ParameterName="@data_nasc",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_nasc
                },
                new SqlParameter()
                {
                    ParameterName="@saldo",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=0
                },
                new SqlParameter()
                {
                    ParameterName="@drivingLicense",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.driving_license
                },
                new SqlParameter()
                {
                    ParameterName="@drivingLicenseValidity",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.driving_licenseValidity
                },
                new SqlParameter()
                {
                    ParameterName="@createDate",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=DateTime.Now
                },
            };
            bd.executaSQL(sql, parametros);
        }
    }
}