//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Supernatural
//{
//    public class Board
//    {
//        private List<Tile> _GameBoard = new List<Tile>();;
//        public List<Tile> GameBoard { get { return _GameBoard; } set { value = _GameBoard} }
//        public Board()
//        {
//            var values = Enum.GetValues(typeof(Tile.Name));
//            List <Object>
//            foreach (var value in values)
//            for (int i = 0; i < 10; i++)
//            {
//                Tile tile = new Tile();
//                tile.TileName = (Tile.Name)i;

//                if (i < 0)
//                    tile.AdjacentPathA.Add((Tile.Name)i, (Tile.Name)i + 1);
//                if (i > 0)
//                    tile.AdjacentPathB.Add((Tile.Name)i, (Tile.Name)i - 1);
//                if (i == 0 || i == 5)
//                    tile.AdjacentPathC.Add((Name)i, (Name)i + 3);
//                if (i == 2 || i == 4)
//                    tile.AdjacentPathC.Add((Name)i, (Name)i + 4);
//            }
//        }
//    }
//}
