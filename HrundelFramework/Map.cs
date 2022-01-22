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
    public abstract class Map
    {
        public readonly string Name;
        private Camera _mainCamera;
        private List<Entity> _prefabEntities = new List<Entity>();
        public Map(string name,Camera camera)
        {
            Name = name;
            _mainCamera = camera;
            MapManager.AddMap(this);
            EntitiesInitialization();
        }
        public Camera GetCamera()
        {
            return _mainCamera;
        }
        public List<Entity> GetEntities()
        {
            return _prefabEntities;
        }
       protected abstract void EntitiesInitialization();
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
            entity.ChangeProperties(entityProperties);
            _prefabEntities.Add(entity);
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

