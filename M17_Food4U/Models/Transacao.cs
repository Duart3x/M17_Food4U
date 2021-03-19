using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Transacao
    {
        public int id { get; set; }
        public int user { get; set; }
        public string source { get; set; }
        public double saldo { get; set; }
        public double valor { get; set; }
        public DateTime createDate { get; set; }

        BaseDados bd;
        public Transacao()
        {
            bd = new BaseDados();
        }

        public static DataTable GetTransacoesUser(int id_user)
        {
            BaseDados bd = new BaseDados();

            string sql = $"SELECT * FROM transacoes WHERE [user] = {id_user} ORDER BY createDate";

            return bd.devolveSQL(sql);
        }
    }
}