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
        //private readonly db_libreriaContext _context;
        private readonly PruebaContext _context;
        //Para obtener el connectionString
        private readonly IConfiguration _configuration;

        // Pasamos las dependencias en el constructor
        public AutorService(/*db_libreriaContext context*/ IConfiguration configuration, PruebaContext context)
        {

            _context = context;
            _configuration = configuration;
        }

        public Autor BuscarAutor(BuscarAutorVM vm)
        {
            /*
                Con stored procedure

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
                conn.Close()
            };
            */
            var autorResultado = _context.Personas.Where(x => x.Nombre == vm.nombre && x.Apellido == vm.apellido).ToList();
            Autor autor = new Autor();
            foreach(var item in autorResultado)
            {
                autor.nombre = item.Nombre;
                autor.apellido = item.Apellido;
                autor.direccion = item.Direccion;
                autor.Genero = item.Sexo;
            }

            return autor;
            
        }






    }
}
