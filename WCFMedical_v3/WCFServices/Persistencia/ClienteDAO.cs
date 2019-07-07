using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFServices.Dominio;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServices.Errores;
using WCFServices.Persistencia;
using System.ServiceModel.Web;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

namespace WCFServices.Persistencia
{
    public class ClienteDAO
    {
        private string CadenaConexion = "Data Source=EDGAR\\SQLEXPRESS; Initial Catalog=DB_AgenciaViajes;Integrated Security=SSPI;";

        public Cliente Crear(Cliente clienteACrear)
        {
            Cliente clienteCreado = null;
            string sql = "INSERT INTO t_clientes VALUES (@nombre, @celular, @fecha)";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@nombre", clienteACrear.Nombre));
                    comando.Parameters.Add(new SqlParameter("@celular", clienteACrear.Celular));
                    comando.Parameters.Add(new SqlParameter("@fecha", clienteACrear.Fecha));
                    comando.ExecuteNonQuery();
                }
            }
            clienteCreado = Obtener(clienteACrear.Nombre);
            return clienteCreado;
        }
        public Cliente Obtener(string nombre)
        {
            Cliente clienteEncontrado = null;
            string sql = "SELECT * FROM t_clientes WHERE tx_nombre=@nombre";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(new SqlParameter("@nombre", nombre));
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        if (resultado.Read())
                        {
                            clienteEncontrado = new Cliente()
                            {
                                Nombre = (string)resultado["tx_nombre"],
                                Celular = (string)resultado["nu_celular"],
                                Fecha = (string)resultado["tx_fecha"]

                            };
                        }
                    }
                }
            }
            return clienteEncontrado;
        }
        public Cliente Modificar(Cliente clienteAModificar)
        {
            Cliente clienteModificado = null;
            string sql = "UPDATE t_clientes SET tx_celular=@celular, tx_fecha=@fecha WHERE tx_nombre=@nombre";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                 
                    comando.Parameters.Add(new SqlParameter("@celular", clienteAModificar.Celular));
                    comando.Parameters.Add(new SqlParameter("@fecha", clienteAModificar.Fecha));
                    comando.ExecuteNonQuery();
                }
            }
            clienteModificado = Obtener(clienteAModificar.Nombre);
            return clienteModificado;
        }
        public List<Cliente> Listar()
        {
            List<Cliente> clienteEncontrados = new List<Cliente>();
            Cliente clienteEncontrado = null;
            string sql = "SELECT * from t_clientes";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    using (SqlDataReader resultado = comando.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            clienteEncontrado = new Cliente()
                            {
                                Nombre = (string)resultado["tx_nombre"],
                                Celular = (string)resultado["nu_celular"],
                                Fecha = (string)resultado["tx_fecha"],
                            };
                            clienteEncontrados.Add(clienteEncontrado);
                        }

                    }
                }
            }
            return clienteEncontrados;
              
        }

    }
}