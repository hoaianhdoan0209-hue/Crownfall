# Crownfall automated Windows build

This repository contains a GitHub Actions workflow at `.github/workflows/build-windows.yml`.
It runs the Crownfall EditMode tests, invokes `Crownfall.Editor.CrownfallWindowsBuilder.BuildWindowsRelease`, verifies `Builds/Windows/Crownfall.exe`, packages the complete Windows build folder, and uploads `Crownfall_Windows_x64.zip` as an artifact.

## Required Unity activation
The workflow cannot legally or technically bypass Unity activation. Configure the Unity/GameCI credentials or license secrets required by your Unity license before running the workflow:
- `UNITY_LICENSE` when using a license file/activation flow supported by GameCI.
- `UNITY_EMAIL` and `UNITY_PASSWORD` when required by that activation flow.

Do not commit license credentials to the repository.

## Run
Open the repository Actions page, choose **Build Crownfall Windows**, and run the workflow. After it succeeds, download the **Crownfall-Windows-x64** artifact. The archive contains `Crownfall.exe` and the Unity runtime files required beside it.

A successful artifact is still a build, not a gameplay certification. Run `Crownfall.exe` on Windows and smoke-test Bootstrap -> Main Menu -> World Map -> Preparation -> Battle -> result/save before calling the release fully verified.
