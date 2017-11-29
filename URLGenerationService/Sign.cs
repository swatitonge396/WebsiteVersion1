using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace URLGenerationService
{
    class Sign : IDisposable
    {
        private string endPoint;
        private string akid;
        private HMAC signer;

        private const string REQUEST_URI = "/onca/xml";
        private const string REQUEST_METHOD = "GET";
        private static readonly string[] UriRfc3986CharsToEscape = new[] { "!", "*", "'", "(", ")" };

        public Sign(Authentication authentication) //authentication have secrete key and Access key
        {
            var domain = "amazon.com"; //country
            var secret = Encoding.UTF8.GetBytes(authentication.SecretKey);

            this.endPoint = $"webservices.{domain}";
            this.akid = authentication.AccessKey;
            this.signer = new HMACSHA256(secret);
        }

        public void Dispose()
        {
            if (this.signer == null)
            {
                return;
            }

            this.signer.Dispose();
        }

        public string SignURL(ParamURL Operation)
        {
            return this.SignURL(Operation.ParamDictionary);
        }


        public string SignURL(IDictionary<string, string> request)
        {
            request.Add("AWSAccessKeyId", this.akid);

            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            //string timestamp = "2017-11-11T14:32:38.000Z";
            request.Add("Timestamp", timestamp);

            request = this.GetRequestArguments(request);

            var canonicalQS = this.ConstructCanonicalQueryString(request);

            var signHeader = string.Format("{0}\n{1}\n{2}\n{3}", REQUEST_METHOD, this.endPoint, REQUEST_URI, canonicalQS);
            var signHeaderBytes = Encoding.UTF8.GetBytes(signHeader);

            // Compute the signature and convert to Base64.
            var sigBytes = signer.ComputeHash(signHeaderBytes);
            var signature = Convert.ToBase64String(sigBytes);

            // now construct the complete URL and return to caller.
            var sb = new StringBuilder();
            sb.Append("http://");
            sb.Append(this.endPoint);
            sb.Append(REQUEST_URI);
            sb.AppendFormat("?{0}", canonicalQS);
            sb.AppendFormat("&Signature={0}", this.EscapeUriDataStringRfc3986(signature));
            return sb.ToString();
        }
        private IDictionary<string, string> GetRequestArguments(IDictionary<string, string> operationArguments)
        {
            var requestArgs = new Dictionary<string, string> { ["Service"] = "AWSECommerceService", ["Sort"] = "salesrank" };
            foreach (var key in operationArguments.Keys)
            {
                requestArgs[key] = operationArguments[key];
            }
            return requestArgs;
        }
        public string EscapeUriDataStringRfc3986(string value)
        {
            // Start with RFC 2396 escaping by calling the .NET method to do the work.
            // This MAY sometimes exhibit RFC 3986 behavior (according to the documentation).
            // If it does, the escaping we do that follows it will be a no-op since the
            // characters we search for to replace can't possibly exist in the string.
            var sb = new StringBuilder(Uri.EscapeDataString(value));
            // Upgrade the escaping to RFC 3986, if necessary.
            for (var i = 0; i < UriRfc3986CharsToEscape.Length; i++)
            {
                sb.Replace(UriRfc3986CharsToEscape[i], Uri.HexEscape(UriRfc3986CharsToEscape[i][0]));
            }
            // Return the fully-RFC3986-escaped string.
            return sb.ToString();
        }
        private string ConstructCanonicalQueryString(IDictionary<string, string> request)
        {
            if (request.Count == 0)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var kvp in request.OrderBy(o => o.Key, StringComparer.Ordinal))
            {
                sb.Append(this.EscapeUriDataStringRfc3986(kvp.Key));
                sb.Append("=");
                sb.Append(this.EscapeUriDataStringRfc3986(kvp.Value));
                sb.Append("&");
            }
            return sb.ToString().TrimEnd('&');
        }
    }
}
