# SuperNatural
 This is a bit of a test project for a board game that I have been working on
 It's a simple Console apllication game with Explicit Controls.
 It will likely be hard to find how to get around the town until a future update when I leave the Console
 players collect and research clues to find the Identity of the Monster
 Then engage the monster in combat to rid the town of its evil
 Monsters when revealed have special abilities to make the game more unique
 This section is for techincal explanation of code

<code>
 private int BoardSize = Enum.GetValues(typeof(Tile.Places)).Length; 
        public Board()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Tile tile = new Tile();
                tile.Name = (Tile.Places)i;//casts value i to a tiles name value
                //.................................................................................
                //.......This Creates the path relationships on the Board..........................
                //.................................................................................
                if (i > 0)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 1));
                if (i < BoardSize-1)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 1));
                if (i < 7)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 9));
                if (i >= 9 && i < 16)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 9));
                if (i == 11 || i == 0 || i==8)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 7));
                if (i == 7 || i == 18 || i == 15)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 7));
                if (i == 13)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 6));
                if (i == 19)
                {
                    tile.AdjacentPaths.Add((Tile.Places)(i - 6));
                    tile.AdjacentPaths.Add((Tile.Places)(i - 3));
                }
                if (i == 9)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 8));
                if (i == 17)
                    tile.AdjacentPaths.Add((Tile.Places)(i - 8));
                if (i == 16)
                    tile.AdjacentPaths.Add((Tile.Places)(i + 3));
                Tiles.Add(tile);
            }
            
        }
</code>