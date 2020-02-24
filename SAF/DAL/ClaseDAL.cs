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
    class ClaseDAL
    {
        string c = "datasource=127.0.0.1;port=3306;username=root;password=Martinez.961116;database=safdb;";
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();

        public void agregar(Clase clase)
        {
            conn.ConnectionString = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO clase (nombre, descripcion) VALUES ('" + clase.nombre + "', '" + clase.descripcion + "')";
            cmd.Connection = conn;
            MySqlDataReader myReader;

            try
            {
                conn.Open();
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

        public List<Clase> cargar()
        {
            MySqlDataReader reader;
            List<Clase> lista = new List<Clase>();

            conn.ConnectionString = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM clase";
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Clase clase = new Clase()
                        {
                            idClase = int.Parse(reader.GetString(0)),
                            nombre = reader.GetString(1),
                            descripcion = reader.GetString(2)
                        };

                        lista.Add(clase);
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

        public Clase consultaGrupo (int id)
        {
            try
            {
                Clase clase = null;
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "consultaGrupo";
                cmd.Connection = conn;
                MySqlDataReader reader;

                cmd.Parameters.AddWithValue("id", id);
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         clase = new Clase()
                        {
                            idClase = int.Parse(reader.GetString(0)),
                            nombre = reader.GetString(1)
                        };
                    }
                }
                return clase;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Clone();
            }
            
        }
    }
}

