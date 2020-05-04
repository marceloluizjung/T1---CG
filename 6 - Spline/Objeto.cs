/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal abstract class Objeto
  {
    protected string rotulo;
    protected BeginMode beginMode;
    protected BeginMode PrimitivaTipo { get => beginMode; set => beginMode = value; }
    private float primitivaTamanho = 2;
    protected float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }
    private BBox bBox = new BBox();
    public BBox BBox { get => bBox; set => bBox = value; }
    private List<Objeto> objetosLista = new List<Objeto>();

    public Objeto(string rotulo, Objeto paiRef, BeginMode beginMode)
    {
      this.rotulo = rotulo;
      this.beginMode = beginMode;
    }

    public void Desenhar()
    {
      DesenharGeometria();
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }
    }
    protected abstract void DesenharGeometria();
    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }
    public void FilhoRemover(Objeto filho)
    {
      this.objetosLista.Remove(filho);
    }
  }
}