using FAQ.Datas.Models;

namespace FAQ.API.Services
{
    /// <summary>
    /// QuestionTranslate class
    /// </summary>
    public interface IQuestionTranslateService
    {
        /// <summary>
        /// Update a question translate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="questionTranslateText"></param>
        /// <returns>QuestionTranslateModel updated</returns>
        public QuestionTranslateModel UpdateQuestionTranslate(int id, string questionTranslateText);
    }
}
