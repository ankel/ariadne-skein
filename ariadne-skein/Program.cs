﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ariadne_skein
{
    class Program
    {
        static void Main(string[] args)
        {
            // read input file
            System.IO.StreamReader input = new System.IO.StreamReader(args[0]);
            
            String line;
            int row = 0, col = 0;
            int i, j;
            Point? start = null,
                    end = null;

            while ((line = input.ReadLine()) != null)
            {
                ++row;
                col = Math.Max(col, line.Length);
            }
            input.Close();

            char[,] maze = new char[row, col];
            for (j = 0; j < col; ++j)
            {
                for (i = 0; i < row; ++i)
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
                    maze[i, j++] = c;
                    if (c == 's')
                    {
                        start = new Point(i, j - 1);
                    }
                    if (c == 'e')
                    {
                        end = new Point(i, j--);
                    }
                }
                ++i;
                j = 0;
            }

            if (start == null || end == null)
            {
                Console.WriteLine("Need start and end point");
                Environment.Exit(1);
            }

            Queue<BreadCrumb> willVisit = new Queue<BreadCrumb>();
            HashSet<Point> hasVisited = new HashSet<Point>();

            willVisit.Enqueue(new BreadCrumb(start.GetValueOrDefault()));
            hasVisited.Add(start.GetValueOrDefault());

            while (willVisit.Count != 0)
            {
                BreadCrumb bc = willVisit.Dequeue();
                Point current = bc.Current;
                if (current == end)
                {
                    Console.WriteLine("Found!");
                    //foreach (var 
                    return;
                }
                foreach (var p in NeighborOf(maze, current))
                {
                    if (!hasVisited.Contains(p))
                    {
                        hasVisited.Add(p);
                        willVisit.Enqueue(new BreadCrumb(current, bc));
                    }
                }
            }

        }

        private static IEnumerable<Point> NeighborOf(char[,] maze, Point point)
        {
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    try
                    {
                        int t = maze[point.X - x, point.Y - y];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }
                    yield return new Point(point.X - x, point.Y - y);
                }
            }
            yield break;
        }
    }
}
