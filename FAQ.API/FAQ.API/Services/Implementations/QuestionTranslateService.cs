using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAQ.API.Services.Implementations
{
    /// <inheritdoc/>
    public class QuestionTranslateService : IQuestionTranslateService
    {

        private readonly IFacade _facade;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="facade"></param>
        public QuestionTranslateService(IFacade facade)
        {
            _facade = facade;
        }

        /// <inheritdoc/>
        public QuestionTranslateModel UpdateQuestionTranslate(int id, string questionTranslateText)
        {
            return _facade.UpdateQuestionTranslate(id, questionTranslateText);
        }
    }
}
