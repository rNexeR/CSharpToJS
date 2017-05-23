﻿using System;
using CStoJS;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string txtContent = System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/compiiiss1.txt");
            // var txtContent = "namespace test.test1.test2{\n}\0";
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            bool print = true;

            try{
                parser.parse();
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
