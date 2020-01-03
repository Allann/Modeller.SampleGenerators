using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hy.Modeller.Generator;

namespace ContractProject
{
    public class GeneratorDetails : MetadataBase
    {
        public GeneratorDetails() : base("1.0.0")
        { }

        public override string Name => "Contract Project";

        public override string Description => "Build a Contract project";

        public override Type EntryPoint => typeof(Generator);

        public override IEnumerable<Type> SubGenerators => new Collection<Type>()
        {
            typeof(ContractClass.Generator)
        };
    }
}
