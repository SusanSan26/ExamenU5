//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFerMex.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class dirEntrega
    {
        public int ID_DIRENTREGA { get; set; }
        public string CALLE { get; set; }
        public string COLONIA { get; set; }
        public string ESTADO { get; set; }
        public string CODIGO_POSTAL { get; set; }
        public string TELEFONO { get; set; }
        public Nullable<int> ID_CLIENTE { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
