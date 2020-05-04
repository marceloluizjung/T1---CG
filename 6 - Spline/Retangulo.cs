/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using OpenTK;

namespace gcgcg
{
    internal class Retangulo : ObjetoGeometria
    {
        private Color color;
        private int lineWidth;
        private Ponto4D ptoInfEsq;
        public Ponto4D PtoInfEsq
        {
            get { return ptoInfEsq; }
            set { ptoInfEsq = value; }
        }
        private Ponto4D ptoSupDir;
        public Ponto4D PtoSupDir
        {
            get { return ptoSupDir; }
            set { ptoSupDir = value; }
        }
        public Retangulo(string rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir, Color color, int lineWidth) : base(rotulo, paiRef, BeginMode.LineLoop)
        {
            this.ptoInfEsq = ptoInfEsq;
            this.ptoSupDir = ptoSupDir;
            this.lineWidth = lineWidth;
            this.color = color;
            base.PontosAdicionar(ptoInfEsq);
            base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
            base.PontosAdicionar(ptoSupDir);
            base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
        }

        protected override void DesenharObjeto()
        {
            GL.LineWidth(this.lineWidth);
            GL.Color3(color);
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
            }
            GL.End();
        }
        //TODO: melhorar para exibir não só a lsita de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }

    }
}