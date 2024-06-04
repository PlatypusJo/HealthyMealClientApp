using HealthyMeal.Utils;

namespace HealthyMealTests
{
    public class MealTypeProviderTests
    {
        [Theory]
        [InlineData("Завтрак", MealType.Breakfast)]
        [InlineData("Обед", MealType.Lunch)]
        [InlineData("Ужин", MealType.Dinner)]
        [InlineData("Перекус", MealType.Snack)]
        public void Provide_PassCorrectName_GetExpectedMealType_Test(string mealTypeName, MealType expectedMealType)
        {
            // Arrange:
            MealTypesProvider.RegisterAll();

            // Act:
            MealType actualMealType = MealTypesProvider.Provide(mealTypeName);

            // Assert:
            Assert.Equal(expectedMealType, actualMealType);
        }

        [Theory]
        [InlineData("завтрак")]
        [InlineData("за_утро_так")]
        [InlineData("")]
        public void Provide_PassIncorrectName_CatchKeyNotFoundException(string mealTypeName)
        {
            // Arrange:
            MealTypesProvider.RegisterAll();

            // Act:
            Exception exception = Record.Exception(() => MealTypesProvider.Provide(mealTypeName));

            // Assert:
            Assert.IsType<KeyNotFoundException>(exception);
        }
    }
}