using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using MyOwnLogin.Core.Models;

namespace MyOwnLogin.DataAccess.Helpers
{
    public class RestSharpHelper
    {
        private string WebApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

        public IRestResponse Execute(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters = null,
            bool isAuthorized = false,
            DataFormat dataFormat = DataFormat.Json)
        {
            return !isAuthorized
                ? ExecuteImpl(methodName, method, parameters, null, dataFormat)
                : ExecuteImpl(methodName, method, parameters, GetAccessToken(), dataFormat);
        }

        public IRestResponse<TResult> Execute<TResult>(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters = null,
            bool isAuthorized = false,
            DataFormat dataFormat = DataFormat.Json) where TResult : new()
        {
            return !isAuthorized
                ? ExecuteImpl<TResult>(methodName, method, parameters, null, dataFormat)
                : ExecuteImpl<TResult>(methodName, method, parameters, GetAccessToken(), dataFormat);
        }

        private IRestResponse ExecuteImpl(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters,
            string authorizedToken,
            DataFormat dataFormat)
        {
            RestClient client = new RestClient(WebApiUrl);
            RestRequest request = CreateRequest(methodName, method, parameters, authorizedToken, dataFormat);
            IRestResponse response = client.Execute(request);
            return response;
        }

        private IRestResponse<TResult> ExecuteImpl<TResult>(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters,
            string authorizedToken,
            DataFormat dataFormat) where TResult : new()
        {
            RestClient client = new RestClient(WebApiUrl);
            RestRequest request = CreateRequest(methodName, method, parameters, authorizedToken, dataFormat);
            IRestResponse< TResult> response = client.Execute<TResult>(request);
            return response;
        }

        private RestRequest CreateRequest(
            RestServiceNames methodName,
            Method method,
            Dictionary<string, object> parameters,
            string authorizedToken,
            DataFormat dataFormat)
        {
            string methodUrl = ConfigurationManager.AppSettings[methodName.ToString()];
            UpdateMethodNameWithParameters(ref methodUrl, parameters);

            RestRequest request = new RestRequest(methodUrl, method);
            request.Parameters.Clear();
            request.AddHeader("Accept", "application/json");
            if(authorizedToken != null)
            {
                request.AddHeader("Authorization", authorizedToken);
            }

            AddParameters(request, parameters, dataFormat);

            return request;
        }

        private string GetAccessToken()
        {
            Token token = (Token)HttpContext.Current.Session["access_token"];
            if(token != null)
            {
                var response = ExecuteImpl<object>(RestServiceNames.GetVersion, Method.GET, null, $"{token.TokenType} {token.AccessToken}", DataFormat.None);
                if(response.StatusCode != HttpStatusCode.OK)
                {
                    token = null;
                    HttpContext.Current.Session["access_token"] = null;
                }
            }

            if(token == null)
            {
                System.Web.HttpCookie phoneBookCookie = HttpContext.Current.Request.Cookies["PhoneBookCookie"];

                var response = ExecuteImpl<Token>(RestServiceNames.GetAccessToken,
                    Method.POST,
                    new Dictionary<string, object>()
                    {
                        {"application/json",
                        $"grant_type=refresh_token&refresh_token={phoneBookCookie.Value}"}
                    },
                    null,
                    DataFormat.None);

                token = response.Data;

                phoneBookCookie.Value = token.RefreshToken;
                HttpContext.Current.Response.Cookies.Add(phoneBookCookie);
                HttpContext.Current.Session["access_token"] = token;
            }

            return $"{token.TokenType} {token.AccessToken}";
        }

        private void UpdateMethodNameWithParameters(ref string methodName, Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return;

            var parameterList = parameters.ToList();

            foreach (var item in parameterList)
            {
                string pattern = $"{{{item.Key}}}";

                if(methodName.Contains(pattern))
                {
                    methodName = methodName.Replace(pattern, $"{item.Value}");
                    parameters.Remove(item.Key);
                }
            }
        }

        private void AddParameters(RestRequest request,
            Dictionary<string, object> parameters,
            DataFormat dataFormat)
        {
            if (parameters == null)
                return;

            foreach (var item in parameters)
            {
                if (dataFormat == DataFormat.Json)
                {
                    if (item.Key.ToUpper() == "BODY")
                        request.AddJsonBody(item.Value);
                }
                else
                {
                    request.AddParameter(item.Key, item.Value, ParameterType.RequestBody);
                }
            }
        }
    }
}