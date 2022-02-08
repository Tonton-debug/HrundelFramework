using System;
namespace ResourseLibrary
{
    [Serializable]
    public struct DescriptionCamera
    {
        public readonly float Size;
        public DescriptionCamera(float size)
        {
            Size = size;
        }
    }
}