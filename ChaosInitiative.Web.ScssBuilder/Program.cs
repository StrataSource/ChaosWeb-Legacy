using System;
using System.IO;
using System.Linq;
using SharpScss;

// This program is meant for use from the IDE. It's not intended to be used as a standalone application.
// All this program does is compile the sass for the individual websites
namespace ChaosInitiative.Web.ScssBuilder
{
    class Program
    {
        private static readonly string OUTPUT_FOLDER_NAME = "css_output";
        private static BuildConfig[] Configs;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Chaos Initiative Web - SCSS Compiler");
            Configure();
            BuildAll();
            DeployAll();
        }

        static void Configure()
        {
            Configs = new[]
            {
                new BuildConfig
                {
                    Name = "p2ce",
                    TargetFolders = new []
                    {
                        "ChaosInitiative.Web.P2CE/wwwroot/css/"
                    },
                    InputFile = "chaotic.scss",
                    OutputFileName = "chaotic_p2ce"
                },
                new BuildConfig
                {
                    Name = "chaos",
                    TargetFolders = new []
                    {
                        "ChaosInitiative.Web.HomePage/wwwroot/css/",
                        "ChaosInitiative.Web.ControlPanel/wwwroot/css/"
                    },
                    InputFile = "chaotic.scss",
                    OutputFileName = "chaotic_chaos"
                }
            };
        }

        static void BuildAll()
        {
            Console.WriteLine("Building all");
            Console.WriteLine("===================================");
            foreach(BuildConfig config in Configs)
            {
                Console.WriteLine($"Building '{config.Name}' at '{config.MainFilePath}'");
                Build(config);
            }
        }

        static void DeployAll()
        {
            Console.WriteLine("Deploying all");
            Console.WriteLine("===================================");
            foreach (BuildConfig config in Configs)
            {
                Console.WriteLine($"Deploying '{config.Name}'");
                foreach (string targetFolder in config.TargetFolders)
                {
                    Console.WriteLine($"-> {targetFolder}");
                }
                Deploy(config);
            }
        }

        static void Build(BuildConfig config)
        {
            ScssOptions options = new ScssOptions
            {
                OutputStyle = ScssOutputStyle.Compressed
            };

            var result = Scss.ConvertFileToCss(config.MainFilePath, options);
            Console.WriteLine($"Built '{config.Name}'");

            Directory.CreateDirectory(OUTPUT_FOLDER_NAME);
            File.WriteAllText($"{OUTPUT_FOLDER_NAME}/{config.OutputCss}", result.Css);
            File.WriteAllText($"{OUTPUT_FOLDER_NAME}/{config.OutputCssMap}", result.SourceMap);
        }

        static void Deploy(BuildConfig config)
        {
            foreach (string targetFolder in config.TargetFolders)
            {
                
                // We're moving down the file tree to find directory we're trying to deploy to.
                // Is is done because using the program in an ide compiles everything into something like
                // 'bin/Debug/netcoreapp3.1/' and sets the current directory to that
            
                // We're only going down 10 levels
                string directoryLevelsDown = "./";
                for (int i = 0; i < 10; i++)
                {
                    if (!Directory.Exists($"{directoryLevelsDown}/{targetFolder}"))
                    {
                        directoryLevelsDown += "../";
                        continue;
                    }

                    break;
                }
                
                File.Copy($"{OUTPUT_FOLDER_NAME}/{config.OutputCss}", $"{directoryLevelsDown}/{targetFolder}/{config.OutputFileName}.min.css", true);
                File.Copy($"{OUTPUT_FOLDER_NAME}/{config.OutputCssMap}", $"{directoryLevelsDown}/{targetFolder}/{config.OutputFileName}.min.css.map", true);
            }
            
        }
        
    }

    class BuildConfig
    {
        public string Name { get; set; }
        public string[] TargetFolders { get; set; }
        public string InputFile { get; set; }
        public string OutputFileName { get; set; }
        
        public string MainFilePath => $"scss/{Name}/{InputFile}";
        public string OutputCss => $"{Name}.css";
        public string OutputCssMap => $"{Name}.css.map";
    }
}