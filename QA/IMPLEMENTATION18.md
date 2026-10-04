# Crownfall Implementation 18 — Runtime Preparation Economy

- Connected existing BattleEconomy, BattleShop, QuickSummonSystem, MergeService and SellService through a runtime preparation adapter.
- Uses canonical RuntimeContentFactory definitions and deterministic preparation RNG.
- Preserves the existing economy values: 10 starting battle gold, 3 quick summon, 4 shop hero price, existing reroll escalation and sell table.
- Fixed the release-blocking RuntimeBattleHud CampaignService constructor mismatch found during final-pass audit.
- Added stabilization coverage for canonical deck/shop bootstrap and quick summon economy.

Unity compile/play-mode remains a validation boundary because Unity Editor is not installed in this environment.
