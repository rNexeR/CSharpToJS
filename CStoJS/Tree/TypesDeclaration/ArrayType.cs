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
    }
}