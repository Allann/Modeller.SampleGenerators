using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractProject
{
    public class CreatedResponse : IGenerator
    {
        private readonly Module _module;

        public CreatedResponse(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            if (!Settings.SupportRegen)
                return null;

            var sb = new StringBuilder();
            sb.al(((ISnippet)new Header.Generator(Settings, new GeneratorDetails()).Create()).Content);
            sb.al("using System;");
            sb.al($"namespace {_module.Namespace}.Contracts.v1.Responses");
            sb.al("{");
            sb.i(1).al("public class CreatedResponse");
            sb.i(1).al("{");
            sb.i(2).al("public CreatedResponse(int affected, Guid newId)");
            sb.i(2).al("{");
            sb.i(3).al("Affected = affected;");
            sb.i(3).al("NewId = newId;");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al("public int Affected { get; }");
            sb.b();
            sb.i(2).al("public Guid NewId { get; }");
            sb.i(1).al("}");
            sb.al("}");

            return new File("CreatedResponse.cs", sb.ToString(), path: $"v1\\Responses");
        }
    }
}