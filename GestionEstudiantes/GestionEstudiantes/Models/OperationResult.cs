namespace GestionEstudiantes.Models
{
    // Clase que usamos para manejar errores de forma uniforme en toda la app
    public class OperationResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public dynamic Data { get; set; }

        public OperationResult(bool success, string message, dynamic data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        // Resultado exitoso
        public static OperationResult Ok(string message, dynamic data = null)
            => new OperationResult(true, message, data);

        // Resultado con error
        public static OperationResult Fail(string message)
            => new OperationResult(false, message, null);
    }
}
