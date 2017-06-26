using System;
using System.Collections.Generic;
using CStoJS.LexerLibraries;
using CStoJS.Tree;

namespace CStoJS.Semantic
{
    public static class Utils
    {
        public static bool debug_enabled = false;
        public static List<string> GetParentsNames(ContextManager ctx_man)
        {
            var ret = new List<string>();
            var contexts = ctx_man.getParentsContexts();
            foreach (var ctx in contexts)
            {
                ret.Add(ctx.name);
            }
            return ret;
        }

        public static Dictionary<string, TypeDeclarationNode> MethodsToImplements(ContextManager ctx_man)
        {
            var ret = new Dictionary<string, TypeDeclarationNode>();
            var parents = ctx_man.getParentsContexts();

            foreach (var parent in parents)
            {
                if (parent.type == ContextType.PARENT_INTERFACE_CONTEXT)
                {
                    //add all methods
                    foreach (var method in parent.methods)
                    {
                        ret[method.Key] = method.Value;
                    }
                }
                else
                {
                    var clase = ctx_man.api.GetTypeDeclaration(parent.name) as ClassNode;
                    if (clase.isAbstract)
                    {
                        foreach (var method in clase.methods)
                        {
                            if (method.body == null)
                            {
                                ret[method.ToString()] = method.returnType;
                            }
                        }
                    }
                }
            }

            foreach (var parent in parents)
            {
                if (parent.type == ContextType.PARENT_INTERFACE_CONTEXT)
                    continue;
                var clase = ctx_man.api.GetTypeDeclaration(parent.name) as ClassNode;
                foreach (var method in clase.methods)
                {
                    if (ret.ContainsKey(method.ToString()) && method.body != null)
                    {
                        ret.Remove(method.ToString());
                    }
                }
            }

            return ret;
        }

        public static String GetMethodCallerName(List<ArgumentNode> args)
        {
            var params_types = "";
            foreach (var param in args)
            {
                if (param.argument.returnType is ArrayType)
                {
                    var temp = (param.argument.returnType as ArrayType);
                    params_types += $"_{temp.baseType.ToString()}{temp.arrayOfArrays}D";
                }
                else
                {
                    params_types += "_" + param.argument.returnType.ToString();
                }
            }
            return params_types;
        }

        public static string GetFullName(string nsp_name, string type_name)
        {
            if (nsp_name == "")
                return $"GeneratedCode.{type_name}";
            else
                return $"GeneratedCode.{nsp_name}.{type_name}";
        }

        public static bool IsChildOf(string parent, string child, API api)
        {
            var ctx_man = new ContextManager(api);
            ctx_man.Push(new Context(ContextType.CLASS_CONTEXT, child), child);

            var parents = Utils.GetParentsNames(ctx_man);

            return parents.Contains(parent);
        }

        public static string GetClassName(string class_name, List<UsingNode> usings, API api)
        {
            foreach (var _using in usings)
            {
                var _using_name = _using.ToString();
                var n_class_name = _using_name == "" ? class_name : $"{_using_name}.{class_name}";
                if (api.TypeDeclarationExists(n_class_name))
                {
                    return n_class_name;
                }
            }
            return "";
        }

        public static bool AreEquivalentsTypes(TypeDeclarationNode type1, TypeDeclarationNode type2, List<UsingNode> _usings, API api)
        {
            var type1_name = Utils.GetClassName(type1.identifier.ToString(), _usings, api);
            var type2_name = Utils.GetClassName(type2.identifier.ToString(), _usings, api);
            if (type1_name != "" && type2_name != "")
            {
                if (type1_name == type2_name || Utils.IsChildOf(type1_name, type2_name, api))
                    return true;
            }
            else if (type1 is ArrayType && type2 is ArrayType &&
               ((type1 as ArrayType).arrayOfArrays == (type2 as ArrayType).arrayOfArrays || (type1 as ArrayType).dimensions == (type2 as ArrayType).dimensions))
            {
                var assign_type = Utils.GetClassName((type2 as ArrayType).baseType.identifier.ToString(), _usings, api);
                var var_type = Utils.GetClassName((type1 as ArrayType).baseType.identifier.ToString(), _usings, api);
                if ((type1 as ArrayType).baseType.identifier.ToString() == (type2 as ArrayType).baseType.identifier.ToString() || Utils.IsChildOf(var_type, assign_type, api))
                    return true;
            }
            return false;
        }

