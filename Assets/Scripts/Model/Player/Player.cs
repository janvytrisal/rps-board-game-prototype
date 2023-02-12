/*
 * Author: Jan Vytrisal
 */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a player - an actor in the game.
/// </summary>
/// <remarks>
/// It is expected instance of each player will be created at the start of the game.
/// </remarks>
public abstract class Player : ScriptableObject
{
    private List<Card> _cardsInHand;

    /// <summary>
    /// All cards curently in player's hand.
    /// </summary>
    /// <remarks>
    /// Set at the start of the game.
    /// </remarks>
    public List<Card> CardsInHand 
    { 
        get
        {
            if (_cardsInHand == null)
                _cardsInHand = new List<Card>();
            return _cardsInHand;
        }
    }
    /// <summary>
    /// Selected card to be played.
    /// </summary>
    public Card SelectedCard { get; protected set; }
    /// <summary>
    /// How many tiles should player move this round.
    /// </summary>
    public int TilesToMove { get; set; }
    /// <summary>
    /// A card player favours.
    /// </summary>
    /// <remarks>
    /// Used by some AI players and Avatar core rules.
    /// </remarks>
    public Card Avatar { get; set; }

    /// <summary>
    /// Notification informing that player selected a card to play.
    /// </summary>
    public event Action<Player> CardSelected;

    /// <summary>
    /// Request for player to select a card to play.
    /// </summary>
    public abstract void SelectCardToPlay();

    /// <summary>
    /// Event invoker for derived class.
    /// </summary>
    protected void InvokeCardSelectedEvent()
    {
        CardSelected?.Invoke(this);
    }
}
