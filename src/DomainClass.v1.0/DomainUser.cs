using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace DomainClass
{
    public class DomainUser : IGenerator
    {
        private readonly Module _module;
        private readonly Model _model;

        public DomainUser(ISettings settings, Module module, Model model)
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

            var relationships = _module.FindRelationshipsByModel(_model);

            var sb = new StringBuilder();
            sb.al(((ISnippet)new Header.Generator(Settings, new GeneratorDetails()).Create()).Content);
            sb.al("using FourMi.Interfaces;");
            sb.al("using System;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Domain");
            sb.al("{");
            sb.i(1).a($"public partial class {_model.Name}");
            if (_model.Key.Fields.Count() == 1 && _model.Key.Fields.Any(f => f.Name.Value == "Id"))
                sb.a(" : Entity");
            sb.b();
            sb.i(1).al("{");

            sb.i(2).a($"public {_model.Name}(");
            foreach (var field in _model.Fields)
                sb.a($"{field.GetDataType()} {field.Name.Singular.LocalVariable}, ");
            sb.TrimEnd(", ");
            sb.al(")");
            sb.i(2).al("{");
            foreach (var field in _model.Fields.Where(f => !f.Nullable && f.DataType == DataTypes.String))
            {
                sb.i(3).al($"if(string.IsNullOrWhiteSpace({field.Name.Singular.LocalVariable}))");
                sb.i(4).al($"throw new ArgumentException(\"Must include a value for {field.Name.Singular.Display}\");");
            }
            foreach (var field in _model.Fields)
                sb.i(3).al($"{field.Name.Value} = {field.Name.Singular.LocalVariable};");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al("void SetDefaults()");
            sb.i(2).al("{");
            sb.i(2).al("}");
            sb.i(1).al("}");
            sb.al("}");

            return new File(_model.Name + ".cs", sb.ToString() );
        }
    }
}