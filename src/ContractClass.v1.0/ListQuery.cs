﻿using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractClass
{
    public class ListQuery : IGenerator
    {
        private readonly Module _module;
        private readonly Model _model;

        public ListQuery(ISettings settings, Module module, Model model)
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
            sb.al($"using {_module.Namespace}.Contracts.v1.Responses.{_model.Name.Plural.Value};");
            sb.al("using MediatR;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Contracts.v1.Queries.{_model.Name.Plural.Value}");
            sb.al("{");
            sb.i(1).al($"public class {_model.Name}ListQuery : IRequest<{_model.Name}Response>");
            sb.i(1).al("{ }");
            sb.al("}");

            return new File(_model.Name + "ListQuery.cs", sb.ToString(), path: $"v1\\Queries\\{_model.Name.Plural.Value}");
        }
    }
}