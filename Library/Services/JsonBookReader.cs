using Library.Abstractions;
using Library.Models;
using Newtonsoft.Json;

namespace Library.Services
{
    public class JsonBookReader : IReader<Book>
    {
        private readonly string _fileName;

        public JsonBookReader(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<Book> ReadData()
        {
            using (StreamReader r = new StreamReader(_fileName))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Book>>(json);
            }
        }
    }
}
