using FAQ.Datas.DAO;
using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using System.Collections.Generic;
using System.Linq;

namespace FAQ.Datas.Facades.Implementations
{
    /// <summary>
    /// Facade class
    /// </summary>
    public class Facade : IFacade
    {
        private readonly QuestionDAO _questionDAO;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public Facade(string connectionString)
        {
            var faqContext = new FAQContext(connectionString);

            _questionDAO = new QuestionDAO(faqContext);
        }

        #region Questions
        /// <inheritdoc/>
        public IEnumerable<QuestionModel> GetQuestions(string language)
        {
            var result = _questionDAO.GetQuestions(language);

            // Check we have any question with no translatation in the language, if not => load default language
            foreach (var question in result)
            {
                if (question?.QuestionTranslates.Any() == false)
                    result.ElementAt(question.Id-1).QuestionTranslates = _questionDAO.GetQuestion("en_US", question.Id).QuestionTranslates;
            }

            return result;
        }

        /// <inheritdoc/>
        public QuestionModel GetQuestion(string language, int id)
        {
            var result = _questionDAO.GetQuestion(language, id);

            if (result?.QuestionTranslates.Any() == false)
                result = _questionDAO.GetQuestion("en_US", id);

            return result;
        }

        /// <inheritdoc/>
        public void CreateQuestion(QuestionModel question)
        {
            if (question.QuestionTranslates.Where(qt => qt.Language == "en_US").Any() == false)
            {
                throw new System.Exception(Resources.En_resources.Need_enUS_Language);
            }

            _questionDAO.CreateQuestion(question);
        }

        /// <inheritdoc/>
        public bool RemoveQuestion(int id)
        {
            var question = _questionDAO.GetQuestion("en_US", id);

            if (question == null)
            { 
                return false;
            }

            try
            {
                _questionDAO.RemoveQuestion(question);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region QuestionTranslate
        /// <inheritdoc/>
        public bool RemoveQuestionTranslate(string language, int questionParentId)
        {
            var questionTranslate = _questionDAO.GetQuestionTranslate(language, questionParentId);

            if (questionTranslate == null || questionTranslate.Language == "en_US")
            {
                return false;
            }

            try
            {
                _questionDAO.RemoveQuestionTranslate(questionTranslate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
