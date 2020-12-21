using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAQ.API.Services.Implementations
{
    /// <inheritdoc/>
    public class AnswerService : IAnswerService
    {
        private readonly IFacade _facade;

        /// <summary>
        /// Default construtor
        /// </summary>
        public AnswerService(IFacade facade)
        {
            _facade = facade;
        }

        /// <inheritdoc/>
        public int CreateAnswer(AnswerModel answer)
        {
            if (!Helpers.LanguageHelper.IsLanguageOK(answer.Language))
            {
                throw new ArgumentException(Resources.En_resource.TranslateBadLanguageAnswer);
            }

            return _facade.CreateAnswer(answer);
        }
    }
}
