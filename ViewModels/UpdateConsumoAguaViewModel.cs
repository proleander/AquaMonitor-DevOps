namespace AquaMonitor.Api.ViewModels
{
    public class UpdateConsumoAguaViewModel
    {
        public string? Local { get; set; }
        public decimal LitrosConsumidos { get; set; }
        public int NivelAlerta { get; set; }
    }
}
