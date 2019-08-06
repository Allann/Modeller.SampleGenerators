using Hy.Modeller.GeneratorBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Enumerator
{
    public class GeneratorDetails : MetadataBase
    {
        public GeneratorDetails() : base("1.0.0")
        { }

        public override string Name => "Enumerator";

        public override string Description => "Build a Enumerator class";

        public override Type EntryPoint => typeof(Generator);

        public override IEnumerable<Type> SubGenerators => new Collection<Type>() { typeof(Property.Generator), typeof(Header.Generator) };
    }
}
