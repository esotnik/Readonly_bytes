using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes: IEnumerable<byte>
    {
        private readonly byte[] items;
        private readonly int length;
        private int hash = -1;
        public int Length { get { return length; } }

        public ReadonlyBytes(params byte[] p)
        {
            if (p == null) throw new ArgumentNullException();
            items = new byte[p.Length];
            Array.Copy(p, items, p.Length);
            length = items.Length;
        }

        public byte this[int index]
        {
            get 
            {
                if (index < 0 || index > length - 1) throw new IndexOutOfRangeException();
                return items[index]; 
            }
        }

        /*public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes) || obj is null || obj.GetType().Name != "ReadonlyBytes") return false;
            if (!GetHashCode().Equals(obj.GetHashCode())) return false;
            var that = obj as ReadonlyBytes;
            if (items.Length != that.items.Length)
                return false;
            var a = GetEnumerator();
            var b = that.GetEnumerator();
            for (; ; )
            {
                bool hasA = a.MoveNext();
                bool hasB = b.MoveNext();
                if (hasA != hasB) return false;
                if (!hasA) return true;
                if (a.Current != b.Current) return false;
            }
        }*/

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes) || obj is null || obj.GetType().Name != "ReadonlyBytes") return false; 
            var arr = obj as ReadonlyBytes;
            if (arr.Length != items.Length) return false;
            for (var i = 0; i < items.Length; i++)
                if (items[i] != arr[i]) return false;
            return true;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i <= length - 1; i++)
                yield return items[i];
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public override int GetHashCode()
        {
            const long prime = 17; 
            const long prime1 = 2147483029;
            const long prime2 = 331;
            if (hash == -1)
            {
                long t = prime;
                foreach (var e in items)
                    t = (t * prime2 +  e) % prime1;
                hash = (int)t;
            }
            return hash;
        }

        public override string ToString()
        {
            return String.Format("[{0}]", String.Join(", ", items));
        }
    }
}