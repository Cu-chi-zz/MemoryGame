using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Project.Data
{
    public class UserData
    {
        public int MaxScore { get; set; }
        public bool NextLevelAuto { get; set; }
        public string Version { get; } = "1.2";
    }
}
