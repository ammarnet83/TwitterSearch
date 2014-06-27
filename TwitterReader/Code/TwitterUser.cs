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

namespace TwitterReader
{
    public class TwitterUser
    {
        //public class members
        public string id_str { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string screen_name { get; set; }
        public string profile_image_url { get; set; }
        public bool geo_enabled { get; set; }
    }
}