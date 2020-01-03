using Hy.Modeller.Generator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ContractClass
{
    public class GeneratorDetails : MetadataBase
    {
        public GeneratorDetails() : base("1.0.0")
        { }

        public override string Name => "ContractClass";

        public override string Description => "Build a contract class file group";

        public override Type EntryPoint => typeof(Generator);

        public override IEnumerable<Type> SubGenerators => new Collection<Type>() { typeof(Property.Generator), typeof(Header.Generator) };
    }
}
