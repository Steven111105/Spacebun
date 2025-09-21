## ℹ️About
Help bunnies in space to cross between planets and avoid stellar object. Buy space-traffic facilities and reach certain score to unlock stories every level. See yourself the importance of traffic facilities on pedestrian crossing, while learning its uses in a cute and fun way!

## 🎮Controls
WASD to move

Space to dash

E to pick up and release bunnies

Q to use traffic lights

## 📜Scripts

## Gameplay
|  Scripts | Description |
| --- | --- |
| `Car.cs` | Script for space object behaviours |
| `CarSpawner.cs` | Spawns cars according to level and initialize space objects when spawned |
| `LightManager.cs` | Script for hologram police to warn of incoming objects. Gets called from `CarSpawner.cs` and calls `WarningLights.cs`|
| `WarningLight.cs` | Script for individual lights that gets called to blink from `LightManager.cs` | 
| `NPC.cs` | Script for different NPC stats and behaviours |
| `NPCSpawner.cs` | Spawns NPC and sets goal logic |
| `PlayerMovement.cs` | Script from player movement, including dash, picking up bunnies, and player animation |
| `Tower.cs` | Handles interaction logic for traffic lights |
| `TowerManager.cs` | Handles all the logic for stopping objects from spawning by activating lights, setting timers, and sprite change on the lights|
| `UIManager.cs` | Manages the UI in gameplay such as carrots, HP, and pause panel |
| `UpgradesManager.cs` | Applies bought upgrades to the current level |
| `ZebraCross.cs` | Handles NPC end goal and stopping blind bunnies when the upgrade is bought |

## Shop
| Scripts | Description |
| --- | --- |
| `LevelStory.cs` | Plays the story for each level when first entered |
| `ShopTutorial.cs` | Manages the slideshow on how each upgrade in the shop works |
| `ReplayShopTutorial.cs` | Script for button to open `ShopTutorial.cs` |
| `ShopSFXManager.cs` | Handles all shop related sfx like buying, not enough money, and play button |
| `ShopUIManager.cs` | Handles the UI in the shop like  making the bought items opaque, updating cost, and showing carrots |

## MainMenu
| Scripts | Description |
| --- | --- |
| `HowToPlay.cs` | Manages the controls tutorial slideshow |
| `MainMenuBGM.cs` | Keeps the BGM continuous from main menu to shops |
| `MainMenuSFX.cs` | Manages SFX calls for the main menu |
| `Settings.cs` | Manages settings such as resolution, fullscreen, and volume |


## Contributors
Clarabelle Karin Wijaya - Game Artist

Natania Maria Kurniawan - Game Designer

Steven Wijaya (Me) - Game Programmer