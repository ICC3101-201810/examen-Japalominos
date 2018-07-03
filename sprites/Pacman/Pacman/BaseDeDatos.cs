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
    class BaseDeDatos
    {
        List<Record> records;
        public List<Record> DesRecords()
        {
            try
            {
                
                using (Stream stream = File.Open("data1.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    records = (List<Record>)bin.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return new List<Record>();
            }
            return records;
        }
        public void SerRecords(List<Record> records )
        {
            try
            {
               
                using (Stream stream = File.Open("data1.bin", FileMode.OpenOrCreate))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    bin.Serialize(stream,records);
                }
            }
            catch (IOException)
            {
            }
            
        }
       
        
    }
}
