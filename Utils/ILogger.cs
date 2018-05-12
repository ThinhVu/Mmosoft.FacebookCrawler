namespace FbFarm.Utils
{
    using System;

    public interface ILogger : IDisposable
    {
        void WriteLine(string message);
    }
}
