using System;
using System.IO;
using CStoJS.Outputs;
using CStoJS.Semantic;

namespace CStoJS.CodeGenerator
{
    public class CodeGenerator
    {
        private API api;
        private IOutput output;

        public CodeGenerator(IOutput output, API api)
        {
            this.output = output;
            this.api = api;
        }

        public void GenerateCode()
        {
            this.output.WriteString(Utils.js_header);

            output.WriteStringLine("");

            foreach(var nsp in this.api.namespaces_hash){
                if(nsp.Key.ToString() == "" || nsp.Key.ToString().StartsWith("System"))
                    continue;
                Console.WriteLine($"To generate: {nsp.Key}");
                output.WriteStringLine($"GeneratedCode.{nsp.Key} = {{}};");
            }

            var enums = api.GetEnums();
            foreach (var _enum in enums)
            {
                output.WriteStringLine("");
                _enum.Value.GenerateCode(output, api);
            }

            foreach(var nsp in api.namespaces){
                if(nsp.ToString() == "" || nsp.ToString().StartsWith("System"))
                    continue;
                // output.WriteStringLine("");
                nsp.GenerateCode(output, api);
            }

            output.Finish();
        }
    }
}