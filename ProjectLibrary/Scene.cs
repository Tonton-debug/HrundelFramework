using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectLibrary
{
    [Serializable]
    public  class Scene
    {
        public Dictionary<string, PrefabEntity> PrefabEntities = new Dictionary<string, PrefabEntity>();
        public Camera MyCamera { get; private set; }
        public Scene(Camera get)
        {
            MyCamera = get;
        }
        public void Rendering()
        {
            foreach (var item in PrefabEntities)
            {
                item.Value.Rendering(MyCamera.GetOrthoMatrix());
            }
        }
    }
}
