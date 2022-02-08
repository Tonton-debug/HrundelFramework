using System;
using System.Collections.Generic;
using System.Text;
using HrundelFramework;
using HrundelFramework.Input;
using OpenTK;
namespace ITBOX_GAME
{
    class Platform:SolidEntity
    {
        private float z;
        public Platform():base("platform")
        {

        }
        public override void Load()
        {
          
            base.Load();
        }
        public void Push()
        {
            Position -= new Vector2(0.1f,0);
        }
        public override void Update()
        {
            Random random = new Random();
         //    Position += new Vector2(MathF.Sin((float)random.NextDouble() - 0.5f), MathF.Sin((float)random.NextDouble()-0.5f));
          
            base.Update();
        }

        
    }
}