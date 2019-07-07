using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Messaging;
using System.Xml;
using System.Xml.Serialization;

namespace WCFAPPAgenciaViaje
{
    //IMPLEMENTACION RECOMENDACIONES
    public partial class ConsultarClima : Form
    {
        public ConsultarClima()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Cliente clienteAInsertar = new Cliente();

            clienteAInsertar.Nombre = textBoxNombre.Text;
            clienteAInsertar.Celular = textBoxCelular.Text;
            clienteAInsertar.Fecha = textBoxFecha.Text;
            string postdata = js.Serialize(clienteAInsertar);
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest request = (HttpWebRequest)WebRequest.
                Create("http://localhost:50386/Clientes.svc/Clientes");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string TramaJson = reader.ReadToEnd();
            Cliente medicoCreado = js.Deserialize<Cliente>(TramaJson);
        }

        private void button2_Click(object sender, EventArgs e)
        {


            HttpClient Clima = new HttpClient();
            Clima.BaseAddress = new Uri("http://localhost:50386");
            HttpResponseMessage response2 = Clima.GetAsync("/Climas.svc/Climas/" + textBoxFecha.Text).Result;
            ClimaConsultas emp = response2.Content.ReadAsAsync<ClimaConsultas>().Result;
            List<ClimaConsultas> emplist = new List<ClimaConsultas>();
            emplist.Add(emp);
            dataGridView1.DataSource = emplist;

            System.Messaging.Message msg = new System.Messaging.Message();
            msg.Body = emp;
            MessageQueue msgQ = new MessageQueue(".\\Private$\\in");
            msgQ.Send(msg);

        }
    }
}
