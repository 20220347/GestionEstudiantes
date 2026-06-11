using System;

namespace GestionEstudiantes.UI
{
    // Métodos para mostrar cosas en consola y leer datos sin errores
    public static class Consola
    {
        public static void Titulo(string texto)
        {
            Console.WriteLine("\n" + new string('=', 55));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  " + texto);
            Console.ResetColor();
            Console.WriteLine(new string('=', 55));
        }

        public static void Ok(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  [OK] " + msg);
            Console.ResetColor();
        }

        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [!] " + msg);
            Console.ResetColor();
        }

        public static string Leer(string label)
        {
            while (true)
            {
                Console.Write("  " + label + ": ");
                string val = Console.ReadLine()?.Trim() ?? "";
                if (val != "") return val;
                Error("No puede estar vacío.");
            }
        }

        public static int LeerInt(string label, int min, int max)
        {
            while (true)
            {
                Console.Write("  " + label + ": ");
                if (int.TryParse(Console.ReadLine(), out int n) && n >= min && n <= max)
                    return n;
                Error($"Ingrese un número entre {min} y {max}.");
            }
        }

        public static double LeerDouble(string label)
        {
            while (true)
            {
                Console.Write("  " + label + " (0-100): ");
                if (double.TryParse(Console.ReadLine(),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double n) && n >= 0 && n <= 100)
                    return n;
                Error("Ingrese un número entre 0 y 100.");
            }
        }

        public static void Pausar()
        {
            Console.Write("\n  Presione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
