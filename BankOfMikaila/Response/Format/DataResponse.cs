using System.Text.Json.Serialization;

namespace BankOfMikaila.Response.Format
{

    public class DataResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }
    }
}
