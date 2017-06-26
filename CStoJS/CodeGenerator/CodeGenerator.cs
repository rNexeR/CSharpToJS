using System;
using System.Collections.Generic;
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

            var to_write = new List<string>();

            foreach(var nsp in this.api.namespaces_hash){
                if(nsp.Key.ToString() == "" || nsp.Key.ToString().StartsWith("System"))
                    continue;
                Console.WriteLine($"To generate: {nsp.Key}");
                to_write.Add($"GeneratedCode.{nsp.Key} = {{}};");
            }

            var to_write_array = to_write.ToArray();
            Utils.SortNamespaces(to_write_array);

            output.WriteStringLines(to_write_array);

            var enums = api.GetEnums();
            foreach (var _enum in enums)
            {
                output.WriteStringLine("");
                _enum.Value.GenerateCode(output, api);
            }

            var i = 0;
            foreach(var nsp in api.namespaces){
                if(i == 0 || nsp.ToString().StartsWith("System")){
                    i++;
                    continue;
                }
                Console.WriteLine($"Generating {nsp}");
                // output.WriteStringLine("");
                nsp.GenerateCode(output, api);
                i++;
            }

            output.WriteStringLine("module.exports = GeneratedCode;");

            output.Finish();
        }
    }
}