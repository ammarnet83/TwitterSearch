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
    public class Tweet
    {
        //public class members
        public string created_at { get; set; }
        public string id_str { get; set; }
        public string text { get; set; }
        public TwitterUser user { get; set; }
        public Dictionary<string, object> geo;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Tweet()
        {
            created_at = "";
            id_str = "";
            text = "";
            user = null;
            geo = null;
        }
    }
}