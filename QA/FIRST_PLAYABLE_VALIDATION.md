# Crownfall First Playable Validation Gate

Automatable/static gates in this environment:
- Source braces balanced.
- Canonical content IDs unique and referenced IDs valid through ContentValidator tests.
- Deterministic gameplay RNG tests present.
- Reward/save/vertical-slice tests remain in project.
- No real-money monetization implementation may exist.
- Package integrity must pass `unzip -t`.

Requires Unity-capable build machine and is NOT claimed as passed here:
- Unity C# compilation.
- EditMode/PlayMode execution.
- Scene serialization/import validation.
- Windows x64 player build.
- Launch/smoke/performance test.

Release must not be labeled First Playable executable until all Unity-machine gates pass.
