namespace CStoJS.Parser{
    public partial class Parser{
        void TypeDeclarationList(){
            if( MatchAny( this.class_modifiers.Concat(this.encapsulation_modifiers).Concat({TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD })) ){ 
                TypeDeclaration();
                TypeDeclarationList();
            }else{
                //EPSILON
            }
        }

        void TypeDeclaration(){
            MatchAny(this.encapsulation_modifiers);
            GroupDeclaration();
        }

        void TypeDeclarationList(){
            if( MatchAny( this.class_modifiers.Concat(this.encapsulation_modifiers).Concat({TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD })) ){
                TypeDeclaration();
                TypeDeclarationList();
            }else{
                //EPSILON
            }
        }

        void GroupDeclaration(){
           if( MatchAny( this.class_modifiers.Concat({ TokenType.CLASS_KEYWORD }) ) ){ 
                ClassDeclaration();
            }else if( MatchAny( { TokenType.ENUM_KEYWORD } ) ){
                EnumDeclaration();
            }else if( MatchAny( { TokenType.INTERFACE_KEYWORD } ) ){
                InterfaceDeclaration();
            }else{
                throw new SyntaxException("Class, Enum or Interface Declaration expected.");
            }
        }
    }
}