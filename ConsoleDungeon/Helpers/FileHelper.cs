using ConsoleDungeon.Models;
using System;
using System.IO;
using System.Text.Json;

namespace ConsoleDungeon.Helpers
{
    public class FileHelper
    {
        private static readonly string SavePath = "Saves";
        private static readonly string SaveFilePath = $"{SavePath}\\saved_game.json";

        private static readonly string MapsPath = "Assets\\Maps";
        private static readonly string MapsFilePath = $"{MapsPath}\\{{0}}.json";

        public static Map LoadMap(string mapName)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
                Converters = { new ItemJsonConverter() }
            };

            var mapJsonString = File.ReadAllText(string.Format(MapsFilePath, mapName));
            return JsonSerializer.Deserialize<Map>(mapJsonString, options);
        }

        public static bool SaveGame(GameSave save)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true,
                    Converters = { new ItemJsonConverter() }
                };

                var saveJson = JsonSerializer.Serialize(save, options);

                Directory.CreateDirectory(SavePath);
                var stream = File.CreateText(SaveFilePath);
                stream.Write(saveJson);
                stream.Close();
            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public static GameSave LoadGame()
        {
            if (!HasSavedGame()) return null;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true,
                    Converters = { new ItemJsonConverter() }
                };

                var mapJsonString = File.ReadAllText(SaveFilePath);
                return JsonSerializer.Deserialize<GameSave>(mapJsonString, options);
            }
            catch
            {
                return null;
            }
        }

        public static bool HasSavedGame()
        {
            return File.Exists(SaveFilePath);
        }
    }
}
