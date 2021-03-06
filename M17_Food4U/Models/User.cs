﻿using M17_Food4U.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17_Food4U.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string nif { get; set; }
        public string password { get; set; }
        public int estado { get; set; }
        public int perfil { get; set; }
        public DateTime data_nasc { get; set; }
        public double saldo { get; set; }
        public string driving_license { get; set; }
        public DateTime driving_licenseValidity { get; set; }
        public DateTime createDate { get; set; }

        internal void atualizarPassword(string guid, string novaPassword)
        {
            string sql = "UPDATE users SET password=HASHBYTES('SHA2_512',@password),lnkRecuperar=null, data_lnk=NULL WHERE lnkRecuperar=@lnk";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@password",SqlDbType=SqlDbType.VarChar,Value=novaPassword},
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            bd.executaSQL(sql, parametros);
        }

        BaseDados bd;

        internal double getSaldo()
        {
            double saldo = 0;
            string sql = "SELECT saldo FROM users WHERE id = " + id;
            DataTable dados = bd.devolveSQL(sql);
            if (dados == null || dados.Rows.Count <= 0)
                return saldo;

            return double.Parse(dados.Rows[0]["saldo"].ToString());
        }

        public User()
        {
            bd = new BaseDados();
        }
        public User(int id)
        {
            bd = new BaseDados();
            this.id = id;
        }

        internal static User GetUser(int id_user)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT * FROM users WHERE id = {id_user}";

            DataTable dados = bd.devolveSQL(sql);

            User user = new User();

            user.email = dados.Rows[0]["email"].ToString();
            user.nome = dados.Rows[0]["name"].ToString();
            user.nif = dados.Rows[0]["nif"].ToString();
            user.estado = int.Parse(dados.Rows[0]["estado"].ToString());
            user.perfil = int.Parse(dados.Rows[0]["perfil"].ToString());
            user.data_nasc = DateTime.Parse(dados.Rows[0]["data_nasc"].ToString());
            user.saldo = double.Parse(dados.Rows[0]["saldo"].ToString());
            user.driving_license = dados.Rows[0]["drivingLicense"].ToString();
            if(dados.Rows[0]["drivingLicenseValidity"].ToString() != "")
                user.driving_licenseValidity = DateTime.Parse(dados.Rows[0]["drivingLicenseValidity"].ToString());
            user.createDate = DateTime.Parse(dados.Rows[0]["createDate"].ToString());

            return user;
        }

        internal static DataTable GetUser(string email)
        {
            BaseDados bd = new BaseDados();

            string sql = $@"SELECT * FROM users WHERE email = @email";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
            };

            return bd.devolveSQL(sql,parametros);
        }

        public void recuperarPassword(string email, string guid)
        {
            string sql = "UPDATE users SET lnkRecuperar=@lnk, data_lnk = getdate() WHERE email=@email";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@email",SqlDbType=SqlDbType.VarChar,Value=email },
                new SqlParameter() {ParameterName="@lnk",SqlDbType=SqlDbType.VarChar,Value=guid },
            };
            bd.executaSQL(sql, parametros);
        }

        internal static void MudarPerfil(int id_user, int value)
        {
            BaseDados bd = new BaseDados();
            string sql = $"UPDATE users SET perfil = {value} WHERE id = {id_user}";

            bd.executaSQL(sql);

        }

        internal List<Address> getMoradas()
        {
            string sql = "SELECT TOP 3 * FROM addresses WHERE [user] = " + id;
            DataTable dados = bd.devolveSQL(sql);
            List<Address> moradas = new List<Address>();
            foreach (DataRow row in dados.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                int user = int.Parse(row["user"].ToString());
                string city = row["city"].ToString();
                string cp = row["cp"].ToString();
                string address = row["address"].ToString();
                Address address1 = new Address(id,user, city, cp, address);

                moradas.Add(address1);
            }
            return moradas;
        }

        public void Adicionar()
        {
            string sql = @"INSERT INTO users(email,name,nif,password,estado,perfil,data_nasc,saldo,createDate)
                            VALUES (@email,@name,@nif,HASHBYTES('SHA2_512',@password),@estado,@perfil,@data_nasc,@saldo,@createDate)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter()
                {
                    ParameterName="@name",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },
                new SqlParameter()
                {
                    ParameterName="@nif",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nif
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.password
                },
                new SqlParameter()
                {
                    ParameterName="@estado",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=1
                },
                new SqlParameter()
                {
                    ParameterName="@perfil",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.perfil
                },
                new SqlParameter()
                {
                    ParameterName="@data_nasc",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_nasc
                },
                new SqlParameter()
                {
                    ParameterName="@saldo",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=0
                },
                 new SqlParameter()
                {
                    ParameterName="@createDate",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=DateTime.Now
                },
            };
            bd.executaSQL(sql, parametros);
        }

        internal static void ToggleUtilizador(int id_utilizador, int value)
        {
            BaseDados bd = new BaseDados();
            
            string sql = $"UPDATE users SET estado = {value} WHERE id = {id_utilizador}";
            bd.executaSQL(sql);
        }

        internal void Atualizar()
        {
            string sql = "";

            if (this.password == "")
                sql = "UPDATE users SET email = @email, name = @name, nif = @nif, data_nasc = @data_nasc WHERE id = @id_user";
            else
                sql = "UPDATE users SET email = @email, name = @name, nif = @nif, data_nasc = @data_nasc, password = HASHBYTES('SHA2_512',@password) WHERE id = @id_user";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter()
                {
                    ParameterName="@name",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },
                new SqlParameter()
                {
                    ParameterName="@nif",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nif
                },
                new SqlParameter()
                {
                    ParameterName="@data_nasc",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_nasc
                },
                new SqlParameter()
                {
                    ParameterName="@id_user",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.id
                }
            };
            if (this.password != "")
                parametros.Add(new SqlParameter()
                {
                    ParameterName = "@password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = this.password
                });

            bd.executaSQL(sql, parametros);
        }

        internal static void DeleteUser(int num_id_utilizador)
        {
            BaseDados bd = new BaseDados();

            string sql = $"DELETE FROM users WHERE id = {num_id_utilizador}";

            bd.executaSQL(sql);
        }

        public void AdicionarEstafeta()
        {
            string sql = @"INSERT INTO users(email,name,nif,password,estado,perfil,data_nasc,saldo,drivingLicense,drivingLicenseValidity,createDate)
                            VALUES (@email,@name,@nif,HASHBYTES('SHA2_512',@password),@estado,@perfil,@data_nasc,@saldo,@drivingLicense,@drivingLicenseValidity,@createDate)";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter()
                {
                    ParameterName="@name",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nome
                },
                new SqlParameter()
                {
                    ParameterName="@nif",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.nif
                },
                new SqlParameter()
                {
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.password
                },
                new SqlParameter()
                {
                    ParameterName="@estado",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=1
                },
                new SqlParameter()
                {
                    ParameterName="@perfil",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.perfil
                },
                new SqlParameter()
                {
                    ParameterName="@data_nasc",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_nasc
                },
                new SqlParameter()
                {
                    ParameterName="@saldo",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=0
                },
                new SqlParameter()
                {
                    ParameterName="@drivingLicense",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.driving_license
                },
                new SqlParameter()
                {
                    ParameterName="@drivingLicenseValidity",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.driving_licenseValidity
                },
                new SqlParameter()
                {
                    ParameterName="@createDate",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=DateTime.Now
                },
            };
            bd.executaSQL(sql, parametros);
        }

        static public DataTable ListarUtilizadores()
        {
            BaseDados bd = new BaseDados();
            string sql = "SELECT id,email,name,nif,data_nasc,saldo,estado,perfil FROM users";

            DataTable dados = bd.devolveSQL(sql);
            return dados;
        }

        public bool IsAddressFromUser(int address)
        {
            string sql = $"SELECT id FROM addresses WHERE id = {address} AND [user] = {id}";

            DataTable dados = bd.devolveSQL(sql);

            if (dados == null || dados.Rows.Count == 0 || dados.Rows.Count > 1)
                return false;
            return true;
        }
    }
}