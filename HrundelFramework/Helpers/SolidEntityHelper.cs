using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace HrundelFramework.Helpers
{
  internal static class SolidEntityHelper
    {
        public static Vector2 MathDistanceNeededCompleteWhile(Vector2 offset,int offsetOrdinate=0)
        {
            //if (offset.X.ToString().Split(',').Length > 1&& (offset.X.ToString().Split(',')[1].Length>9))
            //    offset.X = MathF.Round(offset.X, 9);
            //if (offset.Y.ToString().Split(',').Length > 1 && (offset.Y.ToString().Split(',')[1].Length > 9))
            //    offset.Y = MathF.Round(offset.Y, 9);
            Tuple<string, string> offsetString = new Tuple<string, string>(MathF.Abs(offset.X).ToString(), MathF.Abs(offset.Y).ToString());
            Vector2 returnVector2 = Vector2.Zero;
            returnVector2.X = MathDistanceOrdinateNeededCompleteWhile(offsetString.Item1);
            returnVector2.X = offsetOrdinate == 0 ? returnVector2.X : returnVector2.X / MathF.Pow(10, offsetOrdinate);
            returnVector2.Y = MathDistanceOrdinateNeededCompleteWhile(offsetString.Item2);
            returnVector2.Y = offsetOrdinate == 0 ? returnVector2.Y : returnVector2.Y / MathF.Pow(10, offsetOrdinate);
            //returnVector2.X = returnVector2.X == 0 ? 0 : returnVector2.X;
            //returnVector2.Y = returnVector2.Y == 0 ?0 : returnVector2.Y;
            return returnVector2;
        }
        private static float MathDistanceOrdinateNeededCompleteWhile(string offsetString)
        {
            bool hasComma = false;
            for (int i = 0; i < offsetString.Length; i++)
            {
                switch (offsetString[i])
                {
                    case ',':
                        hasComma = true;
                        break;
                    default:
                        if (offsetString[i] != '0' && offsetString[i] != '9' && hasComma && offsetString[0]=='0')
                            return 1 / MathF.Pow(10, i);
                        else if(offsetString[i] != '0' && offsetString[i] == '9' && hasComma && offsetString[0] == '0')
                            return 1 / MathF.Pow(10, i-1);
                        break;
                }
               
            }
            //if (offsetString[0] == '0')
            //    return 0;
            return 0.1f;
        }
    }
}
