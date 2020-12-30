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
        public int CreateQuestion(QuestionModel question);

        /// <summary>
        /// Update a question translate
        /// </summary>
        /// <param name="id">Id of the question translate to update</param>
        /// <param name="questionTranslateText">Text of the question translate to update</param>
        /// <returns>Question translate updated</returns>
        public QuestionTranslateModel UpdateQuestionTranslate(int id, string questionTranslateText);

        /// <summary>
        /// Remove a question
        /// </summary>
        /// <param name="id">Id of the question</param>
        public bool RemoveQuestion(int id);

        /// <summary>
        /// Remove a question translate
        /// </summary>
        /// <param name="question"></param>
        public bool RemoveQuestionTranslate(string language, int questionParentId);

        /// <summary>
        /// Create an answer
        /// </summary>
        /// <param name="answer">Answer to create</param>
        public int CreateAnswer(AnswerModel answer);

        /// <summary>
        /// Update an answer
        /// </summary>
        /// <param name="answer">Answer to update</param>
        /// <returns>Answer updated</returns>
        public AnswerModel UpdateAnswer(AnswerModel answer);

        /// <summary>
        /// Remove an answer
        /// </summary>
        /// <param name="answer">Answer to remove</param>
        /// <returns>Boolean from result removing</returns>
        public bool RemoveAnswer(int answerId);
    }
}
