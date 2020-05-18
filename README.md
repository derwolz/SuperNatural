# SuperNatural
 This is a bit of a test project for a board game that I have been working on
 It's a simple Console apllication game with Explicit Controls.
 It will likely be hard to find how to get around the town until a future update when I leave the Console
 players collect and research clues to find the Identity of the Monster
 Then engage the monster in combat to rid the town of its evil
 Monsters when revealed have special abilities to make the game more unique
 This section is for techincal explanation of code
 
 ## Application executable located in 
 Supernatural\bin\Debug\netcoreapp3.1\Supernatural.exe
 
 # Start Up
 ## Board
 ![Tile Assignment](https://user-images.githubusercontent.com/32439939/75485268-a0a6a200-5967-11ea-967e-aeb89db3fde1.png)

First Covered is the board, which is a collection of objects with a name, adjacent list, and panic number. Here I Create a collection of those objects based on the total number of tile name enumerations within the tile class, and add tile references to each of their own adjacent tiles, so they form two equally sized concentric circles with a square in the center.

## Monster
Next is the Creation of the monster, this is first called after the board is created, and the game will randomly select a monster's name enumeration and call the MonsterFactory method and add it to a list of monsters held by a game master.

![Vampire Assignment](https://user-images.githubusercontent.com/32439939/75485269-a13f3880-5967-11ea-86d3-6110ded7828c.png)

Here I use a switch statement to cover the different enumerations to select what to apply to the monster template. Here it sets various attributes of the monster and adds the Enum values of their abilities so the game knows what they can use.

![ClueTemplate](https://user-images.githubusercontent.com/32439939/75485258-9edcde80-5967-11ea-8cbe-bc308cd4c7f0.png)

It also assigns the Clue Enum values to the monster so they game's reveal condition can be set up.

![Lastt Monster Changes](https://user-images.githubusercontent.com/32439939/75485259-9edcde80-5967-11ea-847a-fa8769b88def.png)

Finally, the game adds any changes to the monsters that would be applied no matter what the monster is, Here it increases the speed if there are more than two players, and it assigns the maximum health value to the monsters current health.

## Clue Deck
The purpose of the Game is to find the identity of the monster, then fight it. In order to set this up, I add a clue deck that first takes from the Monsters Clue list I added to earlier, creating those "Card" objects, adding those to the deck, then adding a slew of dummy cards to the deck to ensure the game lasts a satisfying amount of time.

![Clues to cluedeck](https://user-images.githubusercontent.com/32439939/75485254-9e444800-5967-11ea-815a-9d4c66a2a450.png)

## Monster Logic
Now that the game is more or less set up I created a function for the Monster to move around the board, while avoiding getting too close to any players. First it creates a list of possible paths in the _\_tempList_  then it removes any paths where a player may be residing in with a loop. Then it assigns the position in the List based off of .NET's basic random number generator.
![Monster move](https://user-images.githubusercontent.com/32439939/75485261-9f757500-5967-11ea-8118-6f9bd09fabfd.png)

Once the monster is revealed, it has abilities it needs to use in order to make the game more challenging, as well as interesting. This is accomplished through creating a cooldown called CastSpeed and having it count up to 100, then subtracting 100. This is done so that abilities that are better take more time than abilities that are weaker, but keeps overflowing amounts so it may use its abilities twice in a row or more if the player is unlucky. Abilities are selected through a switch of enumerated values, and then executed, with the CastSpeed then modified.

![Select Ability](https://user-images.githubusercontent.com/32439939/75485265-a00e0b80-5967-11ea-8e32-63ec378f354e.png)
