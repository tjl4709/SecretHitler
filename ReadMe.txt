"Secret Hitler" is the game that the users will play through while "Secret Hitler Server" is the
server side that must be running where all users can connect to it.

Read the pdf of rules to understand how to play the game, but note the following differences:
* The rules say that after a Special Election, the presidency moves to the left of the player who made
  the Special Election, but in this version, the presidency continues to the left of the player who
  was elected during the Special Election, skipping the players in between.

------------------------------------------------------------------------------------------------------
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

------------------------------------------------------------------------------------------------------
How To Use Secret Hitler Server: