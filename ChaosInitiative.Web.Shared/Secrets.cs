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

            string secretsFilePath = SECRETS_FILE;
            bool configFileExists = File.Exists(secretsFilePath);

            if (!configFileExists && deploymentType == DeploymentType.Development)
            {
                secretsFilePath = $"../{secretsFilePath}";
                configFileExists = File.Exists(secretsFilePath); // I know, I love this double execution too :)
            }

            if (configFileExists)
            {

                string jsonString = File.ReadAllText(secretsFilePath);
                Console.WriteLine(jsonString);
                SecretsModel model = JsonSerializer.Deserialize<SecretsModel>(jsonString);
                
                if (model == null) return null;
                var appSecrets = model.appSecrets;
                
                switch (deploymentType)
                {
                    case DeploymentType.Production:
                        return appSecrets.production.FirstOrDefault(i => i.Key == key).Value;
                    case DeploymentType.Staging:
                        return appSecrets.staging.FirstOrDefault(i => i.Key == key).Value;
                    case DeploymentType.Development:
                        return appSecrets.development.FirstOrDefault(i => i.Key == key).Value;
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
        public SecretsModelAppSecrets appSecrets { get; set; }
    }

    public class SecretsModelAppSecrets
    {
        public Dictionary<string, string> production { get; set; }
        public Dictionary<string, string> staging { get; set; }
        public Dictionary<string, string> development { get; set; }
    }
}