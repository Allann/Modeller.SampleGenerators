using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using System;
using System.Text;

namespace DomainClass
{
    public class DomainGenerated : IGenerator
    {
        private readonly Module _module;
        private readonly Model _model;

        public DomainGenerated(ISettings settings, Module module, Model model)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public IOutput Create()
        {
            var sb = new StringBuilder();
            sb.al(((ISnippet)new OverwriteHeader.Generator(Settings, new GeneratorDetails()).Create()).Content);
            sb.al("using System;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Domain");
            sb.al("{");
            sb.i(1).al($"partial class {_model.Name}");
            sb.i(1).al("{");

            sb.i(2).al($"private {_model.Name}()");
            sb.i(2).al("{");
            sb.i(3).al("SetDefaults();");
            sb.i(2).al("}");
            sb.b();

            foreach (var field in _model.Fields)
                sb.al(((ISnippet)new Property.Generator(field).Create()).Content);
            sb.TrimEnd(Environment.NewLine);
            sb.i(1).al("}");
            sb.al("}");

            return new File (_model.Name + ".generated.cs",sb.ToString(), canOverwrite : true );
        }

        public ISettings Settings { get; }
    }
}