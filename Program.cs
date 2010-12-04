using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;

namespace Stock
{
    class Program
    {
        static float GetStockPrice()
        {
            string baseUrl = "http://www.google.com/finance?q=BOM:533098";

            float stockPrice = 0;

            HttpWebRequest req = null;
            HttpWebResponse res = null;
            StreamReader reader = null;

            try
            {

                req = (HttpWebRequest)HttpWebRequest.Create(baseUrl);
                res = (HttpWebResponse)req.GetResponse();
                reader = new StreamReader(res.GetResponseStream());
                
                string pageContents = reader.ReadToEnd();

                int startPricePanel = pageContents.IndexOf("<div id=price-panel");
                int endPricePanel = pageContents.IndexOf("</span>", startPricePanel);
                string pricePanel = pageContents.Substring(startPricePanel, endPricePanel - startPricePanel);

                int startPriceIndex = pricePanel.LastIndexOf(">") + 1;
                int endPriceIndex = endPricePanel - startPricePanel;

                string price = pricePanel.Substring(startPriceIndex, endPriceIndex - startPriceIndex);

                stockPrice = float.Parse(price);

            }
            catch(Exception e) {
                
                //Log this error
            }
            
            finally{
                if (reader != null) { reader.Close(); }
                if (req != null) { req = null; }
                if (res != null) { res.Close(); }
            }

            return stockPrice;

        }


        static void Main(string[] args)
        {
            float price = Program.GetStockPrice();
            int i = 0;

        }
    }
}


//Make it a web service and do this task every 1 hour with some randomness
//make the Request object mimic a firefox broswer
//put valued into database
//add some logging in mechanism
//extend it to take multiple stocks
//error handling for non standard inputs.
//make it work for yahoo finance if it does nto get stock values from google finance.
