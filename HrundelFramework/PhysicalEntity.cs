using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace HrundelFramework
{
  public abstract  class PhysicalEntity:SolidEntity
    {
        private Vector2 _impulseVector= Vector2.Zero;
        protected float Gravity = 0.1f;
        protected void AddImpulse(Vector2 imulse)
        {
            _impulseVector = imulse;
        }
        public override void LateUpdate()
        {  
            if(_impulseVector.Y==0)
            Position -= new Vector2(0, Gravity);
            if (_impulseVector != Vector2.Zero)
            {
                Console.WriteLine("{0},{1}", _impulseVector.X, _impulseVector.Y);
                Position += _impulseVector;
                _impulseVector -=new Vector2(0.1f, 0.1f);
                switch (PositionCollision)
                {
                    case PositionCollision.None:
                        break;
                    case PositionCollision.Left:
                    case PositionCollision.Right:
                        _impulseVector.X = 0;
                        break;
                    case PositionCollision.Up when _impulseVector.Y > 0:
                        _impulseVector.Y = 0;
                        break;
                    case PositionCollision.Down when _impulseVector.Y < 0:
                        _impulseVector.Y = 0;
                        break;
                    default:
                        break;
                }
               
                _impulseVector.X = _impulseVector.X < 0 ? 0 : _impulseVector.X;
                _impulseVector.Y = _impulseVector.Y < 0 ? 0 : _impulseVector.Y;
            }
            base.LateUpdate();
        }
    }
}
