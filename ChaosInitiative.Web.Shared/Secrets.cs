using System;
using System.IO;

namespace ChaosInitiative.Web.Shared
{
    public class Secrets
    {

        protected static readonly string SECRETS_FILE = "secrets.json";
        
        public static string Get(string key)
        {
            if (File.Exists(SECRETS_FILE))
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new FileNotFoundException("Can't load secrets.json.");
            }
        }
    }
}