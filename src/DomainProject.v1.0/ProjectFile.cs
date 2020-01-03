using System;
using System.Text;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace Domain
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
            var projectName = _module.Namespace + ".Domain";
            var project = (IProject)new Project(projectName) { Path = System.IO.Path.Combine("src", projectName) };

            var files = new FileGroup();
            project.AddFileGroup(files);

            var sb = new StringBuilder();
            sb.al("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sb.b();
            sb.al("  <PropertyGroup>");
            sb.al("    <TargetFramework>netstandard2.0</TargetFramework>");
            sb.al($"    <RootNamespace>{project.Name}</RootNamespace>");
            sb.al("  </PropertyGroup>");
            sb.b();
            sb.al("  <ItemGroup>");
            sb.al("    <ProjectCapability Include=\"DynamicDependentFile\"/>");
            sb.al("    <ProjectCapability Include=\"DynamicFileNesting\"/>");
            sb.al("  </ItemGroup>");
            sb.b();
            sb.al("  <ItemGroup>");
            sb.al($"    <PackageReference Include=\"FluentValidation\" Version=\"{Settings.GetPackageVersion("FluentValidation")}\" />");
            sb.al($"    <PackageReference Include=\"MediatR\" Version=\"{Settings.GetPackageVersion("MediatR")}\" />");
            sb.al("  </ItemGroup>");
            sb.b();
            sb.al("</Project>");

            var projectFile = new File(project.Name + ".csproj", sb.ToString());
            files.AddFile(projectFile);
            return project;
        }
    }
}