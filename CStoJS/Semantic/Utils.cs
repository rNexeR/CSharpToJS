using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.Semantic
{
    public static class Utils
    {
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
    }
}