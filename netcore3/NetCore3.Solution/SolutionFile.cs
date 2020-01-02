using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Hy.Modeller.Outputs;
using System;
using System.Text;

namespace NetCore3.Solution
{
    internal class SolutionFile : IGenerator
    {
        private readonly Module _module;

        public SolutionFile(ISettings settings, Module module)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        public ISettings Settings { get; }

        public IOutput Create()
        {
            var projectName = _module.Namespace;
            var project = (ISolution)new Hy.Modeller.Outputs.Solution("src");

            var files = new FileGroup();
            //project.AddProject();

            var sb = new StringBuilder();

            sb.al("Microsoft Visual Studio Solution File, Format Version 12.00");
            sb.al("# Visual Studio Version 16");
            sb.al("VisualStudioVersion = 16.0.29521.150");
            sb.al("MinimumVisualStudioVersion = 10.0.40219.1");
            sb.al("Project(\"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}\") = \"Jbssa.ASAP.Api\", \"Jbssa.ASAP.Api\\Jbssa.ASAP.Api.csproj\", \"{5C6EE233-DA11-41E9-A501-29B73FBFC5C7}\"");
            sb.al("EndProject");
            sb.al("Project(\"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}\") = \"Jbssa.ASAP.Contracts\", \"Jbssa.ASAP.Contracts\\Jbssa.ASAP.Contracts.csproj\", \"{15BAA6ED-4AB2-4353-8D27-B4C5050B94C2}\"");
            sb.al("EndProject");
            sb.al("Project(\"{9A19103F-16F7-4668-BE54-9A1E7A4F7556}\") = \"Jbssa.ASAP\", \"Jbssa.ASAP.Business\\Jbssa.ASAP.csproj\", \"{C526BACD-0897-46B9-8429-1F2CB2F69DEB}\"");
            sb.al("EndProject");
            sb.al("Project(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\") = \"clients\", \"clients\", \"{241ED872-4DBC-47D2-AF1B-BC088D21E46F}\"");
            sb.al("EndProject");
            sb.al("Project(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\") = \"core\", \"core\", \"{98A16A4C-4E12-416D-888F-E6A10BDE1E87}\"");
            sb.al("EndProject");
            sb.al("Project(\"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}\") = \"Jbssa.ASAP.Web\", \"Jbssa.ASAP.Web\\Jbssa.ASAP.Web.csproj\", \"{3B6750D4-6D56-4FE7-A87D-02928514ACE3}\"");
            sb.al("EndProject");
            sb.al("Project(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\") = \"tests\", \"tests\", \"{7E10A88A-53AD-4327-885E-BBBD20C44318}\"");
            sb.al("EndProject");
            sb.al("Project(\"{2150E333-8FDC-42A3-9474-1A3956D46DE8}\") = \"Solution Items\", \"Solution Items\", \"{BC21D543-8425-4025-AC14-7312D1849307}\"");
            sb.i(1).al("ProjectSection(SolutionItems) = preProject");
            sb.i(2).al("Readme.md = Readme.md");
            sb.i(1).al("EndProjectSection");
            sb.al("EndProject");
            sb.al("Global");
            sb.i(1).al("GlobalSection(SolutionConfigurationPlatforms) = preSolution");
            sb.i(2).al("Debug|Any CPU = Debug|Any CPU");
            sb.i(2).al("Release|Any CPU = Release|Any CPU");
            sb.i(1).al("EndGlobalSection");
            sb.i(1).al("GlobalSection(ProjectConfigurationPlatforms) = postSolution");
            sb.i(2).al("{5C6EE233-DA11-41E9-A501-29B73FBFC5C7}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            sb.i(2).al("{5C6EE233-DA11-41E9-A501-29B73FBFC5C7}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            sb.i(2).al("{5C6EE233-DA11-41E9-A501-29B73FBFC5C7}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            sb.i(2).al("{5C6EE233-DA11-41E9-A501-29B73FBFC5C7}.Release|Any CPU.Build.0 = Release|Any CPU");
            sb.i(2).al("{15BAA6ED-4AB2-4353-8D27-B4C5050B94C2}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            sb.i(2).al("{15BAA6ED-4AB2-4353-8D27-B4C5050B94C2}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            sb.i(2).al("{15BAA6ED-4AB2-4353-8D27-B4C5050B94C2}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            sb.i(2).al("{15BAA6ED-4AB2-4353-8D27-B4C5050B94C2}.Release|Any CPU.Build.0 = Release|Any CPU");
            sb.i(2).al("{C526BACD-0897-46B9-8429-1F2CB2F69DEB}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            sb.i(2).al("{C526BACD-0897-46B9-8429-1F2CB2F69DEB}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            sb.i(2).al("{C526BACD-0897-46B9-8429-1F2CB2F69DEB}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            sb.i(2).al("{C526BACD-0897-46B9-8429-1F2CB2F69DEB}.Release|Any CPU.Build.0 = Release|Any CPU");
            sb.i(2).al("{3B6750D4-6D56-4FE7-A87D-02928514ACE3}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            sb.i(2).al("{3B6750D4-6D56-4FE7-A87D-02928514ACE3}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            sb.i(2).al("{3B6750D4-6D56-4FE7-A87D-02928514ACE3}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            sb.i(2).al("{3B6750D4-6D56-4FE7-A87D-02928514ACE3}.Release|Any CPU.Build.0 = Release|Any CPU");
            sb.i(1).al("EndGlobalSection");
            sb.i(1).al("GlobalSection(SolutionProperties) = preSolution");
            sb.i(2).al("HideSolutionNode = FALSE");
            sb.i(1).al("EndGlobalSection");
            sb.i(1).al("GlobalSection(NestedProjects) = preSolution");
            sb.i(2).al("{5C6EE233-DA11-41E9-A501-29B73FBFC5C7} = {241ED872-4DBC-47D2-AF1B-BC088D21E46F}");
            sb.i(2).al("{15BAA6ED-4AB2-4353-8D27-B4C5050B94C2} = {98A16A4C-4E12-416D-888F-E6A10BDE1E87}");
            sb.i(2).al("{C526BACD-0897-46B9-8429-1F2CB2F69DEB} = {98A16A4C-4E12-416D-888F-E6A10BDE1E87}");
            sb.i(2).al("{3B6750D4-6D56-4FE7-A87D-02928514ACE3} = {241ED872-4DBC-47D2-AF1B-BC088D21E46F}");
            sb.i(1).al("EndGlobalSection");
            sb.i(1).al("GlobalSection(ExtensibilityGlobals) = postSolution");
            sb.i(2).al("SolutionGuid = {2534C3B5-2B34-4F1B-B2E8-304057E44B0F}");
            sb.i(1).al("EndGlobalSection");
            sb.al("EndGlobal");

            var projectFile = new File { Name = project.Name + ".sln", Content = sb.ToString(), CanOverwrite = false };
            files.AddFile(projectFile);
            return project;
        }
    }

}
