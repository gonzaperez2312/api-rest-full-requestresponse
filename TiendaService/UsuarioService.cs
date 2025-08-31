using UsuariosData;
using UsuarioRequests;
using UsuarioResponses;

namespace UsuariosService
{
    public class UsuarioService
    {
        public ApiResponse<List<UsuarioResponse>> ObtenerTodosUsuarios()
        {
            var usuarios = LeerUsuarios();
            if (!usuarios.Success)
            {
                return new ApiResponse<List<UsuarioResponse>>(usuarios.Message ?? "Error desconocido", usuarios.Errors);
            }

            var usuariosResponse = usuarios.Data!.Select(MapToUsuarioResponse).ToList();
            return new ApiResponse<List<UsuarioResponse>>(usuariosResponse, "Usuarios obtenidos exitosamente");
        }

        public ApiResponse<UsuarioResponse> CrearUsuario(CrearUsuarioRequest request)
        {
            var usuarios = LeerUsuarios();
            if (!usuarios.Success)
            {
                return new ApiResponse<UsuarioResponse>(usuarios.Message ?? "Error desconocido", usuarios.Errors);
            }

            if (usuarios.Data!.Any(u => u.CorreoElectronico?.Equals(request.CorreoElectronico, StringComparison.OrdinalIgnoreCase) == true))
            {
                return new ApiResponse<UsuarioResponse>("Usuario duplicado", new List<string> { "Ya existe un usuario con ese correo electrónico" });
            }

            var usuarioDB = new Usuario
            {
                NombreCompleto = request.NombreCompleto,
                CorreoElectronico = request.CorreoElectronico
            };

            var usuarioGuardado = GuardarUsuario(usuarioDB);
            if (!usuarioGuardado.Success)
            {
                return new ApiResponse<UsuarioResponse>(usuarioGuardado.Message ?? "Error desconocido", usuarioGuardado.Errors);
            }

            var usuarioResponse = MapToUsuarioResponse(usuarioGuardado.Data!);
            return new ApiResponse<UsuarioResponse>(usuarioResponse, "Usuario creado exitosamente");
        }

        public ApiResponse<UsuarioResponse> ObtenerUsuarioPorId(ObtenerUsuarioRequest request)
        {
            var usuario = BuscarUsuarioPorId(request.Id);
            if (!usuario.Success)
            {
                return new ApiResponse<UsuarioResponse>(usuario.Message ?? "Error desconocido", usuario.Errors);
            }

            var usuarioResponse = MapToUsuarioResponse(usuario.Data!);
            return new ApiResponse<UsuarioResponse>(usuarioResponse, "Usuario encontrado");
        }

        public ApiResponse<UsuarioResponse> ActualizarUsuario(ActualizarUsuarioRequest request)
        {
            var usuario = BuscarUsuarioPorId(request.Id);
            if (!usuario.Success)
            {
                return new ApiResponse<UsuarioResponse>(usuario.Message ?? "Error desconocido", usuario.Errors);
            }

            var usuarios = LeerUsuarios();
            if (!usuarios.Success)
            {
                return new ApiResponse<UsuarioResponse>(usuarios.Message ?? "Error desconocido", usuarios.Errors);
            }

            if (usuarios.Data!.Any(u => u.Id != request.Id && u.CorreoElectronico?.Equals(request.CorreoElectronico, StringComparison.OrdinalIgnoreCase) == true))
            {
                return new ApiResponse<UsuarioResponse>("Usuario duplicado", new List<string> { "Ya existe otro usuario con ese correo electrónico" });
            }

            var usuarioDb = usuario.Data;
            usuarioDb.NombreCompleto = request.NombreCompleto;
            usuarioDb.CorreoElectronico = request.CorreoElectronico;

            var usuarioActualizado = GuardarUsuario(usuarioDb);
            if (!usuarioActualizado.Success)
            {
                return new ApiResponse<UsuarioResponse>(usuarioActualizado.Message ?? "Error desconocido", usuarioActualizado.Errors);
            }

            var usuarioResponse = MapToUsuarioResponse(usuarioActualizado.Data!);
            return new ApiResponse<UsuarioResponse>(usuarioResponse, "Usuario actualizado exitosamente");
        }

        public ApiResponse<bool> EliminarUsuario(EliminarUsuarioRequest request)
        {
            var usuario = BuscarUsuarioPorId(request.Id);
            if (!usuario.Success)
            {
                return new ApiResponse<bool>(usuario.Message ?? "Error desconocido", usuario.Errors);
            }

            var usuarioDb = usuario.Data!;
            usuarioDb.FechaEliminacion = DateTime.Now;

            var usuarioEliminado = GuardarUsuario(usuarioDb);
            if (!usuarioEliminado.Success)
            {
                return new ApiResponse<bool>(usuarioEliminado.Message ?? "Error desconocido", usuarioEliminado.Errors);
            }

            return new ApiResponse<bool>(true, "Usuario eliminado exitosamente");
        }


        private ApiResponse<List<Usuario>> LeerUsuarios()
        {
            var usuarios = Archivos.LeerDesdeArchivoJson();
            if (usuarios == null)
            {
                return new ApiResponse<List<Usuario>>("Error al leer usuarios", new List<string> { "No se pudieron obtener los usuarios del archivo" });
            }
            return new ApiResponse<List<Usuario>>(usuarios);
        }

        private ApiResponse<Usuario> BuscarUsuarioPorId(int id)
        {
            var usuarios = LeerUsuarios();
            if (!usuarios.Success)
            {
                return new ApiResponse<Usuario>(usuarios.Message ?? "Error desconocido", usuarios.Errors);
            }

            var usuario = usuarios.Data!.FirstOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                return new ApiResponse<Usuario>("Usuario no encontrado", new List<string> { $"No se encontró un usuario con el ID {id}" });
            }

            return new ApiResponse<Usuario>(usuario);
        }

        private ApiResponse<Usuario> GuardarUsuario(Usuario usuario)
        {
            var usuarioGuardado = Archivos.GuardarEnArchivoJson(usuario);
            if (usuarioGuardado == null)
            {
                return new ApiResponse<Usuario>("Error al guardar usuario", new List<string> { "No se pudo guardar el usuario en el archivo" });
            }
            return new ApiResponse<Usuario>(usuarioGuardado);
        }

        private static UsuarioResponse MapToUsuarioResponse(Usuario usuario)
        {
            return new UsuarioResponse
            {
                Id = usuario.Id,
                NombreCompleto = usuario.NombreCompleto ?? string.Empty,
                CorreoElectronico = usuario.CorreoElectronico ?? string.Empty
            };
        }
    }
}