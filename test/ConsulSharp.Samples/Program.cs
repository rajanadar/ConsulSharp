using System;
using System.IO;
using System.Net;
using System.Text;
using ConsulSharp.Core;
using ConsulSharp.V1;
using ConsulSharp.V1.ACL.Models;
using ConsulSharp.V1.Commons;
using ConsulSharp.V1.Event.Models;
using ConsulSharp.V1.KeyValue.Models;
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

    // raja todo: request equality.

namespace ConsulSharp.Samples
{
    class Program
    {
        private const string ExpectedConsulVersion = "1.2.2";

        private static string _responseContent;
        private static IConsulClient _consulClient;

        private static string _managementToken;
        private static StringBuilder output = new StringBuilder();

        public static void Main(string[] args)
        {
            const string path = "ProgramOutput.txt";

            try
            {
                Console.WriteLine();
                Console.Write("Writing results to file. Hang tight...");

                RunAllSamples();
            }
            finally
            {
                File.WriteAllText(path, output.ToString());
        
                Console.WriteLine();
                Console.Write("I think we are done here. Press any key to exit...");
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

                    output.AppendLine();
                    output.AppendLine("==============================================================================");
                    output.AppendLine();

                    _responseContent = "Actual Consul Server Response: " + value + content;
                    output.AppendLine(_responseContent);

                    output.AppendLine("-------------------------------------");
                }
            });

