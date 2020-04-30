using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    class SegReta
    {

        public static void drawTriangle(Ponto4D ponto1, Ponto4D ponto2, Ponto4D ponto3, Color color, PrimitiveType primitiveType)
        {
            GL.Color3(Color.Aqua);
            //P1 / P2
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(ponto1.X, ponto1.Y);
            GL.Vertex2(ponto2.X, ponto2.Y);
            GL.End();
            //P2 / P3
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(ponto2.X, ponto2.Y);
            GL.Vertex2(ponto3.X, ponto3.Y);
            GL.End();
            //P3 / P1
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(ponto3.X, ponto3.Y);
            GL.Vertex2(ponto1.X, ponto1.Y);
            GL.End();
        }

        public static void dynamicLine(Ponto4D ponto4DBase, Color color, int size, int rotationGrados)
        {
            GL.Color3(color);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(ponto4DBase.X, ponto4DBase.Y);

            double degInRad = rotationGrados * 3.1416 / 180;
            GL.Vertex2(Math.Cos(degInRad) * size + ponto4DBase.X, Math.Sin(degInRad) * size + ponto4DBase.Y);
            GL.End();

        }

    }
}