using CStoJS.LexerLibraries;

namespace CStoJS.Inputs
{
    public interface IInput
    {
        Symbol GetNextSymbol();
    }
}