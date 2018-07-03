using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Pacman
{
    [Serializable]
    class Record
    {
        public string usuario;
        public int score;
        public Record(string usuario, int score)
        {
            this.usuario = usuario;
            this.score = score;
        }
    }
}
