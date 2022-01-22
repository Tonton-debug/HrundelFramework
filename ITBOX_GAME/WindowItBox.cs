using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using HrundelFramework;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using System.Windows;
namespace ITBOX_GAME
{
    class WindowItBox:GameWindow
    {
        Map map;
       
        public WindowItBox()
        {
            Load += WindowItBox_Load;
            RenderFrame += WindowItBox_RenderFrame;
            Resize += WindowItBox_Resize;
            KeyDown += WindowItBox_KeyDown;
        }

        private void WindowItBox_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
           
              
        }

        private void WindowItBox_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0,0,Width, Height);
        }

        private void WindowItBox_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(0.5f, 0.5f, 0.5f,1);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            MapManager.RenderingMap();
           

            SwapBuffers();
        }

        private void WindowItBox_Load(object sender, EventArgs e)
        {
            MapManager.LoadAllMap();
           MapManager.SetCurrentMap("level_1");
          
        }
    }
}
