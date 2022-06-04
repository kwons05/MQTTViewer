using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttViewer.Utility
{
    public class Logger
    {
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());

        public static Logger Instance { get { return lazy.Value; } }

        private Logger()
        {
        }

        private static readonly object _lock = new object();

        public Action<string> ConsoleBox;

        public void Write(string message)
        {
            lock (_lock)
            {
                Debug.WriteLine(message);

                ConsoleBox?.Invoke(message);
            }
        }

    }
}

