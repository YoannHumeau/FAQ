﻿using FAQ.Datas.DAO;
using FAQ.Datas.DataAccess;
using FAQ.Datas.Models;
using System.Collections.Generic;

namespace FAQ.Datas.Facades.Implementations
{
    public class Facade : IFacade
    {
        private readonly QuestionDAO _questionDAO;

        public Facade(string connectionString)
        {
            var faqContext = new FAQContext(connectionString);

            _questionDAO = new QuestionDAO(faqContext);
        }

        public void CreateQuestion(QuestionModel question)
        {
            _questionDAO.CreateQuestion(question);

            return;
        }

        public IEnumerable<QuestionModel> GetQuestions()
        {
            var result = _questionDAO.GetQuestions();

            return result;
        }
    }
}