Dedicated Server Essentials - Plugin
====================================

Overview
--------
This plugin is aimed at being an essential plugin to run on dedicated servers.  It will cover a lot of very basic requirements for a server administrator.  This plugin looks to really show off how valuable server extender can be, by providing a lot of built in cleaning and adminstrative options and commands.

There are many options in this plugin, and those options will expand as time goes on.  Every section of the plugin can be disabled if desired, to tightly control what an administrator wants to do.

Major Feature Overview
----------------------

- Automated Backup
- Automated Restart with notifications
- Chat Information commands with interval based repeating
- Automated Join Messages for new and old players 
- Automated new player spawn movement
- Advanced Adminsitrator Commands

Indepth Features Anaylsis
-------------------------

Automated Backup
----------------

This is an option that all administrators should use.  It will automatically backup your world save files in a backup directory.  It will also compress them down so they don't take up too much room.  And lastly this option will also cleanup old backups.  

Options:
- BackupEnabled - This allows you to turn Backup off or on
- BackupCleanup - This allows you to turn Cleaning up of the backups off or on
- BackupCleanupTime - The amount of time, in days, that a backup will last before cleaned up
- BackupCreateSubdirectories - This option forces the backup to put a separate backup in a new directory each time it occurs
- BackupAsteroids - Enabling this option will make the backin process include asteroids in the backup file.  If disabled, the .vx2 files will not be saved.
- BackupItems - This is where you define when you want a backup to occur.  You specify the hour and minute of the day you wish the backup to happen.  Items are defined as follows:
  - Enabled - Enable / Disable this backup item
  - Hour - The hour to run this item in the range between 0-23.  If you specify -1 for this option it will run every hour
  - Minute - The minute to run this item in the range between 0-59.

Automated Restart with Notifications
------------------------------------

This option allows you to schedule automated restarts of your server.  Sadly the game is not memory leak proof, and a quick restart can fix a lot of issues.  This option also comes with the ability to notify your users of impending restarts at timed intervals.

Options:
- RestartEnabled - This allows you to turn Restart off or on
- RestartAddedProcesses - This is a multline field that allows you to run things in between restarts.  Each line is a seperate process in the restart batch file.
- RestartItems - These items allow you to define notifications that occur before a restart happens. You set a message, you set the minutes before restart the message will be shown, and you can force a save or stop all ships.  They are defined as followed:
  - Message - This is the message that will be broadcasted to all users
  - MinutesBeforeRestart - This is the amount of time before a restart that this message is sent
  - Save - This option allows you to force a save
  - StopAllShips - This option allows you to forcefully stop all ships that are not piloted
- RestartTimeItems - These items allow you to define a time of day of when you'd like a restart to take place.  They are defined as followed:
  - Enabled - Enable / Disable this restart time item
  - RestartTime - 24 hour time of day when this restart should occur

Chat Information Commands with interval based repeats
-----------------------------------------------------

This option allows you to setup commands that users can access that allow administrators to display server information to the user.  The command /info is the base command, and the administrator then defines subcommands to display different types of information.  For example defining a subcommand 'motd' that gives a general message to users is setup with a subcommand of motd.  The user then types /info motd to see it.  You can then specify if you'd like that message to be displayed for everyone at intervals.  So for example you can set it up to send that message once every few hours, even if a player doesn't type the /info motd command.

Options:
- InformationEnabled - This allows you to turn Information commands off or on
- InformationItems - This lets you define information commands.  Defining an item is pretty simple. 
  - Enabled - Enable / Disable this information item
  - IntervalSeconds - The amount of time it takes for this item to be broadcasted publically.  Set to 0 to not have it broadcast
  - SubCommand - The command a user types to view this information item
  - SubText - The actual text that is displayed with this item is queried using the /info command or broadcasted.  You may use the %name% tag which gets replaced by the user's name.  This is a multiline text, and each line will be broadcasted individually per interval as well.  So this allows you to setup messages that get sent in order.

Automated Join Messages for new and old players
-----------------------------------------------

This option allows you to greet players with a custom message.  New and old players can receive different messages.  You may also use the %name% tag that will be replaced by a users username.  This allows for a highly customized greeting.

Options:
- GreetingEnabled - Enable / Disable Greetings
- GreetingMessage - Message to normal users.  You may use %name% which gets replaced with the user's name, for personalized greetings.
- NewUserGreetingMessage - Different message to new users.  You can use %name% as well.

Automated New Player Spawn Change
---------------------------------

This option allows you to move players closer to viable asteroids.  A viable asteroid is one that has more than 3 different base materials.  It will then move them closer to that asteroid.  This is useful for servers with asteroids that are very spread out.  Right now the asteroid selected will be random.  More options are coming for this

Options:
- NewUserTransportEnabled - This allows you to turn automated transport off or on.
- NewUserTransportDistance - Distance from a viable asteroid that they will be moved.

Advanced Administrator Chat Commands
------------------------------------

We've added new administrator commands that we will expand upon slowly.  They will aid in moving grids and stations around, along with trying to keep things clean.

For options, do not include braces.  [] stand for required, while () stand for optional.

Command| Options|Example
-------|--------|-----------------------------------------------------------------------------------------------------
/admin scan area at|[x] [y] [z] [radius] | This command allows you to scan an area of space within [radius] for ships and stations.
- 
- /admin scan area towards sx sy sz tx ty tz distance - This command allows you to scan an area of space from one position towards another within distance.  For example if you specify sx, sy, sz as 0, 0, 0 and tx, ty, tz as 100, 100, 100 and you specify distance as 1000 it will scan at a position that is 1000 meters from 0, 0, 0 in the direction of 100, 100, 100.  This command is usually used with the move area towards command so you can see if you're moving ships into an area that contains other ships.
- /admin scan nobeacon - This command scans for ships and stations that have no beacons.  This allows you to preview a list of ships before running the cleanup on it in case something is wrong.

Move commands:
- /admin move player <username> x y z - This moves a player to x, y, z.  Player must be in space suit.
- /admin move area to sx sy sz tx ty tz radius - This command allows you to move ships and stations from one area to another.  Please note that tx, ty, and tz are relative movement.  So if you specify sx, sy, sz as 0, 0, 0 and tx, ty, tz as 5000, 5000, 5000, the new position will be sx + tx, sy + ty, sz + tz, so in this example: 5000, 5000, 5000.
- /admin move area towards sx sy sz tx ty tz radius - This command allows you to move ships and stations from one area to another at a certain distance.  For example if you want to move ships closer to the center of the world, you would specify sx, sy, sz at the general area of the ships you want to move, and tx, ty, tz as 0, 0, 0 and distance indicates how far to move those ships towards 0, 0, 0.

Delete commands:
- /admin delete grids area x y z radius - Deletes all ships and stations in the sphere of radius at position x, y, z
- /admin delete ships area x y z radius - Deletes all ships in the sphere of radius at position x, y, z
- /admin delete stations area x y z radius - Deletes all stations in the sphere of radius at position x, y, z
- /admin delete nobeacon - Deletes all ships that have no beacons.  This checks to see if ships are connected via - connector, piston or rotor.

Ownership commands:
- /admin ownership change <username> <entityId> - Changes the owner of all the functional blocks on a grid to the playerId of username

To come:
- Block delete commands (over limit of drills, prohibited blocks, etc)
- Grid delete commands with more attributes (no power)
- More cleanup commands
- Commands that disable blocks, for example thrusters
