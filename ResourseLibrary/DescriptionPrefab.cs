using OpenTK;
using OpenTK.Graphics;
using System;

namespace ResourseLibrary
{
    [Serializable]
    public struct DescriptionPrefab
    {
       public readonly Vector2 Position;
        public readonly Vector2 Scale;
        public readonly string NameMainEntity;
        public readonly string Name;
        public DescriptionPrefab(Vector2 pos, Vector2 scale,string name, string name2)
        {
            Position = pos;
            Scale = scale;
            Name = name;
            NameMainEntity = name2;
        }
    }
}