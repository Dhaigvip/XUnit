using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject1
{
    public class MyAssemblyFixture : IDisposable
    {
        public static int InitialCount;

        public MyAssemblyFixture()
        {
            InitialCount++;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
