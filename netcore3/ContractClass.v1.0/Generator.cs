using System;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Hy.Modeller.Outputs;

namespace ContractClass
{
    public class Generator : IGenerator
    {
        private readonly Module _module;
        private readonly Model _model;

        public Generator(ISettings settings, Module module, Model model)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _model = model;
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var files = new FileGroup("v1");
            if (_model == null)
            {
                foreach (var model in _module.Models)
                {
                    AddCommandFiles(files, model);
                }
            }
            else
            {
                AddCommandFiles(files, _model);
            }
            return files;
        }

        private void AddCommandFiles(FileGroup files, Model model)
        {
            files.AddFile((IFile)new CreateCommand(Settings, _module, model).Create());
            files.AddFile((IFile)new DeleteCommand(Settings, _module, model).Create());
            files.AddFile((IFile)new UpdateCommand(Settings, _module, model).Create());
        }

        private void AddQueryFiles(FileGroup files, Model model)
        {
            files.AddFile((IFile)new ByIdQuery(Settings, _module, model).Create());
            files.AddFile((IFile)new EmptyModelQuery(Settings, _module, model).Create());
            files.AddFile((IFile)new ListQuery(Settings, _module, model).Create());
        }
    }
}
