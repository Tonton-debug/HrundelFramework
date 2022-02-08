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
        public PhysicalEntity(string descriptionEntityName) : base(descriptionEntityName)
        {

        }
        public PhysicalEntity()
        {

        }
        public override void LateUpdate()
        {  
            if(_impulseVector.Y==0)
            Position -= new Vector2(0, Gravity);
            if (_impulseVector != Vector2.Zero)
            {
             
                  Position += _impulseVector;
                _impulseVector -=new Vector2(0.1f, 0.1f);
                if (IsCollision)
                    _impulseVector = Vector2.Zero;
               
                _impulseVector.X = _impulseVector.X < 0 ? 0 : _impulseVector.X;
                _impulseVector.Y = _impulseVector.Y < 0 ? 0 : _impulseVector.Y;
            }
            base.LateUpdate();
        }
    }
}
