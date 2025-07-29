using System;
using System.IO;

namespace com.rorisoft.log2.behaviours
{
    public class LogFileBehaviour : ILogBehaviour 
    {

        private readonly string _path;

        public LogFileBehaviour(string path)
        {
            this._path = path;
        }
        public int log(ILogData data)
        {
            string ttw = $"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}|{ data.className}.{data.method} > {data.message}{Environment.NewLine}";
            File.AppendAllText(this._path, ttw);
            return 0;

            /*
            using (FileStream fs = new FileStream(this._path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Seek(0, SeekOrigin.End);
                string ttw = $"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}|{this._info.className}.{this._info.method} > {this._info.message}{Environment.NewLine}";
                fs.Write(Encoding.UTF8.GetBytes(ttw), 0, ttw.Length);
                //fs.Write(new UTF8Encoding(true).GetBytes(ttw), 0, ttw.Length);
            }
            */

            //using (StreamWriter sw = new StreamWriter(this._path, true))
            //{
            //    string ttw = $"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}|{this._info.className}.{this._info.method} > {this._info.message}";
            //    sw.WriteLine(ttw);
            //    sw.Flush();
            //}
        }

        public int log(string msg, ILogData data)
        {
            data.message = msg;
            return log(data);
        }
    }
}
