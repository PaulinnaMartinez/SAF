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
    class UsuarioDAL
    {
        string c = "datasource=127.0.0.1;port=3306;username=root;password=Martinez.961116;database=safdb;";
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();

        public int agregar(Usuario usuario)
        {
            conn.ConnectionString = c;
            cmd.CommandType = CommandType.StoredProcedure;  cmd.CommandText = "agregarUsuario";
            cmd.Connection = conn;
            MySqlDataReader myReader;
            int matricula =0;

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("pnombre",usuario.nombre);
                cmd.Parameters.AddWithValue("papellido",usuario.apellido);
                cmd.Parameters.AddWithValue("pcorreo",usuario.correo);
                cmd.Parameters.AddWithValue("pcelular",usuario.celular);
                cmd.Parameters.AddWithValue("ppassword",usuario.password);
                cmd.Parameters.AddWithValue("pmodalidad",usuario.modalidad);
                

                myReader = cmd.ExecuteReader();

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        matricula = int.Parse(myReader.GetString(0));
                    };

                }

                return matricula;
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

        public void editar(Usuario usuario)
        {
            conn.ConnectionString = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE usuario SET nombre = '" + usuario.nombre + "', apellido  = '" + usuario.apellido + "' , correo = '" + usuario.correo + 
                "', password = '" + usuario.password + "', celular = '" + usuario.celular + "', modalidad = '" + usuario.modalidad + "' WHERE matricula = "+ usuario.matricula +""; 
               
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

        public Usuario cargarUsuario(int matricula)
        {
            conn.ConnectionString = c;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM usuario WHERE matricula = " + matricula + "";
            cmd.Connection = conn;
            MySqlDataReader myReader;

            try
            {
                conn.Open();
                myReader = cmd.ExecuteReader();
                Usuario user = null;

                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        user = new Usuario()
                        {
                            matricula = int.Parse(myReader.GetString(0)),
                            nombre = myReader.GetString(1),
                            apellido = myReader.GetString(2),
                            correo = myReader.GetString(3),
                            celular = myReader.GetString(4),
                            password = myReader.GetString(5),
                            modalidad = myReader.GetString(6)
                        };
                    }
                }

                return user;
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
