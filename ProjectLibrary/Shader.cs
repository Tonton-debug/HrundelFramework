using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary
{
    [Serializable]
    public sealed class Shader:IDisposable
    {
        private int _handle;
        private int _vertexShader;
        private int _fragmentShader;
        private bool disposedValue = false;
        public Shader(string vertexPath, string fragmentPath)
        {
            GenerateShader(ShaderType.VertexShader, vertexPath, out _vertexShader);
            GenerateShader(ShaderType.FragmentShader, fragmentPath, out _fragmentShader);
            CreateProgram();
        }  
        public void CreateProgram()
        {
            _handle = GL.CreateProgram();
            GL.AttachShader(_handle, _vertexShader);
            GL.AttachShader(_handle, _fragmentShader);
            GL.LinkProgram(_handle);
        }
        public void DeleteProgram()
        {
                GL.DeleteProgram(_handle);
        }
        public void Dispose()
        {
          
            GC.SuppressFinalize(this);
        }
       
        public int GetUniform(string name)
        {
            return GL.GetUniformLocation(_handle, name);
        }
        public void SetUniform4(Matrix4 matrix,string name)
        {
            int location = GL.GetUniformLocation(_handle, name);

            GL.UniformMatrix4(location, true, ref matrix);
        }
        
        public void Use()
        {
            GL.UseProgram(_handle);
        }
        private void GenerateShader(ShaderType shaderType,string path,out int _vertexId)
        {
            string ShaderSource;
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                ShaderSource = reader.ReadToEnd();
            }
            _vertexId = GL.CreateShader(shaderType);
            GL.ShaderSource(_vertexId, ShaderSource);
            GL.CompileShader(_vertexId);
            string infoLogVert = GL.GetShaderInfoLog(_vertexId);
            if (infoLogVert != string.Empty)
                Console.WriteLine(infoLogVert);
        }
    }
}
