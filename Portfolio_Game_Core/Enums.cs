namespace Portfolio_Game_Core;

public enum PlayerState
{
   Neutral,Up,Down,Left,Right 
}

public enum Direction
{
   Up,Down,Left,Right,UpLeft,UpRight,DownLeft,DownRight,Neutral
}

public enum ResultAction
{
   Nothing,ShowText,AddToInventory,MovePlayer,ResetPlayer,Delay,SwitchObjectState,AddObject,RemoveObject,NoMoreInteraction
}