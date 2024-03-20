# Dagger Mod for Death Blow #

## Summary ##

Currently, Death Blow only offers melee weapon option. To spice up the combat experience, this mod adds in throwable daggers to improve gameply variety.

## Gameplay Explanation ##

During combat, the player can press 'F' to throw a dagger and deal 25 damage to the enemy. 
Since it ruins the fun of combat if the player can just spam dagger throwing, this mod limits the numbers of daggers the player has to 10. However, if the player misses and the dagger is on the floor, player walk to the dagger and it will be picked up automatically.

Due to the limited nature of daggers, it is both unwise and also unfeasible to keep throwing daggers.

# Main Roles #

## Blacksmith 

### Dagger sprite:
For the 3D model of the dagger, I used a free asset from the Unity Asset Store. It looks really cool and fits well with the game's art style.

Credit to Dwarven Curse the creator of the model (https://assetstore.unity.com/packages/3d/props/weapons/demonic-dagger-100125)

### Initiate throwing:
I have added an extra key detection for 'F' in the Update function of PlayerMovement script. On key pressed, if the player still has a non-0 amount of daggers left, the Throw function will be called to spawn the dagger and the number of daggers will be deducted by 1.
https://github.com/GC03/Death_Blow_Mod/blob/50052a526d35edf48f8ac6fa7a641d2d2f7fdd2e/Project%20179/Assets/Scripts/PlayerMovement.cs#L78

### Dagger spawning:
Once Throw function is called, the dagger prefab will be instantiated. However, it is spawned in the wrong orientation initially. Therefore I had to rotate it so the dagger blade actually faces the direction of trajectory. This took me a long time, as I was trying to deal with the quaternion system to make the dagger face the correct direction, and I in fact had no idea what quaternion is. After frustrating amount of hours, I suddenly realised I can just rotate the transform rather than manually calculating the correct quaternion to rotate the dagger.
https://github.com/GC03/Death_Blow_Mod/blob/50052a526d35edf48f8ac6fa7a641d2d2f7fdd2e/Project%20179/Assets/Scripts/PlayerMovement.cs#L134

### Dagger movement:
To make the dagger actually moves towards the center of where the player is looking at, I had to use raycasting and vector arithmatics to calculate the force vector for the dagger's trajectory. Once direction is determined, apply force to the dagger in the determine direction.
Without the raycast, I originally just applied forward force to the dagger, but this resulted in the dagger moving to a point right of the center.
https://github.com/GC03/Death_Blow_Mod/blob/50052a526d35edf48f8ac6fa7a641d2d2f7fdd2e/Project%20179/Assets/Scripts/PlayerMovement.cs#L141-L144

### Dagger collision:
This is another frustrating part of development, due to my lack of knowledge of the CharacterController component that Death Blow's developers used.
I started with adding a DaggerController script(https://github.com/GC03/Death_Blow_Mod/blob/50052a526d35edf48f8ac6fa7a641d2d2f7fdd2e/Project%20179/Assets/Scripts/DaggerController.cs) and attached it to the dagger prefab.
I first coded in the part of the dagger hitting the enemy and dealing damage. This part was not too much of a headache, just the usual collision detection between colliders. However, when I tried to code the logic for the player picking up the dagger on the floor, I tried using the same logic and it did not work. I was confused by this behaviour, and I made sure that the player had a capsule collider and the dagger had a box collider. Yet despite hours of testing, I was not able to make the collision detection work. The worst thing was that, in game the player was able to step on top of the dagger, so that implied that the colliders did work, just not the detection.
After a long time of research, I finally found out the culprit was the CharacterController component, something I never used before. Turns out this component has its own collider and collision detection system, and it would not work with the dagger's collider detection. In the end, I relocated the detection of player touching dagger to the PlayerMovement script https://github.com/GC03/Death_Blow_Mod/blob/50052a526d35edf48f8ac6fa7a641d2d2f7fdd2e/Project%20179/Assets/Scripts/PlayerMovement.cs#L255-L264 and the detection finally worked. However, the collision detection was triggered multiple times resulting in more than 1 dagger replenished. I had to use a boolean variable firstTouched to ignore the multiple calls to the function.




