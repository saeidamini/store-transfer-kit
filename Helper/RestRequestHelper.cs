using Newtonsoft.Json;
using RestSharp;
using StoreTransferKit.Models;

namespace StoreTransferKit.Helper {
    /// <summary>
    /// Request need to set some properties in every call. To avoid this setting in all method,
    /// create `request` object in class property level.
    /// </summary>
    public class RestRequestHelper {

        public Method Method { get; set; }
        public string Resource { get; set; }
        public RestRequestHelper() {
            Resource = "";
            Method = Method.GET;
        }

        public RestRequestHelper(string resource) {
            Resource = resource ?? "";
            Method = Method.GET;
        }

        public RestRequestHelper(string resource, Method method) {
            Resource = resource ?? "";
            Method = method;
        }

        public RestRequest prepare() {

            var method = this.Method;
            var resource = this.Resource;
            var request = new RestRequest(this.Resource, this.Method);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var AuthenticationToken = ConfigurationHelper.ReadSetting("authenticationToken");
            if (!string.IsNullOrEmpty(AuthenticationToken)) {
                request.AddHeader("Authorization", string.Format("Bearer {0}", AuthenticationToken));
            } else {
                var client = new RestClient(ConfigurationHelper.ReadSetting("serverUrl"));
                request.Method = Method.POST;
                request.Resource = "authenticate";

                request.AddParameter("login_data", $"{{\"password\":\"{ConfigurationHelper.ReadSetting("password")}\",\"username\":\"{ConfigurationHelper.ReadSetting("username")}\"}}", ParameterType.RequestBody);
                var response = client.Execute(request);
                var returnData = JsonConvert.DeserializeObject<Login>(response.Content);

                if (!string.IsNullOrEmpty(returnData.id_token)) {
                    AuthenticationToken = returnData.id_token;
                    ConfigurationHelper.AddUpdateAppSettings("authenticationToken", AuthenticationToken);
                    request.AddHeader("Authorization", string.Format("Bearer {0}", AuthenticationToken));
                } else {
                    throw new System.Security.Authentication.AuthenticationException("Invalid username or password. Plese check `app.config` file.");
                }
                request.Resource = resource;
                request.Method = method;
                request.AddOrUpdateParameter("login_data", "");
            }
            return request;
        }
    }
}