//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gustov.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class registro_ventas
    {
        public int id_ventas { get; set; }
        public int id_plato { get; set; }
        public string nombre_plato { get; set; }
        public Nullable<int> cantidad_plato { get; set; }
        public Nullable<decimal> precio_actual { get; set; }
        public Nullable<System.DateTime> fecha_venta { get; set; }
        public string descripcion_venta { get; set; }
    }
}
