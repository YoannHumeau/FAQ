using FAQ.Datas.DAO;
using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using System.Collections.Generic;

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
        public void CreateQuestion(QuestionModel question)
        {
            _questionDAO.CreateQuestion(question);

            return;
        }

        /// <inheritdoc/>
        public IEnumerable<QuestionModel> GetQuestions()
        {
            var result = _questionDAO.GetQuestions();

            return result;
        }
    }
}
