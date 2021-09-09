namespace Web.Options
{
    public class SwaggerOptions
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description => Title + " " + Version;
        public string JsonRoute { get; set; }
        public string UiEndpoint { get; set; }
    }
}