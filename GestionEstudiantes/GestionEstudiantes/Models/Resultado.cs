namespace GestionEstudiantes.Models
{
    // Respuesta estándar para cualquier operación del sistema
    public class Resultado
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public static Resultado Ok(string msg, dynamic data = null) =>
            new Resultado { Success = true, Message = msg, Data = data };

        public static Resultado Fail(string msg) =>
            new Resultado { Success = false, Message = msg };
    }
}
