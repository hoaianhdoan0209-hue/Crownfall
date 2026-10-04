# Implementation 15 — Runtime Scene / UI Composition Bridge

Purpose: remove the dead-shell startup/UI blocker without redesigning Crownfall.

Static checks completed here:
- Runtime entry point exists and initializes existing services.
- Bootstrap routes to canonical Main Menu.
- Main Menu binds New Game / Continue to AppFlowService.
- World Map derives stage availability from actual SaveData and ChapterOneDefinition.
- Battle bridge derives encounter/wave display from FirstPlayableContentCatalog.
- No debug button grants rewards or marks waves/stages complete.
- Existing canonical scene IDs and progression rules remain unchanged.

Still requires Unity-capable machine:
- C# compile/import check under Unity 6 LTS.
- InputSystem UI module runtime verification.
- PlayMode click-through.
- Actual combat presentation/spawn integration.
- Windows x64 build and executable smoke test.
