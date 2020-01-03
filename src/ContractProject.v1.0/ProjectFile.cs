using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractProject
{

    internal class ProjectFile : IGenerator
    {
        private readonly Module _module;

        public ProjectFile(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var sb = new StringBuilder();
            sb.al("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.b();
            sb.i(1).al("<PropertyGroup>");
            sb.i(2).al("<TargetFramework>netstandard2.1</TargetFramework>");
            sb.i(2).al("<nullable>enable</nullable>");
            sb.i(1).al("</PropertyGroup>");
            sb.b();
            sb.i(1).al("<ItemGroup>");
            sb.i(2).al($"<PackageReference Include=\"MediatR.Extensions.Microsoft.DependencyInjection\" Version=\"{Settings.GetPackageVersion("MediatR.Extensions.Microsoft.DependencyInjection")??"8.0.0"}\" />");
            sb.i(1).al("</ItemGroup>");
            sb.b();
            sb.al("</Project>");

            return new File(_module.Namespace + ".Contracts.csproj", sb.ToString());
        }
    }
}
