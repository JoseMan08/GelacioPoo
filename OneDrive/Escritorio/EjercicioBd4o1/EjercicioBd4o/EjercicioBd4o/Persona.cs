using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EjercicioBd4o
{
    public class Persona
    {

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }


        public Persona() { }

        public Persona(string nombre, string apellidos, string direccion, string telefono)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}
