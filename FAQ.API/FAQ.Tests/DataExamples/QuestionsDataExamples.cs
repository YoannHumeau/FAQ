using FAQ.Datas.Models;
using System.Collections.Generic;
using System.Linq;

namespace FAQ.Tests.DataExamples
{
    public static class QuestionsDataExamples
    {


        public static List<List<QuestionTranslateModel>> ListQuestionsTranslatesEnglish = new List<List<QuestionTranslateModel>>
        {
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The first question" } },
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The second question" } },
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The third question" } }
        };

        public static List<string> ListQuestionsTranslatesEnglishString = new List<string>
        {
            "The first question",
            "The second question",
            "The third question"
        };

        public static List<List<QuestionTranslateModel>> ListQuestionsTranslatesFrench = new List<List<QuestionTranslateModel>>
        {
            new List<QuestionTranslateModel> { new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La première question" } },
            new List<QuestionTranslateModel> { new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La deuxième question" } },
            new List<QuestionTranslateModel> { new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La troisième question" } }
        };

        public static List<string> ListQuestionsTranslatesFrenchString = new List<string>
        {
            "La première question",
            "La deuxième question",
            "La troisième question"
        };

        public static QuestionModel NewQuestionEnglish = new QuestionModel
        {
            QuestionTranslates = new List<QuestionTranslateModel> 
            {
                new QuestionTranslateModel { Language = "en_US", QuestionText = "The fourth question" } 
            }         
        };
        
        public static QuestionModel NewQuestionFrench = new QuestionModel
        {
            QuestionTranslates = new List<QuestionTranslateModel> 
            {
                new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La quatrième question" } 
            }         
        };

        public static List<QuestionModel> QuestionsListFrench = new List<QuestionModel>
        {
            new QuestionModel { Id = 1, QuestionTranslates = ListQuestionsTranslatesFrench.ElementAt(0) },
            new QuestionModel { Id = 2, QuestionTranslates = ListQuestionsTranslatesFrench.ElementAt(1) },
            new QuestionModel { Id = 3, QuestionTranslates = ListQuestionsTranslatesFrench.ElementAt(2) }
        };

        public static List<QuestionModel> QuestionsListEnglish = new List<QuestionModel>
        {
            new QuestionModel { Id = 1, QuestionTranslates = ListQuestionsTranslatesEnglish.ElementAt(0) },
            new QuestionModel { Id = 2, QuestionTranslates = ListQuestionsTranslatesEnglish.ElementAt(1) },
            new QuestionModel { Id = 3, QuestionTranslates = ListQuestionsTranslatesEnglish.ElementAt(2) }
        };

        //public static List<QuestionModel> QuestionsListEnglish = new List<QuestionModel>
        //{
        //    new QuestionModel { Id = 1, TextContent = ListQuestionsTranslatesEnglishString.ElementAt(0) },
        //    new QuestionModel { Id = 2, TextContent = ListQuestionsTranslatesEnglishString.ElementAt(1) },
        //    new QuestionModel { Id = 3, TextContent = ListQuestionsTranslatesEnglishString.ElementAt(2) }
        //};

        //public static List<QuestionModel> QuestionsListEnglishAddedOne = new List<QuestionModel>
        //{
        //    QuestionsListEnglish.ElementAt(0),
        //    QuestionsListEnglish.ElementAt(1),
        //    QuestionsListEnglish.ElementAt(2),
        //    NewQuestionEnglish
        //};

        //public static List<QuestionModel> QuestionsListFrenchhAddedOne = new List<QuestionModel>
        //{
        //    QuestionsListEnglish.ElementAt(0),
        //    QuestionsListEnglish.ElementAt(1),
        //    QuestionsListEnglish.ElementAt(2),
        //    NewQuestionEnglish
        //};

        //public static List<QuestionModel> QuestionsListEnglishRemovedOne = new List<QuestionModel>
        //{
        //    QuestionsListEnglish.ElementAt(0),
        //    QuestionsListEnglish.ElementAt(2)
        //};

        public static List<QuestionModel> QuestionsListFrenchRemovedOne = new List<QuestionModel>
        {
            QuestionsListFrench.ElementAt(0),
            QuestionsListFrench.ElementAt(2)
        };

        //public static List<QuestionModel> QuestionsList = new List<QuestionModel>
        //{
        //    new QuestionModel
        //    {
        //        Id = 1,
        //        QuestionTranslates = new List<QuestionTranslateModel>
        //        {
        //            new QuestionTranslateModel { Language = "en_US", QuestionText = "The first question" },
        //            new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La première question" }
        //        }
        //    },
        //    new QuestionModel
        //    {
        //        Id = 1,
        //        QuestionTranslates = new List<QuestionTranslateModel>
        //        {
        //            new QuestionTranslateModel { Language = "en_US", QuestionText = "The second question" }
        //        }
        //    },
        //    new QuestionModel
        //    {
        //        Id = 1,
        //        QuestionTranslates = new List<QuestionTranslateModel>
        //        {
        //            new QuestionTranslateModel { Language = "en_US", QuestionText = "The third question" },
        //            new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La troisième question" }
        //        }
        //    }
        //};
    }
}
