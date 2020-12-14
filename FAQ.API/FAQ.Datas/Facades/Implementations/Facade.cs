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
            _questionDAO.CreateQuestion(question);
        }

        /// <inheritdoc/>
        public bool RemoveQuestion(int id)
        {
            try
            {
                _questionDAO.RemoveQuestion(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public bool RemoveQuestionTranslate(int id)
        {
            try
            {
                _questionDAO.RemoveQuestionTranslate(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