        public static string js_header = @"
var ToString = function(target) {
    let temp = target;
    return temp.toString();
};
	
var CharToInt = function(target) {
    return target.charCodeAt(0);
};
	
var ToIntPrecision = function(target) {
    return Math.floor(target);
};

var GeneratedCode = {};
GeneratedCode.System = {};
GeneratedCode.System.IO = {};

GeneratedCode.Object = class {
    Object() {}
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    ToString() {
        return json.stringify(this);
    }
}

GeneratedCode.IntType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    ToString() {
        return json.stringify(this);
    }
    static Parse_StringType(s) {
        return +(s);
    }
}

GeneratedCode.CharType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    ToString() {
        return json.stringify(this);
    }
    static Parse_StringType(s) {
        return s;
    }
}

GeneratedCode.FloatType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    ToString() {
        return json.stringify(this);
    }
    static Parse_StringType(s) {
        return +(s);
    }
}

GeneratedCode.StringType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    ToString() {
        return json.stringify(this);
    }
}

GeneratedCode.BoolType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    ToString() {
        return json.stringify(this);
    }
}

GeneratedCode.System.Console = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    static WriteLine_StringType(message) {
        console.log(message);
    }
    static ReadLine() {}
}

GeneratedCode.System.IO.TextWriter = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    static WriteLine_StringType(message) {
        console.log(message);
    }
}

GeneratedCode.System.Console.Out = new GeneratedCode.System.IO.TextWriter();
GeneratedCode.System.IO.TextReader = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
    }
    static ReadLine() {}
}
        ";

        public static String ctors_caller = @"
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);";

        public static List<TokenType> assignment_operators = new List<TokenType>{ TokenType.OP_ASSIGN, TokenType.OP_ASSIGN_PLUS, TokenType.OP_ASSIGN_MINUS, TokenType.OP_ASSIGN_MULTIPLICATION, TokenType.OP_ASSIGN_DIVISION, TokenType.OP_ASSIGN_MODULO, TokenType.OP_ASSIGN_AND, TokenType.OP_ASSIGN_OR, TokenType.OP_ASSIGN_XOR, TokenType.OP_ASSIGN_SHIFT_LEFT, TokenType.OP_ASSIGN_SHIFT_RIGHT };

        public static String GetCtorName(String identifier, ConstructorNode ctor)
        {
            var params_types = "";
            var params_names = "";
            var i = 0;
            foreach (var param in ctor.parameters)
            {
                if (param.type is ArrayType)
                {
                    var temp = (param.type as ArrayType);
                    params_types += $"_{temp.baseType.ToString()}{temp.arrayOfArrays}D";
                }
                else
                {
                    params_types += "_" + param.type.identifier.ToString();
                }
                params_names += param.identifier.ToString();
                if (i != ctor.parameters.Count - 1 && ctor.parameters.Count > 1)
                    params_names += ",";
                i++;
            }
            return identifier + params_types + "(" + params_names + ")";
        }

        public static String GetMethodName(String identifier, MethodNode ctor)
        {
            var params_types = "";
            var params_names = "";
            var i = 0;
            foreach (var param in ctor.parameters)
            {
                if (param.type is ArrayType)
                {
                    var temp = (param.type as ArrayType);
                    params_types += $"_{temp.baseType.ToString()}{temp.arrayOfArrays}D";
                }
                else
                {
                    params_types += "_" + param.type.identifier.ToString();
                }
                params_names += param.identifier.ToString();
                if (i != ctor.parameters.Count - 1 && ctor.parameters.Count > 1)
                    params_names += ",";
                i++;
            }
            return identifier + params_types + "(" + params_names + ")";
        }

        internal static void PrintDebug(string v)
        {
            if (Utils.debug_enabled)
                Console.WriteLine(v);
        }

        internal static bool FieldIsStatic(string var_name, TypeDeclarationNode clase)
        {
            var clas = clase as ClassNode;

            if (var_name.StartsWith("this."))
                var_name = var_name.Replace("this.", "");

            if (var_name.StartsWith("base."))
                var_name = var_name.Replace("base.", "");

            foreach (var field in clas.fields)
            {
                if (field.ToString() == var_name)
                    if (field.modifier != null && field.modifier.lexema == "static")
                        return true;
                    else
                        return false;
            }

            return false;
        }

        public static void exchange(string[] data, int m, int n)
        {
            string temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
        public static void SortNamespaces(string[] data)
        {
            int i, j;
            int N = data.Length;

            for (j = 1; j < N; j++)
            {
                for (i = j; i > 0 && data[i].Length < data[i - 1].Length; i--)
                {
                    exchange(data, i, i - 1);
                }
            }
        }
    }
}