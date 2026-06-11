using System;
using GestionEstudiantes.Models;
using GestionEstudiantes.Services;
using GestionEstudiantes.UI;

namespace GestionEstudiantes
{
    class Program
    {
        static Docente docente;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Registro del docente
            Consola.Titulo("GESTIÓN DE ESTUDIANTES POR ASIGNATURA");
            string cedula   = Consola.Leer("Cédula");
            string nombre   = Consola.Leer("Nombre");
            string apellido = Consola.Leer("Apellido");
            docente = new Docente(cedula, nombre, apellido);
            Consola.Ok($"Bienvenido, {nombre} {apellido}");

            MenuPrincipal();
        }

        // ─── MENÚ PRINCIPAL ───────────────────────────────────────────────────

        static void MenuPrincipal()
        {
            while (true)
            {
                Consola.Titulo($"MENÚ PRINCIPAL — {docente.Nombre} {docente.Apellido}");
                Console.WriteLine("  [1] Agregar asignatura");
                Console.WriteLine("  [2] Agregar grupo");
                Console.WriteLine("  [3] Agregar estudiante");
                Console.WriteLine("  [4] Registrar nota");
                Console.WriteLine("  [5] Ver reporte de grupo");
                Console.WriteLine("  [0] Salir");

                int op = Consola.LeerInt("Opción", 0, 5);

                switch (op)
                {
                    case 1: AgregarAsignatura(); break;
                    case 2: AgregarGrupo();      break;
                    case 3: AgregarEstudiante(); break;
                    case 4: RegistrarNota();     break;
                    case 5: VerReporte();        break;
                    case 0: return;
                }
            }
        }

        // ─── ACCIONES ─────────────────────────────────────────────────────────

        static void AgregarAsignatura()
        {
            Consola.Titulo("NUEVA ASIGNATURA");
            string codigo = Consola.Leer("Código (ej. MAT101)");
            string nombre = Consola.Leer("Nombre");

            var res = docente.AgregarAsignatura(new Asignatura(codigo, nombre));
            if (res.Success) Consola.Ok(res.Message);
            else Consola.Error(res.Message);
            Consola.Pausar();
        }

        static void AgregarGrupo()
        {
            Consola.Titulo("NUEVO GRUPO");
            var asig = ElegirAsignatura();
            if (asig == null) return;

            string codigo = Consola.Leer("Código del grupo (ej. G01)");
            string nombre = Consola.Leer("Nombre (ej. Grupo 1 Mañana)");

            var res = asig.AgregarGrupo(new Grupo(codigo, nombre));
            if (res.Success) Consola.Ok(res.Message);
            else Consola.Error(res.Message);
            Consola.Pausar();
        }

        static void AgregarEstudiante()
        {
            Consola.Titulo("NUEVO ESTUDIANTE");
            var asig = ElegirAsignatura();
            if (asig == null) return;

            var grupo = ElegirGrupo(asig);
            if (grupo == null) return;

            string matricula = Consola.Leer("Matrícula");
            string nombre    = Consola.Leer("Nombre");
            string apellido  = Consola.Leer("Apellido");

            Console.WriteLine("  [1] Presencial  [2] A Distancia");
            int tipo = Consola.LeerInt("Tipo", 1, 2);

            Estudiante est;
            if (tipo == 1)
            {
                string aula = Consola.Leer("Aula");
                est = new Presencial(matricula, nombre, apellido, aula);
            }
            else
            {
                string plataforma = Consola.Leer("Plataforma (Moodle, Teams...)");
                est = new Distancia(matricula, nombre, apellido, plataforma);
            }

            var res = grupo.Agregar(est);
            if (res.Success) Consola.Ok(res.Message);
            else Consola.Error(res.Message);
            Consola.Pausar();
        }

        static void RegistrarNota()
        {
            Consola.Titulo("REGISTRAR NOTA");
            var asig = ElegirAsignatura();
            if (asig == null) return;

            var grupo = ElegirGrupo(asig);
            if (grupo == null) return;

            string matricula = Consola.Leer("Matrícula del estudiante");
            var busqueda = grupo.Buscar(matricula);
            if (!busqueda.Success) { Consola.Error(busqueda.Message); Consola.Pausar(); return; }

            Estudiante est = busqueda.Data;
            Consola.Ok("Estudiante: " + est.Info());

            Console.WriteLine("  [1] Examen  [2] Práctica");
            int tipo = Consola.LeerInt("Tipo", 1, 2);
            TipoEval tipoEval = tipo == 1 ? TipoEval.Examen : TipoEval.Practica;

            string desc = Consola.Leer("Descripción (ej. Parcial 1)");
            double nota = Consola.LeerDouble("Nota");

            var res = est.AgregarNota(new Calificacion(desc, nota, tipoEval));
            if (res.Success) Consola.Ok(res.Message);
            else Consola.Error(res.Message);
            Consola.Pausar();
        }

        static void VerReporte()
        {
            var asig = ElegirAsignatura();
            if (asig == null) return;

            var grupo = ElegirGrupo(asig);
            if (grupo == null) return;

            Reporte.MostrarGrupo(grupo);
            Consola.Pausar();
        }

        // ─── HELPERS ──────────────────────────────────────────────────────────

        static Asignatura ElegirAsignatura()
        {
            var lista = docente.GetAsignaturas();
            if (lista.Count == 0)
            {
                Consola.Error("No hay asignaturas. Agréguela primero.");
                Consola.Pausar();
                return null;
            }

            Console.WriteLine("\n  Asignaturas:");
            for (int i = 0; i < lista.Count; i++)
                Console.WriteLine($"  [{i + 1}] {lista[i]}");

            int sel = Consola.LeerInt("Elegir", 1, lista.Count);
            return lista[sel - 1];
        }

        static Grupo ElegirGrupo(Asignatura asig)
        {
            var grupos = asig.GetGrupos();
            if (grupos.Count == 0)
            {
                Consola.Error("Esta asignatura no tiene grupos todavía.");
                Consola.Pausar();
                return null;
            }

            Console.WriteLine("\n  Grupos:");
            for (int i = 0; i < grupos.Count; i++)
                Console.WriteLine($"  [{i + 1}] {grupos[i].Nombre} ({grupos[i].Total} estudiante(s))");

            int sel = Consola.LeerInt("Elegir", 1, grupos.Count);
            return grupos[sel - 1];
        }
    }
}
