using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg 
{
    class Camera
    {

        private CameraOrtho camera = new CameraOrtho();
        private double quantidade = 2;

        public void setInicial(double xmin, double ymin, double xmax, double ymax) 
        {
            this.camera.xmin = xmin;
            this.camera.ymin = ymin;
            this.camera.xmax = xmax;
            this.camera.ymax = ymax;
        }

        public double getXmin() {
           return camera.xmin;
        }

        public double getXmax() {
           return camera.xmax;
        }

        public double getYmin() {
           return camera.ymin;
        }

        public double getYmax() {
           return camera.ymax;
        }

        public void goEsquerda() 
        {
            this.camera.xmin += quantidade;
            this.camera.xmax += quantidade;
        }

        public void goDireita() 
        {
            this.camera.xmin -= quantidade;
            this.camera.xmax -= quantidade;
        }

        public void goCima() 
        {
            this.camera.ymin += quantidade;
            this.camera.ymax += quantidade;
        }

        public void goBaixo() 
        {
            this.camera.ymin -= quantidade;
            this.camera.ymax -= quantidade;
        }

        public void goZoomIn() 
        {
            this.camera.ymin += quantidade;
            this.camera.ymax -= quantidade;
            this.camera.xmin += quantidade;
            this.camera.xmax -= quantidade;
        }

        public void goZoomOut() 
        {
            this.camera.ymin -= quantidade;
            this.camera.ymax += quantidade;
            this.camera.xmin -= quantidade;
            this.camera.xmax += quantidade;
        }        
    }
}
