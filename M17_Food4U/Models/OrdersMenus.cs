using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class OrdersMenus
    {
        public int id { get; set; }
        public int menu { get; set; }
        public int quantity { get; set; }
        public int order { get; set; }
        public int state { get; set; }

        BaseDados bd;
        public OrdersMenus()
        {
            bd = new BaseDados();
        }
        public OrdersMenus(int id)
        {
            bd = new BaseDados();
            this.id = id;
        }

        public void AlterarEstado(int novo_estado)
        {
            string sql = "UPDATE orders_menus SET [state] = @estado WHERE id = @id";
            SqlTransaction transacao = bd.iniciarTransacao(System.Data.IsolationLevel.Serializable);

            try
            {
                List<SqlParameter> parametrosUpdate = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@id",SqlDbType=SqlDbType.Int,Value=this.id },
                    new SqlParameter() {ParameterName="@estado",SqlDbType=SqlDbType.Int,Value=novo_estado },
                };
                bd.executaSQL(sql, parametrosUpdate, transacao);

                transacao.Commit();
            }
            catch (Exception)
            {
                transacao.Rollback();
            }


        }
    }
}