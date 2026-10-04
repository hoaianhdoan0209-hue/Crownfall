# Crownfall Implementation 20 — Final Source Release Candidate

Release-candidate scope reached without redesigning the approved v0.3 / Implementation 14 direction.

Final-pass changes after I17:
- I18: runtime preparation economy adapter for existing shop, quick summon, merge and sell core; fixed CampaignService constructor compile blocker.
- I19: connected existing Gorruk phase controller to runtime battle and canonical reinforcements.
- I20: added deterministic Windows x64 editor build entry point and release instructions.

Static release gates performed in the non-Unity environment:
- C# source brace scan.
- Canonical scene presence.
- Package manifest / Unity project version presence.
- No parameterless CampaignService construction remains.
- ZIP integrity check.

Required before claiming Crownfall.exe:
- Unity 6 import + C# compilation.
- EditMode/PlayMode test execution.
- Windows x64 BuildPipeline success.
- Executable smoke test through Chapter 1 and save/continue.
