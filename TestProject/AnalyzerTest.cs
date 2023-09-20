namespace TestProject
{

    using ChalengeBackend.Services;
    using Xunit;

    public class AnalyzerTest
    {
        [Fact]
        public void TryReverse_ValidInput_ShouldSucceed()
        {
            // Arrange
            uint input = 12345;
            uint expectedReversedNumber = 54321;

            // Act
            bool result = AnalyzerService.TryReverse(input, out uint reversedNumber);

            // Assert
            Assert.True(result);
            Assert.Equal(expectedReversedNumber, reversedNumber);
        }

        [Fact]
        public void TryReverse_InvalidInput_ShouldFail()
        {
            // Arrange
            uint input = uint.MaxValue; // This input is too large and will fail

            // Act
            bool result = AnalyzerService.TryReverse(input, out uint reversedNumber);

            // Assert
            Assert.False(result);
        }
    }
}