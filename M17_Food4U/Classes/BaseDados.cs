using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Classes
{
    public class BaseDados
    {
        private string strLigacao;
        private SqlConnection ligacaoBD;
        public BaseDados()
        {
            //ligação à bd
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }
        ~BaseDados()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region Transações
        public SqlTransaction iniciarTransacao()
        {
            return ligacaoBD.BeginTransaction();
        }
        public SqlTransaction iniciarTransacao(IsolationLevel isolamento)
        {
            return ligacaoBD.BeginTransaction(isolamento);
        }
        #endregion
        #region SQL de ação
        public void executaSQL(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }

        public void executaSQL(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public void executaSQL(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.Transaction = transacao;
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public int executaEDevolveSQL(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        public int executaEDevolveSQL(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        public int executaEDevolveSQL(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.Transaction = transacao;
            var id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        #endregion
        #region SQL Consultas
        /// <summary>
        /// Devolve o resultado de uma consulta
        /// </summary>
        /// <param name="sql">Select à base de dados</param>
        /// <returns></returns>
        public DataTable devolveSQL(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        ///
        public DataTable devolveSQL(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        public DataTable devolveSQL(string sql, List<SqlParameter> parametros, SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Transaction = transacao;
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        #endregion
    }
}