using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace VSOConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyValue();
            sum(5, 6);
            VerifyVisualize();
            Foo().Wait();
            InvokeFunc(); //set breakpoint1
            VerifyException();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void VerifyVisualize()
        {
            string ht = "<html><body>hello world!</body><html>";
            string xml2 = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<configuration>\r\n    <startup> \r\n        <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.6.1\" />\r\n    </startup>\r\n</configuration>";
            string json = "{\r\n    \"name\":\"中国\",\r\n    \"province\":[\r\n    {\r\n       \"名字\":\"黑龙江\",\r\n        \"城市\":{\r\n            \"城市\":[\"海冰\",\"长春\"]\r\n        }\r\n     },\r\n      {\r\n        \"名字\": \"广东\",\r\n        \"城市\": {\r\n          \"城市\": [ \"广州\", \"深圳\", \"厦门\" ]\r\n        }\r\n      },\r\n    {\r\n        \"名字\":\"陕西\",\r\n      \"城市\": {\r\n        \"城市\": [ \"西安\", \"延安\" ]\r\n      }\r\n    },\r\n    {\r\n        \"名字\":\"甘肃\",\r\n      \"城市\": {\r\n        \"城市\": [ \"兰州\" ]\r\n      }\r\n    }\r\n]\r\n}\r\n";

            DataTable dt = new DataTable("Table_AX");
            dt.Columns.Add("column0", System.Type.GetType("System.String"));
            DataColumn dc = new DataColumn("column1", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("column2", System.Type.GetType("System.Int32"));
            dt.Columns.Add("column3", System.Type.GetType("System.Guid"));
            dt.Columns.Add(dc);
            Guid guid = new Guid("00000000-0000-0000-0000-000000000000");
            for (int i = 0; i < 50; i++)
            {
                DataRow dr = dt.NewRow();
                dr["column0"] = "AX_" + i;
                dr["column1"] = true;
                dr["column2"] = i;
                dr["column3"] = guid;
                dt.Rows.Add(dr);
            }
            DataRow dr1 = dt.NewRow();
            dt.Rows.Add(dr1);
            int a = 10;  //add a breakpoint here

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
            return v1 + v2;
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
