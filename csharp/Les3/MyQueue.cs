using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les3
{
    internal class MyQueue<T>
    {
        List<T> list = new List<T>();
        public MyQueue() {
           
        }

        public void Add(T item) {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public T NextOut() {
            T first = list[0];
            list.RemoveAt(0);
            return first;

        }

        public override string ToString()
        {
            string output = "";
            for(int i=0; i<list.Count; i++)
            {
                output += "\n" + list[i].ToString();
            }
            return output;
        }

        
    }
}
