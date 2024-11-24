namespace Hackacont2024.Models {
    public class Cargo {
        public int Id { get; set; }
        public string CargoName { get; set; } = "vazio";
        public string Category { get; set; }
        public int Quantity { get; set; }
        public float Weight { get; set; }
    }
}