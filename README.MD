# Buff Bar
A simple mod for Soulstone Survivors that displays a collection of icons summarising your currently active buffs, shields and minions.

# Building From Source
Firstly, ensure MelonLoader is installed and the game has been run at least once.

Second, after pulling this repo, create a file in the root of the project called `paths.csproj` that looks like this:
```xml
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
        <SoulstoneSurvivorsPath>G:\SteamLibrary\steamapps\common\Soulstone Survivors</SoulstoneSurvivorsPath>
    </PropertyGroup>
</Project>
```

This file is ignored by VCS so it allows machine-specific installs to be handled a little more gracefully.