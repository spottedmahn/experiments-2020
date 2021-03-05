using System;
using System.Collections.Generic;

namespace MemoryLeakApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                new StaticReferenceTest();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            Console.WriteLine("Hello World!");
            StaticReferenceTest.leak.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private class StaticReferenceTest
        {
            internal static List<object> leak;

            public StaticReferenceTest()
            {
                if (leak == null)
                    leak = new List<object>();
                leak.Add(this);
            }
        }
    }
}
