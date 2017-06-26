using System;
using System.IO;

namespace CStoJS.Outputs
{
    public class FileOutput : IOutput
    {
        private StreamWriter writer;
        private string file_name;

        public FileOutput(string path_to_file)
        {
            this.file_name = path_to_file;
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

        public void Finish()
        {
            this.writer.Flush();
            this.writer.Dispose();
        }

        public void WriteStringLines(string[] to_write)
        {
            foreach (var str in to_write)
                writer.WriteLine(str);
        }

        public void RemoveCharacter(int count)
        {
            this.Finish();
            FileStream fs = new FileStream(this.file_name, FileMode.Open, FileAccess.ReadWrite);
            fs.SetLength(fs.Length - count);
            fs.Flush();
            fs.Dispose();
            this.writer = File.AppendText(this.file_name);
        }
    }
}