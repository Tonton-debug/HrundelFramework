using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
namespace HrundelFramework.Helpers
{
  internal static  class Vector2Helper
    {
        public static Vector2 Round(this Vector2 vector2,int x)
        {          
                return new Vector2(MathF.Round(vector2.X, x), MathF.Round(vector2.Y, x));         
        }
       
    }
}
