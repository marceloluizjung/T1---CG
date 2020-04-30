using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg 
{
    class Circulo
    {

        public static void drawCircle(Color color, int lineWith, int lineStrip, int radius, Ponto4D center)
        {
            GL.Color3(color);
            GL.PointSize(lineWith);
            GL.Begin(BeginMode.Points);
            for (int i = 0; i < 360; i = i + lineStrip)
            {
                double degInRad = i * 3.1416 / 180;
                GL.Vertex2(Math.Cos(degInRad) * radius + center.X, Math.Sin(degInRad) * radius + center.Y);
            }
            GL.End();
        }
    }
}
