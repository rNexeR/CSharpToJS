using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void FieldOrMethodFactorized(Token encap, Token modifier, Token type, ref ClassNode clase){
            printDebug("Field Or Method Factorized");
            
            //retType or FieldType
            var identifier = new List<Token>();
            identifier.Add(type);
            IdentifierAttribute(ref identifier);
            var arr = new ArrayType();
            OptionalRankSpecifierList(ref arr);

            TypeDeclarationNode retOrFieldType = TypeDetector(type.type, new IdentifierNode(identifier));

            if(arr.arrayOfArrays >0 || arr.dimensions > 0){
                arr.baseType = retOrFieldType;
                retOrFieldType = arr;
            }
            
            if (Match(TokenType.ID)){
                var name = MatchExactly(TokenType.ID);
                FieldOrMethod(encap, modifier, retOrFieldType, name, ref clase);
            }
        }

        private void FieldOrMethod(Token encap, Token modifier, TypeDeclarationNode type, Token name, ref ClassNode clase)
        {
            printDebug("Field Or Method");
            if( Match(TokenType.PAREN_OPEN) ){
                MethodDeclaration(encap, modifier, type, name, ref clase);
            }else{
                FieldDeclaration(encap, modifier, type, name, ref clase);
            }
        }

        void ConstructorDeclaration(Token encap, Token modifier, Token name, ref ClassNode clase){
            printDebug("Constructor Declaration");
            MatchExactly( new TokenType[]{ TokenType.PAREN_OPEN } );

            var ctor = new ConstructorNode(new IdentifierNode(name), new EncapsulationNode(encap), modifier);

            var paremeters = FixedParameters();
            
            MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
            
            var args = ConstructorInitializer();
            
            MaybeEmptyBlock();

            ctor.parameters = paremeters;
            clase.constructors.Add(ctor);
        }

        private List<ArgumentNode> ConstructorInitializer()
        {
            printDebug("Constructor Initializer");
            if(Match(TokenType.OP_HIERARCHY)){
                MatchExactly( new TokenType[]{ TokenType.OP_HIERARCHY, TokenType.BASE_KEYWORD, TokenType.PAREN_OPEN } );
                var args = ArgumentList();
                MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
                return args;
            }else{
                return null;
            }
        }

        void MethodDeclaration(Token encap, Token modifier, TypeDeclarationNode type, Token name, ref ClassNode clase){
            printDebug("Method Declaration");

            var method = new MethodNode(new IdentifierNode(name), type, new EncapsulationNode(encap), modifier);

            MatchExactly( new TokenType[]{ TokenType.PAREN_OPEN } );
            
            var parameters = FixedParameters();
            method.parameters = parameters;

            MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
            MaybeEmptyBlock();

            clase.methods.Add(method);
        }

        void FieldDeclaration(Token encap, Token modifier, TypeDeclarationNode type, Token name, ref ClassNode clase){
            printDebug("Field Declaration");
            var field = new FieldNode(type, new IdentifierNode(name), new EncapsulationNode(encap), modifier);
            VariableAssigner(ref field);
            var fields = VariableDeclaratorListPrime(encap, modifier, type);

            fields.Insert(0, field);

            MatchExactly( new TokenType[]{ TokenType.END_STATEMENT } );

            clase.fields.AddRange(fields);
        }
    }
}