using System;
using System.Collections.Generic;
using System.Text;
using HrundelFramework;
using OpenTK;
using HrundelFramework.Input;
namespace ITBOX_GAME
{
    class Player:PhysicalEntity,ICollider
    {
       
        private float speed=2;
        public Player() : base("player")
        {

        }
        public override void Load()
        {
            Gravity = 2f;
            base.Load();
        }
        void ICollider.CollisionHasOccurred(List<Entity> entities)
        {
            if (entities.Count >= 1 && (entities[0] is Platform))
                (entities[0] as Platform).Push();


        }
        public override void Update()
        {
            if (KeyManager.KeyPressed(Key.D))
            {
                Position += new Vector2(0.5f, 0);
            }else if (KeyManager.KeyPressed(Key.A))
            {
                Position -= new Vector2(0.5f, 0);
            }
            if (KeyManager.KeyPressed(Key.Add))
            {
                speed++;
                Console.WriteLine(speed);
            }
            if (KeyManager.KeyPressed(Key.Minus))
            {
                speed--;
                Console.WriteLine(speed);
            }
            if (KeyManager.KeyPressed(Key.Space)&&IsCollision)
            {
                AddImpulse(new Vector2(0, speed));
            }
            if (Position.Y < -200)
            {
                Position = Vector2.Zero;
            }
            base.LateUpdate();
        }

        
    }
}
