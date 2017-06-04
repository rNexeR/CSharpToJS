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
            // var txtContent = @"
            // using System.IO;
            // using Test.Test;

            // public class Nx{
            //     public int x, y, z = 0;
            //     public Nexer nx = new Nexer();

            //     public Nx(){

            //     }

            //     public void sayHello(){
            //         if(x == 0){
            //             while(x <= 5){
            //                 ++x;
            //                 x++;
            //                 var x = new int[]{1,2,3};
            //             }
            //         }
            //     }
            // }
            // ";
            var txtContent =  System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/compiiiss1_ori.txt");
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
            System.Type[] types = { 
                //Arguments and Parameters
            typeof(ArgumentNode), typeof(ParameterNode)

                //Expression
            , typeof(ExpressionNode), typeof(ArrayInitializerNode), typeof(VariableInitializer)
                //UnaryExpression
            , typeof(CastingExpressionNode), typeof(PreOperatorExpressionNode), typeof(UnaryExpressionNode)
                //PrimaryExpressions
            , typeof(ReferenceAccessNode), typeof(PrimaryExpressionNode), typeof(PostAdditiveExpressionNode), typeof(ParenthesizedExpressionNode)
            , typeof(LiteralExpressionNode), typeof(IdentifierExpressionNode), typeof(FunctionCallExpressionNode), typeof(BuiltInTypeExpressionNode)
            , typeof(ArrayAccessNode), typeof(ArrayAccessExpressionNode), typeof(AccessMemoryExpressionNode)
                //InstanceInitializer
            , typeof(ArrayInitializerExpressionNode), typeof(ConstructorCallExpressionNode), typeof(InstanceInitilizerExpressionNode)
                //Binary Expression
            , typeof(TernaryExpressionNode), typeof(BinaryExpressionNode), typeof(AssignationExpressionNode), typeof(ConditionalExpressionNode)
            , typeof(BitwiseExpressionNode), typeof(ArithmeticExpressionNode)

                //Identifiers
            , typeof(IdentifierNode), typeof(EncapsulationNode),typeof(UsingNode)

                //Statements
            , typeof(StatementNode), typeof(StatementExpressionNode), typeof(LocalVariableNode), typeof(LocalVariablesNode), typeof(EmbeddedStatementNode)
            , typeof(JumpStatementNode), typeof(DoStatementNode), typeof(ForeachStatementNode), typeof(ForStatementNode), typeof(WhileStatementNode)
            , typeof(IfStatementNode), typeof(SwitchStatementNode), typeof(CaseExpressionNode), typeof(CaseNode), typeof(ElseNode)
            , typeof(BlockStatementNode)
            
            , typeof(EnumDefinitionNode), typeof(ArrayType), typeof(EnumDefinitionNode), typeof(IdentifierTypeNode)
            , typeof(StatementTypeNode), typeof(TypeDeclarationNode), typeof(VarType), typeof(VoidType)
            , typeof(EnumNode), typeof(InterfaceNode), typeof(ClassNode), typeof(FieldNode)
            , typeof(BoolType), typeof(FloatType)
            , typeof(MethodNode), typeof(ConstructorNode), typeof(Token)
            ,  typeof(StringType), typeof(CharType), typeof(IntType)
            , typeof(UnaryStatement)
            };

            var serializer = new XmlSerializer(typeof(NamespaceNode), types);
            var logPath = System.IO.File.Create("LogFile.txt");
            var logFile = System.IO.File.Create("Tree.xml");
            var writer = new System.IO.StreamWriter(logFile);
            serializer.Serialize(logFile, tree);
        }
    }
}
