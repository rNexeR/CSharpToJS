using System;
using System.IO;

namespace CStoJS.Outputs
{
    public class FileOutput : IOutput
    {
        private StreamWriter writer;

        public FileOutput(string path_to_file){
            this.writer = File.CreateText(path_to_file);
            // this.writer = File.AppendText(path_to_file);
        }

        public void WriteStringLine(string to_write)
        {
            writer.WriteLine(to_write);
        }

        public void WriteString(string to_write)
        {
            writer.Write(to_write);
        }

        public void Finish(){
            this.writer.Flush();
            this.writer.Dispose();
        }
    }
}