using OpenTK;
using OpenTK.Graphics;
using System;

namespace ResourseLibrary
{
    [Serializable]
    public struct DescriptionEntity
    {
        public readonly Color4 MyColor;
        public readonly string Name;
      
        public DescriptionEntity(Color4 color, string name)
        {
            MyColor = color;
            Name = name;
         
        }
    }
}