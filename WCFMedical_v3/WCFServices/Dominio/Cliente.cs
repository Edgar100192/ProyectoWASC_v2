using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using WCFServices.Dominio;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace WCFServices.Dominio
{
    [DataContract]
    public class Cliente
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Celular { get; set; }
        [DataMember]
        public string Fecha { get; set; }
   
    }
}