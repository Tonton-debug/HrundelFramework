using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
namespace ProjectLibrary
{
    [Serializable]
    public class PrefabEntity
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public EntityCharacteristics MyEntityCharacteristics { get; private set; }
      
        public PrefabEntity(EntityCharacteristics entityCharacteristics)
        {
            MyEntityCharacteristics = entityCharacteristics;
            Position = Vector2.Zero;
            Scale = Vector2.One;
        }
        public void Rendering(Matrix4 orthoCamera)
        {
            MyEntityCharacteristics.Rendering(orthoCamera, Position, Scale);
        }
    }
}
