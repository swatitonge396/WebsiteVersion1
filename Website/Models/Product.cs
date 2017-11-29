using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Product
    {
        public int Id { get; set; }

        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ASIN { set; get; }
        [DataType(DataType.Url)]
        public string DetailPageURL { get; set; }
        [DataType(DataType.ImageUrl)]
        public string LargeImage { get; set; }

        public string Title { get; set; }
        public string Brand { get; set; }
        public int Category { get; set; }
        public string OfferListingId { set; get; }
        public string OtherInfo { get; set; }

        public ICollection<WishList> WishList { get; set; }
        public ICollection<ScrapList> ScrapList { get; set; }
         
    }
}