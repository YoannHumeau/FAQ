using FAQ.Datas.Models;

namespace FAQ.API.Services
{
    public interface IQuestionService
    {
        /// <summary>
        /// Get a question
        /// </summary>
        /// <param name="language">Local language code ("en_US")</param>
        /// <param name="id">Question ID</param>
        /// <returns>Question</returns>
        public QuestionModel GetQuestion(string language, int id);
    }
}
