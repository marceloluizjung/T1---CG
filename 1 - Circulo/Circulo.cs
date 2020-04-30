using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace gcgcg 
{
    class Circulo
    {

        public static void drawCircle(Color color, int lineWith, int lineStrip, int radius)
        {
            GL.Color3(color);
            GL.PointSize(lineWith);
            GL.Begin(BeginMode.Points);
            for (int i = 0; i < 360; i = i + lineStrip)
            {
                double degInRad = i * 3.1416 / 180;
                GL.Vertex2(Math.Cos(degInRad) * radius, Math.Sin(degInRad) * radius);
            }
            GL.End();
        }
    }
}
