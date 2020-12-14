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
        /// Get all questions
        /// </summary>
        /// <returns><see cref="List{QuestionModel}"/></returns>
        public IEnumerable<QuestionModel> GetQuestions(string language);

        /// <summary>
        /// Get a specific question by the id
        /// </summary>
        /// <param name="id">Id of the question</param>
        /// <returns></returns>
        public QuestionModel GetQuestion(string language, int id);

        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="question"><see cref="QuestionModel"/></param>
        public void CreateQuestion(QuestionModel question);

        /// <summary>
        /// Remove a question
        /// </summary>
        /// <param name="question"></param>
        public bool RemoveQuestion(int id);

        /// <summary>
        /// Remove a question translate
        /// </summary>
        /// <param name="question"></param>
        public bool RemoveQuestionTranslate(int id);
    }
}
