namespace BlazorApp.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static void AddPaginationInfo(this HttpContext httpContext, double total, int recordsPerPage)
        {
            var pagesQuantity = Math.Ceiling(total / recordsPerPage);
            httpContext.Response.Headers.Add("pagesQuantity", pagesQuantity.ToString());
        }
    }
}
