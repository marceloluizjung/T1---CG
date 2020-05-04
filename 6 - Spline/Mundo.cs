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
        private Ponto4D ponto4DBase = new Ponto4D();
        private Spline spline;

        private Ponto4D pontoEsq;
        private Ponto4D pontoDir;

        private Color colorRetangulo;
        private Circulo circuloMaior;
        private Circulo circuloMenor;
        private Retangulo retangulo;
        private Ponto4D centerCirculoMenor;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");
            GL.ClearColor(Color.Gray);
            centerCirculoMenor = new Ponto4D(0, 0);
            this.circuloMaior = new Circulo(null, null, Color.Black, 3, 1, 100, new Ponto4D(0, 0), BeginMode.LineLoop);
            colorRetangulo = Color.Red;

            this.pontoEsq = new Ponto4D(-100, 0);
            this.pontoDir = new Ponto4D(100, 0);
            this.spline = new Spline(null, null, pontoEsq, pontoDir, 2, Color.Yellow);
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

            //Eixo X
            GL.Color3(Color.Red);
            GL.LineWidth(3);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(300, 0);
            GL.End();

            //Eixo Y
            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 300);
            GL.End();

            this.spline.Desenhar();

            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.T)
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
            else if (e.Key == Key.E)
            {
                this.spline.goEsquerda();
            }
            else if (e.Key == Key.D)
            {
                this.spline.goDireita();
            }
            else if (e.Key == Key.C)
            {
                this.spline.goCima();
            }
            else if (e.Key == Key.B)
            {
                this.spline.goBaixo();
            }
            else if (e.Key == Key.KeypadPlus)
            {
                this.spline.addPontos();
            }
            else if (e.Key == Key.KeypadMinus)
            {
                this.spline.subPontos();
            }
            else if (e.Key == Key.Keypad1)
            {
                this.spline.changePonto(0);
            }
            else if (e.Key == Key.Keypad2)
            {
                this.spline.changePonto(1);
            }
            else if (e.Key == Key.Keypad3)
            {
                this.spline.changePonto(2);
            }
            else if (e.Key == Key.Keypad4)
            {
                this.spline.changePonto(3);
            }
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

        //TODO: não está considerando o NDC
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {

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
