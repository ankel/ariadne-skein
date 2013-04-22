using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ariadne_Skein
{
    public class BreadCrumb
    {
        public Point Current
        {
            get;
            private set;
        }

        public BreadCrumb ComeFrom
        {
            get;
            private set;
        }

        public BreadCrumb(Point current, BreadCrumb comeFrom)
        {
            this.Current = current;
            this.ComeFrom = comeFrom;
        }

        public BreadCrumb(Point current)
        {
            Current = current;
            ComeFrom = null;
        }

        public Stack<Point> BackTrack()
        {
            Stack<Point> retVal = new Stack<Point>();
            retVal.Push(Current);
            BreadCrumb temp = ComeFrom;
            while (temp != null)
            {
                retVal.Push(temp.Current);
                temp = temp.ComeFrom;
            }
            return retVal;
        }
    }
}
