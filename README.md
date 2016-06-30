# GameBoyEmu
A C# gameboy emulator

This is a C# gameboy emulator, still in development. It can run most commercial games correctly.

The idea was just to dive into gameboy mecanics and play a little with assembly. Since the result is yet unfinished but pretty cool, I leave it here to the curiosity of all. Feel free to read, comment, use, debug the code. Everything is under WTFPL licence  (https://fr.wikipedia.org/wiki/WTFPL) but feel free to send me a message if you find anything interesting in this project.

I should continue it when I'm in mood and have some time. Cool thing would be to make it gbcolor compatible. 

WORKING :
- most games run on this emulator
- speed is quite accurate
- XBOX360 joypad compatibility
- state saves
- debug menu (F12) with code explorer, stepping, ram viewer

TODO :
- 4th sound channel (noise channel) is not accurate 
- RAM cartridge save is not available (indeed state saves are)
- Some cartridges with bank switching are not compatible 
- Some glitches can be seen on some games (mainly a vblank problem)
- Closing debug menu while running crashes


