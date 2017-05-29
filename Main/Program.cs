using System;
using CStoJS;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Tree;
using System.Xml.Serialization;
using System.IO;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            // string txtContent = @"
            // using Text.Test.Test;
            // using Nx;
            // using Test2;
            // public interface Nexer : Kevin, Kevin.Cantillano{
            //     Nx sayHello(string[] msg);
            // }
            // namespace N1{
            //     using Here;
            //     namespace N1.L2{
            //         using Here2;
            //     }
            // }
            // namespace N2{
            //     public class Nexer : Rodriguez{
            //         public int x1;
            //         public int x, y, z;
            //         public Nexer NewNexer(){
            //             if(x == 0){
            //                 while(x <= 5){
            //                     ++x;
            //                     x++;
            //                 }
            //             }
            //         }
            //         public Nexer(int x, Nexer nx){}
            //     }

            //     public abstract class Rodriguez{

            //     }
            // }
            // namespace N3{}
            // namespace Hi{
            //     public enum Hola{
            //         HOLA,
            //         ADIOS,
            //     }

            //     public enum Hola2{
            //         HOLA,
            //         ADIOS,
            //     }

            //     public interface Hola3 : Hola2, Hola1{

            //     }
            // }

            // public enum Hola2{
            //         HOLA,
            //         ADIOS,
            //     }
            // ";
            var txtContent = @"
            using System.IO;
            using Test.Test;

            public class Nx{
                public int x, y, z = 0;
                public Nexer nx = new Nexer();

                public Nx(){

                }

                public void sayHello(){
                    if(x == 0){
                        while(x <= 5){
                            ++x;
                            x++;
                            var x = new int[]{1,2,3};
                        }
                    }
                }
            }
            ";
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            bool print = true;
            NamespaceNode tree;
            try
            {
                tree = parser.parse();
                SerializeTree(tree);
            }
            catch (Exception ex)
            {
                print = false;
                Console.WriteLine(ex.Message);
            }

            if (print)
            {
                Console.WriteLine("EXIT!");
            }
            // var current_token = lexer.GetNextToken();
            // do{
            //     Console.WriteLine(current_token);
            //     current_token = lexer.GetNextToken();
            // }while(current_token.type != TokenType.EOF);
            // Console.WriteLine(current_token);
        }

        private static void SerializeTree(NamespaceNode tree)
        {
            // System.Type[] types = { typeof(UsingNode), typeof(NamespaceNode), typeof(EnumDefinitionNode)
            // , typeof(EnumNode), typeof(InterfaceNode), typeof(ClassNode), typeof(FieldNode)
            // , typeof(MethodNode), typeof(ConstructorNode),typeof(ConstructorInitializerNode), typeof(IdentifierNode), typeof(Token)
            // , typeof(ExpressionNode), typeof(ParameterNode), typeof(IdentifierNode), typeof(PrimitiveType)
            // , typeof(LiteralNode), typeof(StatementExpressionNode), typeof(FunctionCallExpression), typeof(AccessMemory)
            // , typeof(ReferenceAccessNode), typeof(ForStatementNode), typeof(ForeachStatementNode), typeof(WhileStatementNode)
            // , typeof(DoStatementNode), typeof(IfStatementNode),typeof(SwitchStatementNode) , typeof(BodyStatement), typeof(LocalVariableNode)
            // , typeof(EmbeddedStatementNode), typeof(IdentifierTypeNode), typeof(ClassInstantiation), typeof(ArrayInstantiation), typeof(ConditionExpression)
            // , typeof(AssignmentNode), typeof(PostAdditiveExpressionNode), typeof(UnaryExpressionNode), typeof(ExpressionUnaryNode)};
            System.Type[] types = {typeof(NamespaceNode)};
            var serializer = new XmlSerializer(typeof(NamespaceNode), types);
            var logPath = System.IO.Path.GetTempFileName();
            var logFile = System.IO.File.Create("Tree.xml");
            var writer = new System.IO.StreamWriter(logFile);
            serializer.Serialize(writer, tree);
        }
    }
}
