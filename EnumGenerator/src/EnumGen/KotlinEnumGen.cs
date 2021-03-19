﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGenerator
{
    public class KotlinEnumGen : IEnumGenerator
    {
        public string Def => @"
    /* AutoGenerated By Hamster Lib */ 
    enum class @className(val status: Int){ 
        @enums 
    }
        ";

        public void Generate(SerializedEnum enums, System.Action<string, string> generatedCodeCallback)
        {
            string genDef = Def;
            genDef = genDef.Replace("@className", enums.EnumClassName);

            StringBuilder enumBuilder = new StringBuilder();
            int i = 0;
            int count = enums.Datas.Count;
            foreach (var value in enums.Datas)
            {
                if (i + 1 == count)
                {
                    enumBuilder.AppendLine($@"
          //{value.Description}
          {value.Name}({value.Value})
                ");

                }
                else
                {
                    enumBuilder.AppendLine($@"
          //{value.Description}
          {value.Name}({value.Value}),
                ");
                }

                i++;
            }
            genDef = genDef.Replace("@enums", enumBuilder.ToString());
            Console.WriteLine(genDef);
            generatedCodeCallback?.Invoke("kotlin", genDef);

        }
    }
}