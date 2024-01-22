using System.Text.Json.Serialization;

namespace Customer.Models
{
    public class DataResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
    }
}
