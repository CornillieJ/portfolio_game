namespace Portfolio_Game_Core.Interfaces;

public interface IMovable
{
    public void GoRight(float deltaTime, bool canMove);
    public void GoLeft(float deltaTime, bool canMove);
    public void GoUp(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral);
    public void GoDown(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral);
}