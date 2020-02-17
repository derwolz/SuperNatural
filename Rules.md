# SuperNatural
 This is a bit of a test project for a board game that I have been working on
 It's a simple Console apllication game with Explicit Controls.
 It will likely be hard to find how to get around the town until a future update when I leave the Console
 players collect and research clues to find the Identity of the Monster
 Then engage the monster in combat to rid the town of its evil
 Monsters when revealed have special abilities to make the game more unique
# How To Play
This game is played in two phases: Discovery and Damage mitigation
# Phase One
The Identity of the monster is not revealed to the player, The monster, named mysterious figure, will travel around the board increasing
the panic of the areas it moves to. written in blue before the players turn will be the current location of the monster, Red after the players
have moved will be the spaces it has moved to. When a player enters a square where the monster has been it will display the current panic
of the area and the search option will become available. If a player is adjacent to the monster or in the same area the Attack option enables.
If the player enters the Library, city hall, or motel Investigate becomes an option. If the player enters the Trailer Home, School or Junkyard
Research becomes enabled. Every action a player takes requires the discard of one card out of that players hand. The game will prompt every time an
action is taken.

# Search
As mentioned earlier, panic increases each time the monster enters an area, if the panic level reaches 4 it can no longer decrease, players
can combat panic by searching for clues. Search will give the player a clue, and decrease the panic for the area in which it has been recieved.
# Investigate
Once a player has a clue, they may then Investigate that clue to see if it will lead them to the monster. Travel to the Library, city hall, or motel
and use Investigate to look up the identity of the clue. When the final clue is found, the identity of the monster is revealed and the game moves to phase
2.
# Research 
When a player enters the Church, Shops, Bar, or Junkyard they may begin to research a weapon. They will be prompted to either research
or build a weapon. when researching 3 cards will be drawn from the weapon deck, and the player will discard one. When building a weapon
The player must have all the pieces of the weapon before the weapon may be built and will discard any part cards used by the weapon when
it is built.
# Attack
When adjacent to the monster or in the same area, the player may attack the monster, when the monster has taken enough damage, it will go
underground for an uncertain amount of time, allowing the players freedom of movement during that time.
# Use Card
There are a variety of cards to assist in the ease, and debugging of the game currently, and almost all of them are worth using more than the
standard options given to the player.
	-rifle increases player range for a single attack
	-shotgun when occupying the same space as the monster deal 3 damage instead of 1.
	-double* does double of the specified action

# PHASE 2
With the Identity of the monster revealed, the players game becomes less about Searching for clues, though that may be important, and more
about damage mitigation to the city. The players cannot be killed, but if more than 50% of the city reaches panic level 4, it is destroyed and
the players lose the game. The players must outwit the basic AI of the monsters and kill them, but the monsters gain a few abilities to give them a fighting
edge
# Vampire
HP: Mid
speed Slow
Ability 1: Summon bat swarms
	Vampire will summon up to 4 hordes of bats around himself. these bats will increase the panic of squares they move to if not dealt with
HP: 1 Speed: 1.
Ability 2: Vampiric Drain
	Vampire will damage all surrounding areas and increase his health by 2 per area drained (if it is at panic 4 no health is gained)
The Vampire has no maximum limit to his health making him a very tough, if not currently impossible opponent.

# Werewolf
HP: High
Speed: High
Ability 1: Extreme Panic
	Werewolf will cause the areas around him to increase in panic by one.
Ability 2: Extreme Speed
	Werewolf's speed increases by one
The Werewolf has more hitpoints, but is ultimately much easier to deal with currently
# Ghoul
HP Mid
Speed: mid
No Abilities

# DoppelGanger
HP Mid
Speed: mid
No Abilities

# Ghost
HP Mid
Speed: Low
No Abilities

# Wendigo
HP Mid
Speed: Very fast
No Abilities

# Banshees
HP Mid
Speed: Low
No Abilities


	