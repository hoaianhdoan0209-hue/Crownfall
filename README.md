# Crownfall — Unity Project Foundation

Implementation checkpoint 01 for the Crownfall tactical auto-battler.

Canonical locked rules: 7x6 grid, bench 6, core deck 6, board 6 / boss 7, 1–5 stars, quick summon 3 Gold, shop hero 4 Gold, 3 offers, Gold cap 99, and no monetization/payment/ads systems.

## Environment note
This project skeleton was generated in an environment without Unity Editor. It has not been compiled or built here. Open it with a compatible Unity 6 LTS editor, allow packages to resolve, then run EditMode tests before continuing.

## Next implementation checkpoint
Combat foundation: GridSystem, GridCell occupancy/reservation, UnitDefinition/Unit, stats/health, DamageSystem, targeting, A* pathfinding, UnitBrain, BattleController, and combat tests.

## Implementation 02 — Combat Core
Added logical 7x6 grid cells with occupancy/reservations, A* pathfinder, runtime Unit, physical/magic/true mitigation, reachable-target selection, and BattleController state/result handling. This snapshot is source-level only because Unity Editor is not installed in the current environment.

## Implementation 03 — Autonomous Combat Loop
Added source for UnitEnergy, UnitMovementController, UnitCombatController, UnitBrain, UnitFactory and AttackPositionResolver. Units can now acquire reachable hostile targets, choose legal attack cells, reserve/path/move across the 7x6 grid, basic-attack using the DamageSystem, gain Energy, release cells on death, retarget after path failure, and drive Victory/Defeat through BattleController. Unity compilation is still unverified because Unity Editor is not installed in this environment.

## Implementation 04 — Skills / Status / Shield
Added data-driven SkillDefinition + modular effects, SkillController casting/recovery, target resolution, ShieldSystem, StatusDefinition/StatusContainer, periodic Burn/Poison groundwork, CC permission gates, and BattleClock-driven status/shield timing. UnitDefinition now exposes Skill Power and a skill asset reference.

Canonical content assets to create in Unity Editor when available:
- Kael Infernal Cleave: 220% ATK Physical + Burn (20 + 20% ATK/sec, 3 sec).
- Seraphine Radiant Grace: 220 + 180% SP heal + 12% target MaxHP shield for 4 sec.
- Thorne Toxic Volley: five 100% ATK arrows + 2 Poison stacks; basic 30% Poison hook follows in hero-feature integration.

Environment note: source validation only; Unity Editor is unavailable here, so no claim of Unity compilation or Windows build success is made.


## Implementation 05 — Preparation Core
Added BattleEconomy, BattleDeck, canonical 6-slot Bench, BattleHeroInstance, BoardState, QuickSummon (3 Gold), deployment/return/swap, atomic-style merge 2-to-1 through 5-star, canonical separate star scaling, sell values, and preparation tests.

Unity Editor is not installed in this execution environment, so this snapshot has source/static checks but no claimed Unity compilation.

## Implementation 06 reconstructed
Tactical shop, wave economy and retry snapshot source were reconstructed because the prior milestone ZIP was not actually persisted in the workspace.

## Implementation 07
Added canonical stat modifier pipeline, artifact slots/upgrades and War Drum/Arcane Crown/Blood Chalice hooks, plus formation evaluation/snapshot for Battle Line, Shield Wall, Hunter Line and Vanguard. Arcane Triangle enum/data hook is reserved for the mage roster integration pass.

## Implementation 08 restored — Enemy / Waves
Data-driven EnemyArchetypeDefinition, AIProfileDefinition, WaveDefinition, EncounterDefinition and WaveManager are included. Canonical IDs cover Goblin, Archer, Tank, Shaman, Skulker, Bannerman, Riftling and Rift Hound. Special-role fields are data hooks; presentation/advanced behaviors remain to be wired in Unity.

## Implementation 09 — Gorruk Boss
Adds reusable BossPhaseController plus Gorruk canonical constants: 4500 HP, 65 ATK, 50 Armor, 30 MR, 0.75 AS; 70% one-shot reinforcement phase (2 Goblins + 2 Archers request, 30% DR for 5s); 30% one-shot enrage; Ground Breaker telegraph 1.1s and 180% ATK constant. EditMode tests verify one-shot phase behavior including a direct 100% -> 20% threshold crossing.

