using System;
using System.Linq;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace ContractProject
{
    public class Generator : IGenerator
    {
        private readonly Module _module;

        public Generator(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var projectName = _module.Namespace + ".Contracts";
            var project = (IProject)new Project(projectName) { Path = System.IO.Path.Combine("src", projectName) };

            var root = new FileGroup();
            root.AddFile((IFile)new ProjectFile(Settings, _module).Create());
            root.AddFile((IFile)new IdClass(Settings, _module).Create());

            root.AddFile((IFile)new DeleteCommand(Settings, _module).Create());
            root.AddFile((IFile)new BlockCreateQuery(Settings, _module).Create());
            root.AddFile((IFile)new LookupQuery(Settings, _module).Create());
            root.AddFile((IFile)new CreatedResponse(Settings, _module).Create());
            root.AddFile((IFile)new DeletedResponse(Settings, _module).Create());
            root.AddFile((IFile)new ExistsResponse(Settings, _module).Create());
            root.AddFile((IFile)new LookupResponse(Settings, _module).Create());
            root.AddFile((IFile)new UpdatedResponse(Settings, _module).Create());
            project.AddFileGroup(root);

            foreach (var item in _module.Models)
                project.AddFileGroup((IFileGroup)new ContractClass.Generator(Settings, _module, item).Create());
            return project;
        }
    }
}
