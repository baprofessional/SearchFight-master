namespace SF.Service.Api.Settings {
    public class SearchSettings {
        public BaseSearchSettings Bing { get; set; }
        public BaseSearchSettings Google { get; set; }
    }

    public class BaseSearchSettings {
        public string BaseUrl { get; set; }
    }
}