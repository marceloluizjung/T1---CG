/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
// #define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
    class Mundo : GameWindow
    {
        private static Mundo instanciaMundo = null;

        private Mundo(int width, int height) : base(width, height) { }

        public static Mundo GetInstance(int width, int height)
        {
            if (instanciaMundo == null)
                instanciaMundo = new Mundo(width, height);
            return instanciaMundo;
        }

        protected List<Objeto> objetosLista = new List<Objeto>();
        private ObjetoGeometria objetoSelecionado = null;
        private bool bBoxDesenhar = false;
        int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
        private bool mouseMoverPto = false;
        private Retangulo obj_Retangulo;
        private BeginMode beginMode;
        private int keyControll = 0;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");
            GL.ClearColor(Color.Gray);
            this.beginMode = BeginMode.Points;
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-400, 400, -400, 400, -1, 1);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.PointSize(3);
            GL.LineWidth(3);

            //Eixo X
            GL.Color3(Color.Red);
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(200, 0);
            GL.End();
            //Eixo Y
            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 200);
            GL.End();

            GL.Begin(beginMode);

                GL.Color3(0.0, 0.0, 0.0);
                GL.Vertex2(200, -200);
                GL.Color3(255.0, 255.0, 0.0);
                GL.Vertex2(-200, -200);

                GL.Color3(0.0, 255.0, 255.0);
                GL.Vertex2(-200, 200);
                GL.Color3(255.0, 0.0, 255.0);
                GL.Vertex2(200, 200);

            GL.End();

            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.W)
            {
                Console.WriteLine("--- Objetos / Pontos: ");
                for (var i = 0; i < objetosLista.Count; i++)
                {
                    Console.WriteLine(objetosLista[i]);
                }
            }
            else if (e.Key == Key.Space) // Alterar
            {
                this.keyControll++;
                if (this.keyControll == 1)
                {
                    this.beginMode = BeginMode.Lines;
                }
                else if (this.keyControll == 2)
                {
                    this.beginMode = BeginMode.LineLoop;
                }
                else if (this.keyControll == 3)
                {
                    this.beginMode = BeginMode.LineStrip;
                }
                else if (this.keyControll == 4)
                {
                    this.beginMode = BeginMode.Triangles;
                }
                else if (this.keyControll == 5)
                {
                    this.beginMode = BeginMode.Triangles;
                }
                else if (this.keyControll == 6)
                {
                    this.beginMode = BeginMode.Quads;
                }
                else if (this.keyControll == 7)
                {
                    this.beginMode = BeginMode.TriangleFan;
                }
                else if (this.keyControll == 8)
                {
                    this.beginMode = BeginMode.QuadStrip;
                }
                else if (this.keyControll == 9)
                {
                    this.beginMode = BeginMode.Quads;
                }
                else
                {
                    this.keyControll = 0;
                    this.beginMode = BeginMode.Points;
                }
            }
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

        //TODO: não está considerando o NDC
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
            if (mouseMoverPto && (objetoSelecionado != null))
            {
                objetoSelecionado.PontosUltimo().X = mouseX;
                objetoSelecionado.PontosUltimo().Y = mouseY;
            }
        }

#if CG_Gizmo
        private void Sru3D()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
            GL.End();
        }
#endif
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mundo window = Mundo.GetInstance(600, 600);
            window.Title = "CG-N2";
            window.Run(1.0 / 60.0);
        }
    }
}
