using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class VirtualList<T> : IList<T>
    {
        private Func<int, T> materializer;

        public VirtualList(Func<int, T> materializer, int count)
        {
            this.materializer = materializer;
            this.Count = count;
        }

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public int Count { get; }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                Debug.Print("Yielding {0}", i);
                yield return materializer(i);
            }
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
