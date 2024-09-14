using MagicShar.Models;

namespace MagicSharTest
{
    public class Tests
    {
        /// <summary>
        /// Проеврка ответа на правильный запрос пользователя.
        /// </summary>
        [Test]
        public void MagicSharQuestionInValid()
        {

            var res = MagicBall.GetAnswer("Я сдам курсовой проект?");

            Assert.Contains(res, new[] {
                "Да",
                "Нет",
                "Скорее всего да",
                "Скорее всего нет",
                "Возможно",
                "Имеются перспективы",
            });
        }

        /// <summary>
        /// Проверка на пустую строку.
        /// </summary>
        [Test]
        public void MagicSharQuestionEmpty()
        {

            var res = MagicBall.GetAnswer("");

            Assert.That(res, Is.EqualTo("Вопрос задан неверно!"));
        }

        /// <summary>
        /// Проверка на null.
        /// </summary>
        [Test]
        public void MagicSharQuestionNull()
        {

            var res = MagicBall.GetAnswer(null);

            Assert.That(res, Is.EqualTo("Вопрос задан неверно!"));
        }


        /// <summary>
        /// Проверка на пробелы.
        /// </summary>
        [Test]
        public void MagicSharQuestionSpace()
        {

            var res = MagicBall.GetAnswer("         ");

            Assert.That(res, Is.EqualTo("Вопрос задан неверно!"));
        }
    }
}