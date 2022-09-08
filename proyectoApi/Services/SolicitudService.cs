using proyectoApi.Interfaces;
using proyectoApi.Models;
using proyectoApi.ViewModels;

namespace proyectoApi.Services
{
    public class SolicitudService : ISolicitudService
    {
        // Dependencias
        private readonly PruebaContext _context;

        // Pasamos las dependencias en el constructor
        public SolicitudService(PruebaContext context)
        {

            _context = context;
        }


        public async Task<bool> UpdateState(cambiarEstadoVM vm)
        {
            try
            {
                // Buscar solicitud que tenga ese id
                var soli = await _context.Solicituds.FindAsync(vm.id);

                // Cambio estado por el nuevo
                soli.EstadoId = vm.estado;


                // Guardar los cambios
                await _context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }

        }
    }
}
