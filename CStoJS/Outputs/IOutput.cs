namespace CStoJS.Outputs
{
    public interface IOutput
    {
        void WriteString(string to_write);
        void WriteStringLine(string to_write);
        void Finish();
    }
}