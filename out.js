
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
        
GeneratedCode.RaimProgram = {};
GeneratedCode.RaimProgram.Base = {};
GeneratedCode.RaimProgram.Common = {};
GeneratedCode.RaimProgram.Common.Sorting = {};
GeneratedCode.RaimProgram.Base.Derivatives = {};

GeneratedCode.RaimProgram.Common.StudentType = {
	FRESHMAN: 0,
	SOPHOMORE: 1,
	JUNIOR: 2,
	SENIOR: 3,
}

GeneratedCode.RaimProgram.Base.Person = class extends GeneratedCode.Object {

	constructor() {
		super();
		this.Name = null;
		this.Age = null;

        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
	};

	Person() {
	};

	Person_StringType_IntType(name,age) {
		this.Name = name;
		this.Age = age;
	};

	print(){
		GeneratedCode.System.Console.WriteLine_StringType(this.ToString());
	};

	PrintClassTypeWithNumberCall(){
		let numberCall = ToIntPrecision(this.GetAndIncrementNumberCall());
		GeneratedCode.System.Console.WriteLine_StringType((("Person" + " ") + GeneratedCode.RaimProgram.Base.Person._numberCall));
	};

	fn(){
		GeneratedCode.System.Console.WriteLine_StringType("Person");
	};

	metodoPrivado(){
		GeneratedCode.System.Console.WriteLine_StringType("Soy el metodo Privado");
	};

	GetAndIncrementNumberCall(){
		GeneratedCode.RaimProgram.Base.Person._numberCall++;
		return GeneratedCode.RaimProgram.Base.Person._numberCall;
	};

	SortPersons_Person1D_IntType(persons,size){};
};
GeneratedCode.RaimProgram.Base.Person._numberCall = 0;

GeneratedCode.RaimProgram.Program = class extends GeneratedCode.Object {

	constructor() {
		super();

        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
	};

	static Main_StringType1D(args){
		let student =  new GeneratedCode.RaimProgram.Base.Derivatives.Student("Student");
		let students = [];
		students[0] =  new GeneratedCode.RaimProgram.Base.Derivatives.Student("Student_StringType_IntType","D", 50);
		students[1] =  new GeneratedCode.RaimProgram.Base.Derivatives.Student("Student_StringType_IntType","C", 22);
		students[2] =  new GeneratedCode.RaimProgram.Base.Derivatives.Student("Student_StringType_IntType","B", 40);
		students[3] =  new GeneratedCode.RaimProgram.Base.Derivatives.Student("Student_StringType_IntType","A", 35);
		student.SortPersons_Person1D_IntType(students, 4);
		GeneratedCode.RaimProgram.Program.PrintPersonsInfo_Person1D(students);
		GeneratedCode.System.Console.WriteLine_StringType("");
		let teacher =  new GeneratedCode.RaimProgram.Base.Derivatives.Teacher("Teacher");
		let teachers = [];
		teachers[0] =  new GeneratedCode.RaimProgram.Base.Derivatives.Teacher("Teacher_StringType_IntType","Za", 50);
		teachers[1] =  new GeneratedCode.RaimProgram.Base.Derivatives.Teacher("Teacher_StringType_IntType","Yb", 22);
		teachers[2] =  new GeneratedCode.RaimProgram.Base.Derivatives.Teacher("Teacher_StringType_IntType","Xc", 40);
		teachers[3] =  new GeneratedCode.RaimProgram.Base.Derivatives.Teacher("Teacher_StringType_IntType","Wd", 35);
		teacher.SortPersons_Person1D_IntType(teachers, 4);
		GeneratedCode.RaimProgram.Program.PrintPersonsInfo_Person1D(teachers);
	};

	static PrintPersonsInfo_Person1D(persons){
			for(let p of persons){
		p.print();
		if(p instanceof GeneratedCode.RaimProgram.Base.Derivatives.Teacher){
		let t = p;
		GeneratedCode.System.Console.WriteLine_StringType((t.Name + " is a teacher."));
		GeneratedCode.System.Console.WriteLine_StringType(t.ToString());
		t.PrintClassTypeWithNumberCall();
		GeneratedCode.System.Console.WriteLine_StringType((("Grumpiness: " + t.GetRandomGrumpiness_FloatType_FloatType(1, 100)) + "\n"));
		}
		else{
		let s = p;
		s.fn();
		GeneratedCode.System.Console.WriteLine_StringType((s.Name + " is not a teacher."));
		s.PrintClassTypeWithNumberCall();
		}
			}
	};
};



