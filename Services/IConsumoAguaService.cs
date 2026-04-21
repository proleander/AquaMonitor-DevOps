using AquaMonitor.Api.ViewModels;

namespace AquaMonitor.Api.Services
{
    public interface IConsumoAguaService
    {
        Task<PagedResultViewModel<ConsumoAguaViewModel>> GetAllAsync(int page, int pageSize);
        Task<ConsumoAguaViewModel?> GetByIdAsync(int id);
        Task<ConsumoAguaViewModel> CreateAsync(CreateConsumoAguaViewModel model);
        Task<ConsumoAguaViewModel?> UpdateAsync(int id, UpdateConsumoAguaViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
