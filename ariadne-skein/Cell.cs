using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ariadne_Skein {
    public class Cell : IComparable<Cell> {
        public Point Current {
            get;
            private set;
        }
        public double Score {
            get;
            private set;
        }

        public Cell ( Point p, double score ) {
            Current = p;
            Score = score;
        }

        #region IComparable<Cell> Members

        int IComparable<Cell>.CompareTo ( Cell other ) {
            return this.Score.CompareTo (other.Score);
        }

        #endregion
    }
}
