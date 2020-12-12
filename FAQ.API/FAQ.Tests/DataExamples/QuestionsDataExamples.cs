using FAQ.Datas.Models;
using System.Collections.Generic;

namespace FAQ.Tests.DataExamples
{
    public static class QuestionsDataExamples
    {
        public static List<QuestionModel> QuestionsList = new List<QuestionModel>
        {
            new QuestionModel { Id = 1, Content = "The first question" },
            new QuestionModel { Id = 2, Content = "The second question" },
            new QuestionModel { Id = 3, Content = "The third question" }
        };
    }
}
