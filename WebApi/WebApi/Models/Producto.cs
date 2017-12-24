using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Http.Tracing;
using System.Net.Http;
using System.Text;

namespace WebApi.Models
{
   public class Producto//:ITraceWriter
    {
        public int idProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int idCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }

}