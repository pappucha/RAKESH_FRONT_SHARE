using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NSEBACKCOPY.BACKCOPY
{
    public partial class BHAVCOPY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class DailyData
        {
            public string SYMBOL { get; set; }
            public string SERIES { get; set; }
            public float OPEN { get; set; }
            public float HIGH { get; set; }
            public float LOW { get; set; }
            public float CLOSE { get; set; }
            public float LAST { get; set; }
            public float PREVCLOSE { get; set; }
            public float TOTALTRADEDQTY { get; set; }
            public float TOTALTRADEVALUE { get; set; }
            public DateTime TIMESTAP { get; set; }
            public float TOTALTRADES { get; set; }
            public string ISIN { get; set; }

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            //Upload and save the file  
            // string csvPath = "C:\\Users\\rakeshchaubey1989\\Downloads\\2016-04-01_2018-05-20\\20160401_NSE.csv";
            string partialName = "_NSE.";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"C:\Users\rakeshchaubey1989\Downloads\2016-04-01_2018-05-20");
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + partialName + "*.*");
            //FileUpload1.SaveAs(csvPath);
            foreach (FileInfo foundFile in filesInDir)
            {
                string csvPath = foundFile.FullName;

                //Create a DataTable.  
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[13] { 
        new DataColumn("SYMBOL", typeof(string)),  
        new DataColumn("SERIES", typeof(string)),  
        new DataColumn("OPEN", typeof(string)),  
        new DataColumn("HIGH", typeof(string)),  
        new DataColumn("LOW", typeof(string)),  
        new DataColumn("CLOSE", typeof(string)),  
        new DataColumn("LAST", typeof(string)),  
        new DataColumn("PREVCLOSE", typeof(string)),  
        new DataColumn("TOTALTRADEDQTY", typeof(string)),  
        new DataColumn("TOTALTRADEVALUE", typeof(string)),  
        new DataColumn("TIMESTAP", typeof(string)),  
        new DataColumn("TOTALTRADES", typeof(string)),  
        new DataColumn("ISIN", typeof(string))  
        });


                //Read the contents of CSV file.  
                string csvData = File.ReadAllText(csvPath);
                List<DailyData> getdailyBhav = new List<DailyData>();
                DailyData innnerlist = new DailyData();



                //Execute a loop over the rows.  
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {

                        dt.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.  
                        foreach (string cell in row.Split(','))
                        {

                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                dt.Rows[0].Delete();

                getdailyBhav = (from DataRow row in dt.Rows
                                select new DailyData
                                {

                                    SYMBOL = row["SYMBOL"].ToString(),
                                    SERIES = row["SERIES"].ToString(),
                                    OPEN = float.Parse(row["OPEN"].ToString()),
                                    HIGH = float.Parse(row["HIGH"].ToString()),
                                    LOW = float.Parse(row["LOW"].ToString()),
                                    CLOSE = float.Parse(row["CLOSE"].ToString()),
                                    LAST = float.Parse(row["LAST"].ToString()),
                                    PREVCLOSE = float.Parse(row["PREVCLOSE"].ToString()),
                                    TOTALTRADEDQTY = float.Parse(row["TOTALTRADEDQTY"].ToString()),
                                    TOTALTRADEVALUE = float.Parse(row["TOTALTRADEVALUE"].ToString()),
                                    TIMESTAP = Convert.ToDateTime(row["TIMESTAP"].ToString()),
                                    TOTALTRADES = float.Parse(row["TOTALTRADES"].ToString()),
                                    ISIN = row["ISIN"].ToString()
                                }).ToList();


                makeDbconnection(getdailyBhav);
            }
        }



        public void makeDbconnection(List<DailyData> DailyData)
        {

            var ConnectionString = ConfigurationManager.ConnectionStrings["Glams"].ConnectionString;
            SqlConnection con = new SqlConnection(ConnectionString);

            foreach (var item in DailyData)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertBhavCopy", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = new SqlParameter("@SYMBOL", item.SYMBOL);
                SqlParameter p2 = new SqlParameter("@SERIES", item.SERIES);
                SqlParameter p3 = new SqlParameter("@OPEN", item.OPEN);
                SqlParameter p4 = new SqlParameter("@HIGH", item.HIGH);
                SqlParameter p5 = new SqlParameter("@LOW", item.LOW);
                SqlParameter p6 = new SqlParameter("@CLOSE", item.CLOSE);
                SqlParameter p7 = new SqlParameter("@LAST", item.LAST);
                SqlParameter p8 = new SqlParameter("@PREVCLOSE", item.PREVCLOSE);
                SqlParameter p9 = new SqlParameter("@TOTALTRADEDQTY", item.TOTALTRADEDQTY);
                SqlParameter p10 = new SqlParameter("@TOTALTRADEVALUE", item.TOTALTRADEVALUE);
                SqlParameter p11 = new SqlParameter("@TIMESTAP", item.TIMESTAP);
                SqlParameter p12 = new SqlParameter("@TOTALTRADES", item.TOTALTRADES);
                SqlParameter p13 = new SqlParameter("@ISIN", item.ISIN);


                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);
                cmd.Parameters.Add(p12);
                cmd.Parameters.Add(p13);
                int a = cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lblerror.Text = "FileDownloaded";
        }



    }
}