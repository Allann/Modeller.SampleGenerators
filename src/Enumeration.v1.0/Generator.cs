using System;
using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace Enumerator
{
    public class Generator : IGenerator
    {
        private readonly Module _module;
        private readonly Enumeration _enumeration;

        public Generator(ISettings settings, Module module, Enumeration enumeration)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _enumeration = enumeration;
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            if (_enumeration == null)
            {
                var files = new FileGroup("Enumerations");
                foreach (var enumeration in _module.Enumerations)
                    AddModelFiles(files, enumeration);
                return files;
            }
            else
                return new EnumerationClass(Settings, _module, _enumeration).Create();
        }

        private void AddModelFiles(FileGroup files, Enumeration enumeration) => files.AddFile((IFile)new EnumerationClass(Settings, _module, enumeration).Create());
    }
}