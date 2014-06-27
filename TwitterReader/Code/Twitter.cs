/*
     * * * * * * * * * * * * * * * * * * *
    *               AMMAR DIB              *
   *       Realese Date: 25 Jun 2014        *
  *          Twitter Reader System           *
   *                                        *
    *      Y!   ammarnet83@yahoo.com       *
     * * * * * * * * * * * * * * * * * * *
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace TwitterReader
{
    public class Twitter
    {
        //public class members
        public const string OauthVersion = "1.0";
        public const string OauthSignatureMethod = "HMAC-SHA1";
        public string ConsumerKey { set; get; }
        public string ConsumerKeySecret { set; get; }
        public string AccessToken { set; get; }
        public string AccessTokenSecret { set; get; }

        private enum resultType
        { 
            /*
            * mixed: Include both popular and real time results in the response.
            * recent: return only the most recent results in the response
            * popular: return only the most popular results in the response.
            */
            mixed,
            recent,
            popular
        }
     
        /// <summary>
        /// Twitter class constructor
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerKeySecret"></param>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenSecret"></param>
        public Twitter(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerKeySecret = consumerKeySecret;
            this.AccessToken = accessToken;
            this.AccessTokenSecret = accessTokenSecret;
        }

        /// <summary>
        /// GetTweets retrieves json object as string
        /// upon twitterHashTag and count parameters
        /// </summary>
        /// <param name="twitterHashTag"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string GetTweets(string twitterHashTag, int count)
        {
            string resourceUrl = "https://api.twitter.com/1.1/search/tweets.json";
            var requestParameters = new SortedDictionary<string,string>();
            requestParameters.Add("q", twitterHashTag);
            requestParameters.Add("count", count.ToString());
            requestParameters.Add("result_type", resultType.mixed.ToString());
            requestParameters.Add("include_entities", "false");
            var response = GetResponse(resourceUrl, "GET", requestParameters);
            return response;
        }

        /// <summary>
        /// uses WebRequest to retrieve tweets from resource url 
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <param name="methodName"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        private string GetResponse(string resourceUrl, string methodName, SortedDictionary<string, string> requestParameters)
        {
            WebRequest request = null;
            string resultString = string.Empty;
            //create web request object
            request = (HttpWebRequest)WebRequest.Create(resourceUrl + "?" + ConvertToWebString(requestParameters));
            request.Method = methodName;
            request.ContentType = "application/x-www-form-urlencoded";
            //add authorization to request header
            if (request != null)
            {
                var authHeader = CreateHeader(resourceUrl, methodName, requestParameters);
                request.Headers.Add("Authorization", authHeader);
 
                var response = (HttpWebResponse)request.GetResponse();
                using (var sd = new StreamReader(response.GetResponseStream()))
                {
                    resultString = sd.ReadToEnd();
                    response.Close();
                }
            }
            return resultString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string CreateOauthNonce()
        {
            return Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        } 
        
        /// <summary>
        /// create request header
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <param name="methodName"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        private string CreateHeader(string resourceUrl, string methodName, SortedDictionary<string, string> requestParameters)
        {
            var oauthNonce = CreateOauthNonce();
            // Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString())); 
            var oauthTimestamp = CreateOAuthTimestamp();
            var oauthSignature = CreateOauthSignature(resourceUrl, methodName, oauthNonce, oauthTimestamp, requestParameters);
            //The oAuth signature is then used to generate the Authentication header. 
            const string headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " + "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " + "oauth_token=\"{4}\", oauth_signature=\"{5}\", " + "oauth_version=\"{6}\"";
            var authHeader = string.Format(headerFormat, Uri.EscapeDataString(oauthNonce), Uri.EscapeDataString(OauthSignatureMethod), Uri.EscapeDataString(oauthTimestamp), Uri.EscapeDataString(ConsumerKey), Uri.EscapeDataString(AccessToken), Uri.EscapeDataString(oauthSignature), Uri.EscapeDataString(OauthVersion));
            return authHeader;
        }

        /// <summary>
        /// create authentication signature by using Open Authentication
        /// this parameters are generated by dev.twitter.com/apps
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <param name="method"></param>
        /// <param name="oauthNonce"></param>
        /// <param name="oauthTimestamp"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        private string CreateOauthSignature(string resourceUrl, string method, string oauthNonce, string oauthTimestamp, SortedDictionary<string, string> requestParameters)
        {
            //firstly we need to add the standard oauth parameters to the sorted list 
            requestParameters.Add("oauth_consumer_key", ConsumerKey);
            requestParameters.Add("oauth_nonce", oauthNonce);
            requestParameters.Add("oauth_signature_method", OauthSignatureMethod);
            requestParameters.Add("oauth_timestamp", oauthTimestamp);
            requestParameters.Add("oauth_token", AccessToken);
            requestParameters.Add("oauth_version", OauthVersion);
            var sigBaseString = ConvertToWebString(requestParameters);
            var signatureBaseString = string.Concat(method, "&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(sigBaseString.ToString()));
             
            var compositeKey = string.Concat(Uri.EscapeDataString(ConsumerKeySecret), "&", Uri.EscapeDataString(AccessTokenSecret));
            string oauthSignature;
            using (var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauthSignature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(signatureBaseString)));
            }
            return oauthSignature;
        }

        /// <summary>
        /// Create Open Auth Time stamp
        /// </summary>
        /// <returns></returns>
        private string CreateOAuthTimestamp()
        {
            var nowUtc = DateTime.UtcNow;
            var timeSpan = nowUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString(); return timestamp;
        }

        /// <summary>
        /// convert dictionary collection of pairs(key, value)
        /// into url web string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string ConvertToWebString(SortedDictionary<string, string> source)
        {
            var body = new StringBuilder();
            if (source.Count != 0)
            {
                foreach (var requestParameter in source)
                {
                    body.Append(requestParameter.Key);
                    body.Append("=");
                    body.Append(Uri.EscapeDataString(requestParameter.Value));
                    body.Append("&");
                }
                //remove trailing '&'
                body.Remove(body.Length - 1, 1);

            }
            return body.ToString();
        }
    }
}