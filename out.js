
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
        

GeneratedCode.sort = class extends GeneratedCode.Object {

	constructor() {
		super();

        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
	};

	static Main_StringType1D(args){
		let c7 = ToIntPrecision((ToIntPrecision(CharToInt('a') + ToIntPrecision(1))));
		let c = ToIntPrecision((ToIntPrecision(20 + 30)));
		let c1 = ToIntPrecision((ToIntPrecision(((ToIntPrecision(((ToIntPrecision((ToIntPrecision(20 + 30)) - (ToIntPrecision(10 * 50))))) / 5))) * 10)));
		let c2 = ToIntPrecision((ToIntPrecision(1 << 0x1)));
		let c3 = ToIntPrecision((ToIntPrecision(c2 ^ c1)));
		let c4 = ToIntPrecision((ToIntPrecision(1 & c)));
		let c5 = ToIntPrecision((ToIntPrecision(100 % 7)));
		let c6 = ToIntPrecision((ToIntPrecision(c4 | c1)));
		let c8 = ToIntPrecision((ToIntPrecision(CharToInt('b') * CharToInt('r'))));
		GeneratedCode.System.Console.WriteLine_StringType(("Suma: " + c));
		GeneratedCode.System.Console.WriteLine_StringType(("sumatoria complicada: " + c1));
		GeneratedCode.System.Console.WriteLine_StringType(("let shiting: " + c2));
		GeneratedCode.System.Console.WriteLine_StringType(("XOR operator: " + c3));
		GeneratedCode.System.Console.WriteLine_StringType(("AND operator: " + c4));
		GeneratedCode.System.Console.WriteLine_StringType(("MOD operator: " + c5));
		GeneratedCode.System.Console.WriteLine_StringType(("OR operator: " + c6));
		GeneratedCode.System.Console.WriteLine_StringType(("Suma char and int: " + c7));
		GeneratedCode.System.Console.WriteLine_StringType(("Mult chars: " + c8));
		c8 += c5;
		GeneratedCode.System.Console.WriteLine_StringType(("+= " + c8));
		c7 -= c5;
		GeneratedCode.System.Console.WriteLine_StringType(("-= " + c7));
		c6 *= c5;
		GeneratedCode.System.Console.WriteLine_StringType(("*= " + c6));
		c5 /= c5;
		GeneratedCode.System.Console.WriteLine_StringType(("/= " + c5));
		c4 &= c5;
		GeneratedCode.System.Console.WriteLine_StringType(("&= " + c4));
		c3 |= c5;
		GeneratedCode.System.Console.WriteLine_StringType(("|= " + c3));
		c2 ^= c5;
		GeneratedCode.System.Console.WriteLine_StringType(("^= " + c2));
		c1 %= ~c8;
		GeneratedCode.System.Console.WriteLine_StringType(("%= " + c1));
		let s = ("a" + 1);
		let s2 = ("a" + 1.5);
		GeneratedCode.System.Console.WriteLine_StringType(("Suma string-loat " + s2));
		s2 += " in";
		GeneratedCode.System.Console.WriteLine_StringType(("Suma string-int " + s));
		GeneratedCode.System.Console.WriteLine_StringType(("Suma assign " + s2));
		GeneratedCode.System.Console.WriteLine_StringType("Using selectionsort ");
		let array = [];
		array[0] = 7;
		array[1] = 50;
		array[2] = 20;
		array[3] = 40;
		array[4] = 90;
		array[5] = 6;
		array[6] = 4;
		let size = ToIntPrecision(7);
		GeneratedCode.sort.IntArraySelectionSort_IntType1D_IntType(array, size);
			for(		let i = ToIntPrecision(0);(i < size);		i++){
		GeneratedCode.System.Console.WriteLine_StringType(("" + array[i]));
			}
		GeneratedCode.System.Console.WriteLine_StringType("Using HOLAAaaaaaaaaaaa ");
		let array2 = [];
		array2[0] = 7;
		array2[1] = 35;
		array2[2] = 22;
		array2[3] = 45;
		array2[4] = 92;
		array2[5] = 11;
		array2[6] = 4;
		let size2 = ToIntPrecision(7);
		GeneratedCode.sort.IntArrayQuickSort_IntType1D_IntType(array2, size2);
			for(		let i = ToIntPrecision(0);(i < size2);		i++){
		GeneratedCode.System.Console.WriteLine_StringType(("" + array2[i]));
			}
	};

	static IntArrayQuickSort_IntType1D_IntType_IntType(data,l,r){
		let i;
		let j;
		let x;
		i = l;
		j = r;
		x = data[(ToIntPrecision(((ToIntPrecision(l + r))) / 2))];
			while(true){
			while((data[i] < x)){
		i++;
			}
			while((x < data[j])){
		j--;
			}
		if((i <= j)){
		GeneratedCode.sort.exchange_IntType1D_IntType_IntType(data, i, j);
		i++;
		j--;
		}
		if((i > j)){
		break;
		}
			}
		if((l < j)){
		GeneratedCode.sort.IntArrayQuickSort_IntType1D_IntType_IntType(data, l, j);
		}
		if((i < r)){
		GeneratedCode.sort.IntArrayQuickSort_IntType1D_IntType_IntType(data, i, r);
		}
	};

	static IntArrayQuickSort_IntType1D_IntType(data,size){
		GeneratedCode.sort.IntArrayQuickSort_IntType1D_IntType_IntType(data, 0, (ToIntPrecision(size - 1)));
	};

	static IntArrayMin_IntType1D_IntType_IntType(data,start,size){
		let minPos = ToIntPrecision(start);
			for(		let pos = ToIntPrecision((ToIntPrecision(start + 1)));(pos < size);		pos++){
		if((data[pos] < data[minPos])){
		minPos = pos;
		}
			}
		return minPos;
	};

	static IntArraySelectionSort_IntType1D_IntType(data,size){
		let i;
		let N = ToIntPrecision(size);
		GeneratedCode.System.Console.WriteLine_StringType("Hola");
		let n = ToIntPrecision(GeneratedCode.IntType.Parse_StringType("5"));
			for(		i = 0;(i < (ToIntPrecision(N - 1)));		i++){
		let k = ToIntPrecision(this.IntArrayMin_IntType1D_IntType_IntType(data, i, size));
		if((i != k)){
		GeneratedCode.sort.exchange_IntType1D_IntType_IntType(data, i, k);
		}
			}
	};

	static exchange_IntType1D_IntType_IntType(data,m,n){
		let temporary;
		temporary = data[m];
		data[m] = data[n];
		data[n] = temporary;
	};
};

module.exports = GeneratedCode;
GeneratedCode.sort.Main_StringType1D();