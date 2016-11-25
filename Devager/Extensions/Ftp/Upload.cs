namespace Devager.Extensions.Ftp
{
    using System;
    using System.IO;
    using System.Net;

    public static class Upload
    {
        private static string UploadFile(this Stream stream, string uri, string user, string pass)
        {
            stream.Seek(0, SeekOrigin.Begin);

            try
            {
                var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                reqFtp.Credentials = new NetworkCredential(user, pass);
                reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
                reqFtp.KeepAlive = false;
                reqFtp.UseBinary = true;
                reqFtp.UsePassive = true;
                reqFtp.ContentLength = stream.Length;
                //reqFtp.EnableSsl = true; 
                const int buffLen = 2048;
                var buff = new byte[buffLen];
                try
                {
                    var ftpStream = reqFtp.GetRequestStream();
                    var contentLen = stream.Read(buff, 0, buffLen);
                    while (contentLen != 0)
                    {
                        ftpStream.Write(buff, 0, contentLen);
                        contentLen = stream.Read(buff, 0, buffLen);
                    }
                    ftpStream.Flush();
                    ftpStream.Close();
                }
                catch (Exception exc)
                {
                    return exc.Message;
                }
            }
            catch (Exception exc)
            {
                return exc.Message;
            }

            return "";
        }
    }
}
