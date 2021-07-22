using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Text;

public class PushCustomerMoveCrop
{
    private const String rootURL = "https://api.movecrop.com/v1/";

    private const String accessToken =
        "NzU0ODI4NmNmYzEwOGVkODA5Y2M2ODdhZWE5MGE3ZGY6NDA5NjhmOGZjNWMyZWM2ZGQxYjMwOTRiMGRhNzVhOTQ=";

    [SqlProcedure]
    public static void createOrder(String code, String phone, String address, String name, String invoice,
        String station, String router, out SqlString text)
    {
       
        // ssl2
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType) (0xc0 | 0x300 | 0xc00);
        // initialization  request
        var httpWebRequest = (HttpWebRequest) WebRequest.Create(rootURL + "shippingorders");
        httpWebRequest.ContentType = "application/json";
        // add authorization to header
        httpWebRequest.Headers["Authorization"] = "Basic " + accessToken;
        // add method
        httpWebRequest.Method = "POST";

        // use stream write
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = GetBodyOrder(code, phone, address, name, invoice, station,router);
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        {
            text = (httpResponse.StatusCode == HttpStatusCode.Created) ? "OK" : "ERROR";
        }
    }


    private static String GetBodyOrder(String code, String phone, String address, String name, String invoice,
        String station,String router)
    {
        // get today
        DateTime today = DateTime.Today;
        var now = today.ToString("yyyyMMdd");
        return "{\"invoice_id\":\"" + invoice.Trim() + "\"," +
               "\"order_price_final\":\"\"," +
               "\"cod\":\"\"," +
               "\"order_weight\":\"\"," +
               "\"order_note\":\"" + code.Trim() + " - " + name.Trim() + "\"," +
               "\"price_incurred\":\"\"," +
               "\"order_length\":\"\"," +
               "\"order_width\":\"\"," +
               "\"order_height\":\"\"," +
               "\"from_type\":0," +
               "\"merchant_id\":0," +
               "\"from_warehouse_id\":0," +
               "\"pickup_status\":0," +
               "\"return_status\":0," +
               "\"office_id\":3293," +
               "\"random_code\":\"\"," +
               "\"type\":0," +
               "\"office_id_to\":0," +
               "\"order_customer_id\":0," +
               "\"order_customer_fullname\":\"" + station.Trim() + "\"," +
               "\"order_customer_phone\":\"" + phone.Trim() + "\"," +
               "\"order_customer_email\":\"\"," +
               "\"order_shipping_address\":\"" + address.Trim() + "\"," +
               "\"order_shipping_address_detail\":\"\"," +
               "\"order_shipping_lat\":\"21.01496149925541\"," +
               "\"order_shipping_long\":\"105.85293234021478\"," +
               "\"order_shipping_region\":5," +
               "\"order_shipping_subregion\":1," +
               "\"order_shipping_thirdregion\":\"1\"," +
               "\"ymd\":\"" + now + "\"," +
               "\"order_shipping_hourstart\":-1," +
               "\"order_shipping_hourend\":-1," +
               "\"km\":0," +
               "\"shipping_type\":0," +
               "\"product_type\":1," +
               "\"price_final\":\"\"," +
               "\"route_id\":\""+router.Trim()+"\"," +
               "\"status\":1," +
               "\"file_id_list\":[]," +
               "\"files\":[]," +
               "\"company_id\":\"12381\"," +
               "\"creator_id\":\"21300\"}";
    }

    [SqlProcedure]
    public static void CreateRouter(String email, out SqlString text)
    {
        Dictionary<string, long> MyDic3 = new Dictionary<string, long>();
        MyDic3.Add("shipper22@gmail.com", 21519);
        MyDic3.Add("htshipper21@gmail.com", 21518);
        MyDic3.Add("htshipper20@gmail.com", 21496);
        MyDic3.Add("htshipper19@gmail.com", 21495);
        MyDic3.Add("htshipper18@gmail.com", 21494);
        MyDic3.Add("htshipper17@gmail.com", 21493);
        MyDic3.Add("htshipper16@gmail.com", 21492);
        MyDic3.Add("shipper13@gmail.com", 21489);
        MyDic3.Add("shipper12@gmail.com", 21347);
        MyDic3.Add("shipper001@gmail.com", 21313);
        MyDic3.Add("shipper01@gmail.com", 21313);
        MyDic3.Add("shipper10@gmail.com", 21312);
        MyDic3.Add("shipper09@gmail.com", 21311);
        MyDic3.Add("shipper07@gmail.com", 21309);
        MyDic3.Add("shipper06@gmail.com", 21308);
        MyDic3.Add("shipper05@gmail.com", 21307);
        MyDic3.Add("shipper04@gmail.com", 21306);
        MyDic3.Add("shipper03@gmail.com", 21305);
        MyDic3.Add("shipper02@gmail.com", 21304);
        
        if (!MyDic3.ContainsKey(email.Trim())) text="Email not exists";
        // ssl2
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType) (0xc0 | 0x300 | 0xc00);
        // initialization  request
        var httpWebRequest = (HttpWebRequest) WebRequest.Create(rootURL + "shippingroutes");
        httpWebRequest.ContentType = "application/json";
        // add authorization to header
        httpWebRequest.Headers["Authorization"] = "Basic " + accessToken;
        // add method
        httpWebRequest.Method = "POST";
        // use stream write
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = GetBodyRouter(MyDic3[email.Trim()]);
            streamWriter.Write(json);
        }
        
        var response = (HttpWebResponse) httpWebRequest.GetResponse();
        
        if (response.StatusCode == HttpStatusCode.Created)
        {
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string res = streamReader.ReadToEnd();
                int from = res.IndexOf("\"id\":")+5;
                int to = res.IndexOf(",\"",from);
                text = res.Substring(from,to-from);
            }
        }
        else
        {
            text = "ERROR";
        }
    }

    private static string GetBodyRouter(long shipper_id)
    {
        // get today
        DateTime today = DateTime.Today;
        var nowString = today.ToString("yyyyMMdd");
        long ticks = DateTime.Now.Ticks;
        return "{" +
               "\"company_id\":\"12381\"," +
               "\"creator_id\":\"21300\"," +
               "\"shipper_id\":\"" + shipper_id.ToString() + "\"," +
               "\"from_warehouse_id\":\"3293\"," +
               "\"to_warehouse_id\":0," +
               "\"status\":7," +
               "\"from_type\":\"0\"," +
               "\"ymd\":\"" + nowString + "\"," +
               "\"code\":\"" + ticks.ToString() + "\"" +
               "}";
    }
}