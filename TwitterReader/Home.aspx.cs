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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterReader
{
    public partial class Home : System.Web.UI.Page
    {
        #region public Members
        //special list to view top users
        Dictionary<string, int> topTwitters = new Dictionary<string,int>();
        //special list to view in google map
        List<Tweet> tweetsMap = new List<Tweet>();
        #endregion

        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string hashTag = "#ACME"; //default hash tag
            if (txtHashTag.Text != string.Empty)
            {
                //getting right format for hash tag search word
                txtHashTag.Text = txtHashTag.Text.Replace("#", "");
                txtHashTag.Text = txtHashTag.Text.Replace(" ", "");
                hashTag = "#" + txtHashTag.Text;
            }
            string htmlTweets = string.Empty;
            Twitter twitter = new Twitter(AuthConfig.consumerKey, AuthConfig.consumerSecret, 
                AuthConfig.accessToken, AuthConfig.accessTokenSecret);
            string jsonString = twitter.GetTweets(hashTag, 1000);
            //Deserialize json string to tweets Collection
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            TweetsCollection tweetsCollection = serializer.Deserialize<TweetsCollection>(jsonString);
            //Render tweets to web page:            
            foreach (Tweet t in tweetsCollection.statuses)
            {
                DateTime dt2 = DateTime.ParseExact(t.created_at,
                    "ddd MMM dd HH:mm:ss +0000 yyyy",
                    CultureInfo.InvariantCulture); //Example of current format: Wed Jun 25 17:59:13 +0000 2014
                string created_at = dt2.ToString("ddd MMM dd yyyy, HH:mm:ss");
                string tweetText = t.text;
                tweetText = tweetText.Replace(hashTag, "<span class=\"hashTag\">" + hashTag + "</span>");
                htmlTweets += "<div class=\"twitter-container\">" 
                    + "<img class=\"profileImage\" src=\"" + t.user.profile_image_url + "\" alt=\"\">"
                    + "<a href=\"http://twitter.com/" + t.user.screen_name 
                    + "\" target=_blank>" + t.user.name
                    + "</a> <span class=\"username\">@" + t.user.screen_name + "</span>"
                    + "<div class=\"date\">" + created_at + "</div>"
                    + "<div class=\"tweetTxt\">" + tweetText + "</div>";
                if (t.geo != null)
                {
                    //add tweet to special list to view in google map
                    if (t.geo.ContainsKey("coordinates"))
                    {
                        tweetsMap.Add(t);                      
                    }
                }
                htmlTweets += "</div>";
                AddUserTopTwitters(t.user.name);
            }
            //View tweets
            literalTweets.Text = htmlTweets;
            //Save tweets to view on map
            Session["tweetsMap"] = tweetsMap;
            //get top ten twitters in Dictionary
            topTwitters = GetTopTenTwitters(topTwitters);
            Session["topTwitters"] = topTwitters;
        }

        /// <summary>
        /// Add twitter user to list dictionary
        /// if exist increase value=count by one
        /// </summary>
        /// <param name="twitterUser"></param>
        private void AddUserTopTwitters(string twitterUser)
        {
            if (!topTwitters.ContainsKey(twitterUser))
            {
                topTwitters.Add(twitterUser, 1);
            }
            else
            {
                topTwitters[twitterUser] = topTwitters[twitterUser] + 1;
            }            
        }

        /// <summary>
        /// Sort top twitters dictionary 
        /// and return first ten top twitters
        /// </summary>
        /// <param name="topTwitters"></param>
        /// <returns></returns>
        private Dictionary<string, int> GetTopTenTwitters(Dictionary<string, int> topTwitters)
        {
            Dictionary<string, int> topTwittersTemp = new Dictionary<string, int>();
            int count = 0;
            foreach (var pair in topTwitters.OrderByDescending(pair => pair.Value))
            {
                count++;
                if (count > 10) break;
                topTwittersTemp.Add(pair.Key, pair.Value);
            }
            return topTwittersTemp;
        }
    }
}