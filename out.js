
var ToString = function(target) {
    let temp = target;
    return temp.toString();
};
	
var CharToInt = function(target) {
    return target.charCodeAt(0);
};
	
var toIntPrecision = function(target) {
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
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GeneratedCode.IntType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GeneratedCode.CharType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return s;
    }
}
GeneratedCode.FloatType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
    static ParseStringType(s) {
        return +(s);
    }
}
GeneratedCode.StringType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GeneratedCode.BoolType = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    ToString() {
        console.log(this);
    }
}
GeneratedCode.System.Console = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
    }
    static ReadLine() {}
}
GeneratedCode.System.IO.TextWriter = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static WriteLineStringType(message) {
        console.log(message);
    }
}
GeneratedCode.System.Console.Out = new GeneratedCode.System.IO.TextWriter();
GeneratedCode.System.IO.TextReader = class {
    constructor() {
        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length > 1) this[arguments[0]](...argus);
    }
    static ReadLine() {}
}
        
GeneratedCode.Tests = {};
GeneratedCode.Tests.Testing = {};
GeneratedCode.Tests2 = {};

GeneratedCode.Tests.Testing.types = {
	HOLA : 0,
	ADIOS : 5,
	OTRO : 6,
}

GeneratedCode.Tests.Node = class extends GeneratedCode.Object {};

GeneratedCode.Tests.Tree = class extends GeneratedCode.Object {};

GeneratedCode.Tests.ClaseAbstracta = class extends GeneratedCode.Object {};

GeneratedCode.Tests.X = class extends GeneratedCode.Object {};

GeneratedCode.Tests.X = class extends GeneratedCode.Object {};

GeneratedCode.Tests.Y = class extends GeneratedCode.Tests.X {};

GeneratedCode.Tests2.Hoas = class extends GeneratedCode.Object {};
