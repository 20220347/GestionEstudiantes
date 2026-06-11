namespace GestionEstudiantes.Models
{
    public enum TipoEval { Examen, Practica }

    public class Calificacion
    {
        public string Descripcion { get; set; }
        public double Nota { get; set; }
        public TipoEval Tipo { get; set; }

        public Calificacion(string descripcion, double nota, TipoEval tipo)
        {
            Descripcion = descripcion;
            Nota = nota;
            Tipo = tipo;
        }

        public override string ToString() =>
            $"  {Tipo,-10} | {Descripcion,-20} | {Nota:F1}";
    }
}
