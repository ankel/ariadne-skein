using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ariadne_skein
{
    class BreadCrumb
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

        public List<Point> backTrack()
        {
            List<Point> retVal = new List<Point>();
            retVal.Add(Current);
            BreadCrumb temp = ComeFrom;
            while (temp != null)
            {
                retVal.Add(temp.Current);
                temp = temp.ComeFrom;
            }
            return retVal;
        }
    }
}
