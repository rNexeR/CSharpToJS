using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class MethodNode
    {
        public IdentifierNode identifier;
        public TypeDeclarationNode  returnType;
        public List<TypeDeclarationNode> parameters;
        public List<StatementTypeNode> body;


        public override string ToString(){
            string ret = "";
            ret += $"{returnType.identifier} {identifier}(";
            foreach(var x in parameters){
                ret += $" {x.type} {x.identifier}, ";
            }
            return ret;
        }
    }
}