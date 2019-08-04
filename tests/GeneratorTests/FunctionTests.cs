using Hy.Modeller;
using Hy.Modeller.Core.Outputs;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GeneratorTests
{
    public class FunctionTests
    {
        [Fact]
        public void Builder_Real_Test()
        {
            var config = new GeneratorConfiguration
            {
                GeneratorName = "DomainProject",
                LocalFolder = "F:\\Repos\\Modeller.SampleGenerators\\src\\Generators",
                Target="netstandard2.0",
                OutputPath = "F:\\dev\\test\\members",
                SourceModel = "F:\\dev\\members_model.json"
            };

            var logger = new Mock<ILogger<IPackageService>>();
            var settingLoader = new JsonSettingsLoader();
            var moduleLoader = new JsonModuleLoader();
            var generatorLoader = new GeneratorLoader();
            var packageLoader = new PackageFileLoader();
            var packageService = new PackageService(packageLoader, logger.Object);

            var context = new Context(config, settingLoader, moduleLoader, generatorLoader, packageService);

            var loggerCG = new Mock<ILogger<ICodeGenerator>>();
            var loggerFW = new Mock<ILogger<FileWriter>>();
            var loggerB = new Mock<ILogger<IBuilder>>();
            var codeGenerator = new CodeGenerator(loggerCG.Object);

            var fileWriter = new FileWriter(loggerFW.Object);
            var fc1 = new CreateFile(fileWriter);
            var fc2 = new CreateFileGroup(fileWriter);
            var fc3 = new CreateProject(fileWriter);
            var fc4 = new CreateSnippet(fileWriter);
            var fc5 = new CreateSolution(fileWriter);
            var list = new List<IFileCreator> { fc1, fc2, fc3, fc4, fc5 };
            var outputStrategy = new OutputStrategy(list);

            var builder = new Builder(context, codeGenerator, outputStrategy, loggerB.Object);
            builder.Create();
        }
    }
}
