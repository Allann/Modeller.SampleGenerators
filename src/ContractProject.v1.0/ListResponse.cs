using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractProject
{
    public class LookupResponse : IGenerator
    {
        private readonly Module _module;

        public LookupResponse(ISettings settings, Module module)
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
            sb.al("using System.Collections.Generic;");
            sb.al("using System.Collections.ObjectModel;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Contracts.v1.Responses");
            sb.al("{");
            sb.i(1).al($"public class LookupItem");
            sb.i(1).al("{");
            sb.i(2).al($"public LookupItem(Guid id, string text)");
            sb.i(2).al("{");
            sb.i(3).al("Id = id;");
            sb.i(3).al("Text = text;");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al("public Guid Id { get; }");
            sb.b();
            sb.i(2).al("public string Text { get; }");
            sb.i(1).al("}");
            sb.b();
            sb.i(1).al($"public class LookupResponse");
            sb.i(1).al("{");
            sb.i(2).al("private readonly ReadOnlyCollection<LookupItem> _readOnlyCollection;");
            sb.b();
            sb.i(2).al($"public LookupResponse(ReadOnlyCollection<LookupItem> readOnlyCollection)");
            sb.i(2).al("{");
            sb.i(3).al("_readOnlyCollection = readOnlyCollection;");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al("public IEnumerable<LookupItem> Items => _readOnlyCollection;");
            sb.i(1).al("}");
            sb.al("}");

            return new File ("LookupResponse.cs", sb.ToString(), path: $"v1\\Responses");
        }
    }
}