using System;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Hy.Modeller.Outputs;

namespace DomainClass
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
            var files = new FileGroup();
            if (_model == null)
            {
                foreach (var model in _module.Models)
                    AddModelFiles(files, model);
            }
            else
                AddModelFiles(files, _model);
            return files;
        }

        private void AddModelFiles(FileGroup files, Model model)
        {
            files.AddFile((IFile)new DomainUser(Settings, _module, model).Create());
            files.AddFile((IFile)new DomainGenerated(Settings, _module, model).Create());
            files.AddFile((IFile)new DomainAction(Settings, _module, model).Create());
        }
    }
}