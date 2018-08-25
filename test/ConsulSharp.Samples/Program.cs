using System;
using System.IO;
using System.Net;
using ConsulSharp.Core;
using ConsulSharp.V1.Commons;
using Newtonsoft.Json;
using Xunit;

/*
 * Before running these samples:
 * 1. create a data dir and config dir
 * 2. add the following config file in the config dir
 
    {
     "acl_datacenter": "dc1",
     "acl_default_policy": "deny",
     "acl_down_policy": "extend-cache"
    }

 * 3. start your Consul Server as follows:
  
  consul agent -server -bind 127.0.0.1 -data-dir e:\consul\data -config-dir E:\consul\config\ -bootstrap-expect 1
  
 */

namespace ConsulSharp.Samples
{
    class Program
    {
        private const string ExpectedConsulVersion = "1.2.2";

        private static string _responseContent;

        private static IConsulClient _consulClient;

        private static string _managementToken;

        public static void Main(string[] args)
        {
            const string path = "ProgramOutput.txt";

            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    Console.WriteLine();
                    Console.Write("Writing results to file. Hang tight...");

                    var existingOut = Console.Out;
                    Console.SetOut(sw);

                    RunAllSamples();

                    Console.SetOut(existingOut);

                    Console.WriteLine();
                    Console.Write("I think we are done here. Press any key to exit...");
                }
            }

            Console.ReadLine();
        }

        private static void RunAllSamples()
        {
            _consulClient = new ConsulClient(new ConsulClientSettings("http://127.0.0.1:8500", null)
            {
                AfterApiResponseAction = r =>
                {
                    var value = ((int)r.StatusCode + "-" + r.StatusCode) + "\n";
                    var content = r.Content != null ? r.Content.ReadAsStringAsync().Result : string.Empty;

                    _responseContent = "From Consul Server: " + value + content;

                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        Console.WriteLine(_responseContent);
                    }
                }
            });

            RunAclSamples();
        }

        private static void RunAclSamples()
        {
            var request = new Request
            {
                ConsistencyMode = ConsistencyMode.consistent,
                PrettyJsonResponse = true,
                Wait = "10s"
            };

            var managementTokenResponse = _consulClient.V1.ACL.BootstrapAsync(request).Result;
            Assert.NotNull(managementTokenResponse.ResponseData);

            _managementToken = managementTokenResponse.ResponseData;

            var consulException = Assert.ThrowsAsync<ConsulApiException>(() => _consulClient.V1.ACL.BootstrapAsync()).Result;
            Assert.Equal(HttpStatusCode.Forbidden, consulException.HttpStatusCode);
        }

        private static void DisplayJson<T>(T value)
        {
            string line = "===========";

            var type = typeof(T);
            var genTypes = type.GenericTypeArguments;

            if (genTypes != null && genTypes.Length == 1)
            {
                var genType = genTypes[0];
                var subGenTypes = genType.GenericTypeArguments;

                // single generic. e.g. SecretsEngine<AuthBackend>
                if (subGenTypes == null || subGenTypes.Length == 0)
                {
                    Console.WriteLine(type.Name.Substring(0, type.Name.IndexOf('`')) + "<" + genType.Name + ">");
                }
                else
                {
                    // single sub-generic e.g. SecretsEngine<IEnumerable<AuthBackend>>
                    if (subGenTypes.Length == 1)
                    {
                        var subGenType = subGenTypes[0];

                        Console.WriteLine(type.Name.Substring(0, type.Name.IndexOf('`')) + "<" +
                                          genType.Name.Substring(0, genType.Name.IndexOf('`')) +
                                          "<" + subGenType.Name + ">>");
                    }
                    else
                    {
                        // double generic. e.g. SecretsEngine<Dictionary<string, AuthBackend>>
                        if (subGenTypes.Length == 2)
                        {
                            var subGenType1 = subGenTypes[0];
                            var subGenType2 = subGenTypes[1];

                            Console.WriteLine(type.Name.Substring(0, type.Name.IndexOf('`')) + "<" +
                                              genType.Name.Substring(0, genType.Name.IndexOf('`')) +
                                              "<" + subGenType1.Name + ", " + subGenType2.Name + ">>");
                        }
                    }
                }
            }
            else
            {
                // non-generic.
                Console.WriteLine(type.Name);
            }

            Console.WriteLine(line + line);
            Console.WriteLine(_responseContent);
            Console.WriteLine(JsonConvert.SerializeObject(value));
            Console.WriteLine(line + line);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
