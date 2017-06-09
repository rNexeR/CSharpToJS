
namespace CStoJS.LexerLibraries
{
    public class Token
    {
        public TokenType type;
        public int column;
        public int row;
        public string lexema;

        public Token(){
            
        }
        public Token(TokenType type, string lexema, int row, int column)
        {
            this.type = type;
            this.lexema = lexema;
            this.row = row;
            this.column = column;
        }

        public override string ToString()
        {
            return $"{lexema} of type {type}";
        }
    }
}