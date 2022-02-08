using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ResourseLibrary;
namespace HrundelFramework
{
    public static class MapManager
    {
        private static Shader _shader=null;
        private static Map _currentMap = null;
        private static List<Map> _maps = new List<Map>();
        internal static Dictionary<string, Entity> AllEntitiesDictionary { get; private set; }
        internal static Resourse LoadedResourse { get; private set; }
       
      public static Map GetLoadingMap()
        {
                return _currentMap;
        }
        public static Shader GetShader()
        {
            return _shader;
            
        }
        internal static void AddEntity(Entity get)
        {
            
            if (AllEntitiesDictionary == null)
                AllEntitiesDictionary = new Dictionary<string, Entity>();
            if (AllEntitiesDictionary.ContainsKey(get.Name))
                return;
            AllEntitiesDictionary.Add(get.Name, get);
        }
        public static void LoadAllMap()
        {
            foreach (var item in Assembly.Load("ITBOX_GAME").GetTypes())
            {
               
                if (item.BaseType.Name == "Map")
                {                   
                    AddMap(Activator.CreateInstance(item) as Map);
                }
            }
        }
        public static Map GetMap(string name)
        {
            return _maps.Find((t) => t.Name == name);
        }
        public static void LoadAllMaps()
        {
            foreach (var mapD in LoadedResourse.DescriptionMaps)
            {
                Map map = new Map(mapD.Key, new Camera(mapD.Value.MyCamera.Size));
                foreach (var pref in mapD.Value.DescriptionPrefabs)
                {
                    map.AddEntity(AllEntitiesDictionary[pref.Value.NameMainEntity], new EntityProperties(pref.Value.Position, pref.Value.Scale, pref.Value.Name));
                }
            }
        }
        public static void LoadResourses(string path)
        {
            LoadedResourse = Resourse.Load(path);
            
        }
        public static void SetCurrentMap(string name)
        {
            if (_shader == null)
                _shader = new Shader("shader.vert", "shader.frag");
            _currentMap = _maps.Find((t) => t.Name == name);
           
            foreach (var item in _currentMap.GetEntities())
            {
                item.Load(_currentMap);
            }
        }
        public static void RenderingMap()
        {
           
            if (_currentMap != null)
                _currentMap.Rendering();
            else
                Console.WriteLine("Пустая карта");
        }
       
        internal static void AddMap(Map map)
        {
            _maps.Add(map);
        }
    }
}

