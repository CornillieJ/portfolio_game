using Portfolio_Game_Core.Entities.Base;

namespace Portfolio_Game_Core.Data;

public static class TextData
{
    public static readonly string[] WelcomeTexts =
    {
        "Welcome to my portfolio game, \n I am Jeffrey Cornillie, and in this game you will discover my personal projects Press enter or space to proceed",
        "To walk around use WASD keys. \n Use enter or space to interact",
        "Enjoy my portfolio"
    };

    public static readonly string[] ChestTexts =
    {
        "You took a program out of the chest, have a look in your inventory to run it.",

    };

    public static readonly Dictionary<string, string> ItemTexts = new()
    {
        {"Tankie", "A tank game made on project day in Howest, \n right click the icon to run"},
        {"OuterWilds","This is a Website I made in my first semester in Howest, about my favourite game \n right click the icon to open it in your browser"}
    };
    public static readonly Dictionary<string, string[]> ObjectsTexts = new()
    {
        {"Bath",new []{"Would you like to take a bath?",
                        "Isn't this relaxing?",
                        "Wasn't there something we needed to do?",
                        "Just a little longer ...",
                        "Oh, looks like something was hidden in the drain", 
                        "Is that a website? \n how did that get in there? \n You should take a look in your inventory" 
        }},
    };
    public static readonly Dictionary<string, string> ObjectEndTexts = new()
    {
        {"Bath", "You already took a bath"},
    };
}