namespace AquaMonitor.Api.Models
{
    public class ConsumoAgua
    {
        public int Id { get; set; }                      // Identificador único
        public string? Local { get; set; }               // Local da medição (agora é opcional)
        public DateTime Data { get; set; }               // Data da leitura
        public decimal LitrosConsumidos { get; set; }    // Litros consumidos
        public int NivelAlerta { get; set; }             // 0 normal, 1 alerta, 2 crítico
    }
}
