using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using System.Data;
using System.Xml.Linq;
using System.Web.UI.WebControls;

namespace WebApi.Controllers
{
    public class ProductosController : ApiController
    {
        public static IList<Producto> listaProducto = new List<Producto>{
         new Producto() { idProducto = 1, Nombre = "Robert", Precio = 10, Stock = 1, idCategoria = 1 },
            new Producto() { idProducto = 2, Nombre = "emilio", Precio = 20, Stock = 1, idCategoria = 2 },
            new Producto() { idProducto = 3, Nombre = "int", Precio = 30, Stock = 1, idCategoria = 3 },
            new Producto() { idProducto =4, Nombre = "ibk", Precio = 40, Stock = 1, idCategoria = 4 }
       
    };

        //services/Productos/Listar
        [HttpGet]
        public IEnumerable<Producto> Listar()
        {
            //return listaProducto;
            IList<Producto> lista = new List<Producto>();
            DataSet ds = cn.ejecutar_select("select p.idproducto,p.nombre,p.precio,p.stock,c.nombre "+
            "from productos p inner join categoria c on c.idcategoria=p.idcategoria");
            Producto prd = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int r = 0; r <= ds.Tables[0].Rows.Count - 1; r++)
                {
                    prd = new Producto();
                    prd.idProducto = int.Parse(ds.Tables[0].Rows[r][0].ToString());
                    prd.Nombre = ds.Tables[0].Rows[r][1].ToString();
                    prd.Precio = decimal.Parse(ds.Tables[0].Rows[r][2].ToString());
                    prd.Stock = int.Parse(ds.Tables[0].Rows[r][3].ToString());
                    prd.NombreCategoria = ds.Tables[0].Rows[r][4].ToString();
                    lista.Add(prd);
                }
            }     
            return lista;
        }
            [HttpPost]
        public IEnumerable<Producto> Buscar(Producto prdo)
                 
        {
            //return listaProducto;
            IList<Producto> lista = new List<Producto>();
            DataSet ds = cn.ejecutar_select("select p.idproducto,p.nombre,p.precio,p.stock,p.idcategoria " +
            "from productos p inner join categoria c on c.idcategoria=p.idcategoria where p.idproducto= " + prdo.idProducto);
            Producto prd = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int r = 0; r <= ds.Tables[0].Rows.Count - 1; r++)
                {
                    prd = new Producto();
                    prd.idProducto = int.Parse(ds.Tables[0].Rows[r][0].ToString());
                    prd.Nombre = ds.Tables[0].Rows[r][1].ToString();
                    prd.Precio = decimal.Parse(ds.Tables[0].Rows[r][2].ToString());
                    prd.Stock = int.Parse(ds.Tables[0].Rows[r][3].ToString());
                    prd.idCategoria =  int.Parse(ds.Tables[0].Rows[r][4].ToString());
                    lista.Add(prd);
                }
            }
            return lista;
        }

        [HttpPost]
        public  HttpResponseMessage Post (Producto prd)
        {  
            string sInsert = "INSERT INTO Productos ( Nombre, Precio,Stock,IdCategoria) Values (";
            string sValues = "'"+prd.Nombre + "'," + prd.Precio + "," + prd.Stock + "," + prd.idCategoria + ")";
           cn.ejecutar_comando(sInsert + sValues);         
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Put(Producto prd)
        {
            string sSql = "UPDATE Productos SET Nombre='" + prd.Nombre + "' ,Precio=" + prd.Precio + " , Stock=" + prd.Stock + " , idcategoria=" + prd.idCategoria;            
            cn.ejecutar_comando(sSql);
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Delete(Producto prd)
        {
            string sSql = "DELETE FROM Productos where IdProducto = " + prd.idProducto;
            cn.ejecutar_comando(sSql);
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            return response;
        }
         [HttpPost]
        public HttpResponseMessage XML(Producto prd)
        {
            XElement xml =
              new XElement("xml",
                new XElement("xml",
                  new XElement("idprodcuto", prd.idProducto),
                  new XElement("Nombre", prd.Nombre),
                  new XElement("Precio", prd.Precio),
                  new XElement("Stock", prd.Stock),
                  new XElement("idcategoria", prd.idCategoria)));
               XDocument contactosXml = new XDocument(xml);
            contactosXml.Save(@"d:\Contenido.xml");
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            return response;
        }

         [HttpGet]
         public IEnumerable<Producto> ListarXML()
         {
             IList<Producto> lista = new List<Producto>();
             //return listaProducto;
             using (DataSet ds = new DataSet())
             {
                 ds.ReadXml(@"d:\Contenido.xml");
                 
                 Producto prd = null;
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     for (int r = 0; r <= ds.Tables[0].Rows.Count - 1; r++)
                     {
                         prd = new Producto();
                         prd.idProducto = int.Parse(ds.Tables[0].Rows[r][0].ToString());
                         prd.Nombre = ds.Tables[0].Rows[r][1].ToString();
                         prd.Precio = decimal.Parse(ds.Tables[0].Rows[r][2].ToString());
                         prd.Stock = int.Parse(ds.Tables[0].Rows[r][3].ToString());
                         prd.NombreCategoria = ds.Tables[0].Rows[r][4].ToString();
                         lista.Add(prd);
                     }
                 }
             }
             return lista;
         }
    }
}
