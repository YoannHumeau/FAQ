using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using System;
using System.Linq;

namespace FAQ.Datas.DAO
{
    internal class AnswerDAO
    {
        private readonly FAQContext _faqContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="faqContext"></param>
        internal AnswerDAO(FAQContext faqContext)
        {
            _faqContext = faqContext;
        }

        internal void UpdateAnswer(AnswerModel answer)
        {
            _faqContext.Answers.Update(answer);

            try
            {
                _faqContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal AnswerModel FindAnswer(AnswerModel answer)
        {
            return _faqContext.Answers
                .Where(a => a.Language == answer.Language && a.QuestionModelId == answer.QuestionModelId)
                .FirstOrDefault();
        }

        internal AnswerModel GetAnswer(int id)
        {
            return _faqContext.Answers.Find(id);
        }

        //internal IEnumerable<AnswerModel> GetAnswers(string language)
        //{
        //    var result = _faqContext.Answers
        //        .Where(a => (!String.IsNullOrWhiteSpace(language) ? a.Language == language : true ))
        //        .ToList();

        //    return result;
        //}
    }
}
