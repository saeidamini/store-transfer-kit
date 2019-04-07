using System;
using System.Collections.Generic;
using RestSharp;
using StoreTransferKit.Helper;
using StoreTransferKit.Models;
using Newtonsoft.Json;

namespace StoreTransferKit.Callers {
    public class FactorCaller : ICaller<Factor> {
        private RestClient client;

        public FactorCaller(string baseUrl) {
            client = new RestClient(baseUrl);
        }

        public List<Factor> GetItems() {

            var request = new RestRequestHelper("factors", Method.GET);

            var response = client.Execute<List<Factor>>(request.prepare());
            if (response.ErrorException != null) {
                throw response.ErrorException;
            }
            return response.Data;
        }
        
        public string GetMaxRefID() {

            var request = new RestRequestHelper("factors/maxrefid", Method.GET);

            var response = client.Execute<int>(request.prepare());
            if (response.ErrorException != null) {
                throw response.ErrorException;
            }
            return response.Content;
        }
        
        public string Count() {

            var request = new RestRequestHelper("factors/count", Method.GET);

            var response = client.Execute<int>(request.prepare());
            if (response.ErrorException != null) {
                throw response.ErrorException;
            }
            return response.Content;
        }

        public IRestResponse Create(Factor factor) {
            var request = new RestRequestHelper("factors", Method.POST);

            var req= request.prepare().AddJsonBody(JsonConvert.SerializeObject(factor));
            IRestResponse response = client.Execute(req);

            if (response.ErrorException != null || (int)response.StatusCode > 300) {
                throw  new ApplicationException(response.ErrorMessage + response.Content);
            }

            return response;
        }

        public void Update(int id, Factor Factor) {
            var request = new RestRequestHelper("factor/" + id, Method.PUT);
            request.Resource = "factors";
            request.Method = Method.PUT;

            IRestResponse response = client.Execute(request.prepare().AddJsonBody(Factor));

            if (response.ErrorException != null) {
                throw response.ErrorException;
            }
        }

        public void Delete(int id) {
            var request = new RestRequestHelper("factor/" + id, Method.DELETE);

            IRestResponse response = client.Execute(request.prepare());

            if (response.ErrorException != null) {
                throw response.ErrorException;
            }
        }
    }
}