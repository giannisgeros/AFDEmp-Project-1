using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV4
{
    public class LogRepository
    {
        public void Log(string logMessage, TextWriter w)
        {
            w.WriteLine($"{DateTime.Now} -> {logMessage}");
        }

        public void TypeInLog(string username, string operation)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                Log($"{username} {operation}", w);
            }
        }
    }
}
