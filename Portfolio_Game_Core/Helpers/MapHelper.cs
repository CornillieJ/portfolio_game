using System.Numerics;
using Portfolio_Game_Core.Entities;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Portfolio_Game_Core.Helpers;

public static class MapHelper
{
   public static List<Wall> GetWallsSurroundingMap(Vector2 ScreenSize)
   {
      int width = Wall.WallWidth;
      int height = Wall.WallHeight;
      List<Wall> walls = new List<Wall>();
      for (int i = 1; i < ScreenSize.Y-height; i += height)
      {
         walls.Add(new Wall(0,i,Direction.Right));
         walls.Add(new Wall((int)(ScreenSize.X - width),i,Direction.Left));
      }
      for (int i = 1; i < ScreenSize.X-width; i += width)
      {
         walls.Add(new Wall(i,0,Direction.Down));
         walls.Add(new Wall(i,(int)(ScreenSize.Y - height),Direction.Up));
      }
      walls.Add(new Wall(0,0,Direction.Neutral)); //top left corner
      walls.Add(new Wall((int)(ScreenSize.X - width),0,Direction.Neutral)); //top right corner
      walls.Add(new Wall(0,(int)(ScreenSize.Y - height),Direction.Neutral)); // bottom left corner
      walls.Add(new Wall((int)(ScreenSize.X - width),(int)(ScreenSize.Y - height),Direction.Neutral)); //bottom right corner 
   return walls;
   } 
}