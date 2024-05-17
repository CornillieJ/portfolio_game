using Portfolio_Game_Core.Entities.Base;

namespace Portfolio_Game_Core.Data;

public static class TextData
{
    public static string[] WelcomeTexts =
    {
        "Welcome to my portfolio game, \n I am Jeffrey Cornillie, and in this game you will discover my personal projects Press enter or space to proceed",
        "To walk around use WASD keys. \n Use enter or space to interact",
        "Enjoy my portfolio"
    };

    public static string[] ChestTexts =
    {
        "You took a program out of the chest, have a look in your inventory to run it.",

    };

    public static Dictionary<string, string> ItemTexts = new()
    {
        {"Tankie", "A tank game made on project day in Howest, \n right click the icon to run"},
    };
}