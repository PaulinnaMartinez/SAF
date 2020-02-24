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
    class GrupoDAL
    {
        string c = "datasource=127.0.0.1;port=3306;username=root;password=Martinez.961116;database=safdb;";
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        ClaseDAL cd = new ClaseDAL();
        
        public void agregar(Grupo grupo, Usuario user, int idc, List<int> idt)
        {
            try
            {
                string fecha = string.Format("{0:yyyy-MM-dd}", grupo.fecha);
                string hora = string.Format("{0:HH:mm:ss}", grupo.hora);
                string maestro = user.nombre + " " + user.apellido;

                conn.ConnectionString = c;
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.CommandText = "agregarGrupo";
                cmd.Connection = conn;
                MySqlDataReader myReader;

                cmd.Parameters.AddWithValue("facultad",grupo.facultad);
                cmd.Parameters.AddWithValue("salon", grupo.salon);
                cmd.Parameters.AddWithValue("fecha", grupo.fecha);
                cmd.Parameters.AddWithValue("costo", grupo.costo);
                cmd.Parameters.AddWithValue("maestro", maestro);
                cmd.Parameters.AddWithValue("activo", grupo.activo);
                cmd.Parameters.AddWithValue("hora", grupo.hora);
                cmd.Parameters.AddWithValue("pmatricula", user.matricula);
                cmd.Parameters.AddWithValue("idClase", idc);


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

                try
                {
                    agregarTema(idt);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void agregarTema(List<int> idt)
        {

            int idg = obtenerId();
            try
            {
               
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.Text;
                
                for (int i = 0; i < idt.Count(); i++)
                {
                    cmd.CommandText = "INSERT INTO grupo_tema VALUES (" + idg + ", " + idt[i] + ")";
                    cmd.Connection = conn;
                    MySqlDataReader myReader;
                    conn.Open();
                    myReader = cmd.ExecuteReader();
                    conn.Close();
                }
                
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


        public void editar (Grupo grupo)
        {
            DateTime d = DateTime.Parse(grupo.fecha);
            string fecha = string.Format("{0:yyyy-MM-dd}", d);
            string hora = string.Format("{0:HH:mm:ss}", grupo.hora);

            try
            {
                //conn.Close();
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE grupo SET facultad = '" + grupo.facultad + "', salon= '" + grupo.salon + "', fecha= '" + fecha +
                    "', costo= " + grupo.costo + ", activo = '" + grupo.activo + "', hora = '" + hora + "' WHERE idGrupo = " + grupo.idGrupo + "";
                cmd.Connection = conn;
                MySqlDataReader myReader;
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

        public List<Grupo> cargar(Usuario user, string modo)
        {
            MySqlDataReader reader;
            List<Grupo> lista = new List<Grupo>();

            try
            {
                conn.ConnectionString = c;
               
                cmd.CommandType = CommandType.StoredProcedure;

                if (modo == "M")
                {
                    cmd.CommandText = "cargarGrupo";
                    cmd.Connection = conn;

                    conn.Open();
                    cmd.Parameters.AddWithValue("mat", user.matricula);

                    reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            string f = reader.GetString(5);
                            f = f.Remove(10, 15);
                            
                            Grupo grupos = new Grupo()
                            {
                                idGrupo = int.Parse(reader.GetString(2)),
                                facultad = reader.GetString(3),
                                salon = reader.GetString(4),
                                fecha = f,
                                costo = double.Parse(reader.GetString(6)),
                                activo = reader.GetString(8),
                                hora = reader.GetString(9),
                                clase = new Clase()
                                {
                                    nombre = reader.GetString(12)
                                }
                                
                                

                            };
                            
                            lista.Add(grupos);
                        }
                    }
                    conn.Close();


                    

                    return lista;
                }
                else
                {
                    cmd.CommandText = "cargarGrupoAlumnos";
                    cmd.Connection = conn;

                    conn.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string f = reader.GetString(3);
                            f = f.Remove(10, 15);
                            
                            Grupo grupos = new Grupo()
                            {
                                idGrupo = int.Parse(reader.GetString(0)),
                                facultad = reader.GetString(1),
                                salon = reader.GetString(2),
                                fecha = f,
                                costo = double.Parse(reader.GetString(4)),
                                maestro = reader.GetString(5),
                                hora = reader.GetString(7),
                                clase = new Clase()
                                {
                                    idClase = int.Parse(reader.GetString(8)),
                                    nombre = reader.GetString(10)
                                },
                                usuario = new Usuario()
                                {
                                    matricula = int.Parse(reader.GetString(12))
                                }
                            };


                            lista.Add(grupos);
                        }
                    }

                    return lista;
                }

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

        public Grupo cargarGrupo(int id)
        {
            MySqlDataReader reader;

            try
            {
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM grupo WHERE idGrupo =" + id + "";
                cmd.Connection = conn;

                conn.Open();
                reader = cmd.ExecuteReader();

                Grupo grupos = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string f = reader.GetString(3);
                        f = f.Remove(10, 15);

                         grupos = new Grupo()
                        {
                             idGrupo = int.Parse(reader.GetString(0)),
                             facultad = reader.GetString(1),
                             salon = reader.GetString(2),
                             fecha = f,
                             costo = double.Parse(reader.GetString(4)),
                             maestro = reader.GetString(5),
                             activo = reader.GetString(6),
                             hora = reader.GetString(7)
                         };


                        return grupos;
                    }
                }

                return grupos;
                
                    
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

        public int obtenerId()
        {
            MySqlDataReader reader;
            int id = 0;


            try
            {
                conn.ConnectionString = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT idGrupo FROM grupo ORDER BY idGrupo DESC LIMIT 1";
                cmd.Connection = conn;

                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = int.Parse(reader.GetString(0));
                    };
                }

                return id;
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
