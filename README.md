Hearthstone-Like Card Game Prototype
🃏 A turn-based card game prototype developed with Unity

📌 Documentation
🛠️ Design Decisions & Architecture
This project is designed as a Hearthstone-inspired turn-based card game.
Key components include:
✅ Card System: Utilizes ScriptableObjects for dynamic card data management.
✅ Turn Management: Uses State Pattern to handle Player Turn and Enemy Turn separately.
✅ Animations: Implemented using DOTween for smooth card movement and effects.
✅ Audio Management: Singleton AudioManager for handling menu music, in-game music, and sound effects.
✅ UI & Touch Support: Canvas-based UI system with mobile-friendly drag & drop functionality.
✅ Buff & Debuff System: Turn-based interactions to modify attack and healing power dynamically.

📐 Implemented Patterns & Rationale
This project follows several design patterns to improve maintainability and scalability:

📌 🎯 Singleton Pattern:

Used for GameManager, AudioManager, and TurnManager.
Why? Ensures a single instance for centralized management.
📌 🔄 State Pattern:

Used in the turn system (PlayerTurnState, EnemyTurnState).
Why? Allows dynamic state transitions, making turn management flexible.
📌 📦 Factory Pattern:

Used for Player and Enemy instantiation.
Why? Provides Dependency Injection using Zenject for better object lifecycle management.
📌 🃏 ScriptableObject Pattern:

Used for card data management (name, mana, effects, visuals).
Why? Increases performance and modularity of the card system.
📌 📜 Observer Pattern (Event System):

Used for UI updates on turn change and buff/debuff management.
Why? Automatically updates the UI and game state when player/enemy actions occur.
🚧 Challenges & Solutions
1️⃣ Implementing Drag & Drop Across Platforms (PC & Android)
Issue: The drag & drop system worked on PC but was unresponsive on Android.
Solution: Integrated Input.touchCount to differentiate between touch and mouse input, ensuring smooth functionality on mobile devices.

2️⃣ Handling Unplayed Cards at the End of Player Turn
Issue: Cards remained in the play area even after the turn ended.
Solution: OnGameStateChange event in TurnManager ensures that any unplayed cards return to the player’s hand at the start of the enemy’s turn.

3️⃣ Implementing Buff & Debuff Effects with Duration
Issue: Buff & Debuff effects remained indefinitely and did not expire after a few turns.
Solution: Each turn, UpdateEffects() is called in TurnManager, reducing the duration of active effects until they expire.

📌 Instructions (How to Run the Prototype)
💻 PC & Mac
1️⃣ Open the project in Unity 6
2️⃣ Load the main scene (MainScene.unity).
3️⃣ Press Play to start the game.

📱 Android
1️⃣ Go to File > Build Settings in Unity.
2️⃣ Select Android as the platform and click Switch Platform.
3️⃣ Connect your Android device via USB and select Build & Run to deploy the game.

📌 Controls & Features
🖱️ PC:

Click and drag to select a card.
Release the card over the Play Area to play it.
Click End Turn to switch to the enemy's turn.
📱 Android:

Drag the card using touch gestures to move it.
Release to either return it to your hand or play it.
Press End Turn to pass the turn to the enemy.
📌 Next Steps & Future Improvements
🔹 Multiplayer Support (Photon/Unity Netcode for Online Play)
🔹 Advanced AI (Making the enemy choose cards strategically)
🔹 Enhanced Card Effects (Fire, Ice, Poison, Custom Animations)
🔹 Adding More Cards & Special Abilities