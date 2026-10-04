# Crownfall Implementation 16 — Runtime Battle Integration

Implemented without redesigning the approved first-playable rules/content.

- Battle scene now creates a real GridSystem + BattleController runtime simulation.
- Canonical HeroRecord/EnemyRecord content is adapted into transient UnitDefinition objects; no duplicate content table was introduced.
- Current unlocked heroes (plus encounter guest hero) spawn on the player side.
- Canonical WaveRecord enemy coordinates feed the existing UnitFactory / UnitBrain / targeting / movement / damage systems.
- Battle state automatically advances through encounter waves.
- Final victory commits through AppFlowService/CampaignService; defeat returns without rewards.
- Added runtime HUD for wave/living-unit/result state.
- Added integration invariants for encounter wave/spawn resolution.

Known validation boundary: this environment does not contain a Unity 6 editor executable, so Unity compilation/play-mode and Windows player build are not claimed here. Static source/package validation is performed before packaging.
