using Hy.Modeller;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Hy.Modeller.Outputs;
using System;
using System.Text;

namespace DomainClass
{
    public class DomainAction : IGenerator
    {
        private readonly Module _module;
        private readonly Model _model;

        public DomainAction(ISettings settings, Module module, Model model)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var sb = new StringBuilder();
            sb.al(((ISnippet)new Header.Generator(Settings, new GeneratorDetails()).Create()).Content);
            sb.al("using System;");
            sb.al("using System.Collections.Generic;");
            sb.al("using System.Linq;");
            sb.al("using System.Text;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Domain");
            sb.al("{");
            sb.i(1).al($"partial class {_model.Name}");
            sb.i(1).al("{");
            sb.i(2).al("//todo: Add domain actions here that change the state of a domain instance.");
            sb.i(1).al("}");
            sb.al("}");

            return new File() { Name = _model.Name + ".actions.cs", Content = sb.ToString(), CanOverwrite = false };
        }
    }
}