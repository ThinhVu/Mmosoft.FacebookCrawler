using System;

namespace FbFarm.Utils
{
    public class BlackScreenLogger : ILogger
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Dispose()
        {            
        }
    }
}
