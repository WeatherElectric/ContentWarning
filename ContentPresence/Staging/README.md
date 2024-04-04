# Content Presence
A mod for Content Warning that adds Discord Rich Presence

## What about the other one?
The other one does not handle that many things, it just shows level name and quota.

This one shows:
* Level name AND icon
* Current quota
* Current face
* Whether you're dead or not
* Random string of text if mode is Entries
* How many deaths have occured if mode is Casualties
* How much oxygen is left if mode is Oxygen
* How much film is left
* Whether you're recording or not

## Configuration
The config file is located where normal Bepin config files are.

### Basic Config
* DiscordAppID: The ID of the app you want to use. You can create one at https://discord.com/developers/applications
* Mode: The mode you want to use. Can be one of the following:
  * Entries: Show random strings of text
  * Casualties: Show how many deaths have occured
  * OxygenLeft: Show how much oxygen is left

More modes are planned.

### UserEntries
There is one extra file, in the Bepin config folder there's now a new folder: `Weather Electric/Content Presence"`

Add new lines to UserEntries.txt, and the mod will use them if Entries mode is enabled.

### Discord Application Setup
If you are using a custom Discord app, here are the key names for the images:
* Harbor: harbor
* Factory: factory
* Home: home
* Main Menu: gamelogo
* Camera Is Recording: recording
* Camera Is Not Recording: notrecording