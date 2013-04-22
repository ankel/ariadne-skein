using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ariadne_Skein;
using System.Collections;

namespace Ariadne_Skein_Test {
    [TestClass]
    public class PriorityQueueTest {
        class MyComparable : IComparable<MyComparable> {
            public int i, j;

            public MyComparable ( int i, int j ) {
                this.i = i;
                this.j = j;
            }

            public override string ToString () {
                return "i = " + i + ", j = " + j;
            }

            #region IComparable<MyComparable> Members

            int IComparable<MyComparable>.CompareTo ( MyComparable other ) {
                return j.CompareTo (other.j);
            }

            #endregion
        }
        [TestMethod]
        public void TestMethod1 () {
            PriorityQueue<MyComparable> q = new PriorityQueue<MyComparable> ();
            int j = 3;
            for ( int i = 0; i < 10; ++i ) {
                q.Enqueue (new MyComparable (i, j));
                if ( i % 2 == 0 ) {
                    --j;
                }
            }
            MyComparable[] arr = new MyComparable[q.Count];
            ( (ICollection) q ).CopyTo (arr, 0);
            for ( int i = 0; i < q.Count - 1; ++i ) {
                if ( arr[i].j > arr[i + 1].j ) {
                    Assert.Fail ();
                }
            }
        }
    }
}
