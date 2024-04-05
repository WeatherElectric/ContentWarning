**v1.2.1**
* Forgot to remove the embedded discord_game_sdk.dll

**v1.2.0**
* No longer loading an extra discord_game_sdk now that the game has it already

**v1.1.2**
* README update

**v1.1.1**
* Forgot to add AutoHookGenPatcher as a dependency

**v1.1.0**
* Moved over to AutoHookGen for patches
* With AutoHookGen, if you use an external mod to change config mid-game, it will unpatch the unneeded hooks for the mode that is no longer selected.
* Fixed a nullref in the main menu

**v1.0.0**
* Initial release