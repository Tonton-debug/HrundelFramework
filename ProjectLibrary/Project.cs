using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ResourseLibrary;
using OpenTK.Graphics;
namespace ProjectLibrary
{
    [Serializable]
    public class Project
    {
       
        public Dictionary<string,EntityCharacteristics> EntitiesCharacteristics = new Dictionary<string, EntityCharacteristics>();
        public Dictionary<string, Scene> Scenes = new Dictionary<string, Scene>();
        public void Save(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(Stream stream= new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, this);
            }
        }
        public void SaveToResourse(string path)
        {
            Resourse resourse = new Resourse();

            Dictionary<string, DescriptionMap> descriptionMaps = new Dictionary<string, DescriptionMap>();
            Dictionary<string, DescriptionEntity> descriptionEntities = new Dictionary<string, DescriptionEntity>();
            foreach (var scene in Scenes)
            {
                Dictionary<string, DescriptionPrefab> descriptionPrefabs = new Dictionary<string, DescriptionPrefab>();
                foreach (var prefab in scene.Value.PrefabEntities)
                    descriptionPrefabs.Add(prefab.Key,new DescriptionPrefab(prefab.Value.Position, prefab.Value.Scale, prefab.Key,prefab.Value.MyEntityCharacteristics.Name));
                descriptionMaps.Add(scene.Key,new DescriptionMap(descriptionPrefabs, new DescriptionCamera(scene.Value.MyCamera.Size),scene.Key));
            }
            foreach (var entity in EntitiesCharacteristics)
                descriptionEntities.Add(entity.Key, new DescriptionEntity(entity.Value.MyColor, entity.Key));
            resourse.DescriptionMaps = descriptionMaps;
            resourse.DescriptionEntities = descriptionEntities;
            resourse.Save(path);
        }
        public static Project Load(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                Project project = formatter.Deserialize(stream) as Project;
                foreach (var item in project.EntitiesCharacteristics)
                {
                    item.Value.SetShader();
                }
             return project;
            }
        }
    }
}
