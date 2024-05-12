using Portfolio_Game_Core.Maps;

namespace Portfolio_Game_Core.Services;

public static class MapService
{
    public static Dictionary<string, Map> Maps { get; set; }

    public static void SeedMaps(float screenWidth, float screenHeight)
    {
        Maps = new Dictionary<string, Map>()
        {
            {"bathroom", new Bathroom(screenWidth,screenHeight)},
            {"garden", new Garden(screenWidth,screenHeight)},
            {"house-entry",new FirstMap(screenWidth,screenHeight)}
        };
    }

}