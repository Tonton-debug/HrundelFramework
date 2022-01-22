using System;
using System.Collections.Generic;
using System.Text;
using HrundelFramework;
using OpenTK;
namespace ITBOX_GAME
{
    class OneLevelScene : Map
    {
        public OneLevelScene():base("level_1",new Camera(100))
        {

        }
        protected override void EntitiesInitialization()
        {
        //    AddEntity(new Platform(), new EntityProperties(new Vector2(0,-60), new Vector2(100, 20), "platform", new ColorF(1, 1, 1, 1)));
            for (int i = 0; i < 50; i++)
            {
                Random random = new Random();
                AddEntity(new Platform(), new EntityProperties(new Vector2(random.Next(-100, 100), random.Next(-100, 100)), new Vector2(10, 5), "platform", new ColorF((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1)));
            }
         
            AddEntity(new Player(), new EntityProperties(Vector2.Zero, new Vector2(2, 5), "player", new ColorF(0f, 0f, 0f, 0f)));
        }
    }
}
