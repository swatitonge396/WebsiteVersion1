using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Website.DBService
{
    public enum Httpverb { GET, POST, PUT, DELETE };

    public class DbClient
    {
        public string EndPoint { get; set; }
        public Httpverb httpMethod { get; set; }

        public DbClient()
        {
            EndPoint = string.Empty;
            httpMethod = Httpverb.GET;

        }
        public string MakeRequest()
        {
            string strResponseValue = string.Empty;



            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint);
            request.Method = httpMethod.ToString();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new ApplicationException("error code " + response.StatusCode.ToString());

                using (Stream responseStream = response.GetResponseStream())
                    if (responseStream != null)
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                //return new StreamReader(responseStream); //testing 

                //Console.WriteLine(strResponseValue);
                return strResponseValue;


            }
        }
    }
}


