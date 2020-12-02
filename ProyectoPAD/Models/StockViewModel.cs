using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPAD.Models
{
    public class StockViewModel
    {
        public int IdStock { get; set; }
        public int Cantidad { get; set; }
         
        public ProductoViewModel Producto { get; set; }
    }
}