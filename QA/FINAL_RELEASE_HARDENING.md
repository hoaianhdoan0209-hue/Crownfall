# Crownfall Final Release Hardening

Release-hardening pass after Implementation 20.

- Preserved gameplay/design and existing runtime composition.
- Added explicit EditorBuildSettings scene order for Bootstrap -> MainMenu -> WorldMap -> Battle.
- Hardened Windows builder: scene validation, release settings, Windows x64, IL2CPP, StrictMode, output existence check.
- Added minimal EditorSettings for Force Text serialization and Crownfall root namespace.
- Source/package static gates pass in this environment.
- Unity Editor compilation, EditMode/PlayMode tests, Windows player build, and executable smoke test remain required before claiming a verified Crownfall.exe.
