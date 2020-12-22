using FAQ.Datas.Models;

namespace FAQ.API.Services
{

    /// <summary>
    /// Answer Service
    /// </summary>
    public interface IAnswerService
    {
        /// <summary>
        /// Create a answer
        /// </summary>
        /// <param name="answer">Answer to insert in database</param>
        public int CreateAnswer(AnswerModel answer);

        /// <summary>
        /// Update a question
        /// </summary>
        /// <param name="answer">Answer to update</param>
        public AnswerModel UpdateAnswer(AnswerModel answer);
    }
}
