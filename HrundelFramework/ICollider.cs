using System;
using System.Collections.Generic;
using System.Text;

namespace HrundelFramework
{
   public interface ICollider
    {
        public void CollisionHasOccurred(List<Entity> entities);
    }
}
