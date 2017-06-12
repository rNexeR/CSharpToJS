using System;
using System.IO;
using System.Xml.Serialization;

using CStoJS;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Tree;
using CStoJS.Semantic;
using System.Collections.Generic;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files;
            List<string> CSFiles = new List<string>();
            var path = Path.GetFullPath("./");

            if (args.Length > 0)
            {
                path = Path.GetFullPath(args[0]);
            }

            files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".cs")
                {
                    // Console.WriteLine(file);
                    CSFiles.Add(file);
                }
            }

            var semantic_evaluator = new SemanticEvaluator(CSFiles);
            semantic_evaluator.Evaluate();

            // float flotante = 3.5f;
            // int entero = 5;
            // char caracter = 'c';
            // string cadena = "hola";
            // entero += entero;
            // flotante += entero;
            // entero += flotante;
            // entero += caracter;
            // caracter += caracter;
            // caracter += entero;
            // cadena += cadena;
            // cadena += caracter;
            // var result = caracter << caracter;

            // var api = new API(trees);
            // api.types["System.Object"] = new ClassNode();
            // var contexts = new ContextManager(api);
            // contexts.Push(new Context(ContextType.CLASS_CONTEXT, "Tests.Node"), "Tests.Node");

            Console.WriteLine("EXIT!");
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
            , typeof(StringType), typeof(CharType), typeof(IntType)
            , typeof(InlineExpressionNode), typeof(NullCoalescingExpressionNode), typeof(ConditionalOrExpressionNode)
            , typeof(ConditionalAndExpressionNode), typeof(ConditionalEqualExpressionNode), typeof(ConditionalNotEqualExpressionNode)
            , typeof(BitwiseOrExpressionNode), typeof(BitwiseXorExpressionNode), typeof(BitwiseAndExpressionNode)
            , typeof(BitwiseShiftLeftExpressionNode), typeof(BitwiseShiftRightExpressionNode), typeof(ArithmeticSumExpressionNode)
            , typeof(ArithmeticSubstractExpressionNode), typeof(ArithmeticDivisionExpressionNode), typeof(ArithmeticMultiplicationExpressionNode)
            , typeof(ArithmeticModuloExpressionNode), typeof(ConditionalIsExpressionNode), typeof(ConditionalRelationalExpressionNode)
            };

            var serializer = new XmlSerializer(typeof(NamespaceNode), types);
            var logPath = System.IO.File.Create("LogFile.txt");
            var logFile = System.IO.File.Create("Tree.xml");
            var writer = new System.IO.StreamWriter(logFile);
            serializer.Serialize(logFile, tree);
        }
    }

    public interface IHello{
        void SayHi();
    }

    public abstract class Hello : IHello{
        public abstract void SayHi();
    }
}
