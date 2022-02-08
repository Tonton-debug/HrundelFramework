using System;
using OpenTK;
namespace ITBOX_GAME
{
    class Program
    {
        static void Main(string[] args)
        {
         
            using(WindowItBox window=new WindowItBox())
            {
                window.Run(30, 30);
            }
            
          
        }
    }
}
