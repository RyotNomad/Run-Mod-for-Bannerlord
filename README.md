# Run-Mod-for-Bannerlord
I created a mod for Bannerlord that allows the player to run in combat

Currently there is no documentation for Bannerlord so modding the game requires you to decompile the **dlls**(I used dnSpy) and then make sense of the code. There are no comments and nobody is sure how the different parts of the game all work together.

The mod I created took around 12 hours to both understand the games code, implement my solution, test and debug. Currently I'm not aware of any bugs but I've posted it to NexusMods in the hopes of the public finding mistakes I could not through gameplay.

To change the sprint button change the value in settings.cfg to the corresponding value in https://pastebin.com/v5Vz84ys

To change run speed, edit the native_parameters_CTS.xml file in ModuleData
### How it works:

# tldr: All char movement is set to a high value through **XML** but limited in the games onTick() function, pressing the button causes only the players character to receive a higher limit and thus moves faster while the button is held. 

Decompiling the games **dlls** showed huge amounts of code with no comments and most of the games functions stored the TaleWorlds.MountAndBlade.dll

It took a while but I figured that characters are called Agents and they had quite a few methods. The ones that interested me were setMaximumSpeed, getCurrentSpeedLimit and setMinimumSpeed. 

More tinkering around and the setMaximumSpeed function is an upper limit that the player would never reach. The player is instead limited by the value in getCurrentSpeedLimit. However there's no method to all us to change this value. Eventually I discovered the games **XML** files.

The game stores all the methods in **dlls** but stores the actual values and attributes in **XML** files. After more looking around and testing I discovered the native_parameters.xml file which stored values for human movement. The discord group helped me figure out how to add custom **XML** values without overwriting the games current ones(ensure future patches won't break my mod) and after tinkering around with the different attributes for human movement I found that the bipedal_speed_multiplier value directly affected character movement.

Increasing this however caused ALL human characters(NPC and player) to have their movement speed increased. Having everyone go faster defeated the point of the mod as players were always being out-run by the AI while on foot.

That's when I realized the setMaximumSpeed function might be useful. I had to set the maxSpeed for all characters in the battle and only have my button override the maxspeed for the player. I found the cheer mod and after decompiling the **dll** I was able to set the max speed for all characters in the battle. Then it was just a matter of working on the InputKey system and I was able to get it to work.

Bugs:
Currently increasing the biedl_speed_multiplier above 15 causes the maxSpeedLimit to be ignored and all characters move at full speed all the time. The limited knowledge of the codebase means I'm not sure how to fix this as of right now but moving at 15 looks comical and ruins the fun of the game. Right now, I consider it a non-issue as playable values work fine
