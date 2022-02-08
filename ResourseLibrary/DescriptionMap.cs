using System;
using System.Collections.Generic;

namespace ResourseLibrary
{
    [Serializable]
    public struct DescriptionMap
    {
      public readonly Dictionary<string, DescriptionPrefab> DescriptionPrefabs;
        public readonly DescriptionCamera MyCamera;
        public readonly string Name;
        public DescriptionMap(Dictionary<string, DescriptionPrefab> descriptionPrefabs,DescriptionCamera descriptionCamera,string name)
        {
            DescriptionPrefabs = descriptionPrefabs;
            MyCamera = descriptionCamera;
            Name = name;
        }
    }
}