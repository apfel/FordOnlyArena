# Ford-Only Arena
Replaces all enemies in the fantasy arena with the short-haired Ford guy.  
Requires [MelonLoader](https://github.com/LavaGang/MelonLoader) 0.4 and [ModThatIsNotMod](https://boneworks.thunderstore.io/package/gnonme/ModThatIsNotMod/).

## Settings
You can change whether the mod is disabled (`Disabled`) and whether to keep the turret in the arena (I fucking hate that thing) (`KeepTurret`).  
ModThatIsNotMod's BoneMenu is also supported.

## Compiling
Requires:
- .NET Framework 4.7.2 SDK
- CMake 3.10 or newer
- A copy of BONEWORKS with MelonLoader and ModThatIsNotMod installed

```ps1
cmake -B build                          # Creates a build folder, use -DBONEWORKS_DIR=<path> if you installed BONEWORKS somewhere outside of C:\Program Files (x86)\Steam\steamapps\common
cmake --build build --config Release    # Builds the Release configuration.
```

## ChangeLog
1.0.0:  
Initial release.
