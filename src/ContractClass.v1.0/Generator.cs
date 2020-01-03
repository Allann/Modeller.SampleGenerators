using System;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

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
            var v1 = new FileGroup();
            if (_model == null)
            {
                foreach (var model in _module.Models)
                {
                    AddCommandFiles(v1, model);
                    AddQueryFiles(v1, model);
                    AddResponseFiles(v1, model);
                }
            }
            else
            {
                AddCommandFiles(v1, _model);
                AddQueryFiles(v1, _model);
                AddResponseFiles(v1, _model);
            }
            return v1;
        }

        private void AddCommandFiles(FileGroup files, Model model)
        {
            files.AddFile((IFile)new CreateCommand(Settings, _module, model).Create());
            files.AddFile((IFile)new UpdateCommand(Settings, _module, model).Create());
        }

        private void AddQueryFiles(FileGroup files, Model model)
        {
            files.AddFile((IFile)new ByIdQuery(Settings, _module, model).Create());
            files.AddFile((IFile)new EmptyModelQuery(Settings, _module, model).Create());
            files.AddFile((IFile)new ListQuery(Settings, _module, model).Create());
        }

        private void AddResponseFiles(FileGroup files, Model model)
        {
            files.AddFile((IFile)new ListResponse(Settings, _module, model).Create());
            files.AddFile((IFile)new Response(Settings, _module, model).Create());
        }
    }
}
