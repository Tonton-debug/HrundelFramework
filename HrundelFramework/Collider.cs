using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrundelFramework
{
    internal struct Line
    {
       public Vector2 Position { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public Line(Vector2 position,Vector2 startPosition)
        {
            X = position.X;
            Y = position.Y;
            Position = startPosition;
        }
        private static bool IntersectionCheck(Line line1,Line line2,char sign)
        {
            if (line1.X == line1.Position.X && line2.X == line2.Position.X)
              return  CheckSign(line1.Y, line2.Y, sign);
            else if (line1.Y == line1.Position.Y && line2.Y == line2.Position.Y)
                return CheckSign(line1.X, line2.X, sign);
            else
                return false;
            static bool CheckSign(float XorY, float XorY2, char sign)
            {
                switch (sign)
                {
                    case '>':
                        return XorY > XorY2;
                    case '<':
                        return XorY < XorY2;
                    case '(':
                        return XorY <= XorY2;
                    case ')':
                        return XorY >= XorY2;
                    case '=':
                        return XorY == XorY2;
                    case '!':
                        return XorY != XorY2;
                    default:
                        return false;
                }
            }
        }
        public static bool operator <(Line line1, Line line2)
        {
           return IntersectionCheck(line1, line2, '<');
        }
        public static bool operator >(Line line1, Line line2)
        {
            return IntersectionCheck(line1, line2, '>');
        }
        public static bool operator >=(Line line1, Line line2)
        {
            return IntersectionCheck(line1, line2, ')');
        }
        public static bool operator <=(Line line1, Line line2)
        {
            return IntersectionCheck(line1, line2, '(');
        }
        public static bool operator ==(Line line1, Line line2)
        {
            return IntersectionCheck(line1, line2, '=');
        }
        public static bool operator !=(Line line1, Line line2)
        {
            return IntersectionCheck(line1, line2, '!');
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    internal class Collider
    {
        public bool IsVisible { get; set; }
        public Line UpLine { get;private set; }
        public Line LeftLine { get; private set; }
        public Line RightLine { get; private set; }
        public Line DownLine { get; private set; }
        public Collider()
        {
           
        }
      public void UpdatePosition(Vector2 position, Vector2 size)
        {
            DownLine = new Line(new Vector2(position.X,position.Y-size.Y*0.5f),position);
            UpLine = new Line(new Vector2(position.X, position.Y + size.Y * 0.5f), position);
            LeftLine = new Line(new Vector2((position.X-size.X * 0.5f), position.Y), position);
            RightLine = new Line(new Vector2(position.X + size.X * 0.5f, position.Y), position);
          
          
        }

    }
}
