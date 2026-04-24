namespace Blackbaud.Interview.Cards;

/// <summary>
/// A deck of cards
/// </summary>
public class Deck
{
    private readonly Stack<Card> _stackOfCards;
    private static readonly Random _runningNum = new Random();

    /// <summary>
    /// Private constructor for a new deck of <paramref name="cards"/>.
    /// Use Deck.NewDeck() static factory method.
    /// </summary>
    /// <param name="cards"></param>
    private Deck(IEnumerable<Card> cards)
    {
        _stackOfCards = new Stack<Card>(cards);
    }

    /// <summary>
    /// Creates and returns a new deck of cards.
    /// </summary>
    /// <returns></returns>
    public static Deck NewDeck()
    {
        return new Deck(
            Enum.GetValues<Suit>().SelectMany(suit =>
                Enum.GetValues<Rank>().Select(rank =>
                    new Card(rank, suit))
        ));
    }

    /// <summary>
    /// The number of remaining cards in the deck
    /// </summary>
    public int RemainingCards => _stackOfCards.Count;

    /// <summary>
    /// Returns true if there are no remaining cards in the deck
    /// </summary>
    public bool Empty => RemainingCards == 0;

    /// <summary>
    /// Removes the next card from the deck.
    /// </summary>
    /// <returns>The next card from the deck.
    /// Returns null if no cards remain.</returns>
    public Card NextCard()
    {
        if (!Empty)
        {
            var nextCard = _stackOfCards.Pop();
            return nextCard;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Shuffle Deck of Cards
    /// </summary>
    /// <param name="shuffleTimes"></param>
    /// <exception cref="Exception"></exception>
    public void Shuffle(int shuffleTimes)
    {
        if (shuffleTimes <= 0)
            throw new Exception("Shuffle times must be greater than 0");

        var listOfCards = _stackOfCards.ToList();

        for (int t = 0; t < shuffleTimes; t++)
        {
            for (int i = listOfCards.Count - 1; i > 0; i--)
            {
                int j = _runningNum.Next(i + 1);
                (listOfCards[i], listOfCards[j]) = (listOfCards[j], listOfCards[i]);
            }
        }

        _stackOfCards.Clear();

        for (int i = listOfCards.Count - 1; i >= 0; i--)
        {
            _stackOfCards.Push(listOfCards[i]);
        }
    }
}
