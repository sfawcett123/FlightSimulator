using SimListener;
using System.Text.Json;
using BoardManager;

namespace Listener.Helpers
{
    internal static class Helper
    {
        public static string Serialize(this List<Track> input)
        {
            return JsonSerializer.Serialize(input);
        }
    }
}
