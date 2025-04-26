using ItemService.Models;

namespace ItemService.Services
{
    public interface IRestauranteService
    {
        Task<IEnumerable<Restaurante>> GetAllRestaurante();
    }
}
