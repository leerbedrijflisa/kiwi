using System.ComponentModel.DataAnnotations.Schema;

namespace Lisa.Kiwi.WebApi
{
    [Table("Files")]
    public class FileData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ContentLength { get; set; }
        public string ContentType { get; set; }

        public string Key { get; set; }
        public string Container { get; set; }
    }
}
