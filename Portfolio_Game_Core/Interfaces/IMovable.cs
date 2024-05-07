namespace Portfolio_Game_Core.Interfaces;

public interface IMovable
{
    public void GoRight(float deltaTime);
    public void GoLeft(float deltaTime);
    public void GoUp(float deltaTime);
    public void GoDown(float deltaTime);
}