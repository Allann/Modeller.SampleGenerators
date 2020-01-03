using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractProject
{
    internal class IdClass : IGenerator
    {
        private readonly Module _module;

        public IdClass(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {            
            var sb = new StringBuilder();
            sb.al(((ISnippet)new Header.Generator(Settings, new GeneratorDetails()).Create()).Content);
            sb.al("using System;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Contracts");
            sb.al("{");
            sb.i(1).al("public static class Id");
            sb.i(1).al("{");
            sb.i(2).al("public static bool TryConvert(string value, out Guid id)");
            sb.i(2).al("{");
            sb.i(3).al("id = Guid.Empty;");
            sb.i(3).al("if (string.IsNullOrWhiteSpace(value))");
            sb.i(4).al("return false;");
            sb.b();
            sb.i(3).al("string[] formats = { \"N\", \"D\", \"B\", \"P\", \"X\" };");
            sb.i(3).al("for (var ctr = 0; ctr < formats.Length; ctr++)");
            sb.i(4).al("if (Guid.TryParseExact(value, formats[ctr], out id))");
            sb.i(5).al("return true;");
            sb.i(3).al("return false;");
            sb.i(2).al("}");
            sb.i(1).al("}");
            sb.al("}");

            return new File("Id.cs", sb.ToString() );
        }
    }
}
