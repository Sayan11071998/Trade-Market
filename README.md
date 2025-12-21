# TradeMarket

A 2D action-adventure game built in Unity where players trade items with NPCs, fight enemies, and progress through levels. Features MVC architecture, object pooling, and state-driven gameplay.

## Architecture

The game uses MVC (Model-View-Controller) pattern with service layers. Each system (Player, Enemy, NPC, Bullet) follows the same structure for consistency and maintainability.

### System Overview

```
┌─────────────────────────────────────────────────────────────┐
│                      GameService                            │
│                  (Central Coordinator)                      │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐   │
│  │PlayerService │    │  NPCManager  │    │EnemyManager  │   │
│  └──────┬───────┘    └──────┬───────┘    └──────┬───────┘   │
│         │                   │                   │           │
│         │ Coordinates       │ Coordinates       │           │
│         ▼                   ▼                   ▼           │
│  ┌──────────────┐    ┌──────────────┐    ┌──────────────┐   │
│  │ PlayerModel  │    │   NPCModel   │    │  EnemyModel  │   │
│  │PlayerControl │    │NPCController │    │EnemyControl  │   │
│  │  PlayerView  │    │   NPCView    │    │  EnemyView   │   │
│  └──────────────┘    └──────────────┘    └──────────────┘   │
│         │                   │                   │           │
│         └───────────────────┼───────────────────┘           │
│                             │                               │
│                      ┌──────▼───────┐                       │
│                      │ BulletService│                       │
│                      │  + BulletPool│                       │
│                      └──────────────┘                       │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```
<img width="8238" height="3968" alt="Image" src="https://github.com/user-attachments/assets/12bdab17-57df-48e5-84b5-2936d97e5106" />

### Component Structure

```
PlayerView (MonoBehaviour)
├── PlayerController (Logic layer)
│   └── PlayerModel (Data layer)
│       ├── Health & Stamina
│       ├── Inventory System
│       └── Combat State
├── PlayerStateMachine
│   ├── IdleState
│   ├── WalkState
│   └── DeadState
└── Input System Integration

NPCView (MonoBehaviour)
├── NPCController (Trade logic)
│   └── NPCModel (Trade data)
│       ├── Dialogue States
│       ├── Required Items
│       └── Rewards
└── NPCFactory (Creation)
    └── NPCRepository (Storage)

EnemyView (MonoBehaviour)
├── EnemyController (AI behavior)
│   └── EnemyModel (Enemy stats)
│       ├── Target Tracking
│       ├── Fire Cooldown
│       └── Health System
└── EnemyFactory (Creation)
    └── EnemyRepository (Management)

BulletService
├── BulletPool (Object pooling)
└── BulletController
    ├── BulletModel (Properties)
    └── BulletView (Collision)
        ├── PlayerBullet
        └── EnemyBullet
```

## Core Systems

### Combat System
`BulletService` manages projectile firing with object pooling. When a character fires, `BulletPool` retrieves an inactive bullet or creates a new one. `BulletController` handles lifetime and deactivation. Collision detection uses Unity tags to identify targets, applying damage through the appropriate model.

### Trading System
`NPCController` manages a three-stage dialogue flow (greeting → offer → confirmation). Trade validation checks player inventory against NPC requirements. Successful trades update both inventories and persist the NPC's traded state through `NPCModel`.

### State Management
`PlayerStateMachine` implements the State pattern with `IState` interface. States (Idle, Walk, Dead) handle specific behaviors and transitions. Movement magnitude determines Idle↔Walk transitions, while health triggers the Dead state.

### Save System
`PlayerSaveManager` serializes game state to `PlayerDataScriptableObject`. The system tracks scene completion, inventory state, and NPC trade history. Data persists across sessions and initializes on first load.

## Implementation Details

**Design Patterns:**
- MVC for system separation
- Factory pattern for entity creation
- Repository pattern for data access
- Object Pool for performance
- State Machine for player states
- Observer (delegates/events) for loose coupling
- Singleton for global services

**Key Features:**
- Generic base classes (`GenericObjectPool<T>`, `GenericMonoSingleton<T>`)
- ScriptableObject-based configuration
- Event-driven UI updates
- Component caching for performance
- Tag-based collision detection

**Dependencies:**
- Unity 2021.3+
- TextMeshPro
- Unity Input System
---

## Play Link
- https://sayannandi.itch.io/trade-market
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
