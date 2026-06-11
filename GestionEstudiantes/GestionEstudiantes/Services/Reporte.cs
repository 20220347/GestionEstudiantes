using System;
using GestionEstudiantes.Models;
using GestionEstudiantes.UI;

namespace GestionEstudiantes.Services
{
    public static class Reporte
    {
        // Muestra todas las notas de cada estudiante en un grupo
        public static void MostrarGrupo(Grupo grupo)
        {
            Consola.Titulo($"REPORTE — {grupo.Nombre}");

            var estudiantes = grupo.GetEstudiantes();

            if (estudiantes.Length == 0)
            {
                Console.WriteLine("  No hay estudiantes en este grupo.");
                return;
            }

            foreach (var e in estudiantes)
            {
                Console.WriteLine("\n  " + e.Info());
                Console.WriteLine("  " + new string('-', 45));

                var notas = e.GetNotas();
                if (notas.Count == 0)
                {
                    Console.WriteLine("  Sin notas registradas.");
                }
                else
                {
                    foreach (var n in notas)
                        Console.WriteLine(n);
                }

                Console.ForegroundColor = e.Aprobado() ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"\n  Promedio: {e.Promedio():F2}  —  {(e.Aprobado() ? "APROBADO" : "REPROBADO")}");
                Console.ResetColor();
            }

            Console.WriteLine("\n" + new string('-', 55));
            Console.WriteLine($"  Aprobados: {grupo.PorcentajeAprobados()}%  |  Total: {grupo.Total} estudiante(s)");
        }
    }
}
