using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjercicioBd4o
{
    public class Cliente : Persona
    {
        public string Id_C { get; set; }
        public string RFC { get; set; }

        public Cliente() { }

        public Cliente(string id_C, string rfc, string nombre, string apellidos, string direccion, string telefono)
            : base(nombre, apellidos, direccion, telefono)
        {
            Id_C = id_C;
            RFC = rfc;
        }
    }
}
