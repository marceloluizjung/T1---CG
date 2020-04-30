using System;
using OpenTK;

namespace OlaMundo
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(600, 600);
            window.Run(1.0 / 60.0);
            Console.WriteLine("Hello World!");
        }
    }
}
