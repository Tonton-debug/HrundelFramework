using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK.Graphics;
namespace ProjectLibrary
{
    [Serializable]
    public  class EntityCharacteristics
    {
        public TypeEntity MyType { get; set; }

        public Color4 MyColor { get; set; }
        public readonly string Name;
        private Shader _myShader=null;
        private float POSITION_VERTICES = 0.5f;
        private uint[] _indices = {
    0, 1, 3,
    1, 2, 3
                };
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;
        public EntityCharacteristics(string name)
        {
            Name = name;
            _myShader = new Shader("shader.vert", "shader.frag");
          
        }
        public void SetShader()
        {
            _myShader = new Shader("shader.vert", "shader.frag");
        }
        private void Clear()
        {
       //     GL.DeleteBuffer(_vertexArrayObject);
       //     GL.DeleteBuffer(_elementBufferObject);
            //      _myShader.DeleteProgram();
        }
        private float[] GetVertices()
        {
            return new float[] {
                POSITION_VERTICES, POSITION_VERTICES, 0.0f,
                POSITION_VERTICES, -POSITION_VERTICES, 0.0f,
                -POSITION_VERTICES, -POSITION_VERTICES, 0.0f,
                -POSITION_VERTICES, POSITION_VERTICES, 0.0f
            };
        }
        public void Rendering(Matrix4 orthoMatrix,Vector2 Position,Vector2 Scale)
        {
          
            _myShader.CreateProgram();
            _vertexBufferObject = GL.GenBuffer();
            _elementBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BindVertexArray(_vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, GetVertices().Length * sizeof(float), GetVertices(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            _myShader.Use();
            int id = _myShader.GetUniform("ourColor");
            GL.Uniform4(id, MyColor.R, MyColor.G, MyColor.B, MyColor.A);
          
            Matrix4 transform = Matrix4.CreateScale(Scale.X, Scale.Y, 0) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(0)) * Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, 0)) * orthoMatrix;
            _myShader.SetUniform4(transform, "transform");
            _myShader.SetUniform4(orthoMatrix, "ortho");
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0); 
            Clear();
        }
        
    }
}
