using Morpion;
using System.Reflection;

namespace TD_Morpion_UnitTest;

public class GameUnitTest
{
    [Fact]
    public void Constructor_ShouldInitializeGameWithBoardAndPlayers()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        Assert.NotNull(game);

        // Utilisation de la réflexion pour vérifier l'état interne
        var boardField = typeof(Game).GetField("_board", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(boardField?.GetValue(game));
        Assert.NotNull(playerXField?.GetValue(game));
        Assert.NotNull(playerOField?.GetValue(game));
        Assert.NotNull(currentPlayerField?.GetValue(game));
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerXAsCurrentPlayer()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);

        var playerX = playerXField?.GetValue(game);
        var currentPlayer = currentPlayerField?.GetValue(game);

        Assert.Equal(playerX, currentPlayer);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerXWithSymbolX()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerX = playerXField?.GetValue(game) as Player;

        Assert.NotNull(playerX);
        Assert.Equal('X', playerX.Symbol);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerOWithSymbolO()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerO = playerOField?.GetValue(game) as Player;

        Assert.NotNull(playerO);
        Assert.Equal('O', playerO.Symbol);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerOAsBot()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerO = playerOField?.GetValue(game) as Player;

        Assert.NotNull(playerO);
        Assert.True(playerO.IsBot);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerXAsHuman()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerX = playerXField?.GetValue(game) as Player;

        Assert.NotNull(playerX);
        Assert.False(playerX.IsBot);
    }

    [Fact]
    public void SwitchPlayer_ShouldAlternateFromPlayerXToPlayerO()
    {
        // Arrange
        var game = new Game();
        var switchPlayerMethod = typeof(Game).GetMethod("SwitchPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        switchPlayerMethod?.Invoke(game, null);

        // Assert
        var currentPlayer = currentPlayerField?.GetValue(game);
        var playerO = playerOField?.GetValue(game);
        Assert.Equal(playerO, currentPlayer);
    }

    [Fact]
    public void SwitchPlayer_ShouldAlternateFromPlayerOToPlayerX()
    {
        // Arrange
        var game = new Game();
        var switchPlayerMethod = typeof(Game).GetMethod("SwitchPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act - Switch twice pour revenir à X
        switchPlayerMethod?.Invoke(game, null);
        switchPlayerMethod?.Invoke(game, null);

        // Assert
        var currentPlayer = currentPlayerField?.GetValue(game);
        var playerX = playerXField?.GetValue(game);
        Assert.Equal(playerX, currentPlayer);
    }

    [Fact]
    public void SwitchPlayer_CalledMultipleTimes_ShouldAlternateBetweenPlayers()
    {
        // Arrange
        var game = new Game();
        var switchPlayerMethod = typeof(Game).GetMethod("SwitchPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);

        var playerX = playerXField?.GetValue(game);
        var playerO = playerOField?.GetValue(game);

        // Act & Assert - Vérifier plusieurs alternances
        for (int i = 0; i < 10; i++)
        {
            switchPlayerMethod?.Invoke(game, null);
            var currentPlayer = currentPlayerField?.GetValue(game);

            if (i % 2 == 0)
            {
                Assert.Equal(playerO, currentPlayer);
            }
            else
            {
                Assert.Equal(playerX, currentPlayer);
            }
        }
    }
}
