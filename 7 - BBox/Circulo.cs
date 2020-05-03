using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    class Circulo : ObjetoGeometria
    {
        private Color color;
        private int lineWith;
        private int radius;
        private Ponto4D center;
        public Ponto4D Center
        {
            get { return center; }
            set { center = value; }
        }
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        public Circulo(string rotulo, Objeto paiRef, Color color, int lineWith, int lineStrip, int radius, Ponto4D center, BeginMode beginMode) : base(rotulo, paiRef, beginMode)
        {
            this.center = center;
            this.radius = radius;
            this.lineWith = lineWith;
            this.color = color;
            for (int i = 0; i < 360; i = i + lineStrip)
            {
                double degInRad = i * 3.1416 / 180;
                base.PontosAdicionar(new Ponto4D(Math.Cos(degInRad) * radius + center.X, Math.Sin(degInRad) * radius + center.Y));
            }
        }

        protected override void DesenharObjeto()
        {
            GL.PointSize(this.lineWith);
            GL.Color3(this.color);
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
