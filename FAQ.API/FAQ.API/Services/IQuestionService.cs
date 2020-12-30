using FAQ.Datas.Models;
using System.Collections.Generic;

namespace FAQ.API.Services
{
    /// <summary>
    /// Question Service
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="question">Question to insert in database</param>
        public int CreateQuestion(QuestionModel question);

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

        /// <summary>
        /// Remove a question
        /// </summary>
        /// <param name="id">Id of the question to remove</param>
        /// <returns>Boolean if removed true</returns>
        public bool RemoveQuestion(int id);
    }
}
