using proyectoApi.ViewModels;

namespace proyectoApi.Interfaces
{
    public interface ISolicitudService
    {
        Task<bool> UpdateState(cambiarEstadoVM vm);
    }
}
