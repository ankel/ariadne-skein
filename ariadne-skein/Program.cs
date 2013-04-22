using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ariadne_Skein {
    public class Program {
        static void Main ( string[] args ) {
            // read input file
            System.IO.StreamReader input = new System.IO.StreamReader (args[0]);

            String line;
            int row = 0, col = 0;
            int i, j;
            Point? start = null,
                    end = null;

            while ( ( line = input.ReadLine () ) != null ) {
                ++row;
                col = Math.Max (col, line.Length);
            }
            input.Close ();

            char[,] maze = new char[row, col];
            for ( j = 0; j < col; ++j ) {
                for ( i = 0; i < row; ++i ) {
                    maze[i, j] = ' ';
                }
            }

            input = new System.IO.StreamReader (args[0]);
            i = 0;
            j = 0;
            while ( ( line = input.ReadLine () ) != null ) {
                foreach ( var c in line ) {
                    maze[i, j++] = c;
                    if ( c == 's' ) {
                        start = new Point (i, j - 1);
                    }
                    if ( c == 'e' ) {
                        end = new Point (i, j--);
                    }
                }
                ++i;
                j = 0;
            }

            if ( start == null || end == null ) {
                Console.WriteLine ("Need start and end point");
                Environment.Exit (1);
            }

            Queue<BreadCrumb> willVisit = new Queue<BreadCrumb> ();
            HashSet<Point> hasVisited = new HashSet<Point> ();

            willVisit.Enqueue (new BreadCrumb (start.GetValueOrDefault ()));
            hasVisited.Add (start.GetValueOrDefault ());

            while ( willVisit.Count != 0 ) {
                BreadCrumb bc = willVisit.Dequeue ();
                Point current = bc.Current;
                if ( current == end ) {
                    Console.WriteLine ("Found!");
                    Stack<Point> path = bc.BackTrack ();
                    while ( path.Count != 0 ) {
                        Console.WriteLine (path.Pop ().ToString ());
                    }
                    Console.ReadLine ();
                    return;
                }
                foreach ( var p in NeighborOf (maze, current) ) {
                    if ( !hasVisited.Contains (p) ) {
                        hasVisited.Add (p);
                        willVisit.Enqueue (new BreadCrumb (p, bc));
                    }
                }
            }

            Console.WriteLine ("Not found!");
            Console.ReadLine ();
        }

        private static IEnumerable<Point> NeighborOf ( char[,] maze, Point point ) {
#if DEBUG
            Console.WriteLine ("New NeighborOf");
#endif
            for ( int x = -1; x <= 1; ++x ) {
                int newX = point.X + x;
#if DEBUG
                Console.WriteLine ("NewX: " + newX);
#endif
                if ( newX < 0 || newX > maze.GetLength (0) ) {
                    continue;
                }
                for ( int y = -1; y <= 1; ++y ) {
                    int newY = point.Y + y;
#if DEBUG
                    Console.WriteLine ("NewY: " + newY);
#endif
                    if ( newY < 0 || newY > maze.GetLength (1) ) {
                        continue;
                    }
                    if ( x == 0 && y == 0 ) {
                        continue;
                    }
#if DEBUG
                    Console.WriteLine ("Yield!");
#endif
                    yield return new Point (newX, newY);
                }
            }
            yield break;
        }
    }
}
