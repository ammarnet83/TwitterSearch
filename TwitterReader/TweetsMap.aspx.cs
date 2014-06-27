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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterReader
{
    public partial class TweetsMap : System.Web.UI.Page
    {
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string googleMapScript = string.Empty;
            if (Session["tweetsMap"] == null)
            {
                lblMessage.Text = "Empty list";
                return;
            }
            //get list of tweets that has coordinates from session variable
            List<Tweet> tweetsMap = (List<Tweet>)Session["tweetsMap"];
            //building map script upon google maps api
            googleMapScript = " "
            + "function setupMap() { "
                + " if (GBrowserIsCompatible()) { "
                + " var map = new GMap2(document.getElementById(\"map\")); "
                + " map.addControl(new GLargeMapControl()); "
                + " map.setCenter(new GLatLng(24.698806, 46.72554), 2); "
                + " map.enableDoubleClickZoom(); "
                + " var icon = new GIcon(); "
                + " icon.image = 'http://gmaps-samples.googlecode.com/svn/trunk/markers/circular/greencirclemarker.png'; "
                + " icon.iconSize = new GSize(32, 32); "
                + " icon.iconAnchor = new GPoint(16, 16); "
                + " icon.infoWindowAnchor = new GPoint(25, 7); ";
            int x = 0;
            for (int i = 0; i < tweetsMap.Count; i++)
            {
                x++;
                string mapLatitude = "";
                string mapLongitude = "";
                if (tweetsMap[i].geo != null)
                {
                    if (tweetsMap[i].geo.ContainsKey("coordinates"))
                    {
                        IEnumerable enumCoordinates = tweetsMap[i].geo["coordinates"] as IEnumerable;
                        int y = 0;
                        foreach (object xy in enumCoordinates)
                        {
                            if (y == 0)
                                mapLatitude = xy.ToString();
                            else mapLongitude = xy.ToString();
                            y++;
                        }
                    }
                }
                //create Marker and event to point of location on google map
                //adding name, text and profile image for the marker window on click
                string Fullname = "<a href='http://twitter.com/" + tweetsMap[i].user.screen_name +
                    "' target=_blank>" + tweetsMap[i].user.name +
                    "</a> @" + tweetsMap[i].user.screen_name;
                string tweetTxt = tweetsMap[i].text;
                string PersonalPhoto = tweetsMap[i].user.profile_image_url;
                googleMapScript += "var latlng" + x.ToString() + " = new GLatLng(" + mapLatitude + "," + mapLongitude + "); "
                            + " opts" + x.ToString() + " = {  "
                            + " \"icon\": icon, "
                            + " \"clickable\": true, "
                            + " \"title\": \"" + tweetsMap[i].user.screen_name + "\", "
                            + " \"labelText\": \"" + tweetsMap[i].user.name + "\", "
                            + " \"labelOffset\": new GSize(-6, -10) "
                            + " }; "
                            + " var marker" + x.ToString() + " = new LabeledMarker(latlng" + x.ToString() + ", opts" + x.ToString() + "); "
                            + " GEvent.addListener(marker" + x.ToString() + ", \"click\", function() { "
                            + " marker" + x.ToString() + ".openInfoWindowHtml(\"" + Fullname + "<br /><img src='" + PersonalPhoto + "' width='48' height='48' /><br />" + tweetTxt + "\"); "
                            + " }); "
                            + " map.addOverlay(marker" + x.ToString() + "); ";
            }
            googleMapScript += " } "
                + " } "
                + " ";
            //run the javascript code on the page
            Page.ClientScript.RegisterStartupScript(this.GetType(), "googleMapScriptKey", googleMapScript, true);
        }
    }
}