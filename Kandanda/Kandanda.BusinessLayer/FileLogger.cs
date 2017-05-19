using System;
using System.IO;
using Prism.Logging;

namespace Kandanda.BusinessLayer
{
    public class FileLogger: ILoggerFacade
    {
        private readonly string _path;

        public FileLogger (string path)
        {
            _path = path;
            CreateDirectoryIfNotExists();
        }

        private void CreateDirectoryIfNotExists()
        {
            if (_path != null) Directory.CreateDirectory(Path.GetDirectoryName(_path));
        }

        public void Log(string message, Category category, Priority priority)
        {
            using (var f = File.AppendText(_path))
            {
                f.WriteLine("{0} {1} {2}: {3}", DateTime.Now, category, priority, message);
            }
        }
    }
}
