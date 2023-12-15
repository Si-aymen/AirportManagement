namespace AM.UI.Web.Models
{
    public static class WebExtensions
    {
        public static string AddRequestFile(this HttpRequest httpRequest, IWebHostEnvironment webHostEnvironment, params string[] paths)
        {
            if (httpRequest.Form.Files.Count > 0)
            {
                var allPaths = new string[paths.Length + 2];

                allPaths[0] = webHostEnvironment.ContentRootPath;
                paths.CopyTo(allPaths, 1);

                var file = httpRequest.Form.Files[0];
                allPaths[^1] = file.FileName;

                using (var stream = new FileStream(Path.Combine(allPaths), FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return file.FileName;
            }

            return null;
        }
    }
}
