using Portfolio_Game_Core.Entities;
using Portfolio_Game_Core.Interfaces;

namespace Portfolio_Game_Core.Services;

public class WindowCreator
{
    private int _screenWidth;
    private int _screenHeight;
    public WindowCreator(int screenWidth, int screenHeight)
    {
        _screenHeight = screenHeight;
        _screenWidth = screenWidth;
    }
    public Window GetTextWindow(string title, string content)
    {
        int width = TextWindow.TextWindowWidth;
        int height = TextWindow.TextWindowHeight;
        return new TextWindow(_screenWidth/2 - width / 2, _screenHeight - height - 20,title,content);
    } 
}