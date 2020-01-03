using Hy.Modeller.Generator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetCore3Solution
{
    public class GeneratorDetails : MetadataBase
    {
        public GeneratorDetails() : base("1.0.0")
        { }

        public override string Name => "NET Core 3 Solution";

        public override string Description => "Build a netcore 3.x solution";

        public override Type EntryPoint => typeof(Generator);

        public override IEnumerable<Type> SubGenerators => new Collection<Type>()
        {
            typeof(ContractProject.Generator),
            typeof(Header.Generator),
            typeof(Property.Generator)
        };
    }
}
