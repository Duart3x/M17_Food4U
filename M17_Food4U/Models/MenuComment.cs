using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class MenuComment
    {
        public int id { get; set; }
        public int user { get; set; }
        public int menu { get; set; }
        public string comment { get; set; }
        public int stars { get; set; }
        BaseDados bd;
        public MenuComment()
        {
            bd = new BaseDados();
        }

        public void Adicionar()
        {
            string sql = "INSERT INTO menu_comments([user],menu,comment,stars) VALUES(@user,@menu,@comment,@stars)";

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
                    ParameterName="@menu",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.menu
                },
                new SqlParameter()
                {
                    ParameterName="@comment",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.comment
                },
                new SqlParameter()
                {
                    ParameterName="@stars",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.stars
                }
            };

            bd.executaSQL(sql, parametros);
        }

        internal static DataTable LoadComments(int id_menu)
        {
            BaseDados bd = new BaseDados();

            string sql = $"SELECT menu_comments.id,menu_comments.comment,menu_comments.stars,menu_comments.CreateDate, users.name FROM menu_comments JOIN users ON menu_comments.[user] = users.id WHERE menu_comments.menu = {id_menu} ORDER BY menu_comments.CreateDate DESC";


            // comment
            // stars
            // name
            // CreateDate

            return bd.devolveSQL(sql);
        }

        internal static void DeleteComment(int num_id_comentario)
        {
            BaseDados bd = new BaseDados();

            string sql = $"DELETE FROM menu_comments WHERE id = {num_id_comentario}";

            bd.executaSQL(sql);
        }
    }
}