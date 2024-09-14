using MagicShar.Models;
using System.Net.Http.Headers;

namespace MagicShar.Models
{
    public static class MagicBall
    {
        /// <summary>
        /// Лист с ответами на вопрос пользователя.
        /// </summary>
        static private List<string> responses;

        /// <summary>
        /// Инициализация листа ответов в конструкторе.
        /// </summary>
        static MagicBall()
        {
            responses = new List<string>()
            {
                "Да",
                "Нет",
                "Скорее всего да",
                "Скорее всего нет",
                "Возможно",
                "Имеются перспективы",
            };
        }


        /// <summary>
        /// Рандомный ответ на вопрос пользователя.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static string GetAnswer(string question)
        {
            //Проверка на пустую строку и строку заполненную пробелами.
            if (string.IsNullOrWhiteSpace(question)) 
            {
                return "Вопрос задан неверно!";
            }
            Random random = new Random();
            int index = random.Next(responses.Count);
            return responses[index];
        }
    }
}
