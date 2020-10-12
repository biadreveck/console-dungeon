using ConsoleDungeon.Models;
using ConsoleDungeon.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleDungeon.Helpers
{
    public class ItemJsonConverter : JsonConverter<Item>
    {
        public override Item Read(ref Utf8JsonReader reader,
                                      Type typeToConvert,
                                      JsonSerializerOptions options)
        {
            var typeStr = reader.GetString();

            if (string.IsNullOrEmpty(typeStr))
            {
                return null;
            }

            var parseIsOk = Enum.TryParse(typeStr, true, out ItemType itemType);

            if (!parseIsOk)
            {
                return null;
            }

            switch (itemType)
            {
                case ItemType.Key:
                    return new Key();
                case ItemType.Sword:
                    return new Sword();
                case ItemType.Potion:
                    return new Potion();
                case ItemType.GreatPotion:
                    return new GreatPotion();
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer,
                                   Item value,
                                   JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Type.ToString().ToLower());
        }
    }
}
