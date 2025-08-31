using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UsuariosData
{
    public static class Archivos
    {
        public static Usuario GuardarEnArchivoJson(Usuario data)
        {
            var listado = LeerDesdeArchivoJson();

            if (data.Id != 0)
            {
                listado.RemoveAll(x => x.Id == data.Id);
            }
            else
            {
                data.Id = listado.Count + 1;
            }

            listado.Add(data);

            string directorioDestino = "../UsuariosData/Database";
            string rutaCompleta = Path.Combine(directorioDestino, "archivo.json");
            string rutaAbsolutaDestino = Path.GetFullPath(rutaCompleta);

            Directory.CreateDirectory(Path.GetDirectoryName(rutaAbsolutaDestino));
            string json = JsonConvert.SerializeObject(listado, Formatting.Indented);
            File.WriteAllText(rutaAbsolutaDestino, json);

            return data;
        }

        public static List<Usuario> LeerDesdeArchivoJson()
        {
            string directorioDestino = "../UsuariosData/Database";
            string rutaCompleta = Path.Combine(directorioDestino, "archivo.json");
            string rutaAbsolutaDestino = Path.GetFullPath(rutaCompleta);


            if (File.Exists(rutaAbsolutaDestino))
            {
                string json = File.ReadAllText(rutaAbsolutaDestino);
                return JsonConvert.DeserializeObject<List<Usuario>>(json);
            }
            else
            {
                return new List<Usuario>();
            }
        }
    }
}
