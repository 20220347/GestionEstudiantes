using System;
using System.Collections.Generic;

namespace GestionEstudiantes.Models
{
    // Clase base abstracta — no se instancia directo
    public abstract class Estudiante
    {
        public string Matricula { get; set; }
        public string Nombre    { get; set; }
        public string Apellido  { get; set; }

        private List<Calificacion> _notas = new List<Calificacion>();

        protected Estudiante(string matricula, string nombre, string apellido)
        {
            Matricula = matricula;
            Nombre    = nombre;
            Apellido  = apellido;
        }

        public Resultado AgregarNota(Calificacion c)
        {
            if (c.Nota < 0 || c.Nota > 100)
                return Resultado.Fail("La nota debe estar entre 0 y 100.");

            _notas.Add(c);
            return Resultado.Ok("Nota registrada.");
        }

        public List<Calificacion> GetNotas() => _notas;

        public double Promedio()
        {
            if (_notas.Count == 0) return 0;
            double total = 0;
            foreach (var n in _notas) total += n.Nota;
            return Math.Round(total / _notas.Count, 2);
        }

        public bool Aprobado() => Promedio() >= 70;

        // Cada subclase muestra su info diferente (polimorfismo)
        public virtual string Info() =>
            $"[{Matricula}] {Nombre} {Apellido}";
    }

    // Estudiante que asiste al campus
    public class Presencial : Estudiante
    {
        public string Aula { get; set; }

        public Presencial(string matricula, string nombre, string apellido, string aula)
            : base(matricula, nombre, apellido)
        {
            Aula = aula;
        }

        public override string Info() =>
            $"{base.Info()} | Presencial | Aula: {Aula}";
    }

    // Estudiante que lleva la materia en línea
    public class Distancia : Estudiante
    {
        public string Plataforma { get; set; }

        public Distancia(string matricula, string nombre, string apellido, string plataforma)
            : base(matricula, nombre, apellido)
        {
            Plataforma = plataforma;
        }

        public override string Info() =>
            $"{base.Info()} | Distancia | Plataforma: {Plataforma}";
    }
}
