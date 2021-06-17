using MemoryGame.Project.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Project.Json
{
    class Json
    {
        public bool WriteData(UserData dataToWrite, string p)
        {
            try
            {
                System.IO.File.WriteAllText(p, JsonConvert.SerializeObject(dataToWrite, Formatting.Indented));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserData ReadData(string p)
        {
            UserData data;
            try
            {
                string jString = System.IO.File.ReadAllText(p);
                data = JsonConvert.DeserializeObject<UserData>(jString);
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
