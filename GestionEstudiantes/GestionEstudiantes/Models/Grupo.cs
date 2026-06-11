using System;

namespace GestionEstudiantes.Models
{
    // Almacena estudiantes en un array fijo (sin base de datos)
    public class Grupo
    {
        private Estudiante[] _lista = new Estudiante[50];
        private int _total = 0;

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Total => _total;

        public Grupo(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
        }

        public Resultado Agregar(Estudiante e)
        {
            if (_total >= 50)
                return Resultado.Fail("El grupo está lleno.");

            for (int i = 0; i < _total; i++)
                if (_lista[i].Matricula == e.Matricula)
                    return Resultado.Fail("Esa matrícula ya existe en el grupo.");

            _lista[_total++] = e;
            return Resultado.Ok($"{e.Nombre} agregado al grupo.");
        }

        public Resultado Buscar(string matricula)
        {
            for (int i = 0; i < _total; i++)
                if (_lista[i].Matricula == matricula)
                    return Resultado.Ok("Encontrado.", _lista[i]);

            return Resultado.Fail("Matrícula no encontrada.");
        }

        public Estudiante[] GetEstudiantes()
        {
            var copia = new Estudiante[_total];
            Array.Copy(_lista, copia, _total);
            return copia;
        }

        public double PorcentajeAprobados()
        {
            if (_total == 0) return 0;
            int aprobados = 0;
            for (int i = 0; i < _total; i++)
                if (_lista[i].Aprobado()) aprobados++;
            return Math.Round((double)aprobados / _total * 100, 1);
        }
    }
}
