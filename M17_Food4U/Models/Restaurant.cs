using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Restaurant
    {
        public int id { get; set; }
        public int owner { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string cp { get; set; }
        public string address { get; set; }
        public bool enabled { get; set; }

        BaseDados bd;
        public Restaurant()
        {
            bd = new BaseDados();
        }

        public Restaurant(int id)
        {
            bd = new BaseDados();
            this.id = id;
        }

        public static DataTable ListarRestaurantesDiponiveis()
        {
            BaseDados bd = new BaseDados();
            DataTable dados = bd.devolveSQL("SELECT id,name,city,address FROM restaurants WHERE enabled = 1");
            return dados;
        }

        public int AdicionarRestauranteOwner(User utilizador)
        {
            SqlTransaction transacao =  bd.iniciarTransacao();

            try
            {
                string sql = @"INSERT INTO users(email,name,nif,password,estado,perfil,data_nasc,saldo,createDate)
                            VALUES (@email,@name,@nif,HASHBYTES('SHA2_512',@password),@estado,@perfil,@data_nasc,@saldo,@createDate); SELECT SCOPE_IDENTITY();";


                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@email",
                        SqlDbType= SqlDbType.VarChar,
                        Value=utilizador.email
                    },
                    new SqlParameter()
                    {
                        ParameterName="@name",
                        SqlDbType= SqlDbType.VarChar,
                        Value=utilizador.nome
                    },
                    new SqlParameter()
                    {
                        ParameterName="@nif",
                        SqlDbType= SqlDbType.VarChar,
                        Value=utilizador.nif
                    },
                    new SqlParameter()
                    {
                        ParameterName="@password",
                        SqlDbType= SqlDbType.VarChar,
                        Value=utilizador.password
                    },
                    new SqlParameter()
                    {
                        ParameterName="@estado",
                        SqlDbType= SqlDbType.Int,
                        Value=1
                    },
                    new SqlParameter()
                    {
                        ParameterName="@perfil",
                        SqlDbType= SqlDbType.Int,
                        Value=utilizador.perfil
                    },
                    new SqlParameter()
                    {
                        ParameterName="@data_nasc",
                        SqlDbType= SqlDbType.Date,
                        Value=utilizador.data_nasc
                    },
                    new SqlParameter()
                    {
                        ParameterName="@saldo",
                        SqlDbType= SqlDbType.Int,
                        Value=0
                    },
                     new SqlParameter()
                    {
                        ParameterName="@createDate",
                        SqlDbType= SqlDbType.Date,
                        Value=DateTime.Now
                    },
                };
                DataTable dados = bd.devolveSQL(sql, parametros, transacao);

                int id = int.Parse(dados.Rows[0].ItemArray[0].ToString());


                sql = @"INSERT INTO restaurants(owner,name,city,cp,address)
                            VALUES (@owner,@name,@city,@cp,@address); SELECT SCOPE_IDENTITY();";

                parametros = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@owner",
                        SqlDbType= SqlDbType.Int,
                        Value=id
                    },
                    new SqlParameter()
                    {
                        ParameterName="@name",
                        SqlDbType= SqlDbType.VarChar,
                        Value=this.name
                    },
                    new SqlParameter()
                    {
                        ParameterName="@city",
                        SqlDbType= SqlDbType.VarChar,
                        Value=this.city
                    },
                    new SqlParameter()
                    {
                        ParameterName="@cp",
                        SqlDbType= SqlDbType.VarChar,
                        Value=this.cp
                    },
                    new SqlParameter()
                    {
                        ParameterName="@address",
                        SqlDbType= SqlDbType.VarChar,
                        Value= this.address
                    }
                };

                dados = bd.devolveSQL(sql, parametros,transacao);
                id = int.Parse(dados.Rows[0].ItemArray[0].ToString());
                
                transacao.Commit();
                return id;
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw new Exception(erro.Message);
            }
           
        }

        public DataTable ListarPedidosRestaurante(List<int> estados = null)
        {
            string lista = "'1','2','3'";
            if (estados != null)
            {
                lista = "";
                for (int i = 0; i < estados.Count; i++)
                {
                    int estado = estados[i];
                    if(i != estados.Count -1)
                        lista += $@"{estado},";
                    else
                        lista += $@"{estado}";
                }
            }
            string sql = $@"SELECT * FROM orders_menus INNER JOIN menus ON orders_menus.menu = menus.id WHERE menus.restaurant = {this.id} AND orders_menus.[state] in ({lista})";



            DataTable dados = bd.devolveSQL(sql);

            return dados;
        }
        public static bool UserOwnsRestaurant(int restaurante,int user)
        {
            BaseDados bd = new BaseDados();
            string sql = $@"SELECT * FROM restaurants WHERE owner = {user} AND id = {restaurante}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return false;

            return true;
        }
    }
}