namespace AquaMonitor.Api.ViewModels
{
    public class ConsumoAguaViewModel
    {
        public int Id { get; set; }
        public string? Local { get; set; }
        public DateTime Data { get; set; }
        public decimal LitrosConsumidos { get; set; }
        public int NivelAlerta { get; set; }
    }
}
