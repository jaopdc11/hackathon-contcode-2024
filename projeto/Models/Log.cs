namespace Hackacont2024.Models { 
    public class LogModel
    {
        public int Id {get; set; }
        public string Placa {get; set; }
        public string EntradaSaida {get; set; }
        public DateTime DataHora {get; set; } = DateTime.Now;
        public string Tipo { get; set; } = "Entrada";

    }
}