GeneratedCode.RaimProgram.Base.Derivatives.Student = class extends GeneratedCode.RaimProgram.Base.Person {

	constructor() {
		super();
		this.studentType = null;

        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
	};

	Student() {
	};

	Student_StringType_IntType(name,age) {
		this.Name = name;
		this.Age = age;
	};

	SetStudentType_StudentType(type){
		GeneratedCode.RaimProgram.Common.StudentType = type;
	};

	fn(){
		super.fn();
		GeneratedCode.System.Console.WriteLine_StringType("Student");
	};

	SetStudentType_IntType(type){
		switch(type){
			case 0: 		this.studentType = GeneratedCode.RaimProgram.Common.StudentType.FRESHMAN;
		break;
			case 1: 		this.studentType = GeneratedCode.RaimProgram.Common.StudentType.SOPHOMORE;
		break;
			case 2: 		this.studentType = GeneratedCode.RaimProgram.Common.StudentType.JUNIOR;
		break;
			case 3: 		this.studentType = GeneratedCode.RaimProgram.Common.StudentType.SENIOR;
		break;
			default : 		GeneratedCode.System.Console.WriteLine_StringType("Invalid entry for StudentType");
		break;
		}
	};

	GetStudentType(){
		return GeneratedCode.RaimProgram.Common.StudentType;
	};

	SortPersons_Person1D_IntType(persons,size){
		this.QuickSort_Person1D_IntType_IntType(persons, 0, (ToIntPrecision(size - 1)));
	};

	ToString(){
		return ((("Name: " + this.Name) + "\nAge: ") + this.Age);
	};

	QuickSort_Person1D_IntType_IntType(persons,left,right){
		let i = ToIntPrecision(left);
		let j = ToIntPrecision(right);
		let pivot = persons[(ToIntPrecision(((ToIntPrecision(left + right))) / 2))];
			do{
			while((((persons[i].Age < pivot.Age)) && ((i < right)))){
		i++;
			}
			while((((pivot.Age < persons[j].Age)) && ((j > left)))){
		j--;
			}
		if((i <= j)){
		let temp = persons[i];
		persons[i] = persons[j];
		persons[j] = temp;
		i++;
		j--;
		}
			}while((i <= j));
		if((left < j)){
		this.QuickSort_Person1D_IntType_IntType(persons, left, j);
		}
		if((i < right)){
		this.QuickSort_Person1D_IntType_IntType(persons, i, right);
		}
	};
};


GeneratedCode.RaimProgram.Base.Derivatives.Random = class extends GeneratedCode.Object {

	constructor() {
		super();

        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
	};

	Random() {
	};

	Nextfloat(){
		return 5.5;
	};
};



GeneratedCode.RaimProgram.Base.Derivatives.Teacher = class extends GeneratedCode.RaimProgram.Base.Person {

	constructor() {
		super();

        let argumentos = Array.from(arguments);
        let argus = argumentos.slice(1);
        if (argumentos.length >= 1) this[arguments[0]](...argus);
	};

	Teacher() {
		GeneratedCode.RaimProgram.Base.Derivatives.Teacher.random =  new GeneratedCode.RaimProgram.Base.Derivatives.Random("Random");
	};

	Teacher_StringType_IntType(name,age) {
		this.Name = name;
		this.Age = age;
		GeneratedCode.RaimProgram.Base.Derivatives.Teacher.random =  new GeneratedCode.RaimProgram.Base.Derivatives.Random("Random");
	};

	GetRandomGrumpiness_FloatType_FloatType(lowerLimit,upperLimit){
		let range = (upperLimit - lowerLimit);
		let number = GeneratedCode.RaimProgram.Base.Derivatives.Teacher.random.Nextfloat();
		return (((number * range)) + lowerLimit);
	};

	SortPersons_Person1D_IntType(persons,size){
		GeneratedCode.RaimProgram.Base.Derivatives.Teacher.BubbleSort_Person1D_IntType(persons, size);
	};

	ToString(){
		return ((("Name5: " + this.Name) + "\nAg5: ") + this.Age);
	};

	static BubbleSort_Person1D_IntType(persons,size){
			for(		let pass = ToIntPrecision(1);(pass < size);		pass++){
			for(		let i = ToIntPrecision(0);(i < (ToIntPrecision(size - pass)));		i++){
		if((persons[i].Age >= persons[(ToIntPrecision(i + 1))].Age)){
		let temp = persons[i];
		persons[i] = persons[(ToIntPrecision(i + 1))];
		persons[(ToIntPrecision(i + 1))] = temp;
		}
			}
			}
	};
};
GeneratedCode.RaimProgram.Base.Derivatives.Teacher.random = null;
module.exports = GeneratedCode;
