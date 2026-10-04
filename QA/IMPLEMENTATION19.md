# Crownfall Implementation 19 — Boss Runtime Hook

- Runtime battle now attaches the existing GorrukBossController to the canonical Gorruk unit.
- 70% HP phase requests the canonical 2 goblin + 2 archer reinforcement package.
- 30% HP phase exposes enrage state to the battle HUD.
- Phase state resets naturally with each wave/session and does not alter Chapter 1 encounter definitions.

Note: the existing Gorruk controller's damage-reduction/enrage stat values remain authoritative core data. Full animation/telegraph presentation still requires Unity play-mode validation.
