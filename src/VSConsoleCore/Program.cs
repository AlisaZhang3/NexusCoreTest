using System;
using System.Linq;
using System.Threading.Tasks;

namespace VSOConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyValue();
            sum(5,  6);
            Foo().Wait();
            InvokeFunc(); //set breakpoint1
            VerifyException();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void VerifyValue()
        {
            float[] values = Enumerable.Range(0, 100).Select(i => (float)i / 10).ToArray();
            float[] value = values.Where(v => (int)v == 4).ToArray();

            float value1 = value[1];
            value = values.Where(v => (int)v == 3).ToArray();// decide test type
            value1 = value[1];

            GenericList<float> list = new GenericList<float>();
            foreach (float i in value)
            {
                list.AddHead(i);
            }
        }

        private static void VerifyException()
        {
            
            try
            {
                string s4 = null;
                string s5 = s4.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
        static async Task Foo()
        {
            await GenException();
        }



        static async Task<string> GenException()
        {
            await Task.Delay(1000);
            return string.Format("{1}", "abc");
        }
        private static int sum(int v1, int v2)
        {
            return v1  +  v2;
        }

        static async void InvokeFunc()
        {
            Task theTask = ProcessAsync();
            int x = 2; // assignment
            await theTask; // set breakpoint2
        }

        static async Task ProcessAsync()
        {
            var result = await DoSomethingAsync();  // set breakpoint3 

            int y = 1;  // set breakpoint4
        }

        static async Task<int> DoSomethingAsync()
        {
            int z = 5;
            await Task.Delay(5000);  // set breakpoint5

            return z;
        }

    }
    public class GenericList<T>
    {
        // The nested class is also generic on T.
        private class Node
        {
            // T used in non-generic constructor.
            public Node(T t)
            {
                next = null;
                data = t;
            }

            private Node next;

            // T as private member data type.
            private T data;
        }

        private Node head;

        // constructor
        public GenericList()
        {
            head = null;
        }

        // T as method parameter type:
        public void AddHead(T t)
        {
            Node n = new Node(t);
            head = n; // set breakpoint6

            head = null; // set breakpoint7
        }
    }
}
