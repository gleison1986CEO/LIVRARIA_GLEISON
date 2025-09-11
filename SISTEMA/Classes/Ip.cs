using System.Collections.Specialized;
using System.Net;
using System.Xml;

namespace sistema.Classes
{
    public class IpConn
    {

        // AJUSTES
        private const string WINDOWS = "C:\\Users\\gleis\\Documents\\DEV\\C#\\GARIBALDI\\SISTEMA\\wwwroot\\images\\"; 
        private const string LINUX   = "/home/gleison/Documentos/C#/LOJAGLEISON/SISTEMA/wwwroot/images/";
        
       // AJUSTES
        public String QRCODE(){
            var connetionString = LINUX  +  "qrcode/";
            return connetionString;
        }

        public String PRODUTO(){
            var connetionString = LINUX  +  "produto/";
            return connetionString;
        }

        public String CANAL(){
            var connetionString = LINUX  +  "canal/";
            return connetionString;
        }
        public String FILME(){
            var connetionString = LINUX  +  "filme/";
            return connetionString;
        }
        public String MENSALIDADE(){
            var connetionString = LINUX  +  "mensalidade/";
            return connetionString;
        }        
        public String SERIE()
        {
            var connetionString = LINUX + "serie/";
            return connetionString;
        }
        public String BANNER(){
            var connetionString = LINUX  +  "banner/";
            return connetionString;
        }
        
        public String FOTURL(){
            var connetionString = LINUX  +  "users/";
            return connetionString;
        }
        public String CSV(){
            var connetionString = LINUX  +  "csv/"; 
            return connetionString;
        }        
        public String AERONAVE(){
            var connetionString = LINUX  +  "aeronave/"; 
            return connetionString;
        }             
        public String Ip()
        {
            ///REAL SERVER
            var connetionString = "http://127.0.0.1"; 
            return connetionString;
        }
       public string IPRequestHelper(string url) {

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            responseStream.Close();
            responseStream.Dispose();

        return responseRead;
        }
        public  string GetCountryByIP(string ipAddress)
            {
                string strReturnVal;
                string Region;
                string Country;
                string CountryCode;
                string RegionName;
                string City;
                string Zip;
                string Lat;
                string Long;
                string Ips;
                string Org;
                string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);
            
                //return ipResponse;
                XmlDocument ipInfoXML = new XmlDocument();
                ipInfoXML.LoadXml(ipResponse);
                XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");

                NameValueCollection dataXML = new NameValueCollection();

                dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);

                strReturnVal = responseXML.Item(0).ChildNodes[1].InnerText.ToString(); 
                Country      = responseXML.Item(0).ChildNodes[1].InnerText.ToString(); 
                CountryCode  = responseXML.Item(0).ChildNodes[2].InnerText.ToString(); 
                Region       = responseXML.Item(0).ChildNodes[3].InnerText.ToString(); 
                RegionName   = responseXML.Item(0).ChildNodes[4].InnerText.ToString(); 
                City         = responseXML.Item(0).ChildNodes[5].InnerText.ToString(); 
                Lat          = responseXML.Item(0).ChildNodes[7].InnerText.ToString(); 
                Long         = responseXML.Item(0).ChildNodes[8].InnerText.ToString();
                Ips          = responseXML.Item(0).ChildNodes[9].InnerText.ToString();
                Org          = responseXML.Item(0).ChildNodes[10].InnerText.ToString();
               
        return RegionName + "," + Region  + "," + City + "," + Country + "," + CountryCode + "," + Ips + "," + Org  + "," + Lat + "," + Long;
        }


        public  string GetMapsByIP(string ipAddress)
            {
                string strReturnVal;
                string Region;
                string Country;
                string CountryCode;
                string RegionName;
                string City;
                string Zip;
                string Lat;
                string Long;
                string Ips;
                string Org;
                string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);

                //return ipResponse;
                XmlDocument ipInfoXML = new XmlDocument();
                ipInfoXML.LoadXml(ipResponse);
                XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");

                NameValueCollection dataXML = new NameValueCollection();

                dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);

                Lat          = responseXML.Item(0).ChildNodes[7].InnerText.ToString(); 
                Long         = responseXML.Item(0).ChildNodes[8].InnerText.ToString();
               
        return Lat + "," + Long;
        }
        
    }
}

