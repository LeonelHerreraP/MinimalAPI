using proyectoApi.ViewModels;

namespace proyectoApi.Interfaces
{
    public interface IAutorService
    {
        Autor BuscarAutor(BuscarAutorVM vm);
    }
}
