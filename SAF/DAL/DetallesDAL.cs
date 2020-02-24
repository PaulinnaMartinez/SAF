using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace SAF.DAL
{
    public class DetallesDAL
    {
        string c = "datasource=127.0.0.1;port=3306;username=root;password=Martinez.961116;database=safdb;";
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();


        public List<String> cargarTema(int id)
        {
            try
            {
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cargarTemasD";

                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("pidGrupo", id);

                MySqlDataReader reader;
                List<String> lista = new List<string>();

                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    string nombre;
                    while (reader.Read())
                    {
                        nombre = reader.GetString(0);
                        lista.Add(nombre);
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


        public string cargarModalidad(int matricula)
        {
            string nombre = "";

            try
            {
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cargarModalidadD";
                cmd.Connection = conn;
                MySqlDataReader reader;


                conn.Open();
                cmd.Parameters.AddWithValue("pmatricula", matricula);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nombre = reader.GetString(0);
                    }
                }

                return nombre;
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

        public string cargarDescripcion (int id)
        {
            string descripcion = "";

            try
            {
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cargarDescripcionD";
                cmd.Connection = conn;
                MySqlDataReader reader;


                conn.Open();
                cmd.Parameters.AddWithValue("pidClase", id);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        descripcion = reader.GetString(0);
                    }
                }

                return descripcion;
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