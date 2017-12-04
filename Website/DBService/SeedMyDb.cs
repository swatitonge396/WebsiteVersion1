using System;
using System.Collections.Generic;
using System.Linq;
using URLGenerationService;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using Website.Models;
using System.Configuration;

namespace Website.DBService
{
    class SeedMyDb
    {
        public void FillDB(Website.Models.MyDbContext context)
        {

            //define column of data table
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ASIN", typeof(string)));
            dt.Columns.Add(new DataColumn("DetailPageURL", typeof(string)));
            dt.Columns.Add(new DataColumn("LargeImage", typeof(string)));
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Brand", typeof(string)));
            dt.Columns.Add(new DataColumn("Category", typeof(int)));
            dt.Columns.Add(new DataColumn("OfferListingId", typeof(string)));
            dt.Columns.Add(new DataColumn("OtherInfo", typeof(string)));



            //setup browseNode and ItemPage
            GenerateURL url = new GenerateURL();
            var categoryRec = (from rec in context.Category select rec).ToList();
            foreach (var rec in categoryRec)
            {
                dt.Clear();
                url.BrowseNode = rec.Code;

                for (int i = 1; i < 11; i++)
                {
                    string XmlResponse;
                    XmlResponse = string.Empty;
                    url.ItemPage = i.ToString();
                    DbClient c = new DbClient() { EndPoint = url.GetURL() };
                    //Get Response
                    XmlResponse = c.MakeRequest();

                    //process Response if not null
                    if (!string.IsNullOrEmpty(XmlResponse))
                    {
                        XElement ItemSearchResponse = XElement.Parse(XmlResponse);
                        XNamespace ns = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";
                        IEnumerable<XElement> Items = from node in ItemSearchResponse.Descendants(ns + "Item") select node;


                        //set data table column

                        foreach (var item in Items)
                        {
                            var image = item.Element(ns + "LargeImage")?.Element(ns + "URL")?.Value;
                            if (image != null)
                            {
                                Product p = new Product();
                                p.ASIN = item.Element(ns + "ASIN").Value;
                                p.DetailPageURL = item.Element(ns + "DetailPageURL").Value;
                                p.LargeImage = image;
                                p.Category = rec.Id;
                                p.OfferListingId = item.Descendants(ns + "OfferListingId")?.FirstOrDefault()?.Value;
                                p.Title = item.Descendants(ns + "Title")?.FirstOrDefault()?.Value;
                                p.Brand = item.Descendants(ns + "Brand")?.FirstOrDefault()?.Value;
                                //put switch for category
                                if (rec.Id == 6)
                                    p.OtherInfo = "Author : " + item.Descendants(ns + "Author")?.FirstOrDefault()?.Value; ;
                                // dt.Rows.Add(p.DetailPageURL, p.ASIN, p.LargeImage, p.Title, p.Brand, p.Category, p.OfferListingId, p.OtherInfo);
                                context.Products.Add(p);
                            }
                        }//for
                        //System.Threading.Thread.Sleep(1000);

                    }

                    //connection string
                    // string connectionString= ConfigurationManager.ConnectionStrings["MyConnection1"].ConnectionString;
                    /* SqlConnection cn = new SqlConnection(connectionString);
                     cn.Open();
                         using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cn))
                         {
                             bulkCopy.DestinationTableName =
                              "dbo.Products";

                             try
                             {
                                 // Write from the source to the destination.
                                 bulkCopy.WriteToServer(dt);
                             }
                             catch (Exception ex)
                             {
                                 Console.WriteLine(ex.Message);
                             }
                         }
                         cn.Close();*/

                    // SleepTimer 10 seconds for Amazon Request to avoid error
                     System.Threading.Thread.Sleep(10000);

                } //end of page

            }//end of category
        }
    }
}