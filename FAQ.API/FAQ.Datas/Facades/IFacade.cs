using FAQ.Datas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQ.Datas.Facades
{
    public interface IFacade
    {
        public void CreateQuestion(QuestionModel question);

        public IEnumerable<QuestionModel> GetQuestions();
    }
}
