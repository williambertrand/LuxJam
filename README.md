# LuxJam
 Game made for the 2021 LuxJam

## Theme: Chain Reaction
Use bombs to destroy hostiel alien spaceships. Chaining bomb explosions increses the bomb's radius and damage.
 
 
## Timeline
I'll be keeping a very rough timeline of progress here in the readme durring the jam!
 
### Friday Afternoon
Got basic ship movement, shooting, and bomb dropping functionality done!
<img src="https://user-images.githubusercontent.com/11068205/132922429-e62c1ad7-13f2-469a-b8fd-2b0e58a6e184.gif" width=500 />

### Friday Evening
By the evening I had basic movenet and shooting set up for enemies, but there were a lot of bugs going around. 

<img src="https://user-images.githubusercontent.com/11068205/132955126-1932c103-2bf9-43b5-bd22-455fb5931c9c.gif" width=500 />

### Saturday AM
In the morning on Saturday, I tweaked and improved the enemy movement behavior, to have them try and circle near the player. Also, I added a limitation to the enemies s they can only shoot within a certain angle (`maxFireAngle`) so that they won't shoot at the player if they manage to get behind the enemy. 

I also decied to make everything explodable, so now blowing up an enemy ship can cause enemies within it's explosion radius to also explode (where the explosion radius increases as chains of explosions are formed). There's still a lot of balancing and tweaking needed for how these explosion chains grow in size and damage, but it's getting there... Lastly before taking a break for lunch, I added a `ScoreManager` to keep track of how many enemies a player has killed, and the longest chain that has been set off.

<img src="https://user-images.githubusercontent.com/11068205/132955201-963f570e-21df-4355-a54f-db240a18eb41.gif" width=500 />


### Saturday PM
A lot of Saturday was spent adding Playfab. Logging in, getting player data and stats. Retrieving the ships catalogue for showing players ships to buy... Ended up taking a bit more time than I expected, but I got most of it working Saturday evening, and finished it up Sunday morning.

### Sunday

In the morning, I updated some enemy ship sprites and added an instruction panel to the pre-game menu scene. I also added Pickups (repair and bomb ammo) that are dropped when destroying enemies with direct shots. A lot of the rest of the day was spent tweaking the balance of different enemy ships. 

### Sunday Evening
Sunday Evening I rushed to add the two new ships that a player can buy and use. I wish I had thought of this needed functionality from the start, because my ship prefabs actually required quite a bit of work to replicate and change parameters for. There's also some very messy, quick code used to display the ship items in the store. Hoepfully I'll come back and clean that up sometime in the future...

### Monday AM
Monday morning I changed how the probabilities of each enemy ship spawning, and added two more enemy ship types, the long range and slow shooting ship, and a smaller faster, but more fragie ship. Here's an example sprite sheet for the long range shooting enemy ship:

![enemyShip2](https://user-images.githubusercontent.com/11068205/133122564-ffdc50e6-8960-48bb-9916-8dc12c64d032.png)

In the last hour of the jam I just did some more play testing, tweaked some balances, and fixed any small bugs I could find. Submitted!


