namespace Devager.Extensions
{
    using System.Web;
    using System;

    public static class HttpReq
    {
        /// <summary>
        /// Extension method to retrieve the site url.
        /// e.g.
        /// http://localhost:1234/ or 
        /// http://someremoteserver/virtualdir/
        /// </summary>
        /// <param name="request"/>The site url with application path</param>      
        /// 

        public static string GetSiteUrla(this HttpRequest request)
        {
            return  request.Url.Scheme + @"://" + request.Url.Authority + (request.ApplicationPath.Length > 1 ? request.ApplicationPath + "/" : request.ApplicationPath);
        }
    }
}
