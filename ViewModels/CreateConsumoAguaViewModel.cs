namespace AquaMonitor.Api.ViewModels
{
    public class CreateConsumoAguaViewModel
    {
        public string? Local { get; set; }
        public decimal LitrosConsumidos { get; set; }
        public int NivelAlerta { get; set; }
    }
}
