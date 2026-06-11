using System.Collections.Generic;

namespace GestionEstudiantes.Models
{
    public class Docente
    {
        public string Nombre   { get; set; }
        public string Apellido { get; set; }
        public string Cedula   { get; set; }

        private List<Asignatura> _asignaturas = new List<Asignatura>();

        public Docente(string cedula, string nombre, string apellido)
        {
            Cedula   = cedula;
            Nombre   = nombre;
            Apellido = apellido;
        }

        public Resultado AgregarAsignatura(Asignatura a)
        {
            foreach (var x in _asignaturas)
                if (x.Codigo == a.Codigo)
                    return Resultado.Fail("Esa asignatura ya existe.");

            _asignaturas.Add(a);
            return Resultado.Ok($"Asignatura '{a.Nombre}' agregada.");
        }

        public List<Asignatura> GetAsignaturas() => _asignaturas;
    }
}
