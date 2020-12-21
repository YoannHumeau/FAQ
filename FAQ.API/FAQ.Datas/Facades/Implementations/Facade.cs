using FAQ.Datas.DAO;
using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using FAQ.Datas.Resources;
using System;
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
        private readonly AnswerDAO _answerDAO;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public Facade(string connectionString)
        {
            var faqContext = new FAQContext(connectionString);

            _questionDAO = new QuestionDAO(faqContext);
            _answerDAO = new AnswerDAO(faqContext);
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
                {
                    result.ElementAt(question.Id-1).QuestionTranslates = _questionDAO.GetQuestion("en_US", question.Id).QuestionTranslates;

                    // In case of double language, clear the default language answer
                    if (result.ElementAt(question.Id - 1).Answers.Where(qt => qt.Language == "en_US").Any())
                        result.ElementAt(question.Id-1).Answers.Remove(result.ElementAt(question.Id-1).Answers.Where(qt => qt.Language == "en_US").ElementAt(0));
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public QuestionModel GetQuestion(string language, int id)
        {
            var result = _questionDAO.GetQuestion(language, id);

            if (result == null)
                return null;

            if (result?.QuestionTranslates.Any() == false)
                result = _questionDAO.GetQuestion("en_US", id);

            // In case of double language, clear the default language answer
            if (result.Answers.Count > 1)
                result.Answers.Remove(result.Answers.Where(a => a.Language == language).FirstOrDefault());

            return result;
        }

        /// <inheritdoc/>
        public int CreateQuestion(QuestionModel question)
        {
            // Return an error code
            if (question.QuestionTranslates.Where(qt => qt.Language == "en_US").Any() == false)
            {
                throw new ArgumentException(Resources.En_resources.Need_enUS_Language);
            }

            return _questionDAO.CreateQuestion(question);
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

        #region Answers
        /// <inheritdoc/>
        public int CreateAnswer(AnswerModel answer)
        {
            if (_questionDAO.GetQuestion("en_US", answer.QuestionModelId) == null)
            {
                throw new ArgumentException(En_resources.QuestionDoesNotExists, En_resources.QuestionDoesNotExists);
            }

            return _answerDAO.CreateAnswer(answer);
        }

        /// <inheritdoc/>
        public AnswerModel UpdateAnswer(AnswerModel answer)
        {
            var answerDb = _answerDAO.FindAnswer(answer);

            if (answerDb == null)
            {
                throw new Exception(Resources.En_resources.AnswerDoesNotExists);
            }
            else
            {
                answerDb.Text = answer.Text;
                _answerDAO.UpdateAnswer(answerDb);
                return _answerDAO.FindAnswer(answerDb);
            }
        }

        #endregion
    }
}
