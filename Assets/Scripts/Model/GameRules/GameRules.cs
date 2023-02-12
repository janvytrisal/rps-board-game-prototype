/*
 * Author: Jan Vytrisal
 */

using System.Collections.Generic;

/// <summary>
/// Defines general rules of the game modified by specific rule settings.
/// </summary>
/// <remarks>
/// Glossary:
///     game - from start tile to finishing tile, divided into rounds,
///     round - one Rock Paper Scissors like battle where each player compares their card
///         agains each other player cards to determine winners.
/// </remarks>
public class GameRules
{
    private RuleSettings _ruleSettings;

    /// <summary>
    /// Initialize game rules.
    /// </summary>
    /// <param name="ruleSettings">What rule settings to use. These modifies the general game rules.</param>
    public GameRules(RuleSettings ruleSettings)
    {
        _ruleSettings = ruleSettings;
    }

    #region MainMethods
    /// <summary>
    /// Determines how many tiles each player moves this round.
    /// </summary>
    public void SetHowManyTilesToMove(List<Player> players, GameBoard board)
    {
        ClearTilesToMove(players);
        List<Player> winners = WhoWinsRound(players, board);
        SetHowManyTilesWinnersMove(winners);
    }

    #region GameWinning
    /// <summary>
    /// Returns true if at least one player is on the last tile of the board.
    /// </summary>
    public bool IsAnybodyGameWinner(List<Player> players, GameBoard board)
    {
        return WhoAreGameWinners(players, board).Count > 0;
    }
    /// <summary>
    /// List of players occupying the last tile on the board.
    /// </summary>
    /// <param name="players">Players to be checked.</param>
    /// <param name="board">Game board of interest.</param>
    public List<Player> WhoAreGameWinners(List<Player> players, GameBoard board)
    {
        List<Player> winners = new List<Player>();
        foreach (Player player in players)
            if (board.IsPlayerAtLastTile(player))
                winners.Add(player);
        return winners;
    }
    #endregion
    #region PublicGetMethods
    public List<Card> GetStartingCards()
    {
        return _ruleSettings.StartingCards.Cards;
    }
    #endregion
    #endregion

    #region SupportMethods
    /// <summary>
    /// Returns list of players which won the card vs card round.
    /// </summary>
    private List<Player> WhoWinsRound(List<Player> players, GameBoard board)
    {
        List<Player> playersWithMostWins = WhoHaveMostWins(players);
        if (playersWithMostWins.Count > 1)
            return _ruleSettings.TieResolver.WhoWinsTie(playersWithMostWins, board);
        else
            return playersWithMostWins;
    }

    /// <summary>
    /// Returns list of players with most wins.
    /// </summary>
    private List<Player> WhoHaveMostWins(List<Player> players)
    {
        List<Player> winners = new List<Player>();
        int[] wins = new int[players.Count];
        int maxWins = 0;
        for (int i = 0; i < players.Count; i++)
            for (int j = i + 1; j < players.Count; j++)
            {
                int decision = WhoWins(players[i].SelectedCard, players[j].SelectedCard);
                if (decision == 1)
                {
                    wins[i]++;
                    if (wins[i] > maxWins)
                        maxWins = wins[i];
                }
                else if (decision == -1)
                {
                    wins[j]++;
                    if (wins[j] > maxWins)
                        maxWins = wins[j];
                }
            }
        //Find all players with max wins.
        for (int i = 0; i < wins.Length; i++)
        {
            if (wins[i] == maxWins)
                winners.Add(players[i]);
        }
        return winners;
    }
    /// <summary>
    /// Bit wise comparison to determine which card wins.
    /// </summary>
    /// <returns>
    /// 1 if card 1 wins, -1 if card 2 wins, 0 if it is tie.
    /// </returns>
    /// <remarks>
    /// A card is considered a winner if its win mask has at least one
    /// common bit as a card mask of the other card.
    /// </remarks>
    private int WhoWins(Card card1, Card card2)
    {
        int card1Wins = (card1.WinMask & card2.CardMask);
        int card2Wins = (card2.WinMask & card1.CardMask);

        if (card1Wins != 0)
        {
            if (card2Wins == 0)
                return 1; //Card 1 wins.
            else
                return 0; //Tie, because they win agains each other.
        }
        else
        {
            if (card2Wins != 0)
                return -1; //Card 2 wins.
            else
                return 0; //Tie, because nobody can win agains the other.
        }
    }

    /// <summary>
    /// How many tiles should winners move.
    /// </summary>
    /// <param name="winners">Players that won the round.</param>
    private void SetHowManyTilesWinnersMove(List<Player> winners)
    {
        _ruleSettings.CoreRules.SetHowManyTilesWinnersMove(winners);
    }

    private void ClearTilesToMove(List<Player> players)
    {
        players.ForEach(player => player.TilesToMove = 0);
    }
    #endregion
}
