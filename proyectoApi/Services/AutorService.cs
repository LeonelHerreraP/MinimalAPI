using Microsoft.Data.SqlClient;
using proyectoApi.Interfaces;
using proyectoApi.Models;
using proyectoApi.ViewModels;
using System.Data;

namespace proyectoApi.Services
{
    public class AutorService : IAutorService
    {
        // Dependencias
        private readonly db_libreriaContext _context;
        //Para obtener el connectionString
        private readonly IConfiguration _configuration;

        // Pasamos las dependencias en el constructor
        public AutorService(db_libreriaContext context, IConfiguration configuration)
        {

            _context = context;
            _configuration = configuration;
        }

        public Autor BuscarAutor(BuscarAutorVM vm)
        {
            try
            {
                // Ejecutando stored procedure
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ConexionLibreria")))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Search_Autor";
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = vm.nombre;
                    cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar).Value = vm.apellido;
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader resultado = cmd.ExecuteReader();
                    Autor autor = new Autor();
                    while (resultado.Read())
                    {
                        autor.id_autor = resultado[0].ToString();
                        autor.apellido = resultado[1].ToString();
                        autor.nombre = resultado[2].ToString();
                        autor.telefono = resultado[3].ToString();
                        autor.direccion = resultado[4].ToString();
                        autor.ciudad = resultado[5].ToString();
                        autor.estado = resultado[6].ToString();
                        autor.pais = resultado[7].ToString();
                        autor.cod_postal = (int)resultado[8];
                        autor.Genero = resultado[9].ToString();

                    }
                    conn.Close();
                    return autor;
                };

            }
            catch
            {
                return null;
            }

        }






    }
}
