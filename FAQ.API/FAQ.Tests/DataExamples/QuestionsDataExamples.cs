using FAQ.API.Models.Dto;
using FAQ.Datas.Models;
using System.Collections.Generic;
using System.Linq;

namespace FAQ.Tests.DataExamples
{
    public static class QuestionsDataExamples
    {
        #region Question translats 
        public static List<List<QuestionTranslateModel>> ListQuestionsTranslatesEnglish = new List<List<QuestionTranslateModel>>
        {
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The first question" } },
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The second question" } },
            new List<QuestionTranslateModel> {  new QuestionTranslateModel { Language = "en_US", QuestionText = "The third question" } }
        };

        public static List<List<QuestionTranslateModel>> ListQuestionsTranslatesFrench = new List<List<QuestionTranslateModel>>
        {
            new List<QuestionTranslateModel> { new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La première question" } },
            new List<QuestionTranslateModel> { new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La deuxième question" } },
            new List<QuestionTranslateModel> { new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La troisième question" } }
        };
        #endregion

        #region Answers
        public static List<AnswerModel> AnswersListEnglish = new List<AnswerModel>
        {
            new AnswerModel { Id = 1, Language = "en_US", Text = "The first answer", QuestionModelId = 1 },
            new AnswerModel { Id = 3, Language = "en_US", Text = "The second answer", QuestionModelId = 2 },
            new AnswerModel { Id = 5, Language = "en_US", Text = "The third answer", QuestionModelId = 3 }
        };

        public static List<AnswerModel> AnswersListFrench = new List<AnswerModel>
        {
            new AnswerModel { Id = 2, Language = "fr_FR", Text = "La première reponse", QuestionModelId = 1 },
            new AnswerModel { Id = 4, Language = "fr_FR", Text = "La deuxième réponse", QuestionModelId = 2 },
            new AnswerModel { Id = 6, Language = "fr_FR", Text = "La troisième réponse", QuestionModelId = 3 }
        };
        #endregion

        #region Questions
        public static QuestionModel NewQuestionEnglish = new QuestionModel
        {
            QuestionTranslates = new List<QuestionTranslateModel> 
            {
                new QuestionTranslateModel { Language = "en_US", QuestionText = "The fourth question" } 
            },
            Answers = new List<AnswerModel>()
        };
        
        public static QuestionModel NewQuestionFrench = new QuestionModel
        {
            QuestionTranslates = new List<QuestionTranslateModel> 
            {
                new QuestionTranslateModel { Language = "fr_FR", QuestionText = "La quatrième question" } 
            },
            Answers = new List<AnswerModel>()
        };

        public static List<QuestionModel> QuestionsListFrench = new List<QuestionModel>
        {
            new QuestionModel 
            {
                Id = 1,
                QuestionTranslates = ListQuestionsTranslatesFrench.ElementAt(0), 
                Answers = new List<AnswerModel> { AnswersListFrench.ElementAt(0) } 
            },
            new QuestionModel 
            {
                Id = 2,
                QuestionTranslates = ListQuestionsTranslatesFrench.ElementAt(1),
                Answers = new List<AnswerModel> { AnswersListFrench.ElementAt(1) }
            },
            new QuestionModel
            {
                Id = 3,
                QuestionTranslates = ListQuestionsTranslatesFrench.ElementAt(2),
                Answers = new List<AnswerModel> { AnswersListFrench.ElementAt(2) }
            }
        };

        public static List<QuestionModel> QuestionsListEnglish = new List<QuestionModel>
        {
            new QuestionModel 
            {
                Id = 1,
                QuestionTranslates = ListQuestionsTranslatesEnglish.ElementAt(0),
                Answers = new List<AnswerModel> { AnswersListEnglish.ElementAt(0) }
            },
            new QuestionModel 
            {
                Id = 2,
                QuestionTranslates = ListQuestionsTranslatesEnglish.ElementAt(1),
                Answers = new List<AnswerModel> { AnswersListEnglish.ElementAt(1) }
            },
            new QuestionModel 
            {
                Id = 3,
                QuestionTranslates = ListQuestionsTranslatesEnglish.ElementAt(2),
                Answers = new List<AnswerModel> { AnswersListEnglish.ElementAt(2) }
            }
        };
        #endregion

        #region New answer
        public static AnswerModel NewAnswerEnglish = new AnswerModel
        {
            Language = "en_US",
            Text = "The fourth answer"
        };

        public static AnswerModel NewAnswerFrench = new AnswerModel
        {
            Language = "fr_FR",
            Text = "La quatrième réponse"
        };
        #endregion

        #region Update answer
        public static AnswerModel UpdateAnswerEnglish = new AnswerModel
        {
            Language = "en_US",
            Text = "The updated second answer"
        };

        public static AnswerModel UpdateAnswerFrench = new AnswerModel
        {
            Language = "fr_FR",
            Text = "La deuxième réponse modifiée"
        };
        #endregion

        #region AnswerDto
        public static List<AnswerModelDto> AnswersDtoListEnglish = new List<AnswerModelDto>
        {
            new AnswerModelDto { Language = AnswersListEnglish.ElementAt(0).Language, Text = AnswersListEnglish.ElementAt(0).Text },
            new AnswerModelDto { Language = AnswersListEnglish.ElementAt(1).Language, Text = AnswersListEnglish.ElementAt(1).Text },
            new AnswerModelDto { Language = AnswersListEnglish.ElementAt(2).Language, Text = AnswersListEnglish.ElementAt(2).Text }
        };

        public static List<AnswerModelDto> AnswersDtoListFrench = new List<AnswerModelDto>
        {
            new AnswerModelDto { Language = AnswersListFrench.ElementAt(0).Language, Text = AnswersListFrench.ElementAt(0).Text },
            new AnswerModelDto { Language = AnswersListFrench.ElementAt(1).Language, Text = AnswersListFrench.ElementAt(1).Text },
            new AnswerModelDto { Language = AnswersListFrench.ElementAt(2).Language, Text = AnswersListFrench.ElementAt(2).Text }
        };
        #endregion

        #region QuestionDto
        public static List<QuestionModelDto> QuestionsDtoListEnglish = new List<QuestionModelDto>
        {
            new QuestionModelDto
            {
                Id = 1,
                TextContent = ListQuestionsTranslatesEnglish.ElementAt(0).First().QuestionText,
                Answers = new List<AnswerModelDto> { AnswersDtoListEnglish.ElementAt(0) }
            },
            new QuestionModelDto
            {
                Id = 2,
                TextContent = ListQuestionsTranslatesEnglish.ElementAt(1).First().QuestionText,
                Answers = new List<AnswerModelDto> { AnswersDtoListEnglish.ElementAt(1) }
            },
            new QuestionModelDto
            {
                Id = 3,
                TextContent = ListQuestionsTranslatesEnglish.ElementAt(2).First().QuestionText,
                Answers = new List<AnswerModelDto> { AnswersDtoListEnglish.ElementAt(2) }
            }
        };

        public static List<QuestionModelDto> QuestionsDtoListFrench = new List<QuestionModelDto>
        {
            new QuestionModelDto
            {
                Id = 1,
                TextContent = ListQuestionsTranslatesFrench.ElementAt(0).First().QuestionText,
                Answers = new List<AnswerModelDto> { AnswersDtoListFrench.ElementAt(0) }
            },
            new QuestionModelDto
            {
                Id = 2,
                TextContent = ListQuestionsTranslatesFrench.ElementAt(1).First().QuestionText,
                Answers = new List<AnswerModelDto> { AnswersDtoListFrench.ElementAt(1) }
            },
            new QuestionModelDto
            {
                Id = 3,
                TextContent = ListQuestionsTranslatesFrench.ElementAt(2).First().QuestionText,
                Answers = new List<AnswerModelDto> { AnswersDtoListFrench.ElementAt(2) }
            }
        };
        #endregion
    }
}
