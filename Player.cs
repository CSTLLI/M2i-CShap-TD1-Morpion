namespace Morpion;

public class Player
{
    public char Symbol { get; }
    public bool IsBot { get; }

    public Player(char symbol, bool isBot = false)
    {
        Symbol = symbol;
        IsBot = isBot;
    }
}
