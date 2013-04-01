using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ariadne_skein
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader input = new System.IO.StreamReader(args[0]);
            
            String line;
            int w = 0, h = 0;
            int i, j;

            while ((line = input.ReadLine()) != null)
            {
                ++h;
                w = Math.Max(w, line.Length);
            }
            input.Close();

            char[,] maze = new char[w, h];
            for (j = 0; j < h; ++j)
            {
                for (i = 0; i < w; ++i)
                {
                    maze[i, j] = ' ';
                }
            }
            
            input = new System.IO.StreamReader(args[0]);
            i = 0;
            j = 0;
            while ((line = input.ReadLine()) != null)
            {
                foreach (var c in line)
                {
                    maze[w, h++] = c;
                }
                ++w;
                h = 0;
            }
            
        }
    }
}
