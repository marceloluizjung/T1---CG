using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    class Spline : ObjetoGeometria
    {
        private Color color;
        private int lineWidth;
        private Ponto4D Panterior;
        private Ponto4D PEsqSup;
        private Ponto4D PDirSup;
        private int quantidadePontos;
        private int selecionado;

        public Spline(string rotulo, Objeto paiRef, Ponto4D pontoEsq, Ponto4D pontoDir, int lineWidth, Color color) : base(rotulo, paiRef, BeginMode.Lines)
        {
            this.color = color;
            this.lineWidth = lineWidth;
            this.quantidadePontos = 100;
            this.selecionado = selecionado;
            
            base.PontosAdicionar(new Ponto4D(pontoEsq.X, pontoEsq.Y));
            this.PEsqSup = new Ponto4D(pontoEsq.X, 150);
            base.PontosAdicionar(this.PEsqSup);
            
            this.PDirSup = new Ponto4D(pontoDir.X, 150);
            base.PontosAdicionar(this.PDirSup);
            base.PontosAdicionar(new Ponto4D(pontoDir.X, pontoDir.Y));
        }

        protected Ponto4D Inter(Ponto4D pontoA, Ponto4D pontoB, int i, int desenha) 
        {
            Ponto4D ponto = new Ponto4D(0,0);
            ponto.X = pontoA.X + (pontoB.X - pontoA.X) * i/this.quantidadePontos;
            ponto.Y = pontoA.Y + (pontoB.Y - pontoA.Y) * i/this.quantidadePontos;
            if (desenha == 1) {
                GL.LineWidth(this.lineWidth);
                GL.Begin(BeginMode.Points);
                GL.Color3(this.color);
                GL.Vertex2(ponto.X-2, ponto.Y-2);
                GL.End();
            }
            return ponto;
        }

        public void goCima()
        {
            pontosLista[this.selecionado].Y += 2;
            Console.WriteLine(pontosLista[this.selecionado].Y);
        }

        public void goBaixo() 
        {
            pontosLista[this.selecionado].Y -= 2;
            Console.WriteLine(pontosLista[this.selecionado].Y);
        }

        public void goDireita()
        {
            pontosLista[this.selecionado].X += 2;
            Console.WriteLine(pontosLista[this.selecionado].X);
        }

        public void goEsquerda()
        {
            pontosLista[this.selecionado].X -= 2;
            Console.WriteLine(pontosLista[this.selecionado].X);
        }

        public void addPontos()
        {
            this.quantidadePontos ++;
            Console.WriteLine(this.quantidadePontos);
        }

        public void subPontos()
        {
            this.quantidadePontos --;
            Console.WriteLine(this.quantidadePontos);
        }

        public void changePonto(int ponto)
        {
            if (this.selecionado >= 4)
            {
                this.selecionado = 0;
            } 
            else 
            {
                this.selecionado = ponto;
            }
            Console.WriteLine(this.selecionado);
        }

        protected override void DesenharObjeto()
        {
            this.Panterior = pontosLista[0];
            Ponto4D P1P2;
            Ponto4D P2P3;
            Ponto4D P3P4;
            Ponto4D P1P2P3;
            Ponto4D P2P3P4;
            Ponto4D P1P2P3P4;

            for (var i = 0; i < this.quantidadePontos; i++)
            {
                P1P2 = Inter(pontosLista[0], pontosLista[1], i, 1);
                P2P3 = Inter(pontosLista[1], pontosLista[2], i, 1);
                P3P4 = Inter(pontosLista[2], pontosLista[3], i, 1);
                P1P2P3 = Inter(P1P2, P2P3, i, 1);
                P2P3P4 = Inter(P2P3, P3P4, i, 1);
                
                P1P2P3P4 = Inter(P1P2P3, P2P3P4, i, 0);

                GL.LineWidth(this.lineWidth);
                GL.Begin(BeginMode.Lines);
                GL.Color3(Color.Blue);
                GL.Vertex2(this.Panterior.X, this.Panterior.Y);
                GL.Vertex2(P1P2P3P4.X, P1P2P3P4.Y);
                GL.End();

                this.Panterior = P1P2P3P4;

            }
        }

        public override string ToString()
        {
            return "TODO";
        }

    }
}