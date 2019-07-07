using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServices.Dominio;
using WCFServices.Errores;
using WCFServices.Persistencia;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Net;

namespace WCFServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Medicos" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Medicos.svc o Medicos.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Clientes : IClientes
    {
        private ClienteDAO clienteDAO = new ClienteDAO();
        public Cliente CrearCliente(Cliente clienteACrear)
        {
            Cliente medicoExistente = clienteDAO.Obtener(clienteACrear.Nombre);
            if (medicoExistente !=null) // Ya existe
            {
                throw new WebFaultException<RepetidoException>(new RepetidoException
                    {
                        Codigo = "102",
                        Descripcion = "Medico duplicado"
                    }, HttpStatusCode.Conflict);
                 
            }
            return clienteDAO.Crear(clienteACrear);
        }
        public Cliente ObtenerCliente(string nombre)
        {
            return clienteDAO.Obtener(nombre);
        }
        public Cliente ModificarCliente(Cliente medicoAModificar)
        {
            return clienteDAO.Modificar(medicoAModificar);

        }
        
        public List<Cliente> ListarClientes()
        {
            return clienteDAO.Listar();
        }
    }
}
