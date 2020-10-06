using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ChaosInitiative.Web.Shared
{
    public class Secrets
    {

        protected static readonly string SECRETS_FILE = "secrets.json";
        
        public static string Get(string key, DeploymentType deploymentType = DeploymentType.Production)
        {
            if (File.Exists(SECRETS_FILE))
            {

                string jsonString = File.ReadAllText(SECRETS_FILE);
                Console.WriteLine(jsonString);
                SecretsModel model = JsonSerializer.Deserialize<SecretsModel>(jsonString);

                if (model == null) return null;

                switch (deploymentType)
                {
                    case DeploymentType.Production:
                        return model.production.FirstOrDefault(i => i.Key == key).Value;
                    case DeploymentType.Staging:
                        return model.staging.FirstOrDefault(i => i.Key == key).Value;
                    case DeploymentType.Development:

                        Console.WriteLine(model.development.Count);
                        return model.development.FirstOrDefault(i => i.Key == key).Value;
                    default:
                        throw new Exception("Invalid deployment type");
                }
            }

            throw new FileNotFoundException("Can't load secrets.json.");
        }
    }

    public enum DeploymentType
    {
        Production,
        Staging,
        Development
    }
    
    public class SecretsModel
    {
        public Dictionary<string, string> production { get; set; }
        public Dictionary<string, string> staging { get; set; }
        public Dictionary<string, string> development { get; set; }
    }
}