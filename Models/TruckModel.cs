namespace Hackacont2024.Models {
    public class Caminhao {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int CargaId { get; set; }
        public string Status { get; set; } = "aguardando";

        // Navigation property for related Cargo
        public Cargo Carga { get; set; }
    }
}
