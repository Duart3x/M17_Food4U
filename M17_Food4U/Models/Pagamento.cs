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

            string sql = $"SELECT * FROM pagamentos WHERE [user] = {id_user} ORDER BY createDate";

            return bd.devolveSQL(sql);
        }
    }
}