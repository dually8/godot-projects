# KirbVania Project

## Getting Started

1. Download [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
   1. Make sure it's in your path, specifically the 64-bit version. The "Program Files" path should be before the "Program Files (x86)" path.
2. Download [Godot .NET](https://godotengine.org/download/archive/4.1.3-stable/)
3. Open the [`project.godot`](./project.godot) file.
4. Build
   1. Be sure that all of the exported variables, especially in the main scene, are set
5. Run it

## Purpose

To continue learning Godot 4 by mashing together Kirby and Castlevania NES games.

## Resources

 - [GitHub Projects](https://github.com/users/dually8/projects/3)

## Random Notes

### 2023-12-03

I'm still working through some jitter with the camera, but it seems that [turning off pixel snapping](https://www.reddit.com/r/godot/comments/10rjp3f/godot_4_potential_fix_for_jitteringgap_between/) fixed the bug with the 1px gap between the characterbody2ds and the floor.

### 2023-12-06

I think I've fixed the camera shimmer/jitter bug with this latest commit. Looks like the camera zoom doesn't like to be a float (3.5), so I've reduced it to 3.0 and have scaled the sprites/tilesets up to 1.15. It seems to fix the issue, but literally _all of the sprites_ from now on need to be at a scale of 1.15, which is annoying. Perhaps I can rework the UI and such at some point to remedy that, because it feels gross, but I don't currently have the knowledge/know-how to do it.
