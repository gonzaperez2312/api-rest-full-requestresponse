using TiendaData;
using TiendaRequests;
using TiendaResponses;

namespace TiendaService
{
    public class ProductoService
    {
        public ApiResponse<List<ProductoResponse>> ObtenerTodosProductos()
        {
            var productos = LeerProductos();
            if (!productos.Success)
            {
                return new ApiResponse<List<ProductoResponse>>(productos.Message ?? "Error desconocido", productos.Errors);
            }

            var productosResponse = productos.Data!.Select(MapToProductoResponse).ToList();
            return new ApiResponse<List<ProductoResponse>>(productosResponse, "Productos obtenidos exitosamente");
        }

        public ApiResponse<ProductoResponse> CrearProducto(CrearProductoRequest request)
        {
            var productos = LeerProductos();
            if (!productos.Success)
            {
                return new ApiResponse<ProductoResponse>(productos.Message ?? "Error desconocido", productos.Errors);
            }

            if (productos.Data!.Any(p => p.Codigo?.Equals(request.Codigo, StringComparison.OrdinalIgnoreCase) == true))
            {
                return new ApiResponse<ProductoResponse>("Producto duplicado", new List<string> { "Ya existe un producto con ese código" });
            }

            var productoDB = new Producto
            {
                Codigo = request.Codigo,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Precio = request.Precio,
                Stock = request.Stock,
                Categoria = request.Categoria,
                Especificaciones = request.Especificaciones
            };

            var productoGuardado = GuardarProducto(productoDB);
            if (!productoGuardado.Success)
            {
                return new ApiResponse<ProductoResponse>(productoGuardado.Message ?? "Error desconocido", productoGuardado.Errors);
            }

            var productoResponse = MapToProductoResponse(productoGuardado.Data!);
            return new ApiResponse<ProductoResponse>(productoResponse, "Producto creado exitosamente");
        }

        public ApiResponse<ProductoResponse> ObtenerProductoPorId(ObtenerProductoRequest request)
        {
            var producto = BuscarProductoPorId(request.Id);
            if (!producto.Success)
            {
                return new ApiResponse<ProductoResponse>(producto.Message ?? "Error desconocido", producto.Errors);
            }

            var productoResponse = MapToProductoResponse(producto.Data!);
            return new ApiResponse<ProductoResponse>(productoResponse, "Producto encontrado");
        }

        public ApiResponse<ProductoResponse> ActualizarProducto(ActualizarProductoRequest request)
        {
            var producto = BuscarProductoPorId(request.Id);
            if (!producto.Success)
            {
                return new ApiResponse<ProductoResponse>(producto.Message ?? "Error desconocido", producto.Errors);
            }

            var productos = LeerProductos();
            if (!productos.Success)
            {
                return new ApiResponse<ProductoResponse>(productos.Message ?? "Error desconocido", productos.Errors);
            }

            if (productos.Data!.Any(p => p.Id != request.Id && p.Codigo?.Equals(request.Codigo, StringComparison.OrdinalIgnoreCase) == true))
            {
                return new ApiResponse<ProductoResponse>("Producto duplicado", new List<string> { "Ya existe otro producto con ese código" });
            }

            var productoDb = producto.Data;
            productoDb.Codigo = request.Codigo;
            productoDb.Nombre = request.Nombre;
            productoDb.Descripcion = request.Descripcion;
            productoDb.Marca = request.Marca;
            productoDb.Modelo = request.Modelo;
            productoDb.Precio = request.Precio;
            productoDb.Stock = request.Stock;
            productoDb.Categoria = request.Categoria;
            productoDb.Especificaciones = request.Especificaciones;

            var productoActualizado = GuardarProducto(productoDb);
            if (!productoActualizado.Success)
            {
                return new ApiResponse<ProductoResponse>(productoActualizado.Message ?? "Error desconocido", productoActualizado.Errors);
            }

            var productoResponse = MapToProductoResponse(productoActualizado.Data!);
            return new ApiResponse<ProductoResponse>(productoResponse, "Producto actualizado exitosamente");
        }

        public ApiResponse<bool> EliminarProducto(EliminarProductoRequest request)
        {
            var producto = BuscarProductoPorId(request.Id);
            if (!producto.Success)
            {
                return new ApiResponse<bool>(producto.Message ?? "Error desconocido", producto.Errors);
            }

            var productoDb = producto.Data!;
            productoDb.FechaEliminacion = DateTime.Now;

            var productoEliminado = GuardarProducto(productoDb);
            if (!productoEliminado.Success)
            {
                return new ApiResponse<bool>(productoEliminado.Message ?? "Error desconocido", productoEliminado.Errors);
            }

            return new ApiResponse<bool>(true, "Producto eliminado exitosamente");
        }

        private ApiResponse<List<Producto>> LeerProductos()
        {
            var productos = Archivos.LeerProductosDesdeArchivoJson();
            if (productos == null)
            {
                return new ApiResponse<List<Producto>>("Error al leer productos", new List<string> { "No se pudieron obtener los productos del archivo" });
            }
            return new ApiResponse<List<Producto>>(productos);
        }

        private ApiResponse<Producto> BuscarProductoPorId(int id)
        {
            var productos = LeerProductos();
            if (!productos.Success)
            {
                return new ApiResponse<Producto>(productos.Message ?? "Error desconocido", productos.Errors);
            }

            var producto = productos.Data!.FirstOrDefault(x => x.Id == id);
            if (producto == null)
            {
                return new ApiResponse<Producto>("Producto no encontrado", new List<string> { $"No se encontró un producto con el ID {id}" });
            }

            return new ApiResponse<Producto>(producto);
        }

        private ApiResponse<Producto> GuardarProducto(Producto producto)
        {
            var productoGuardado = Archivos.GuardarProductoEnArchivoJson(producto);
            if (productoGuardado == null)
            {
                return new ApiResponse<Producto>("Error al guardar producto", new List<string> { "No se pudo guardar el producto en el archivo" });
            }
            return new ApiResponse<Producto>(productoGuardado);
        }

        private static ProductoResponse MapToProductoResponse(Producto producto)
        {
            return new ProductoResponse
            {
                Id = producto.Id,
                Codigo = producto.Codigo ?? string.Empty,
                Nombre = producto.Nombre ?? string.Empty,
                Descripcion = producto.Descripcion ?? string.Empty,
                Marca = producto.Marca ?? string.Empty,
                Modelo = producto.Modelo ?? string.Empty,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Categoria = producto.Categoria ?? string.Empty,
                Especificaciones = producto.Especificaciones ?? string.Empty,
                FechaCreacion = producto.FechaCreacion
            };
        }
    }
}
