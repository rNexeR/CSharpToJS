using System;
using CStoJS;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Tree;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string txtContent = @"
            using Text.Test.Test;
            using Nx;
            using Test2;
            public interface Nexer : Kevin, Kevin.Cantillano{
                Nx sayHello(string[] msg);
            }
            namespace N1{
                using Here;
                namespace N1.L2{
                    using Here2;
                }
            }
            namespace N2{
                public class Nexer : Rodriguez{
                    public int x1;
                    public int x, y, z;
                    public Nexer NewNexer(){
                        x++;
                        myfunc(x);
                        ++x;
                    }
                    public Nexer(int x, Nexer nx){}
                }

                public abstract class Rodriguez{

                }
            }
            namespace N3{}
            namespace Hi{
                public enum Hola{
                    HOLA,
                    ADIOS,
                }

                public enum Hola2{
                    HOLA,
                    ADIOS,
                }

                public interface Hola3 : Hola2, Hola1{

                }
            }

            public enum Hola2{
                    HOLA,
                    ADIOS,
                }
            ";
            // var txtContent = "namespace test.test1.test2{\n}\0";
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            bool print = true;
            NamespaceNode tree;
            try{
                tree = parser.parse();
            }catch(Exception ex){
                print = false;
                Console.WriteLine(ex.Message);
            }

            if(print)
                Console.WriteLine("EXIT!");
            // var current_token = lexer.GetNextToken();
            // do{
            //     Console.WriteLine(current_token);
            //     current_token = lexer.GetNextToken();
            // }while(current_token.type != TokenType.EOF);
            // Console.WriteLine(current_token);
        }
    }
}
