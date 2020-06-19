using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Controllers.Utilities
{
    public class UrlBuilder
    {
        private string _url;
        private readonly List<string> _parameters = new List<string>();

        public UrlBuilder(string url)
        {
            _url = url;
        }

        public void SetUrl(string url)
        {
            _url = url;
        }

        public void AddQueryParameter(string name, string value)
        {
            _parameters.Add($"{name}={HttpUtility.UrlEncode(value, Encoding.ASCII)}");
        }

        public string Build()
        {
            return $"{_url}?{string.Join("&", _parameters)}";
        }
    }
}