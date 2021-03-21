using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Pagamento
    {
        public int id { get; set; }
        public int user { get; set; }
        public int restaurant { get; set; }
        public int courier { get; set; }
        public double saldo { get; set; }
        public double valor { get; set; }
        public DateTime createDate { get; set; }

        BaseDados bd;

        public Pagamento()
        {
            bd = new BaseDados();
        }

        public static DataTable getPagamentosUser(int id_user)
        {
            BaseDados bd = new BaseDados();

            string sql = $"SELECT IIF(restaurants.name is NULL, ' ' ,restaurants.name) as Restaurante, IIF(users.name is NULL, ' ' ,users.name) as Estafeta, pagamentos.saldo, valor, pagamentos.createDate as [Data] FROM pagamentos LEFT JOIN restaurants ON pagamentos.restaurant = restaurants.id LEFT JOIN [users] ON pagamentos.courier = users.id WHERE [user] = {id_user} ORDER BY pagamentos.createDate";

            return bd.devolveSQL(sql);
        }
    }
}