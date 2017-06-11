namespace CStoJS.Tree
{
    public class ArrayType : TypeDeclarationNode
    {
        public int arrayOfArrays;
        public int dimensions;
        public TypeDeclarationNode baseType;

        public ArrayType(){
            this.arrayOfArrays = this.dimensions = 0;
        }

        public override string ToString(){
            var arr = "";
            if(arrayOfArrays > 0){
                for(int i = 0; i < arrayOfArrays; i++){
                    arr += "[]";
                }
            }else{
                arr += "[";
                for(int i = 0; i < arrayOfArrays; i++){
                    arr += ",";
                }
                arr += "}";
            }
            return $"{baseType}{arr}";
        }
    }
}