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
    public class TweetsCollection
    {
        //Used to deserialize JSON object into colection of tweet objects
        public IEnumerable<Tweet> statuses { get; set; }
    }
}