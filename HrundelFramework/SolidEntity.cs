using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using HrundelFramework.Helpers;
namespace HrundelFramework
{
   public abstract class SolidEntity:Entity
    {
        private Collider _collider = new Collider();
        protected bool IsCollision { get; private set; }
        public SolidEntity(string descriptionEntityName) : base(descriptionEntityName)
        {

        }
        public SolidEntity()
        {

        }
        public override void Load()
        {
            _collider.UpdatePosition(Position, Scale);
            base.Load();
        }
     
        public override void LateUpdate()
        {
            _collider.UpdatePosition(Position, base.Scale);
            base.LateUpdate();
        }
        public sealed override Vector2 Position 
        { get => base.Position;
            protected set
            {

                IsCollision = false;       
                Vector2 offset = base.Position - value;
                Vector2 offsetSub2 = SolidEntityHelper.MathDistanceNeededCompleteWhile(offset);
                Vector2 offsetSub = SolidEntityHelper.MathDistanceNeededCompleteWhile(offset);
                
               
                if (base.Position == base.Position - value || base.Position == base.Position + value || MapManager.GetLoadingMap() == null || MapManager.GetLoadingMap().FindEntityTypeInRadius<SolidEntity>(value, offset.LengthFast).Count == 0)
                {
                    base.Position = value;
                    return;
                }
                List<Entity> collisionEntities = new List<Entity>();
                Vector2 startPos = base.Position;
                Vector2 endPos =value;
                
                //if (Name == "Player")
                //{
                //    Console.WriteLine("{6}\nOffsetSub:{0}  {1}\nOffsetSub2:{2}  {3}\nOffset:{4}  {5}\n", offsetSub.X, offsetSub.Y, offsetSub2.X, offsetSub2.Y, offset.X, offset.Y, Name);
                //  Console.WriteLine("AbsX:{0} AbsY:{1}", MathF.Abs(startPos.X - endPos.X), MathF.Abs(startPos.Y - endPos.Y));
                //}
                while (MathF.Abs(startPos.X - endPos.X) > offsetSub2.X || MathF.Abs(startPos.Y - endPos.Y) > offsetSub2.Y)
                {
                    //if (Name == "Player")
                    //   Console.WriteLine("AbsX:{0} AbsY:{1}", MathF.Abs(startPos.X - endPos.X), MathF.Abs(startPos.Y - endPos.Y));
                    foreach (var otherSolidEntity in MapManager.GetLoadingMap().FindEntityTypeInRadius<SolidEntity>(value, 150))
                    {
                        Collider otherCollider = otherSolidEntity._collider;
                        if (otherSolidEntity is PhysicalEntity && !(this is PhysicalEntity))
                        {
                            base.Position = value;
                            break;
                        }
                        _collider.UpdatePosition(startPos, base.Scale);
                        if (_collider.HasCollision(Side.Up, otherCollider, offset))
                        {
                            IsCollision = true;
                            if (this is PhysicalEntity)
                                startPos.Y = otherCollider.UpLine.Y + Scale.Y / 2;
                            value = startPos;
                            base.Position = value;
                            break;
                        }
                     else   if (_collider.HasCollision(Side.Down, otherCollider, offset))
                        {
                            IsCollision = true;
                            if (this is PhysicalEntity)
                                startPos.Y = otherCollider.DownLine.Y - Scale.Y / 2;
                            value = startPos;
                            base.Position = value;
                            break;
                        }
                        else if (_collider.HasCollision(Side.Right, otherCollider, offset))
                        {
                            IsCollision = true;
                            if (this is PhysicalEntity)
                                startPos.X = otherCollider.LeftLine.X - Scale.X / 2;
                            value = startPos;
                            base.Position = startPos;
                            break;
                        }
                      else  if (_collider.HasCollision(Side.Left, otherCollider, offset))
                        {
                            IsCollision = true;
                            if (this is PhysicalEntity)
                                startPos.X = otherCollider.RightLine.X + Scale.X / 2;

                            value = startPos;
                            base.Position = value;
                            break;
                        }
                    }
                    if (IsCollision)
                        break;
                    if (offset.Y != 0&& MathF.Abs(startPos.Y - endPos.Y) > offsetSub2.Y)
                        startPos.Y += offset.Y > 0 ? -offsetSub.Y : offsetSub.Y;

                    if (offset.X != 0&& MathF.Abs(startPos.X - endPos.X) > offsetSub2.X)
                        startPos.X += offset.X > 0 ? -offsetSub.X : offsetSub.X;
                }
                value = startPos;
                base.Position = value;
                foreach (var otherSolidEntity in MapManager.GetLoadingMap().FindEntityTypeInRadius<SolidEntity>(value, 10))
                {
                    if(_collider.HasCollision(Side.Up, otherSolidEntity._collider)|| _collider.HasCollision(Side.Down, otherSolidEntity._collider)|| _collider.HasCollision(Side.Left, otherSolidEntity._collider) || _collider.HasCollision(Side.Right, otherSolidEntity._collider))
                        collisionEntities.Add(otherSolidEntity);
                }
                    
                    
                if (this is ICollider)
                    ((ICollider)this).CollisionHasOccurred(collisionEntities);
            }
        }
    }
}