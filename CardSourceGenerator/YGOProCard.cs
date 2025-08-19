using System.Text.Json;

namespace CardSourceGenerator
{
    internal sealed class YGOProCard : INamedYGOCard
    {
        public string Name { get; set; } = string.Empty;
        public string Passcode { get; set; } = string.Empty;

        public YGOProCard(JsonElement element)
        {
            Name = element.TryGetProperty("name");
            Passcode = element.TryGetProperty("id", static jsonElement => jsonElement.GetString() ?? string.Empty);
        }
    }
}