            RunAclSamples();
            RunEventSamples();
            RunKeyValueSamples();
            RunStatusSamples();
        }

        private static void RunEventSamples()
        {
            output.AppendLine("\n Event Samples \n");

            var fireModel = new FireEventModel
            {
                Name = Guid.NewGuid().ToString(),
                RawPayload = "test"
            };

            var newEvent = _consulClient.V1.Event.FireAsync(new ConsulRequest<FireEventModel> { RequestData = fireModel }).Result;
            DisplayJson(newEvent);
            Assert.Equal(fireModel.Name, newEvent.Data.Name);

            var events = _consulClient.V1.Event.ListAsync(new ConsulRequest<EventFilterModel> { RequestData = fireModel }).Result;
            DisplayJson(events);
            Assert.True(events.Data.Count == 1);
        }

        private static void RunStatusSamples()
        {
            output.AppendLine("\n Status Samples \n");

            var leader = _consulClient.V1.Status.GetRaftLeaderAsync().Result;
            DisplayJson(leader);
            Assert.NotNull(leader.Data);

            var peers = _consulClient.V1.Status.GetRaftPeersAsync().Result;
            DisplayJson(peers);
            Assert.True(peers.Data.Count == 1);
        }

        private static void RunKeyValueSamples()
        {
            output.AppendLine("\n KV Samples \n");

            var createKey = new WriteKeyValueModel
            {
                Key = Guid.NewGuid().ToString(),
                Flags = 32,
                Value = "some-value"
            };

            var createResponse = _consulClient.V1.KeyValue.WriteAsync(new ConsulRequest<WriteKeyValueModel> { RequestData = createKey }).Result;
            DisplayJson(createResponse);
            Assert.True(createResponse.Data);

            var readKeyModel = new ReadKeyValueModel
            {
                Key = createKey.Key,
            };

            var readRequest = new ConsulRequest<ReadKeyValueModel> { RequestData = readKeyModel };

            var readKeyResponse = _consulClient.V1.KeyValue.ReadAsync(readRequest).Result;
            DisplayJson(readKeyResponse);
            Assert.Equal(readKeyModel.Key, readKeyResponse.Data.KeyValueModels[0].Key);

            readRequest.RequestData.RawResponse = true;
            var raw = _consulClient.V1.KeyValue.ReadAsync(readRequest).Result;
            DisplayJson(raw);
            Assert.Equal(createKey.Value, raw.Data.RawValue);

            readRequest.RequestData.KeysOnly = true;
            var keys = _consulClient.V1.KeyValue.ReadAsync(readRequest).Result;
            DisplayJson(keys);
            Assert.True(keys.Data.Keys.Count == 1);

            var deleteResponse = _consulClient.V1.KeyValue.DeleteAsync(new ConsulRequest<DeleteKeyValueModel>
            {
                RequestData = new DeleteKeyValueModel
                {
                    Key = createKey.Key
                }
            }).Result;

            DisplayJson(deleteResponse);
            Assert.True(deleteResponse.Data);

            var consulException = Assert.ThrowsAsync<ConsulApiException>(() => _consulClient.V1.KeyValue.ReadAsync(readRequest)).Result;
            Assert.Equal(HttpStatusCode.NotFound, consulException.HttpStatusCode);
        }

        private static void RunAclSamples()
        {
            output.AppendLine("\n ACL Samples \n");

            var request = new ConsulRequest
            {
                ConsistencyMode = ConsistencyMode.consistent,
                PrettyJsonResponse = true,
                Wait = "10s"
            };

            var managementTokenResponse = _consulClient.V1.ACL.BootstrapAsync(request).Result;
            DisplayJson(managementTokenResponse);
            Assert.NotNull(managementTokenResponse.Data);

            _managementToken = managementTokenResponse.Data;
            _consulClient = new ConsulClient(new ConsulClientSettings("http://127.0.0.1:8500", _managementToken)
            {
                AfterApiResponseAction = r =>
                {
                    var value = ((int)r.StatusCode + "-" + r.StatusCode) + "\n";
                    var content = r.Content != null ? r.Content.ReadAsStringAsync().Result : string.Empty;

                    output.AppendLine();
                    output.AppendLine("==============================================================================");
                    output.AppendLine();

                    _responseContent = "Actual Consul Server Response: " + value + content;
                    output.AppendLine(_responseContent);

                    output.AppendLine("-------------------------------------");
                }
            });

            var consulException = Assert.ThrowsAsync<ConsulApiException>(() => _consulClient.V1.ACL.BootstrapAsync()).Result;
            Assert.Equal(HttpStatusCode.Forbidden, consulException.HttpStatusCode);

            var newTokenModel = new TokenRequestModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "my-app-token",
                Rules = "",
                TokenType = TokenType.client
            };

            var newToken = _consulClient.V1.ACL.CreateTokenAsync(new ConsulRequest<TokenRequestModel> { RequestData = newTokenModel }).Result;
            DisplayJson(newToken);
            Assert.Equal(newTokenModel.Id, newToken.Data);

            var updateTokenModel = new TokenRequestModel
            {
                Id = newTokenModel.Id,
                Name = "updated",
                Rules = "",
                TokenType = TokenType.client
            };

            newToken = _consulClient.V1.ACL.UpdateTokenAsync(new ConsulRequest<TokenRequestModel> { RequestData = updateTokenModel }).Result;
            DisplayJson(newToken);
            Assert.Equal(updateTokenModel.Id, newToken.Data);

            var readToken = _consulClient.V1.ACL.ReadTokenAsync(new ConsulRequest<string> { RequestData = updateTokenModel.Id }).Result;
            DisplayJson(readToken);
            Assert.Equal(updateTokenModel.Name, readToken.Data[0].Name);

            var clonedToken = _consulClient.V1.ACL.CloneTokenAsync(new ConsulRequest<string> { RequestData = readToken.Data[0].Id }).Result;
            DisplayJson(clonedToken);
            Assert.NotNull(clonedToken.Data);

            var list = _consulClient.V1.ACL.ListTokensAsync().Result;
            DisplayJson(list);
            Assert.True(list.Data.Count > 1);

            var repStatus = _consulClient.V1.ACL.CheckReplicationAsync().Result;
            DisplayJson(repStatus);
            Assert.NotNull(repStatus.Data);

            var del1 = _consulClient.V1.ACL.DeleteTokenAsync(new ConsulRequest<string> { RequestData = readToken.Data[0].Id }).Result;
            DisplayJson(del1);

            var tokens = _consulClient.V1.ACL.ReadTokenAsync(new ConsulRequest<string> { RequestData = updateTokenModel.Id }).Result;
            DisplayJson(tokens);
            Assert.True(tokens.Data.Count == 0);

            var del2 = _consulClient.V1.ACL.DeleteTokenAsync(new ConsulRequest<string> { RequestData = clonedToken.Data }).Result;
            DisplayJson(del2);
        }

        private static void DisplayJson<T>(T value)
        {
            var type = typeof(T);
            var genTypes = type.GenericTypeArguments;

            if (genTypes != null && genTypes.Length == 1)
            {
                var genType = genTypes[0];
                var subGenTypes = genType.GenericTypeArguments;

                // single generic. e.g. Response<SomeType>
                if (subGenTypes == null || subGenTypes.Length == 0)
                {
                    output.AppendLine(type.Name.Substring(0, type.Name.IndexOf('`')) + "<" + genType.Name + ">");
                }
                else
                {
                    // single sub-generic e.g. SecretsEngine<IEnumerable<AuthBackend>>
                    if (subGenTypes.Length == 1)
                    {
                        var subGenType = subGenTypes[0];

                        output.AppendLine(type.Name.Substring(0, type.Name.IndexOf('`')) + "<" +
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

                            output.AppendLine(type.Name.Substring(0, type.Name.IndexOf('`')) + "<" +
                                              genType.Name.Substring(0, genType.Name.IndexOf('`')) +
                                              "<" + subGenType1.Name + ", " + subGenType2.Name + ">>");
                        }
                    }
                }
            }
            else
            {
                // non-generic.
                output.AppendLine(type.Name);
            }

            output.AppendLine(JsonConvert.SerializeObject(value));
        }
    }
}
