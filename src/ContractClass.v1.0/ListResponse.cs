using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractClass
{
    public class ListResponse : IGenerator
    {
        private readonly Module _module;
        private readonly Model _model;

        public ListResponse(ISettings settings, Module module, Model model)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _model = model ?? throw new ArgumentNullException(nameof(model));
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
            sb.al($"namespace {_module.Namespace}.Contracts.v1.Responses.{_model.Name.Plural.Value}");
            sb.al("{");
            sb.i(1).al($"public class ListItem");
            sb.i(1).al("{");
            foreach (var field in _model.AllFields)
                sb.al(((ISnippet)new Property.Generator(field).Create()).Content);
            sb.i(1).al("}");
            sb.b();
            sb.i(1).al($"public class {_model.Name}ListResponse");
            sb.i(1).al("{");
            sb.i(2).al("private readonly ReadOnlyCollection<ListItem> _readOnlyCollection;");
            sb.b();
            sb.i(2).al($"public {_model.Name}ListResponse(ReadOnlyCollection<ListItem> readOnlyCollection)");
            sb.i(2).al("{");
            sb.i(3).al("_readOnlyCollection = readOnlyCollection;");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al("public IEnumerable<ListItem> Items => _readOnlyCollection;");
            sb.i(1).al("}");
            sb.al("}");

            return new File (_model.Name + "ListResponse.cs", sb.ToString(), path: $"v1\\Responses\\{_model.Name.Plural.Value}");
        }
    }
}