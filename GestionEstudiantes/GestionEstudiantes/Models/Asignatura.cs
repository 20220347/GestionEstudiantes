using System.Collections.Generic;

namespace GestionEstudiantes.Models
{
    public class Asignatura
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        private List<Grupo> _grupos = new List<Grupo>();

        public Asignatura(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }

        public Resultado AgregarGrupo(Grupo g)
        {
            foreach (var x in _grupos)
                if (x.Codigo == g.Codigo)
                    return Resultado.Fail("Ese código de grupo ya existe.");

            _grupos.Add(g);
            return Resultado.Ok($"Grupo '{g.Nombre}' agregado.");
        }

        public List<Grupo> GetGrupos() => _grupos;

        public override string ToString() => $"[{Codigo}] {Nombre}";
    }
}
