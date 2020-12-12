using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAQ.Datas.DAO
{
    public class MySuperClass
    {
        public string Title { get; set; }
    }

    internal class QuestionDAO
    {
        private readonly FAQContext _faqContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="faqContext"></param>
        internal QuestionDAO(FAQContext faqContext)
        {
            _faqContext = faqContext;
        }

        /// <summary>
        /// Get questions
        /// </summary>
        /// <returns><see cref="QuestionModel"/></returns>
        internal IEnumerable<QuestionModel> GetQuestions()
        {
            return _faqContext.Questions.AsQueryable().ToList();
        }

        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="question"><see cref="QuestionModel"/></param>
        internal void CreateQuestion(QuestionModel question)
        {
            _faqContext.Questions.Add(question);

            try
            {
                _faqContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
