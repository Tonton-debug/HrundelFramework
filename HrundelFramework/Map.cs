using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace HrundelFramework
{
    public sealed class Map
    {
        public readonly string Name;
        private Camera _mainCamera;
        private List<Entity> _prefabEntities = new List<Entity>();
        public Map(string name,Camera camera)
        {
            Name = name;
            _mainCamera = camera;
            MapManager.AddMap(this);
          
        }
        public Camera GetCamera()
        {
            return _mainCamera;
        }
        public List<Entity> GetEntities()
        {
            return _prefabEntities;
        }
      
       public List<T> FindEntityTypeInRadius<T>(Vector2 position,float radius) where T:class
        {
            List<T> typeEntities = new List<T>();
            foreach (var entity in _prefabEntities)
            {
                if(entity is T && Math.Abs(position.LengthFast - entity.Position.LengthFast) < radius)
                {
                    typeEntities.Add(entity as T);
                }
            }
            return typeEntities;
        }
        public void AddEntity(Entity entity,EntityProperties entityProperties)
        {
            foreach (var item in Assembly.Load("ITBOX_GAME").GetTypes())
            {
                object[] paramArray=new object[0];
                object obj = Activator.CreateInstance(item,args:paramArray);
                if (obj is Entity&&(obj as Entity).Name== entity.Name)
                {
                    (obj as Entity).ChangeProperties(entityProperties);
                    _prefabEntities.Add(obj as Entity);
                    break;
                }
            }
        }
        public void RemoveEntity(Entity entity)
        {
            _prefabEntities.Remove(entity);
        }
        internal void Rendering()
        {
            foreach (var entity in _prefabEntities)
            {
                entity.Rendering(_mainCamera.GetOrthoMatrix());
               
            }
   
        }
    }
}

