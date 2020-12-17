using FAQ.Datas.Models;
using System.Collections.Generic;

namespace FAQ.API.Services
{
    public interface IQuestionService
    {
        /// <summary>
        /// Get all questions in a language
        /// </summary>
        /// <param name="language">Local language code ("en_US")</param>
        /// <returns>Question</returns>
        public IEnumerable<QuestionModel> GetQuestions(string language);

        /// <summary>
        /// Get a question
        /// </summary>
        /// <param name="language">Local language code ("en_US")</param>
        /// <param name="id">Question ID</param>
        /// <returns>Question</returns>
        public QuestionModel GetQuestion(string language, int id);
    }
}
