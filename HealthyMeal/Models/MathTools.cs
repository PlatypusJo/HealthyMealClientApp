using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
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
        public static double ToPercentage(this double part, double baseValue) => baseValue > 0 ? part / baseValue * 100 : throw new Exception("Деление на ноль невозможно!");
    }
}
