using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using Avalonia.Controls.Shapes;

namespace GraphicEditor.Models.Serializers 
{
    class JSONSerializer<T>
    {
        public static void Save(string path, T item)
        {
            /*using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize<T>(file, item);
            }*/
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<T>(fs, item,
                    new JsonSerializerOptions
                    {
                        Converters = { new PaintShapeJSONConverter() },
                        WriteIndented = true
                    });
            }

        }
        public static T Load(string path)
        {

            /*using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (T)JsonSerializer.Deserialize<T>(file);
            }*/
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<T>(fs, new JsonSerializerOptions
                {
                    Converters = { new PaintShapeJSONConverter() },
                    WriteIndented = true
                });
            }

        }
    }
}
