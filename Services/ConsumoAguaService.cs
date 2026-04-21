using AquaMonitor.Api.Data;
using AquaMonitor.Api.Models;
using AquaMonitor.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AquaMonitor.Api.Services
{
    public class ConsumoAguaService : IConsumoAguaService
    {
        private readonly AquaContext _context;

        public ConsumoAguaService(AquaContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET PAGINADO → Agora retornando um PagedResultViewModel
        // ============================================================
        public async Task<PagedResultViewModel<ConsumoAguaViewModel>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.ConsumosAgua.AsQueryable();

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.Data)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ConsumoAguaViewModel
                {
                    Id = c.Id,
                    Local = c.Local,
                    Data = c.Data,
                    LitrosConsumidos = c.LitrosConsumidos,
                    NivelAlerta = c.NivelAlerta
                })
                .ToListAsync();

            return new PagedResultViewModel<ConsumoAguaViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = items
            };
        }

        // ============================================================
        // GET POR ID
        // ============================================================
        public async Task<ConsumoAguaViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.ConsumosAgua.FindAsync(id);

            if (entity == null) return null;

            return new ConsumoAguaViewModel
            {
                Id = entity.Id,
                Local = entity.Local,
                Data = entity.Data,
                LitrosConsumidos = entity.LitrosConsumidos,
                NivelAlerta = entity.NivelAlerta
            };
        }

        // ============================================================
        // POST — CRIAR
        // ============================================================
        public async Task<ConsumoAguaViewModel> CreateAsync(CreateConsumoAguaViewModel model)
        {
            var entity = new ConsumoAgua
            {
                Local = model.Local,
                Data = DateTime.Now,
                LitrosConsumidos = model.LitrosConsumidos,
                NivelAlerta = model.NivelAlerta
            };

            _context.ConsumosAgua.Add(entity);
            await _context.SaveChangesAsync();

            return new ConsumoAguaViewModel
            {
                Id = entity.Id,
                Local = entity.Local,
                Data = entity.Data,
                LitrosConsumidos = entity.LitrosConsumidos,
                NivelAlerta = entity.NivelAlerta
            };
        }

        // ============================================================
        // PUT — ATUALIZAR
        // ============================================================
        public async Task<ConsumoAguaViewModel?> UpdateAsync(int id, UpdateConsumoAguaViewModel model)
        {
            var entity = await _context.ConsumosAgua.FindAsync(id);

            if (entity == null) return null;

            entity.Local = model.Local;
            entity.LitrosConsumidos = model.LitrosConsumidos;
            entity.NivelAlerta = model.NivelAlerta;

            await _context.SaveChangesAsync();

            return new ConsumoAguaViewModel
            {
                Id = entity.Id,
                Local = entity.Local,
                Data = entity.Data,
                LitrosConsumidos = entity.LitrosConsumidos,
                NivelAlerta = entity.NivelAlerta
            };
        }

        // ============================================================
        // DELETE
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ConsumosAgua.FindAsync(id);

            if (entity == null) return false;

            _context.ConsumosAgua.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
