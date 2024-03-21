**To test this mod, please clone the repository and run it.**
**This mod's development ended on 3/20, any new changes to Death Blow made after this date will not be incorporated into this repository.**

# Dagger Mod for Death Blow #

## Summary ##

Currently, Death Blow only offers melee weapon option. To spice up the combat experience, this mod adds in throwable daggers to improve gameplay variety.

## Gameplay Explanation ##

During combat, the player can press 'F' to throw a dagger and deal 10 damage to the enemy. 
Since it ruins the fun of combat if the player can just spam dagger throwing, this mod limits the numbers of daggers the player has to 10. However, dagger on the floor can be picked up. The players simply has to walk over the dagger and it will be picked up automatically.

Due to the limited nature of daggers, it is both unwise and also unfeasible to keep throwing daggers.

# Main Roles #

Before anything else, I would like to state that this project had been a challenge. The time pressure along with the final exams circumstances added a lot of stress to the development.

Having to understand team Death Blow's code base on my own proved to be a challenge. After forking their repository, I was immediately confused by there being 2 branches called main and new_main. I thought they had moved to working on new_main so I developed the mod there at first. Turns out I was wrong and main branch was still the branch for main development. Merging from new_main into main was another huge problem, as new_main and main had vastly different content.

To be honest, I think developing a mod for an unfinished game had been a bad idea. Without the completed game and not to mention documentation for it, it was hard to develope and test the mod. As of the state of writing this, I could only test my dagger on an enemy that is not moving. Also the enemy health bar had not been implemented, so it was hard to test dealing damage without visual feedback. Not having a complete enemy combat also means I was not able to balance the mod.

## Blacksmith 

### Dagger sprite:
For the 3D model of the dagger, I used a free asset from the Unity Asset Store. It looks really cool and fits well with the game's art style.

Credit to Dwarven Curse the creator of the model (https://assetstore.unity.com/packages/3d/props/weapons/demonic-dagger-100125)

### Initiate throwing:
I have added an extra key detection for 'F' in the Update function of PlayerMovement script. On key pressed, if the player still has a non-0 amount of daggers left, the Throw function will be called to spawn the dagger and the number of daggers will be deducted by 1.
https://github.com/GC03/Death_Blow_Mod/blob/512178bba8365672d722285967aa6c8e09cac5ca/Project%20179/Assets/Scripts/PlayerMovement.cs#L119-L126

### Dagger spawning:
Once Throw function is called, the dagger prefab will be instantiated. However, it is spawned in the wrong orientation initially. Therefore I had to rotate it so the dagger blade actually faces the direction of trajectory. This took me a long time, as I was trying to deal with the quaternion system to make the dagger face the correct direction, and I in fact had no idea what quaternion is. After frustrating amount of hours, I suddenly realised I can just rotate the transform rather than manually calculating the correct quaternion to rotate the dagger.
https://github.com/GC03/Death_Blow_Mod/blob/512178bba8365672d722285967aa6c8e09cac5ca/Project%20179/Assets/Scripts/PlayerMovement.cs#L177-L180

### Dagger movement:
To make the dagger actually moves towards the center of where the player is looking at, I had to use raycasting and vector arithmatics to calculate the force vector for the dagger's trajectory. Once direction is determined, apply force to the dagger in the determine direction.
Without the raycast, I originally just applied forward force to the dagger, but this resulted in the dagger moving to a point right of the center.
https://github.com/GC03/Death_Blow_Mod/blob/512178bba8365672d722285967aa6c8e09cac5ca/Project%20179/Assets/Scripts/PlayerMovement.cs#L184-L194

### Dagger collision:
This is another frustrating part of development, due to my lack of knowledge of the CharacterController component that Death Blow's developers used.
I started with adding a DaggerController script(https://github.com/GC03/Death_Blow_Mod/blob/50052a526d35edf48f8ac6fa7a641d2d2f7fdd2e/Project%20179/Assets/Scripts/DaggerController.cs) and attached it to the dagger prefab.
I first coded in the part of the dagger hitting the enemy and dealing damage. This part was not too much of a headache, just the usual collision detection between colliders. However, when I tried to code the logic for the player picking up the dagger on the floor, I tried using the same logic and it did not work. I was confused by this behaviour, and I made sure that the player had a capsule collider and the dagger had a box collider. Yet despite hours of testing, I was not able to make the collision detection work. The worst thing was that, in game the player was able to step on top of the dagger, so that implied that the colliders did work, just not the detection.
After a long time of research, I finally found out the culprit was the CharacterController component, something I never used before. Turns out this component has its own collider and collision detection system, and it would not work with the dagger's collider detection. In the end, I relocated the detection of player touching dagger to the PlayerMovement script https://github.com/GC03/Death_Blow_Mod/blob/512178bba8365672d722285967aa6c8e09cac5ca/Project%20179/Assets/Scripts/PlayerMovement.cs#L301-L310 and the detection finally worked. However, the collision detection was triggered multiple times resulting in more than 1 dagger replenished. I had to use a boolean variable firstTouched to ignore the multiple calls to the function.

### Dagger Counter UI
To let the player know how many dagger they on hand, I added a counter to the HUD in the bottom right corner.
I added checking the tracking the number of daggers the player has left to the HUDController script.
https://github.com/GC03/Death_Blow_Mod/blob/512178bba8365672d722285967aa6c8e09cac5ca/Project%20179/Assets/Scripts/HUDController.cs#L52

# Subrole

## Audio

### Dagger hit sound effect:
I added a Audio Source component to the dagger prefab. In dagger controller, when the dagger hits an enemy, it will play a sound effect to signify hit.

Credits to leohpaz for the impact sound effect (https://assetstore.unity.com/packages/audio/sound-fx/rpg-essentials-sound-effects-free-227708)




