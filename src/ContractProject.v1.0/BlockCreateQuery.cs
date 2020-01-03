using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractProject
{
    public class BlockCreateQuery : IGenerator
    {
        private readonly Module _module;

        public BlockCreateQuery(ISettings settings, Module module)
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
            sb.al($"using {_module.Namespace}.Contracts.v1.Responses;");
            sb.al("using MediatR;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Contracts.v1.Queries");
            sb.al("{");
            sb.i(1).al($"public class BlockCreateQuery : IRequest<ExistsResponse>");
            sb.i(1).al("{");
            sb.i(2).al($"public BlockCreateQuery(string businessKey)");
            sb.i(2).al("{");
            sb.i(3).al("BusinessKey = businessKey;");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al("public string BusinessKey { get; }");
            sb.i(1).al("}");
            sb.al("}");

            return new File("BlockCreateQuery.cs", sb.ToString() , path: "v1\\Queries");
        }
    }
}