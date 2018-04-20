using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

using System.Data.SqlClient;
using System.Configuration;

namespace DNFShop
{
    public class Item
    {
        public int auctionNo { get; set; }
        public string regDate { get; set; }
        public string expireDate { get; set; }
        public string itemID { get; set; }
        public string itemName { get; set; }
        public int itemAvailableLevel { get; set; }
        public string itemRarity { get; set; }
        public string itemType { get; set; }
        public string itemTypeDetail { get; set; }
        public int refine { get; set; }
        public int reinforce { get; set; }
        public string amplificationName { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int unitPrice { get; set; }
        public int averagePrice { get; set; }
    }
    public class RowMenu
    {
        public Item[] rows { get; set; }
    }
    public partial class WebForm1 : System.Web.UI.Page
    {

        string apiKey = "j7cMqYgq0kol5hMVtCQ8T8bmSYCVgTWf";
        string itemName = "";
        string url = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            setting(itemMagName, MagID, unitPriceMag, secondPriceMag, averagePriceMag);

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            unitPriceLabel.Text = "0";
            secondPriceLabel.Text = "0";

            itemName = TextBox1.Text;
            url = "https://api.neople.co.kr/df/auction?itemName=" + itemName + "&sort=unitPrice:asc&limit=20&wordType=match&apikey=" + apiKey;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            RowMenu rowMenu = JsonConvert.DeserializeObject<RowMenu>(reader.ReadToEnd());
            
         
            if (rowMenu.rows.Length > 0)
            {
                itemNameLabel.Text = itemName;
                for(int i=0; i<rowMenu.rows.Length; i++)
                {
                    if (rowMenu.rows[i].unitPrice == 0)
                        continue;
                    else { 
                        unitPriceLabel.Text = rowMenu.rows[i].unitPrice + "";
                        if ((i + 1) < rowMenu.rows.Length)                       
                        {
                            secondPriceLabel.Text = rowMenu.rows[i + 1].unitPrice + "";
                        }
                        break;
                    }
                }                
                averagePriceLabel.Text = rowMenu.rows[0].averagePrice+"";
            }
            

        }

        

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["MyConnectionString"].ConnectionString);
            string sql = "UPDATE item SET day+=1 WHERE itemID=@itemID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@itemID", MagID.Value);

            con.Open();
            cmd.ExecuteNonQuery();
            

            sql = "INSERT INTO item(itemID, price, day) VALUES(@itemID, @price, 0)";
            cmd = new SqlCommand(sql, con);            
            cmd.Parameters.AddWithValue("@itemID", MagID.Value);
            cmd.Parameters.AddWithValue("@price", unitPriceMag.Text);
           
            cmd.ExecuteNonQuery();
            

            sql = "DELETE item WHERE itemID=@itemID AND day>30";
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@itemID", MagID.Value);
            
            cmd.ExecuteNonQuery();
            con.Close();
        }

        void setting(Label name, HiddenField itemid, Label unitprice, Label second, Label aver)
        {
            itemName = name.Text;
            url = "https://api.neople.co.kr/df/auction?itemName=" + itemName + "&sort=unitPrice:asc&limit=20&wordType=match&apikey=" + apiKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            RowMenu rowMenu = JsonConvert.DeserializeObject<RowMenu>(reader.ReadToEnd());

           
            itemid.Value = rowMenu.rows[0].itemID;

            for (int i = 0; i < rowMenu.rows.Length; i++)
            {
                if (rowMenu.rows[i].unitPrice == 0)
                    continue;
                else
                {
                    unitprice.Text = rowMenu.rows[i].unitPrice + "";
                    if ((i + 1) < rowMenu.rows.Length)
                    {
                        second.Text = rowMenu.rows[i + 1].unitPrice + "";
                    }
                    break;
                }
            }
            aver.Text = rowMenu.rows[0].averagePrice + "";

           
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }
    }
}