using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPAD.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public float Precio { get; set; }
        public ColorViewModel Color { get; set; }
        public TalleViewModel Talle { get; set; }
        public MarcaViewModel Marca { get; set; }
    }
}