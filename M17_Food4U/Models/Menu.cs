using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class Menu
    {
        public int id { get; set; }
        public int restaurant { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int stars { get; set; }
        public bool stock { get; set; }
        public bool enabled { get; set; }

        public static DataTable ListarMenusDiponíveis()
        {
            BaseDados bd = new BaseDados();
            DataTable dados = bd.devolveSQL("SELECT menus.id,menus.title,menus.description,menus.price,menus.stars,menus.stock, restaurants.name as restaurant FROM menus JOIN restaurants ON menus.restaurant = restaurants.id  WHERE menus.enabled = 1 AND restaurants.enabled = 1");
            return dados;
        }
    }
}