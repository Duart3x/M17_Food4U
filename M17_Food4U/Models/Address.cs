using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Address
    {
        public int id { get; set; }
        public int user { get; set; }
        public string city { get; set; }
        public string cp { get; set; }
        public string address { get; set; }
        
        BaseDados bd;
        public Address()
        {
            bd = new BaseDados();
        }

        public Address(int id,int user, string city, string cp, string address)
        {
            bd = new BaseDados();
            this.id = id;
            this.user = user;
            this.city = city;
            this.cp = cp;
            this.address = address;
        }

        public Address(int user, string city, string cp, string address)
        {
            bd = new BaseDados();
            this.user = user;
            this.city = city;
            this.cp = cp;
            this.address = address;
        }

        public void Adicionar()
        {
            string sql = "INSERT INTO addresses([user],city,cp,address) VALUES(@user,@city,@cp,@address)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@user",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.user
                },
                new SqlParameter()
                {
                    ParameterName="@city",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.city
                },
                new SqlParameter()
                {
                    ParameterName="@cp",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.cp
                },
                new SqlParameter()
                {
                    ParameterName="@address",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.address
                },
            };

            bd.executaSQL(sql, parametros);
        }

        internal void Atualizar()
        {
            string sql = "UPDATE addresses SET city = @city, cp = @cp, address = @address WHERE id = @id";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@city",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.city
                },
                new SqlParameter()
                {
                    ParameterName="@cp",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.cp
                },
                new SqlParameter()
                {
                    ParameterName="@address",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.address
                },
                new SqlParameter()
                {
                    ParameterName="@id",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.id
                },
            };

            bd.executaSQL(sql, parametros);


        }
    }
}