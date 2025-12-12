using Morpion;

namespace MorpionUnitTest;

public class GameUnitTest
{
    [Fact]
    public void RunGameTest()
    {
        var game = new Game();
        game.Run();
        
        Assert.True(true);
    }
}