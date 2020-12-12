using FAQ.Datas.Models;
using System.Collections.Generic;

namespace FAQ.Datas.Facades
{
    /// <summary>
    /// Interface facade class
    /// </summary>
    public interface IFacade
    {
        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="question"><see cref="QuestionModel"/></param>
        public void CreateQuestion(QuestionModel question);

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns><see cref="List{QuestionModel}"/></returns>
        public IEnumerable<QuestionModel> GetQuestions();
    }
}
