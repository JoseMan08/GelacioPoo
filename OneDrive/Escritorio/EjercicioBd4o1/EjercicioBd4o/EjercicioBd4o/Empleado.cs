using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EjercicioBd4o
{
    public class Empleado: Persona
    {
        public string Id_E { get; set; }
        public string Puesto { get; set; }

        public Empleado() { }

        public Empleado(string id_E, string puesto)
        {
            Id_E = id_E;
            Puesto = puesto;

        }
    }

}
