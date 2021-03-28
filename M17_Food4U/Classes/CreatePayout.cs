using PayoutsSdk.Core;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace M17_Food4U.Classes
{
    public class CreatePayout
    {
        public static HttpClient client()
        {
            string clientId = ConfigurationManager.AppSettings["pwdEmail"].ToString();
            string secret = ConfigurationManager.AppSettings["pwdEmail"].ToString();

            // Creating a sandbox environment
            PayPalEnvironment environment = new SandboxEnvironment(clientId, secret);

            // Creating a client for the environment
            PayPalHttpClient client = new PayPalHttpClient(environment);
            return client;
        }

    }
}