using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Pagamento
    {
        public int id { get; set; }
        public int user { get; set; }
        public int order { get; set; }
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

            string sql = $"SELECT pagamentos.saldo, valor, pagamentos.createDate as [Data] FROM pagamentos WHERE [user] = {id_user} ORDER BY pagamentos.createDate";

            return bd.devolveSQL(sql);
        }

        public void PagarOrder()
        {
            var transacao = bd.iniciarTransacao(IsolationLevel.Serializable);
            try
            {
                string sql = $"UPDATE users SET saldo = (saldo - {this.valor.ToString().Replace(",", ".")}) WHERE id = {user}";

                bd.executaSQL(sql, new List<SqlParameter>(), transacao);

                sql = $"INSERT INTO pagamentos ([user],saldo,valor,[order]) VALUES({user},{this.saldo.ToString().Replace(",", ".")},{this.valor.ToString().Replace(",", ".")}, {order});";

                bd.executaSQL(sql, new List<SqlParameter>(), transacao);

                transacao.Commit();
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw erro;
            }
        }

        public void PagarEstafeta(int id_courier)
        {
            var transacao = bd.iniciarTransacao(IsolationLevel.Serializable);

            try
            {
                string sql = $"UPDATE users SET saldo = (saldo + {this.valor.ToString().Replace(",", ".")}) WHERE id = {id_courier}";

                bd.executaSQL(sql, new List<SqlParameter>(), transacao);

                transacao.Commit();
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw erro;
            }
        }

        public void PagarRestaurante(int id_restaurante)
        {
            var transacao = bd.iniciarTransacao(IsolationLevel.Serializable);

            string sql = "";
            try
            {
                sql = $"UPDATE restaurants SET saldo = (saldo + {this.valor.ToString().Replace(",", ".")}) WHERE id = {id_restaurante}";

                bd.executaSQL(sql, new List<SqlParameter>(), transacao);

                transacao.Commit();
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw erro;
            }
        }
    }
}