using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using SAF.Models;

namespace SAF.DAL
{
    class TemaDAL
    {
        string c = "datasource=127.0.0.1;port=3306;username=root;password=Martinez.961116;database=safdb;";
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();

        public void agregar(int idClase, Tema tema)
        {
            conn.ConnectionString = c;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "agregarTema";
            cmd.Connection = conn;
            MySqlDataReader myReader;

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("pidClase", idClase);
                cmd.Parameters.AddWithValue("pnombre", tema.nombre);
                myReader = cmd.ExecuteReader();
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

        public List<Tema> cargar(int id)
        {
            MySqlDataReader reader;
            List<Tema> lista = new List<Tema>();

            conn.ConnectionString = c;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarTema";
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("pidClase", id);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tema tema = new Tema()
                        {
                            idTema = int.Parse(reader.GetString(0)),
                            nombre = reader.GetString(1)
                        };

                        lista.Add(tema);
                    }
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
