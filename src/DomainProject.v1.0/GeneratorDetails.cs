using Hy.Modeller.GeneratorBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class GeneratorDetails : MetadataBase
    {
        public GeneratorDetails() : base("1.0.0")
        { }

        public override string Name => "Domain Project";

        public override string Description => "Build a netstandard2.0 Domain project";

        public override Type EntryPoint => typeof(Generator);

        public override IEnumerable<Type> SubGenerators => new Collection<Type>()
        {
            typeof(DomainClass.Generator),
            typeof(Header.Generator),
            typeof(Property.Generator)
        };
    }
}