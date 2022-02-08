using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrundelFramework
{
    internal enum Side
    {
        Left,
        Right,
        Up,
        Down
    }
    internal class Line
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
        public void UpdatePos(Vector2 position, Vector2 startPosition)
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
        private bool _hasLine = false;
        public Line UpLine { get; private set; }
        public Line LeftLine { get; private set; }
        public Line RightLine { get; private set; }
        public Line DownLine { get; private set; }
        public Collider()
        {
            
        }
        public bool HasCollision(Side side,Collider otherCollider,Vector2 offset)
        {
            switch (side)
            {
                case Side.Left:
                    return offset.X > 0 && ((UpLine < otherCollider.UpLine && UpLine > otherCollider.DownLine) || (otherCollider.UpLine < UpLine && otherCollider.UpLine > DownLine)) &&
                        ((RightLine > otherCollider.LeftLine && RightLine < otherCollider.RightLine) || RightLine == otherCollider.LeftLine);
                case Side.Right:
                    return offset.X < 0 && ((UpLine < otherCollider.UpLine && UpLine > otherCollider.DownLine) || (otherCollider.UpLine < UpLine && otherCollider.UpLine > DownLine)) &&
                         ((LeftLine < otherCollider.RightLine && LeftLine > otherCollider.LeftLine) || LeftLine == otherCollider.RightLine);
                    
                case Side.Up:
                    return offset.Y > 0 && ((LeftLine < otherCollider.RightLine && LeftLine > otherCollider.LeftLine) || (otherCollider.LeftLine < RightLine && otherCollider.LeftLine > LeftLine))&&
                       ((DownLine < otherCollider.UpLine && DownLine > otherCollider.DownLine) || (DownLine == otherCollider.UpLine));
                case Side.Down:
                    return offset.Y < 0 && ((LeftLine < otherCollider.RightLine && LeftLine > otherCollider.LeftLine) || (otherCollider.LeftLine < RightLine && otherCollider.LeftLine > LeftLine)) &&
                      ((UpLine > otherCollider.DownLine && UpLine < otherCollider.UpLine) || (UpLine == otherCollider.DownLine));
                default:
                    return false;
            }
        }
        public bool HasCollision(Side side, Collider otherCollider)
        {
            switch (side)
            {
                case Side.Left:
                    return ((UpLine < otherCollider.UpLine && UpLine > otherCollider.DownLine) || (otherCollider.UpLine < UpLine && otherCollider.UpLine > DownLine)) &&
                        ((RightLine > otherCollider.LeftLine && RightLine < otherCollider.RightLine) || RightLine == otherCollider.LeftLine);
                case Side.Right:
                    return  ((UpLine < otherCollider.UpLine && UpLine > otherCollider.DownLine) || (otherCollider.UpLine < UpLine && otherCollider.UpLine > DownLine)) &&
                         ((LeftLine < otherCollider.RightLine && LeftLine > otherCollider.LeftLine) || LeftLine == otherCollider.RightLine);

                case Side.Up:
                    return  ((LeftLine < otherCollider.RightLine && LeftLine > otherCollider.LeftLine) || (otherCollider.LeftLine < RightLine && otherCollider.LeftLine > LeftLine)) &&
                       ((DownLine < otherCollider.UpLine && DownLine > otherCollider.DownLine) || (DownLine == otherCollider.UpLine));
                case Side.Down:
                    return ((LeftLine < otherCollider.RightLine && LeftLine > otherCollider.LeftLine) || (otherCollider.LeftLine < RightLine && otherCollider.LeftLine > LeftLine)) &&
                      ((UpLine > otherCollider.DownLine && UpLine < otherCollider.UpLine) || (UpLine == otherCollider.DownLine));
                default:
                    return false;
            }
        }
        public void UpdatePosition(Vector2 position, Vector2 size)
        {
            if (!_hasLine)
            {
                DownLine = new Line(new Vector2(position.X, position.Y - size.Y * 0.5f), position);
                UpLine = new Line(new Vector2(position.X, position.Y + size.Y * 0.5f), position);
                LeftLine = new Line(new Vector2(position.X - size.X * 0.5f, position.Y), position);
                RightLine = new Line(new Vector2(position.X + size.X * 0.5f, position.Y), position);
                _hasLine = true;
            }
            else
            {
                DownLine.UpdatePos(new Vector2(position.X, position.Y - size.Y * 0.5f), position);
                UpLine.UpdatePos(new Vector2(position.X, position.Y + size.Y * 0.5f), position);
                LeftLine.UpdatePos(new Vector2(position.X - size.X * 0.5f, position.Y), position);
                RightLine.UpdatePos(new Vector2(position.X + size.X * 0.5f, position.Y), position);
            }
        }
    }
}
