# 🏴‍☠️ CannonPirates - Clean Code & Architecture Showcase

> **⚠️ IMPORTANT NOTICE:**
> This repository contains **ONLY the C# source code** of the commercially published mobile game *CannonPirates*. All visual assets, 3D models, textures, shaders, audio files, and proprietary configurations have been explicitly excluded to protect commercial intellectual property.
>
> This repository serves purely as a portfolio showcase to demonstrate my application of **SOLID principles**, **Clean Code**, and scalable software architecture in Unity.


## 🚀 Engineering Focus
This repository highlights software architecture rather than gameplay content. Its primary purpose is to demonstrate scalable Unity architecture, clean code practices, performance optimization techniques, and maintainable gameplay systems used in a published mobile title.

## 🎮 About The Game
**CannonPirates** is a top-down mobile ship combat game published on the App Store and Google Play Store under the **Zeyvis** brand. The game features dynamic wave-based naval combat, deep ship customization, and highly optimized performance for mobile devices utilizing a single-scene architecture.

## 🏗️ Architecture & Design Patterns
This project was built with a strong emphasis on modularity, high performance, and maintainability.

* **Data-Driven Architecture:** Extensive use of `ScriptableObjects` (`ShipData`, `CannonData`, `LevelData`) to completely decouple game configuration from core logic. This ensures scalable content addition without modifying scripts.
* **Finite State Machine (FSM):** The enemy AI (`EnemyShipAI`) utilizes a clean state machine to seamlessly transition between `Approaching`, `Flanking`, and `ReadyToFire` states, using optimized `sqrMagnitude` calculations for distance checks.
* **Object Pooling:** To ensure zero Garbage Collection (GC) spikes and smooth frame rates during intense naval battles, comprehensive pooling systems were built for projectiles (`PlayerBulletPool`, `EnemyBulletPool`), visual effects (`HitParticlePool`, `SplashEffectPool`), and 3D spatial audio (`AudioPoolManager`).
* **Observer Pattern:** Heavy reliance on C# `event` and `Action` (e.g., `OnPlayerDeath`, `OnEnemyDeath`, `OnBallHitTarget`) to create loosely coupled systems. UI, Audio, and Ad managers listen to gameplay events without creating rigid dependencies.
* **Interface Segregation:** Implementation of the `IDamageable` interface allows projectiles to interact with the player, enemy ships, and individual destructible ship parts seamlessly.

## ⚙️ Key Systems Highlight
* **Modular Equipment System:** The `ShipSpawner` and `ShipEquipper` scripts dynamically construct ships at runtime. They instantiate and accurately scale cannons and sails onto predefined physical sockets based on user selections saved via `PlayerPrefs`.
* **Dynamic Difficulty Engine:** The `VaweEnemyShipSpawner` automatically calculates a "Player Power Level" based on equipped gear to dynamically adjust enemy wave frequencies, ship types, and damage multipliers.
* **Mobile Optimization:** Includes a custom `FpsManager` allowing users to toggle a Power Saving mode (dynamically switching between 60 FPS and device native refresh rates) and a `SceneTransition` manager that handles asynchronous loading with eased UI animations.
* **Monetization & Privacy:** Clean abstraction of Google Mobile Ads (AdMob) and the User Messaging Platform (UMP SDK) via dedicated `AdsManager` and `ConsentManager` classes.
