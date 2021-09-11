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

