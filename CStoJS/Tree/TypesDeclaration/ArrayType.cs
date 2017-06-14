namespace CStoJS.Tree
{
    public class ArrayType : TypeDeclarationNode
    {
        public int arrayOfArrays;
        public int dimensions;
        public TypeDeclarationNode baseType;

        public ArrayType()
        {
            this.arrayOfArrays = this.dimensions = 0;
        }

        public override string ToString()
        {
            var arr = "";
            if (dimensions > 0)
            {
                arr += "[";
                for (int i = 0; i < arrayOfArrays; i++)
                {
                    arr += ",";
                }
                arr += "]";
            }
            else
            {
                for (int i = 0; i < arrayOfArrays; i++)
                {
                    arr += "[]";
                }
            }
            return $"{baseType}{arr}";
        }
    }
}