namespace CStoJS.LexerLibraries
{
    public enum TokenType
    {
        ID,
        EOF,
        OP_SUM,
        OP_SUBSTRACT,
        OP_DIVISION,
        OP_MULTIPLICATION,
        OP_MODULO,
        OP_ASSIGN,
        LITERAL_INT,
        PAREN_OPEN,
        PAREN_CLOSE,
        END_STATEMENT,
        INT_KEYWORD,
        FLOAT_KEYWORD,
        BOOL_KEYWORD,
        LITERAL_STRING,
        LITERAL_CHAR,
        TRUE_KEYWORD,
        FALSE_KEYWORD,
        BRACE_OPEN,
        BRACE_CLOSE,
        BRACKET_CLOSE,
        BRACKET_OPEN,
        IS_KEYWORD,
        AS_KEYWORD,
        OP_INC_PP,
        OP_INC_MM,
        OP_ASSIGN_PLUS,
        OP_BITS_COMPLEMENT,
        OP_BITS_XOR,
        OP_TERNARY,
        OP_NULL_COALESCING,
        OP_HIERARCHY,
        OP_MEMBER_ACCESS,
        OP_GREATER_THAN,
        OP_GREATER_EQUAL_THAN,
        OP_BITS_SHIFT_RIGHT,
        OP_LESS_THAN,
        OP_LESS_EQUAL_THAN,
        OP_BITS_SHIFT_LEFT,
        OP_CONDITIONAL_EQUAL,
        OP_BITS_AND,
        OP_CONDITIONAL_AND,
        OP_BITS_OR,
        OP_CONDITIONAL_OR,
        OP_NEGATION,
        OP_CONDITIONAL_NOT_EQUAL,
        CHAR_TYPE,
        STRING_TYPE,
        LITERAL_STRING_VERBATIM,
        LITERAL_FLOAT,
        PUBLIC_KEYWORD,
        PRIVATE_KEYWORD,
        PROTECTED_KEYWORD,
        STATIC_KEYWORD,
        ABSTRACT_KEYWORD,
        VIRTUAL_KEYWORD,
        OVERRIDE_KEYWORD,
        CLASS_KEYWORD,
        ENUM_KEYWORD,
        INTERFACE_KEYWORD,
        NAMESPACE_KEYWORD,
        BASE_KEYWORD,
        IF_KEYWORD,
        ELSE_KEYWORD,
        SWITCH_KEYWORD,
        CASE_KEYWORD,
        DEFAULT_KEYWORD,
        WHILE_KEYWORD,
        FOR_KEYWORD,
        FOREACH_KEYWORD,
        DO_KEYWORD,
        CHAR_KEYWORD,
        STRING_KEYWORD,
        VOID_KEYWORD,
        VAR_KEYWORD,
        BREAK_KEYWORD,
        CONTINUE_KEYWORD,
        RETURN_KEYWORD,
        NEW_KEYWORD,
        THIS_KEYWORD,
        USING_KEYWORD,
        COMMA,
        OP_ASSIGN_MINUS,
        OP_ASSIGN_MODULO,
        OP_ASSIGN_MULTIPLICATION,
        OP_ASSIGN_DIVISION,
        OP_ASSIGN_AND,
        OP_ASSIGN_OR,
        OP_ASSIGN_XOR,
        OP_ASSIGN_SHIFT_LEFT,
        OP_ASSIGN_SHIFT_RIGHT,
        LINE_COMMENT,
        BLOCK_COMMENT,
        IN_KEYWORD,
        NULL_KEYWORD
    }
}