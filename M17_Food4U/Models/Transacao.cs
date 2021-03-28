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

        public static void DepositarDinheiro(int id_user, double saldo, double valor)
        {
            BaseDados bd = new BaseDados();

            var transacao = bd.iniciarTransacao(IsolationLevel.Serializable);

            try
            {
                string sql = $"UPDATE users SET saldo = (saldo + {valor.ToString().Replace(",", ".")}) WHERE id = {id_user}";

                bd.executaSQL(sql, new List<System.Data.SqlClient.SqlParameter>(), transacao);

                sql = $@"INSERT INTO transacoes([user],[source],saldo,valor) 
                    VALUES({id_user},'Depósito PayPal', {saldo.ToString().Replace(",", ".")},{valor.ToString().Replace(",", ".")})";

                bd.executaSQL(sql, new List<System.Data.SqlClient.SqlParameter>(), transacao);

                transacao.Commit();
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw erro;
            }
            
        }

        internal static void LevantarDinheiro(int id_user, double saldo, double valor)
        {
            BaseDados bd = new BaseDados();

            var transacao = bd.iniciarTransacao(IsolationLevel.Serializable);

            try
            {
                string sql = $"UPDATE users SET saldo = (saldo - {valor.ToString().Replace(",", ".")}) WHERE id = {id_user}";

                bd.executaSQL(sql, new List<System.Data.SqlClient.SqlParameter>(), transacao);

                sql = $@"INSERT INTO transacoes([user],[source],saldo,valor) 
                    VALUES({id_user},'Levantamento PayPal', {saldo.ToString().Replace(",", ".")},{valor.ToString().Replace(",", ".")})";

                bd.executaSQL(sql, new List<System.Data.SqlClient.SqlParameter>(), transacao);

                transacao.Commit();
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw erro;
            }
        }

        internal static void LevantarDinheiroRestaurante(int id_restaurante, double valor)
        {
            BaseDados bd = new BaseDados();

            var transacao = bd.iniciarTransacao(IsolationLevel.Serializable);

            try
            {
                string sql = $"UPDATE restaurants SET saldo = (saldo - {valor.ToString().Replace(",", ".")}) WHERE id = {id_restaurante}";

                bd.executaSQL(sql, new List<System.Data.SqlClient.SqlParameter>(), transacao);

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