STATUS: source/static checks only. Unity Editor is not installed in this environment, so Unity compilation and Play Mode are not claimed.

## Implementation 10 — Chapter 1 / Progression / Save
- Canonical Chapter 1 main path IDs and stage ordering.
- Stable hero/flag/challenge IDs; save stores source progression, not calculated combat stats.
- New Game starts at Prologue with Kael + Seraphine.
- Stage 1 unlocks Thorne; Stage 4 unlocks Brakk; Gorruk unlocks Lyra and Chapter 1 completion flag.
- No-backline-death challenge gates Nyx Trial together with Chapter 1 completion; Nyx Trial unlocks Nyx.
- RewardService uses idempotent transaction IDs to prevent duplicate grants.
- SaveService writes JSON to .tmp, verifies parse, backs up previous save, then replaces primary; load falls back to .bak.
- Save schema is versioned with an explicit migration seam.
- Unity Editor is not installed in this environment, so Unity compilation remains unverified here.

## Implementation 11 — UI Flow / Scenes / Bootstrap
- Canonical scene IDs/build order: 00_Bootstrap, 01_MainMenu, 02_WorldMap, 03_Battle.
- RuntimeServices owns SaveService, SceneService and AppFlowService composition.
- MainMenuPresenter exposes New Game / Continue; Continue depends on a valid primary/backup save.
- WorldMapPresenter selects a stable stage ID; BattlePresenter commits victory through CampaignService.
- RewardPresenter is presentation-only and never grants progression.
- FirstPlayableSceneValidator validates canonical Build Settings order in the Unity Editor.
- Placeholder scene shells are included; visual hierarchy/prefabs still require Unity Editor authoring.

## Implementation 12 — Chapter 1 Content Wiring
Added code-generated canonical First Playable content catalog: 6 heroes, 8 regular enemies + Gorruk, Chapter 1 encounters/waves, Stage 3 artifact choice, Stage 4 Brakk guest, stable IDs, lookup APIs, and validation/tests. This avoids requiring manually-authored ScriptableObjects before the project is first opened in Unity.

## Implementation 13 — Vertical Slice Integration
- Added StageSession + StageSessionFactory to bind canonical content encounters to preparation/wave state.
- Added VerticalSliceOrchestrator for New Game/Continue -> Begin Stage -> complete waves -> commit campaign reward -> save.
- Victory commit is rejected until all encounter waves are complete.
- Added full Chapter 1 data/state integration tests: main path, unlocks, idempotent Gorruk reward, Nyx Trial eligibility, canonical Gorruk board/stat validation.
- Unity Editor is not installed in this environment, so Unity compile/PlayMode/build are not claimed.

## Implementation 14 - Stabilization Harness
Adds release invariant tests, deterministic RNG checks and QA validation gates. This package is statically audited only; Unity compile/PlayMode/Windows build remain unverified because Unity Editor is unavailable in this environment.

## Implementation 15 — Runtime Scene / UI Composition Bridge
- Added `RuntimeEntryPoint` so the project initializes even though the four serialized scenes are still minimal shells.
- Added `RuntimeSceneComposer` to build a functional source-authored Main Menu and Chapter 1 World Map, plus a Battle encounter inspection bridge, using the existing `RuntimeServices` / `AppFlowService` state.
- Bootstrap now routes into Main Menu at runtime without requiring manual scene hierarchy authoring first.
- World Map exposes only current/completed canonical Chapter 1 stages and reads actual save progression.
- Battle composition deliberately does **not** fake combat completion; existing Grid/UnitBrain/Skill/Wave systems remain canonical and still need presentation/runtime wiring.
- Added stabilization invariants for canonical scene order and runtime composition types.

Environment note: static/source validation only. Unity Editor is unavailable here, so compilation, PlayMode and Windows player build are not claimed.

## Final RC continuation (I15–I20)
The runtime shell now composes menu/map/battle, drives real combat waves, supports pre-battle formation and existing preparation economy systems, hooks Gorruk runtime phases, and includes a Windows x64 BuildPipeline entry point. See `RELEASE_STATUS.md` and `BUILD_WINDOWS.md`.
