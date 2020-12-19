using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using System;
using System.Collections.Generic;

namespace FAQ.API.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IFacade _facade;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="facade"></param>
        public QuestionService(IFacade facade)
        {
            _facade = facade;
        }

        ///<inheritdoc/>
        public int CreateQuestion(QuestionModel question)
        {
            foreach (var questionTranslate in question.QuestionTranslates)
            {
                if (!Helpers.LanguageHelper.IsLanguageOK(questionTranslate.Language))
                {
                    throw new Exception(Resources.En_resource.TranslateBadLanguageQuestion);
                }
            }

            foreach (var answer in question.Answers)
            {
                if (!Helpers.LanguageHelper.IsLanguageOK(answer.Language))
                {
                    throw new Exception(Resources.En_resource.TranslateBadLanguageAnswer);
                }
            }

            return _facade.CreateQuestion(question);
        }

        ///<inheritdoc/>
        public IEnumerable<QuestionModel> GetQuestions(string language)
        {
            Helpers.LanguageHelper.CheckLanguage(ref language);

            return _facade.GetQuestions(language);
        }

        ///<inheritdoc/>
        public QuestionModel GetQuestion(string language, int id)
        {
            Helpers.LanguageHelper.CheckLanguage(ref language);

            return _facade.GetQuestion(language, id);
        }
    }
}
