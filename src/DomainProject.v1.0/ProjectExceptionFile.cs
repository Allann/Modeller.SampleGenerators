using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace Domain
{
    internal class ProjectExceptionFile : IGenerator
    {
        private readonly Module _module;

        public ProjectExceptionFile(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var sb = new StringBuilder();
            sb.al("using System;");
            sb.b();
            sb.al($"namespace {_module.Namespace}.Exceptions");
            sb.al("{");
            sb.i(1).al($"public class {_module.Project.Value}DomainException : Exception");
            sb.i(1).al("{");
            sb.i(2).al($"public {_module.Project.Value}DomainException()");
            sb.i(2).al("{ }");
            sb.b();
            sb.i(2).al($"public {_module.Project.Value}DomainException(string message) : base(message)");
            sb.i(2).al("{ }");
            sb.b();
            sb.i(2).al($"public {_module.Project.Value}DomainException(string message, Exception innerException) : base(message, innerException)");
            sb.i(2).al("{ }");
            sb.i(1).al("}");
            sb.al("}");

            return new File($"{_module.Project.Value}DomainException.cs", sb.ToString(), path: "Exceptions");
        }
    }
}