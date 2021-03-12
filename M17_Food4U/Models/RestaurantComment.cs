using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class RestaurantComment
    {
        public int id { get; set; }
        public int user { get; set; }
        public int restaurant { get; set; }
        public string comment { get; set; }
        public int stars { get; set; }
        BaseDados bd;
        public RestaurantComment()
        {
            bd = new BaseDados();
        }

        public void Adicionar()
        {
            string sql = "INSERT INTO restaurant_comments([user],restaurant,comment,stars) VALUES(@user,@restaurant,@comment,@stars)";

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
                    ParameterName="@restaurant",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.restaurant
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

        internal static DataTable LoadComments(int id_restaurant)
        {
            BaseDados bd = new BaseDados();

            string sql = $"SELECT restaurant_comments.comment,restaurant_comments.stars,restaurant_comments.CreateDate, users.name FROM restaurant_comments JOIN users ON restaurant_comments.[user] = users.id WHERE restaurant_comments.restaurant = {id_restaurant} ORDER BY restaurant_comments.CreateDate DESC";


            // comment
            // stars
            // name
            // CreateDate

            return bd.devolveSQL(sql);
        }
    }
}