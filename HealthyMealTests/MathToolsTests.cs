using HealthyMeal.Utils;

namespace HealthyMealTests
{
    public class MathToolsTests
    {
        [Theory]
        [InlineData(100, 200, 50)]
        [InlineData(-100, 200, 0)]
        [InlineData(100, -200, 0)]
        [InlineData(0, 100, 0)]
        [InlineData(0, 200, 0)]
        public void ToPercantage_PassCorrectParameters_GetExpectedResult_Test(double part, double baseValue, double expectedPercentage)
        {
            // Arrange:

            // Act:
            double actualPercentage = part.ToPercentage(baseValue);

            // Assert:
            Assert.Equal(expectedPercentage, actualPercentage);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, 1, 12.3)]
        [InlineData(80, 181, 30, 5, 1.55, 2768.7)]
        [InlineData(80, 175, 26, -161, 1.55, 2484.3)]
        public void CalcRdc_PassCorrectParameters_GetExpectedResult_Test(
            double weight, 
            double height, 
            int age, 
            double sexCoeff, 
            double activityFactor, 
            double expectedRdc)
        {
            // Arrange:

            // Act:
            double actualRdc = MathTools.CalcRdc(weight, height, age, sexCoeff, activityFactor);

            // Assert:
            Assert.Equal(expectedRdc, actualRdc);
        }
    }
}
