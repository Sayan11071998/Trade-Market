# Trade Market

**Trade Market** is a top‑down 2D Unity adventure where you guide a lone trader through three bustling levels of barter—and fend off a surprise ambush to claim ultimate victory.

---

## 📜 Game Lore  
Step into the vibrant bazaar of Trade Market. Every NPC you meet holds one prized item and secretly craves another. It’s up to you to uncover their desires, negotiate exchanges, and amass the goods needed to unlock each checkpoint. After collecting the ultimate prize in level three, a sudden wave of enemies attacks—test your combat skills in a climactic battle before you can truly win.

---

## 🚀 Key Features  
- **Three Levels of Trading**  
  - Explore modular, tilemap‑based markets.  
  - Discover NPC desires and execute dynamic trades.  
- **Surprise Final Ambush**  
  - After the last trade, defend against a horde of enemies.  
  - Engage with a seamless damage & cooldown system.  
- **Fluid Top‑Down Controls**  
  - Dash, strafe, and circle NPCs with precision.  
- **Cinematic Camera**  
  - Unity Cinemachine Follow & Confiner keep the action centered and within bounds.

---

## 🏗️ Architecture & Design Patterns  
- **Model‑View‑Controller (MVC)**  
  Clear separation of data, presentation, and game logic for maintainability.  
- **Factory & Repository**  
  Encapsulates object creation and data access to reduce coupling.  
- **Singleton & Service Locator**  
  Global access to core services (audio, game state) via a centralized registry.  
- **Observer / Event‑Driven**  
  Decoupled event broadcasts for trades, state changes, and damage notifications.  
- **State Machine**  
  Modular player modes (idle, trading, combat) with clean interface‑based transitions.  
- **Object Pool**  
  Efficient reuse of trading prompts, market stalls, and enemies to minimize GC spikes.

---

## 🛠️ Coding Principles & Best Practices  
- **SOLID & SRP**: One responsibility per module; high‑level components depend on abstractions.  
- **Dependency Injection & Interfaces**: Decoupled, testable modules with swappable implementations.  
- **DRY & Separation of Concerns**: Shared utilities centralized; UI, data, and core mechanics remain distinct.  
- **Encapsulation & Composition Over Inheritance**: Protected internal states; flexible behavior assembly.  
- **Defensive Programming**: Null‑checks, early returns, and input abstractions guard against errors.  
- **Data‑Driven Design**: ScriptableObjects and enums define items, trades, and states externally.  
- **Generic Programming**: Reusable, type‑safe pools and service bases streamline resource management.

---

## 🎮 Unity Integration  
- **Tilemap System**  
  Modular, artist‑friendly layouts for rapid level iteration.  
- **Cinemachine**  
  - *Camera Follow*: Smooth tracking of the player character.  
  - *Camera Confiner*: Keeps the view strictly within level bounds.  
- **Coroutine‑Based Timing**  
  Non‑blocking cooldowns, animation delays, and ambush triggers.  
- **Performance Optimization**  
  Efficient update loops, collision checks, and minimal GC overhead—even during the final enemy wave.

---

## Play Link

----

[![Watch the video](https://img.youtube.com/vi/tKzj3EghfZY/maxresdefault.jpg)](https://youtu.be/tKzj3EghfZY)
### [Gameplay Video](https://youtu.be/tKzj3EghfZY)

![Image](https://github.com/user-attachments/assets/a2a71760-c55b-48d0-b405-6554cfaf8101)

![Image](https://github.com/user-attachments/assets/080c0e95-a9e0-4769-a379-bad653029ee2)

![Image](https://github.com/user-attachments/assets/285787b9-bf60-45e4-aa82-3563108375d1)

![Image](https://github.com/user-attachments/assets/ebf51ce4-8d81-49a2-9b83-ecd9f743af5c)

![Image](https://github.com/user-attachments/assets/1d63de4c-0776-413c-8015-97f566169006)

![Image](https://github.com/user-attachments/assets/41111e4c-2245-4ae0-bf08-4820295a4153)

![Image](https://github.com/user-attachments/assets/4ac9e68e-b406-4a38-8c46-e4d0f58a280c)

![Image](https://github.com/user-attachments/assets/bdd2415e-6c94-400f-8644-d91a210fe43a)

![Image](https://github.com/user-attachments/assets/eeb80e09-aa95-4d8a-b515-25cff63b1de9)

![Image](https://github.com/user-attachments/assets/f6c1e646-9f84-4f64-8448-746bd9601937)

![Image](https://github.com/user-attachments/assets/4824798a-e701-4566-bd2d-598089419d29)

![Image](https://github.com/user-attachments/assets/d9842c8f-cb60-4c93-a4de-10a7fa0b7d11)

![Image](https://github.com/user-attachments/assets/fc9161ce-6992-431f-a5e4-1f12e7082313)

![Image](https://github.com/user-attachments/assets/b6aea4fd-bbf9-4c92-a39c-6602c8b55e80)
