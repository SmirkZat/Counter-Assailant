<p align="center">
  <img width="100%" alt="Counter Assailant" src="https://github.com/user-attachments/assets/c458031f-e7e0-4d9b-91f7-47e5a05e546f">
  </br>
</p>

## 🔴About
**Counter Assailant** is an engaging arcade-style game that invites players to defend Earth from waves of descending alien invaders. As a spacecraft pilot, the primary objective is to eliminate as many alien ships as possible while dodging enemy fire.

Set against a retro-inspired pixel art backdrop, players can move their spacecraft horizontally across the bottom of the screen to strategically target and shoot at the approaching aliens. Each successful hit contributes to the player’s score, with higher points awarded for quicker eliminations.

Performance metrics play a crucial role in the gameplay experience, focusing on the number of alien ships destroyed, the time taken to complete each wave, and the number of lives lost. The scoring system encourages players to improve their skills and tactics to achieve better results in subsequent rounds.

The game includes a Leaderboard feature that allows players to compare their scores with friends and the global player base. After each game session, players can submit their performance data, which includes Player Name, Score, Lives Remaining, and other relevant stats. This data is seamlessly integrated into Looker Studio, providing a visual representation of player rankings and trends.

Space Invaders is designed for accessibility, supporting various input methods, including keyboard and gamepad controls. Whether for casual enjoyment or competitive play, Space Invaders offers an entertaining platform for honing shooting skills in a classic arcade environment. Players can access their performance data and explore rankings through an intuitive online dashboard available via the game's official website.
<br>
## ▶️ Video Gameplay
<img src="https://github.com/user-attachments/assets/3f46cd99-aa9a-4189-8ae9-337258461c2f.gif" alt="1" style="width:50%;height:auto;">
View Full Gameplay :

## 🕹️Download Game
Itch.io : 

<br>

## 📋 Project Info
This project using Unity 

I make this game by myself.

<br>

##  📜Scripts and Features

|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| AddressableAssetsData |	Manages game assets efficiently, allowing dynamic loading and memory management for textures, audio, and other resources. Ensures optimal performance across different platforms. |
| Art |	Contains all visual assets, including sprites for the player’s ship, alien invaders, and background elements, ensuring a cohesive retro aesthetic. |
| Prefabs |	Houses reusable game objects such as the player’s spacecraft, enemy ships, and projectiles. Prefabs allow for easy instantiation and manipulation during gameplay. |
| Scenes |	Includes different game scenes, such as the main menu, gameplay scene, and leaderboard scene, facilitating smooth transitions and gameplay flow. |
| ScriptableObjects |	Holds configurable game data such as enemy types, wave patterns, and score thresholds. This allows designers to tweak game parameters without modifying code.|
| Scripts |	Contains core scripts for gameplay mechanics and functionality, including: 
| - GameManager.cs |	Oversees game states, such as starting the game, progressing through waves, and handling player input. Manages overall flow and difficulty scaling. |
| - ScoreManager.cs	| Tracks player scores and lives. Responsible for submitting performance data to Google Sheets and updating scores in real-time. |
| - SaturationChanger.cs |	Adjusts the color saturation of the game environment to enhance mood and create atmospheric effects during gameplay. |
| - UIHandler.cs |	Manages UI components like score displays, player lives, and leaderboard integration, ensuring a smooth user experience. |
| - AlienSpawner.cs |	Controls the spawning behavior of alien ships, determining patterns and difficulty as waves progress. |
| - ProjectileManager.cs |	Manages the firing mechanics of the player’s projectiles, including collision detection and interactions with enemy ships. |
| - LeaderboardManager.cs |	Handles data submission for player scores to Google Sheets and retrieves data for display in Looker Studio, ensuring the leaderboard is current and accessible. |
| - BackgroundController.cs |	Manages dynamic background elements, enhancing the immersive space atmosphere with moving stars and parallax effects. |

<br>
## 📂Files description

```
├── Space-Invanders                     # In this Folder, containing all the Unity project files, to be opened by a Unity Editor
   ├── ...
   ├── Assets                         #  In this Folder, it contains all our code, assets, scenes, etcwas not automatically created by Unity
      ├── ...
      ├── AddressableAssetsData                   # In this folder, manage and manage in-game assets efficiently via Unity Package Manager
      ├── Art                     # In this folder, there are art. You can see these arts in the game via Unity
      ├── ....
   ├── ...

```
<br>

## 🕹️Game controls
The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| -> , <-           | Standard movement |
| Space             | Projectile shoot           |

<br>
## 🔥How to open up the project on Unity Editor
This game was developed using **Unity Editor 2021.3.11f1**, and we recommend that you download this specific version because using different ones, especially older versions, might result in problems

You are **required to download several assets from the Unity Asset Store** to properly operate this game. All assets should be placed in the **3rdParty** folder. The assets that need to be downloaded are as follows:

**Download Here:**
