using FAQ.Datas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FAQ.Tests.DataExamples
{
    public static class QuestionExamples
    {
        public static string TestString = "Helloooooo";

        public static List<List<QuestionTranslateModel>> ListQuestionsTranslatesEnglish = new List<List<QuestionTranslateModel>>
        {
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The first question" } },
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The second question" } },
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The third question" } }
        };


        /// <summary>
        /// Question list base for unit tests
        /// </summary>
        public static List<QuestionModel> QuestionsList = new List<QuestionModel>
        {
            new QuestionModel
            {
                Id = 1,
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    new QuestionTranslateModel { Language = "en_US", QuestionText = "The first question" },
                    new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La première question" }
                }
            },
            new QuestionModel
            {
                Id = 1,
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    new QuestionTranslateModel { Language = "en_US", QuestionText = "The second question" }
                }
            },
            new QuestionModel
            {
                Id = 1,
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    new QuestionTranslateModel { Language = "en_US", QuestionText = "The third question" },
                    new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La troisième question" },
                    ListQuestionsTranslatesEnglish.ElementAt(0).ElementAt(0)
                }
            }
        };

    }
}
