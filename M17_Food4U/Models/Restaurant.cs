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

        internal static DataTable ListarRestaurantesUser(int id_user)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT * FROM restaurants WHERE owner = {id_user}";

            return bd.devolveSQL(sql);

            
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

        internal static bool OwnsOrderMenu(int id_restaurante, int id_order)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT orders_menus.id
                        FROM orders_menus INNER JOIN menus ON orders_menus.menu = menus.id
                        WHERE menus.restaurant = {id_restaurante} AND orders_menus.id = {id_order}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return false;

            return true;

        }

        public DataTable ListarPedidosRestaurante(List<int> estados = null, DateTime? data = null, DateTime? Inicio = null, DateTime? Fim = null)
        {
            string lista = "'1','2','3'";
            if (estados != null)
            {
                if (estados.Count > 0)
                {
                    lista = "";
                    for (int i = 0; i < estados.Count; i++)
                    {
                        int estado = estados[i];
                        if (i != estados.Count - 1)
                            lista += $@"{estado},";
                        else
                            lista += $@"{estado}";
                    }
                }                
            }
            /*
            orders_menus
            - Em espera 
            - A ser preparada
            - Concluida
            */

            string sql = $@"SELECT orders_menus.id as [ID Pedido],menus.id as [ID Menu], menus.title as Menu,  orders.createDate as [data] ,case
                            when orders_menus.[state]=1 then 'Em espera'
                            when orders_menus.[state]=2 then 'A ser preparado'
                            when orders_menus.[state]=3 then 'Concluído'
                        end as estado 
                        FROM orders_menus INNER JOIN menus ON orders_menus.menu = menus.id INNER JOIN orders on orders_menus.[order] = orders.id
                        WHERE menus.restaurant = {this.id} AND orders_menus.[state] in ({lista})";

            if (data != null)
            {
                if (Inicio != null)
                    Inicio = new DateTime(data.Value.Year, data.Value.Month, data.Value.Day, Inicio.Value.Hour, Inicio.Value.Minute, Inicio.Value.Second);
                else
                    Inicio = new DateTime(data.Value.Year, data.Value.Month, data.Value.Day, 0, 0, 0);

                if (Fim != null)
                    Fim = new DateTime(data.Value.Year, data.Value.Month, data.Value.Day, Fim.Value.Hour, Fim.Value.Minute, Fim.Value.Second);
                else
                    Fim = new DateTime(data.Value.Year, data.Value.Month, data.Value.Day, 23, 59, 59);

                sql += $" AND orders.createDate between CONVERT(datetime,'{Inicio.ToString()}') AND CONVERT(datetime,'{Fim.ToString()}')";

            }

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
        public DataTable ListarMenusRestaurante()
        {
            BaseDados bd = new BaseDados();

            string sql = @"SELECT menus.id ,menus.title as nome, menus.[description] as descricao, menus.price as preco, 
menus.stars as estrelas, menus.stock, menus.[enabled] as ativado, IIF(SUM(quantity) is NULL, 0,SUM(quantity)) as quantidade
FROM menus LEFT JOIN orders_menus ON menus.id = orders_menus.menu WHERE restaurant = @id GROUP BY menus.id, menus.title, menus.[description], 
menus.price, menus.stars, menus.stock,menus.[enabled]";
            
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=this.id }
            };

            return bd.devolveSQL(sql, parametros);
        }
    }
}