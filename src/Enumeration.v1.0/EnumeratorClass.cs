using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace Enumerator
{
    public class EnumerationClass : IGenerator
    {
        private readonly Module _module;
        private readonly Enumeration _enumeration;

        public EnumerationClass(ISettings settings, Module module, Enumeration enumeration)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _enumeration = enumeration ?? throw new ArgumentNullException(nameof(enumeration));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var sb = new StringBuilder();
            sb.al(((ISnippet)new OverwriteHeader.Generator(Settings, new GeneratorDetails()).Create()).Content);
            sb.al($"using {_module.Namespace}.Exceptions;");
            sb.al("using System;");
            sb.al("using System.Collections.Generic;");
            sb.al("using System.Linq;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Domain");
            sb.al("{");
            sb.i(1).al($"public class {_enumeration.Name.Plural.Value} : Enumeration");
            sb.i(1).al("{");
            foreach (var item in _enumeration.Items)
                sb.i(2).al($"public static {_enumeration.Name.Plural.Value} {item.Name} = new {_enumeration.Name.Plural.Value}({item.Value}, nameof({item.Name}).ToLowerInvariant());");
            sb.b();
            sb.i(2).al($"public {_enumeration.Name.Plural.Value}(int id, string name) : base(id, name)");
            sb.i(2).al("{");
            sb.i(2).al("}");
            sb.b();
            sb.i(2).al($"public static IEnumerable<{_enumeration.Name.Plural.Value}> List() =>");
            sb.i(3).a("new [] {");
            foreach (var item in _enumeration.Items)
                sb.a($" {item.Name},");
            sb.TrimEnd(",");
            sb.a(" };");
            sb.b();
            sb.b();
            sb.i(2).al($"public static {_enumeration.Name.Plural.Value} FromName(string name)");
            sb.i(2).al("{");
            sb.i(3).al("var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));");
            sb.i(3).al("if (state == null)");
            sb.i(4).al($"throw new {_module.Project}DomainException($\"Possible values for {_enumeration.Name.Plural.Value}: {{ string.Join(\",\", List().Select(s => s.Name)) }}\");");
            sb.i(2).al($"    return state;");
            sb.i(2).al("}");
            sb.i(2).b();
            sb.i(2).al($"public static {_enumeration.Name.Plural.Value} From(int id)");
            sb.i(2).al("{");
            sb.i(3).al("var state = List().SingleOrDefault(s => s.Id == id);");
            sb.i(3).al("if (state == null)");
            sb.i(4).al($"throw new {_module.Project}DomainException($\"Possible values for {_enumeration.Name.Plural.Value}: {{ string.Join(\",\", List().Select(s => s.Name)) }}\");");
            sb.i(3).al("return state;");
            sb.i(2).al("}");
            sb.i(1).al("}");
            sb.al("}");

            return new File(_enumeration.Name + ".generated.cs", sb.ToString(), canOverwrite: true);
        }
    }
}