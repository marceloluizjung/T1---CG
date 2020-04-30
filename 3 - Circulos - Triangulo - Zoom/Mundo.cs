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

        private CameraOrtho camera = new CameraOrtho();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private ObjetoGeometria objetoSelecionado = null;
        private bool bBoxDesenhar = false;
        int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
        private bool mouseMoverPto = false;
        private Retangulo obj_Retangulo;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");
            GL.ClearColor(Color.Gray);
            camera.xmin = -150;
            camera.ymin = -150;
            camera.xmax = 150;
            camera.ymax = 150;
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, -1, 1);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            //Eixo X
            GL.Color3(Color.Red);
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(100, 0);
            GL.End();
            //Eixo Y
            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 100);
            GL.End();

            //Circulos
            Ponto4D center1 = new Ponto4D();
            center1.X = 0;
            center1.Y = 50;
            Circulo.drawCircle(Color.Black, 3, 5, 50, center1);
            Ponto4D center2 = new Ponto4D();
            center2.X = 50;
            center2.Y = -50;
            Circulo.drawCircle(Color.Black, 3, 5, 50, center2);
            Ponto4D center3 = new Ponto4D();
            center3.X = -50;
            center3.Y = -50;
            Circulo.drawCircle(Color.Black, 3, 5, 50, center3);

            //Triângulo
            Ponto4D ponto1 = new Ponto4D();
            ponto1.X = 0;
            ponto1.Y = 50;

            Ponto4D ponto2 = new Ponto4D();
            ponto2.X = 50;
            ponto2.Y = -50;
            Ponto4D ponto3 = new Ponto4D();
            ponto3.X = -50;
            ponto3.Y = -50;
            SegReta.drawTriangle(ponto1, ponto2, ponto3, Color.Aqua, PrimitiveType.Lines);

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
            else if (e.Key == Key.U)
                bBoxDesenhar = !bBoxDesenhar;
            else if (e.Key == Key.V)
                mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
            else if (e.Key == Key.C) //Cima
            {
                camera.ymin++;
                camera.ymax++;
            }
            else if (e.Key == Key.B) //Baixo
            {
                camera.ymin--;
                camera.ymax--;
            }
            else if (e.Key == Key.D) //Direita
            {
                camera.xmin--;
                camera.xmax--;
            }
            else if (e.Key == Key.E) //Esquerda
            {
                camera.xmin++;
                camera.xmax++;
            }
            else if (e.Key == Key.I) //Zoom in
            {
                camera.ymin++;
                camera.ymax--;
                camera.xmin++;
                camera.xmax--;
            }
            else if (e.Key == Key.O) //Zoom Out
            {
                camera.ymin--;
                camera.ymax++;
                camera.xmin--;
                camera.xmax++;
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
