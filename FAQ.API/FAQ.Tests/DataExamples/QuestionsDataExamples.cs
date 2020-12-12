using FAQ.Datas.Models;
using System.Collections.Generic;
using System.Linq;

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

        public static List<QuestionModel> QuestionsListRemovedThird = new List<QuestionModel>
        {
            QuestionsList.ElementAt(0),
            QuestionsList.ElementAt(2)
        };

        public static QuestionModel NewQuestion = new QuestionModel
        {
            Content = "This is a new question"
        };

        public static List<QuestionModel> QuestionsListAddedOne = new List<QuestionModel>
        {
            QuestionsList.ElementAt(0),
            QuestionsList.ElementAt(1),
            QuestionsList.ElementAt(2),
            new QuestionModel { Id = 4, Content = NewQuestion.Content }
        };
    }
}
