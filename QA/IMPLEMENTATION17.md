# Crownfall Implementation 17 — Runtime Preparation / Formation

Implemented as a continuation of I16 without changing the approved combat/content rules.

- Battle scene now begins in a real preparation phase instead of immediately starting simulation.
- Available heroes come from the current save plus the encounter guest hero.
- Player can deploy/bench heroes before combat; encounter BoardCapacity is enforced.
- START COMBAT is gated until at least one hero is deployed.
- The transient RuntimeFormationPlan carries hero IDs and valid player-side grid coordinates into RuntimeBattleSession.
- RuntimeBattleSession now spawns the selected formation on every encounter wave; its old unlocked-hero overload remains as a compatibility fallback.
- Formation is intentionally not persisted into SaveData: it belongs to the current battle attempt, preserving save schema v1.
- Added stabilization tests for capacity/start gating and deterministic slot repacking.

Validation boundary: Unity Editor is not installed in this environment, so Unity compilation/play-mode and Windows player build are not claimed. Static/package checks are run before packaging.
