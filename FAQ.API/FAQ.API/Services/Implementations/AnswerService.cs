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
        public int CreateAnswer(AnswerModel question)
        {
            return _facade.CreateAnswer(question);
        }
    }
}
