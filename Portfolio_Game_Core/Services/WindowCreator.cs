using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Services;

public class WindowCreator
{
    private float _screenWidth;
    private float _screenHeight;
    public static int WindowMargin { get; set; } = 20;

    public WindowCreator(float screenWidth, float screenHeight)
    {
        _screenHeight = screenHeight;
        _screenWidth = screenWidth;
    }
    public Window GetTextWindow(string title, string content)
    {
        int width = TextWindow.TextWindowWidth;
        int height = TextWindow.TextWindowHeight;
        return new TextWindow(_screenWidth/2 - width / 2, _screenHeight - height - WindowMargin,title,content);
    } 
    public IEnumerable<TextWindow> GetTextWindows(IEnumerable<string> titles, IEnumerable<string> contents)
    {
        
        int width = TextWindow.TextWindowWidth;
        int height = TextWindow.TextWindowHeight;
        string previousTitle = "";
        for(int i = 0; i < contents.Count(); i++)
        {
            string title = titles.Count() > i ? titles.ElementAt(i): previousTitle;
            previousTitle = title;
            string content = contents.ElementAt(i) ?? "";
            yield return new TextWindow(_screenWidth/2 - width / 2, _screenHeight - height - 20,title,content);
        }
    } 
}