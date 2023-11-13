# Level Design for Beginners Notes

## Guiding the player

### Signposting

Basically, a way to show your player where to go without explicitly telling them.

For example:
- Lit area in an otherwise dark place
- Giant mountain top
- Walking path

You can accomplish this by using things like:

- Light
- Color
- Leading lines (i.e., the level draws a subtle line to a destination using contrasting colors, pathways, corridors, etc.)
- Landmarks (e.g., mountains, towers)
- Literal signs (if it works in the realm of your world. see skyrim, pokemon, etc.)
- Breadcrumbing (e.g., collectables going towards a different area)

## Environmental Storytelling

Make the level feel like it's part of the story. For example, if the story takes place during a war, show destroyed towns, blown up or burned down buildings, etc.

- Antagonist's Presence
    - > Showing the player that the bad guy has some kind of dominion over the area
    - This can be shown with things like propaganda posters, statues, etc.
- Events of the past
    - > Showing the player how things were in the world before they picked up the controller.
    - This can be shown with things like abandoned buildings, notes left behind, overgrown cities, etc.
- Danger
    - > Showing the player what may lie ahead if they're not careful
    - Can be done through lighting, signs, etc.

### Challenge 1

> How does your favorite use environmental storytelling? The antagonists presence? Events of the past? Text? Danger? State of the world? Who the player is?

Thinking of Pokemon Red/Blue, you get a sense of who the player is through explicit text at the beginning, but it's reinforced by how the opening area (palette town) is designed. It's shown to be a small town. I believe this tells the player that, while you have humble beginnings, you're destined to go and do great things. Later, you get various bits and pieces of events of the past showing you that the region is coming out of a war. There's also a bit of antagonist's presence in the sense that you keep running into team rocket and learn of their boss, who happens to be a gym leader. While they aren't the final foe, they do have a grip on the region due to how big their crime syndicate is.

## Affordances

> How the player knows what they can interact with

- They need to be obvious to the player.
- They need to be consistent.
- They need to serve the purpose that the player expects.

You can teach an affordance by introducing it in a controlled environment (e.g., tutorial level), further reinforce it down the line, then implement them into the level.

Thinking about 1-1 in Super Mario Bros, you're introduced to different types of blocks. Some ❓ and some plain brick. You notice that if you hit the brick for the first time, it just bounces. If you hit the ❓ brick, something comes out (this time a mushroom). If you grab it, you turn big. Now if you hit the brick, it breaks. So this introduces the idea of a power-ups and destructible environments.

## Open World Design

While you may travel anywhere, it may be that you're not supposed to be in a given area yet. You can demonstrate this via drastically harder enemies, blocking off access via an ability that the player does not yet have, etc.

GTA5 does this by locking away parts of the world until certain story beats have been completed. This also happens in Breath of the Wild.

You can use landmarks in such a way to make sure the player explores the entire map. Bethesda games are great at this because they spread major towns/cities out evenly. This encourages the player to explore as much as the surface area as possible of the world.

You can use barriers to guide the player. This can be a solid barrier (e.g., the sea in GTA5), or a soft barrier. A solid barrier cannot be ignored by the player, but a soft barrier can. A soft barrier might be a gate that you could go over, but you may not due to some seen or unforeseen consequence.

## Theme Park Design

And how it relates to level design.

If you look at something like Disneyland, it has a hub and spoke design. Basically, a giant hub in the middle with "spokes" that branch out. The idea is that you always meet back at the hub whenever you're done with one of the spokes. Mario 64 utilizes this via the castle (the hub) and the paintings (the spokes/levels).

### Alternate Pathways

Provide other routes for the player so that they don't have to always back track the same way. You should be able to traverse the map without having to go the "right way"

### Challenge 2

- Choose a theme park - it can be a local one, one you’ve
been to, or one you want to go to.
- See how it relates to level design.
- Does it have a hub & spoke system?
- Landmarks? Alternative pathways?

I grew up going to [Dollywood](https://www.dollywood.com/themepark/map/) in Pigeon Forge, TN, so I'll go with that. It looks like it doesn't use a hub and spoke design. It looks rather linear. Most of the attractions are in the middle of an area and you travel the perimeter to get to everything. The park has the shape of a triangle, so I image you come in at one vertex and make your way to the other two and eventually make your way back to the origin (the entrance).

## Multiplayer Level Design

- Symmetric Levels
  - Good for if teams have the same objective
  - Maps are mirrored copy of one another in order to be balanced
    - e.g., MOBAs like DOTA2
- Asymmetric Levels
  - Good for if teams have different objectives
    - e.g., Counter-Strike
- Negative Space
  - Defines the inaccessible parts of your map
  - Useful for listening for sounds
- Sightlines
  - Avoid long sightlines where players can see long-distances. For example, it's generally bad practice to be able to see from one end of the map to the other.
  - A good example of this is the Counter-Strike (CS) map de_inferno.

## Level Design Workflow

- Built by iterating (like most software, I'd say)
- Process
  - Create the sketch/shape -> Create the layout -> Work on blockout <-> Test it -> Add in finishing touches

### Challenge 3

- Go through the design process and create a level of your
own
- You can use whatever software you wish (Unity, Unreal, Godot, Hammer, etc.)

> TODO: Go back and do this :)
