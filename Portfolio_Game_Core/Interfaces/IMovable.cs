namespace Portfolio_Game_Core.Interfaces;

public interface IMovable
{
    public float Speed { get; set; }
    public void GoRight(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral);
    public void GoLeft(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral);
    public void GoUp(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral);
    public void GoDown(float deltaTime, bool canMove, Direction secondDirection = Direction.Neutral);
}