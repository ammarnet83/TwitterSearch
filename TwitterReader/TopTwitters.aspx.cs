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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterReader
{
    public partial class TopTwitters : System.Web.UI.Page
    {
        /// <summary>
        /// page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["topTwitters"] == null)
            {
                lblMessage.Text = "Empty List";
                return;
            }
            Dictionary<string, int> topTwitters = (Dictionary<string, int>)Session["topTwitters"];
            pieChart1.ChartTitle = "Top Twitters";
            foreach (var pair in topTwitters.OrderByDescending(pair => pair.Value))
            {
                AjaxControlToolkit.PieChartValue item = new AjaxControlToolkit.PieChartValue();
                item.Category = pair.Key;
                item.Data = pair.Value;
                item.PieChartValueStrokeColor = "black";
                pieChart1.PieChartValues.Add(item);
            }
            
        }
    }
}