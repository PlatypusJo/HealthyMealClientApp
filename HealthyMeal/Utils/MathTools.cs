using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Utils
{
    public static class MathTools
    {
        /// <summary>
        /// Метода для перевода доли чего-либо в процентное представление.
        /// </summary>
        /// <param name="part"> Доля, процентаж которой ищем. </param>
        /// <param name="baseValue"> База, от которой ищем процент. </param>
        /// <returns> Процентаж доли <see cref="part"/>. </returns>
        /// <exception cref="Exception"> Исключение при делении на ноль. </exception>
        public static double ToPercentage(this double part, double baseValue) => baseValue > 0 && part >= 0 ? part / baseValue * 100 : 0;

        public static double CalcRdc(double weight, double height, int age, double sexCoeff, double activityFactor)
        {
            double rdc = (10 * weight + 6.25 * height - 5 * age + sexCoeff) * activityFactor;
            return Math.Round(rdc, 1, MidpointRounding.AwayFromZero);
        }
    }
}
