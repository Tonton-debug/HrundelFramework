using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ResourseLibrary
{
    [Serializable]
    public  class Resourse
    {
        public Dictionary<string,DescriptionMap> DescriptionMaps = new Dictionary<string, DescriptionMap>();
        public Dictionary<string, DescriptionEntity> DescriptionEntities = new Dictionary<string, DescriptionEntity>();
        public void Save(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream fileStream=new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, this);
            }
        }
        public static Resourse Load(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
              return formatter.Deserialize(fileStream) as Resourse;
            }
        }
    }
}
