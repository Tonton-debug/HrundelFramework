using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace HrundelFramework
{
    public struct ColorF
    {
        //private float _r;
        //private float _g;
        //private float _b;
        //private float _a;
        public float R;
        public float G;
        public float B;
        public float A;
        public ColorF(float r,float g,float b,float a)
        {
            if (r > 1 || g > 1 || b > 1 || a > 1)
                throw new Exception("Max value 1");
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
    public struct EntityProperties
    {
        public readonly Vector2 Position;
        public readonly Vector2 Scale;
        public readonly string Name;
        public readonly ColorF Color;
        public EntityProperties(Vector2 position,Vector2 scale,string name, ColorF color)
        {
            Position = position;
            Name = name;
            Scale = scale;
            Color = color;
        }
    }
 public  abstract class Entity
    {
        private Shader _myShader;
      
       
        private float POSITION_VERTICES = 0.5f;
        private 
        uint[] _indices = {
    0, 1, 3,
    1, 2, 3
                };
        private int _vertexBufferObject;
       private int _vertexArrayObject;
        private int _elementBufferObject;
        private Vector2 _position;
        private float _rotate;
        private Vector2 _scale;
        private ColorF _color;
        public virtual Vector2 Position { get => _position; protected set => _position = value; }
        public virtual float Rotate { get => _rotate; protected set => _rotate = value; }
      
        public virtual Vector2 Scale { get => _scale; protected set => _scale = value; }
        public virtual ColorF Color { get => _color; protected set => _color = value; }
        public string Name { get; private set; }
        protected Map MyMap { get; private set; }

        public Entity()
        {
            Name = DateTime.Now.ToBinary().ToString();
        }
        internal void ChangeProperties(EntityProperties entityProperties)
        {
            Position = entityProperties.Position;
            Scale = entityProperties.Scale;
            Name = entityProperties.Name;
            Color = entityProperties.Color;
        }

       
        private void GenBuffersAndGetShader()
        {
            _myShader = new Shader("shader.vert", "shader.frag");
          
        }
        public void Clear()
        {
            GL.DeleteBuffer(_vertexArrayObject);
            GL.DeleteBuffer(_elementBufferObject);
            _myShader.DeleteProgram();
        }
        public virtual float[] GetVertices()
        {
         return   new float[] {
                POSITION_VERTICES, POSITION_VERTICES, 0.0f,
                POSITION_VERTICES, -POSITION_VERTICES, 0.0f,
                -POSITION_VERTICES, -POSITION_VERTICES, 0.0f,
                -POSITION_VERTICES, POSITION_VERTICES, 0.0f
            };
        }
            
        public void Rendering(Matrix4 orthoMatrix)
        {
                LateUpdate();
            _myShader.CreateProgram();
            _vertexBufferObject = GL.GenBuffer();
            _elementBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BindVertexArray(_vertexArrayObject);
            GL.BufferData(BufferTarget.ArrayBuffer, GetVertices().Length * sizeof(float), GetVertices(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            _myShader.Use();
            int id = _myShader.GetUniform("ourColor");
            GL.Uniform4(id,_color.R, _color.G, _color.B, _color.B);
            _myShader.SetUniform4(orthoMatrix,"ortho");
            Matrix4 transform = Matrix4.CreateScale(_scale.X, _scale.Y, 0) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_rotate)) * Matrix4.CreateTranslation(new Vector3(_position.X, _position.Y, 0))*orthoMatrix;
            _myShader.SetUniform4(transform, "transform");
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            Clear();
            Update();
        }
        public virtual void Update()
        {
        }
        public virtual void LateUpdate()
        {
           
            
        }
        public virtual void Load()
        {
            GenBuffersAndGetShader();
        }
        internal void Load(Map map)
        {
            MyMap = map;
            Load();
        }
    }
}
