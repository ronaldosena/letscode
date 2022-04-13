using System.Text.Json.Serialization;

namespace Application.Cards.Dtos
{
    public class CardDto
    {
        public string Id { get; set; }
        [JsonPropertyName("titulo")]
        public string Title { get; set; }
        [JsonPropertyName("conteudo")]
        public string Body { get; set; }
        [JsonPropertyName("lista")]
        public string Group { get; set; }
    }
}
