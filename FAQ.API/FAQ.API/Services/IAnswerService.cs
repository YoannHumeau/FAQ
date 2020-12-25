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

        /// <summary>
        /// Remove an answer
        /// </summary>
        /// <param name="answerId">Id of the answer</param>
        /// <returns>Boolean that say removing is OK and a string message</returns>
        public (bool, string) RemoveAnswer(int id);
    }
}
