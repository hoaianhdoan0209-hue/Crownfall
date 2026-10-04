# Crownfall — Windows x64 release build

Target editor: Unity 6 (`6000.0.x`).

1. Extract/open the project root in Unity 6 and allow package import/compilation to finish.
2. Run **Crownfall > Build > Prepare Release Settings** once. This validates the four release scenes and writes the release scene order.
3. Run EditMode/PlayMode tests and resolve any Unity-version-specific compiler/import issue.
4. Run **Crownfall > Build > Windows x64 Release**.
5. Output: `Builds/Windows/Crownfall.exe` plus the Unity player data folder beside it.
6. Smoke-test New Game -> Chapter 1 stages -> Gorruk -> save/quit/continue before distribution.

The release builder targets Windows x64 + IL2CPP and uses StrictMode. It fails if a release scene is missing, Unity reports build errors, or the expected executable is absent after a reported successful build.

Command-line equivalent after a successful editor import:
`Unity.exe -batchmode -quit -projectPath <project> -executeMethod Crownfall.Editor.CrownfallWindowsBuilder.BuildWindowsRelease -logFile build.log`

A source ZIP is not the executable. `Crownfall.exe` is release-complete only after Unity reports a successful build and the executable is smoke-tested.
