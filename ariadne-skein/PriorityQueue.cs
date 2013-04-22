using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ariadne_Skein {
    /// <summary>
    /// This is the priority queue implemented for A* algorithm
    /// The queue returns item first by its values, then by order of it insertion.
    /// The underneath implementation is of a LinkedList so 'dequeue' should be fast but not 'enqueue'
    /// This class is not thread-safe (locking is only experimental - doesn't guarantee to work)
    /// 
    /// *Style point: make enqueue fast.
    /// </summary>
    class PriorityQueue<T> : IEnumerable<T>, ICollection, IEnumerable where T : IComparable<T> {
        LinkedList<T> list;
        object l;

        /// <summary>
        /// Instantialize the object
        /// </summary>
        public PriorityQueue () {
            l = new object ();
            list = new LinkedList<T> ();
        }

        /// <summary>
        /// Return the head of the queue without altering the queue itself.
        /// </summary>
        /// <returns>head of the queue</returns>
        public T Peek () {
            return list.First.Value;
        }

        /// <summary>
        /// Return and remove the head of the queue.
        /// </summary>
        /// <returns>head of the queue</returns>
        public T Dequeue () {
            lock ( l ) {
            T retVal = list.First.Value;
            list.RemoveFirst ();
            return retVal;                
            }
        }

        public void Enqueue (T toBeInserted) {
            lock ( l ) {
                if ( list.Count == 0 ) {
                // list is empty, doesn't matter where to add
                list.AddLast (toBeInserted);
                return;
            }

            LinkedListNode<T> node = list.First;
            while ( node.Next != null ) {
                if ( node.Value.CompareTo (toBeInserted) > 0 ) {    // look for the first object that is 'greater than' toBeInserted
                    list.AddBefore (node, toBeInserted);
                    return;
                }
            }

            // didn't return
            list.AddLast (toBeInserted);
            }
        }


        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator () {
            return list.GetEnumerator ();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
            return list.GetEnumerator ();
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo ( Array array, int index ) {
            foreach ( var item in list ) {
                array.SetValue (item, index++); // increment index after access its value
            }
        }

        int ICollection.Count {
            get { return list.Count; }
        }

        bool ICollection.IsSynchronized {
            get { return true; }
        }

        object ICollection.SyncRoot {
            get { return l; }
        }

        #endregion
    } // class PriorityQueue
}
