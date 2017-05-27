using System.Linq;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        List<TypeDeclarationNode> TypeDeclarationList()
        {
            printDebug("Type Declaration List");
            if (MatchAny(this.class_modifiers.Concat(this.encapsulation_modifiers).Concat(new TokenType[] { TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD }).ToArray()))
            {
                var decl = TypeDeclaration();
                var decls = TypeDeclarationList();
                decls.Insert(0, decl);
                return decls;
            }
            else
            {
                //EPSILON
                return new List<TypeDeclarationNode>();
            }
        }

        TypeDeclarationNode TypeDeclaration()
        {
            printDebug("Type Declaration");
            var encapsulation_mod = new EncapsulationNode();
            if (MatchAny(this.encapsulation_modifiers))
            {
                encapsulation_mod.token = currentToken;
                ConsumeToken();
            }
            var decl =  GroupDeclaration();
            decl.encapsulation_modifier = encapsulation_mod;

            return decl;
        }

        TypeDeclarationNode GroupDeclaration()
        {
            printDebug("Group Declaration");
            if (MatchAny(this.class_modifiers.Concat(new TokenType[] { TokenType.CLASS_KEYWORD }).ToArray()))
            {
                ClassDeclaration();
                return new ClassNode();
            }
            else if (MatchAny(new TokenType[] { TokenType.ENUM_KEYWORD }))
            {
                return EnumDeclaration();
            }
            else if (MatchAny(new TokenType[] { TokenType.INTERFACE_KEYWORD }))
            {
                return InterfaceDeclaration();
                // return new InterfaceNode();
            }
            else
            {
                ThrowSyntaxException("Class, Enum or Interface Declaration expected");
                return null;
            }
        }
    }
}