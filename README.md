# rps-board-game-prototype
Rock Paper Scissors Board game prototype

General game rules:
The game is played in rounds where each player secretly selects a card (either Rock, Paper or Scissors), then cards are simultaneously revealed. Comparison against each player proceeds to determine who wins the most. The winners move on the game board. A player who reaches the last tile on the game board first wins the match.

Two game rules modifications:
Classic - the winners of each round will move one tile forward.
Avatar - the winners of each round will move one tile forward and by an additional tile if they won by playing their Avatar card. Players avatar card is randomly selected at the start of the match and is known to all players.

As game data are stored in Scriptable Object assets, following exposed properties can be changed: game rules, number of players and their strategies, game board size, random seed. All Scriptable Object assets are expected to be set in the Unity Editor.

Notes:
Currently only simulated play of AI vs AI is supported. 1000 matches is played by default. The result is logged to the Console.
The game is designed to be easily extended by adding new cards, implementing different AI strategies and game rules.
