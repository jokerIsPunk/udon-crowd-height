EASY SETUP

1. drag and drop the Udon Crowd Height prefab into your world
2. place and scale the Limit Zone box collider so that it covers your crowd area
    - do NOT scale the parent "Udon Crowd Height" object!

---------------------------------------------------------

DETAILS

- scale the min height and max height stick figures to adjust the scale limits
- scaling limits apply only to players inside the limit zone
    + "inside" means the player's head is inside the zone
    + zones can be rotated to any orientation
- if you want the limits to apply everywhere, just delete the limit zone and set the Hard Limit values in the inspector
- for different configurations of zones, see the Zones folder
- for extending the logic of when scaling limits apply, see the Extensions folder

Please do not apply scaling limits where they're not needed. Please leave space on the margins of crowds for people to scale up tall so they can see. Some players prefer to be tall and this prefab allows you to create space for that.