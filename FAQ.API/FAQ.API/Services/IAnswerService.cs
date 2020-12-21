using FAQ.Datas.Models;

namespace FAQ.API.Services
{

    /// <summary>
    /// Answer Service
    /// </summary>
    public interface IAnswerService
    {
        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="question">Answer to insert in database</param>
        public int CreateAnswer(AnswerModel question);
    }
}
