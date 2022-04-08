using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace BothServer
{
    public class JsonManager
    {
        internal long uId;
        public JsonManager(long uId)
        {
            this.uId = uId;
        }

        /// <summary>
        /// обновить данные о ванне
        /// </summary>
        /// <param name="newBoth"></param>
        public void SetMyBoth(Both newBoth)
        {
            var path = GetPath();
            using (StreamWriter sw = new StreamWriter(path))
                sw.WriteLine(JsonSerializer.Serialize(newBoth));
        }

        /// <summary>
        /// получить данные о ванне
        /// </summary>
        /// <returns></returns>
        public Both GetMyBoth()
        {
            string path = GetPath();
            using (StreamReader sr = new StreamReader(path))
                return JsonSerializer.Deserialize<Both>(sr.ReadToEnd());
        }
        
        /// <summary>
        /// получить путь до файла с данными о ванне
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            string path = $"data\\{uId}.json";

            if (!File.Exists($"data\\{uId}.json"))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(JsonSerializer.Serialize(new Both()
                    { drainStatus = true, craneActie = false, fillingProcent = 0, temp = 35, uId = this.uId, waterColor = Color.White }
                    ));
                }
            }
            return path;
        }
    }
}
