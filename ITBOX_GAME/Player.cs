using System;
using System.Collections.Generic;
using System.Text;
using HrundelFramework;
using OpenTK;
using HrundelFramework.Input;
namespace ITBOX_GAME
{
    class Player:PhysicalEntity
    {
        
        public override void Load()
        {
            Gravity = 2f;
            base.Load();
        }
        public override void LateUpdate()
        {
            if (KeyManager.KeyPressed(Key.D))
            {
                Position += new Vector2(0.5f, 0);
            }else if (KeyManager.KeyPressed(Key.A))
            {
                Position -= new Vector2(1, 0);
            }
          
            if (KeyManager.KeyPressed(Key.Space)&&IsCollision)
            {
                AddImpulse(new Vector2(0, 3));
            }
            
            base.LateUpdate();
        }
    }
}
