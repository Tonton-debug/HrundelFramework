using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
namespace ProjectLibrary
{
    [Serializable]
    public sealed class Camera
    {
        public readonly float Size;
        private bool _narrowDown = false;
        public Matrix4 GetOrthoMatrix()
        {
            if (!_narrowDown)
                return Matrix4.CreateOrthographicOffCenter(-Size, Size, -Size, Size, 1, -1);
            else
                return Matrix4.CreateOrthographicOffCenter(-Size , Size , -Size / 2, Size / 2, 1, -1);
        }
        public Camera(float size, bool narrowDown = false)
        {
            Size = size;
            _narrowDown = narrowDown;
        }
    }
}
