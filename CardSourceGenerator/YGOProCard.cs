using System;
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
            Passcode = element.TryGetProperty("id", static jsonElement =>
            {
                if(jsonElement.TryGetInt32(out _))
                {
                    return jsonElement.ToString().PadLeft(8, '0');
                }

                throw new InvalidOperationException("Cannot deserialize passcode.");
            });
        }
    }
}
