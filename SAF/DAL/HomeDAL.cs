using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using SAF.Models;

namespace SAF.DAL
{
    public class HomeDAL
    {
        string c = "datasource=127.0.0.1;port=3306;username=root;password=Martinez.961116;database=safdb;";
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();

        public Usuario verificar(Home home)
        {
            Usuario user= null;

            conn.ConnectionString = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT matricula,nombre,apellido,modalidad FROM usuario WHERE matricula = '" + home.matricula + "' AND password = '" + home.password + "' ";
            cmd.Connection = conn;
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new Usuario()
                        {
                            matricula = int.Parse(reader.GetString(0)),
                            nombre = reader.GetString(1),
                            apellido = reader.GetString(2),
                            modalidad = reader.GetString(3)
                        };
                    }
                    
                }
                
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}