namespace Devager.Extensions.HttpRequest
{
    using System.Web;
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Extension method to retrieve the site url.
        /// e.g.
        /// http://localhost:1234/ or 
        /// http://someremoteserver/virtualdir/
        /// </summary>
        /// <param name="request"/>The site url with application path</param>      
        public static string GetSiteUrl(this HttpRequest request)
        {
            return request.Url.Scheme + @"://" + request.Url.Authority +
                   (request.ApplicationPath.Length > 1 ? request.ApplicationPath + "/" : request.ApplicationPath);
        }
    }
}
