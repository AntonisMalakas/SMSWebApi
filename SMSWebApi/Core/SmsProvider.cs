using Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;

namespace Core
{
    public class SmsProvider : ISmsProvider
    {
        public object SendSms(dynamic payload)
        {
            string myURI = "https://api.bulksms.com/v1/messages"; //payload.gatewayUrl.ToString();

            string myUsername = payload.username.ToString(); //"anthonyhaddad1";
            string myPassword = payload.password.ToString();  //"password"; 

            dynamic xx = new ExpandoObject();
            xx.to = payload.receiverPhoneNumber.ToString();
            xx.body = payload.bodyMessage.ToString();

            var test = JsonConvert.SerializeObject(xx);


            // the details of the message we want to send
            string myData = test;// "{to: \'" + payload.receiverPhoneNumber.ToString() + "\', body:\'" + payload.bodyMessage.ToString() + "\'}";
            //string myData = "{to: \"1111111\", body:\"Hello Mr. Smith!\"}";
            //string myData = "{to: \"+961\", body:\"Hello Mr. Smith! Im from C#\"}";

            var request = WebRequest.Create(myURI);

            request.Credentials = new NetworkCredential(myUsername, myPassword);
            request.PreAuthenticate = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(myData);

            var stream = request.GetRequestStream();
            stream.Write(encodedData, 0, encodedData.Length);
            stream.Close();

            var returnMessage = "";
            try
            {
                // make the call to the API
                var response = request.GetResponse();

                // read the response and print it to the console
                var reader = new StreamReader(response.GetResponseStream());
                var responseReader = reader.ReadToEnd();
                Console.WriteLine(reader.ReadToEnd());
                returnMessage = "Sent " + reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                // show the general message
                Console.WriteLine("An error occurred:" + ex.Message);

                // print the detail that comes with the error 
                var reader = new StreamReader(ex.Response.GetResponseStream());
                Console.WriteLine("Error details:" + reader.ReadToEnd());
                returnMessage = "Error: " + ex.Message + " " + reader.ReadToEnd();

            }

            return returnMessage;
        }
    }
}
