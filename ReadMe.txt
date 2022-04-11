"Secret Hitler" is the game that the users will play through while "Secret Hitler Server" is the
server side that must be running where all users can connect to it.

Read the pdf of rules to understand how to play the game, but note the following differences:
* The rules say that after a Special Election, the presidency moves to the left of the player who
  made the Special Election, but in this version, the presidency continues to the left of the player
  who was elected during the Special Election, skipping the players in between.

Read UpdateLog.txt to see the changes for each version.

----------------------------------------------------------------------------------------------------
How To Use Secret Hitler:
1) Run the executable
2) In "IP and Port" textbox type your the IPv4 address and port of the server separated by a colon.
   For example: 127.0.0.1:25565
3) Type in a username and click the submit button
4) The first user to connect to the server is made VIP, which just means that they're the one who
   starts the game(s). If the VIP disconnects from the server at any time, VIP is reassigned.

 Through command line:
  SecretHitler.exe [IP:port [username]]
 * IP:port must be formatted in the same manner as step 2 above
 * username an only be given if IP:port is also given
 * if just IP:port is given, it autofills the "IP and Port" textbox, but you must type in your
   username and click the submit button
 * If both parameters are given, it will automatically connect to the server

----------------------------------------------------------------------------------------------------
How To Use Secret Hitler Server:
1) Run the executable
2) Communicate your IP and the port that the server reported in the console to the users

 Through command line:
  SecretHitlerServer.exe [logging [port]]
 * specify whether logging is wanted or not
 * default is to log
 * accepted values for logging: disable(0, no, f, false) enable(1, yes, t, true)
 * specify a port to have it run on that port (make sure the port is not already in use)
 * logging must be specified to enter port
 * if no port is specified, it will choose an open one

----------------------------------------------------------------------------------------------------
Connecting To A Server:
  If all players and the server are on the same LAN (connected to the same router), launch the
server, then open a command prompt window on the same computer (cmd.exe) and type in "ipconfig".
Look for an entry labeled "IPv4 Address" and use this value as the IP to connect to the server.

  If players or the server are on separate networks, port forwarding/triggering is required. For the
computer running the server, use a static IP (look up a tutorial for your OS/device). Open a new
notepad window and type in the path to the server executable followed by the port you want to use,
then save this as a .bat file. Run this file instead of the server executable so that it uses the
same port eveytime. Look up hot to set up port forwarding/triggering on your router and enable it
for the port you chose in the .bat file and the static IP that you set for the computer. Players
will use the IP of your router (this can be found by opening a web browser on the server computer
and searching "my ip") and the port that you chose to connect to the server.