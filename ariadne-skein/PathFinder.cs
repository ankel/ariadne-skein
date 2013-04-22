using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ariadne_skein {
    abstract class PathFinder {
        protected int maxX, maxY;

        protected const char START    = 's';
        protected const char END      = 'e';
        protected const char VISITED  = 'v';
        protected const char WALL     = 'w';
        protected const char EMPTY    = ' ';

        protected Point start, end;

        public char[,] Map {
            get;
            protected set;
        }

        public PathFinder () {
            throw new NotSupportedException ("Algorithm class must be initialize with a map");
        }

        public PathFinder ( char[,] map ) {
            Array.Copy (map, Map, map.Length);
            maxX = map.GetLength (0);
            maxY = map.GetLength (1);
            bool foundStart = false, foundEnd = false;
            for ( int i = 0; i < maxX; ++i ) {
                for ( int j = 0; j < maxY; ++j ) {
                    if ( !foundStart && Map[i, j] == START ) {
                        foundStart = true;
                        start = new Point (i, j);
                        continue;
                    }
                    if ( !foundEnd && Map[i, j] == END ) {
                        foundEnd = true;
                        end = new Point (i, j);
                        continue;
                    }
                    if ( Map[i, j] != EMPTY && Map[i,j] != WALL) {
                        throw new ArgumentException ("Map contains nonsupported character at " + i + ", " + j);
                    }
                }
            }
        }

        /// <summary>
        /// Get the itterable-list of neighbour cells of a given point p
        /// </summary>
        /// <param name="p">point p</param>
        /// <returns>list of p's visit-able neighbor</returns>
        public virtual IEnumerable<Point> neigbourCell ( Point p ) {
            int x = p.X, y = p.Y;
            for ( int i = -1; i <= 1; ++i ) {
                if ( i == 0 ) {
                    continue;
                }
                int newX = x + i;
                if ( newX < 0 || newX >= maxX ) {
                    continue;
                }
                for ( int j = -1; j <= 1; ++j ) {
                    if ( j == 0 ) {
                        continue;
                    }
                    int newY = y + j;
                    if ( newY < 0 || newY >= maxY ) {
                        continue;
                    }
                    if ( Map[newX, newY] != VISITED ) {
                        yield return new Point (newX, newY);
                    }
                }
            }
            yield break;
        }

        public abstract bool PointLeft ();

        public abstract bool IsGoal ( Point p );

        public abstract void Add2Visited ( Point p );

        public abstract void Add2ToBeVisited ( Point p );
    } // abstract class PathFinder
}
