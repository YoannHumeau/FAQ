using System;
using System.Collections.Generic;
using System.Linq;
using FAQ.API.Services.Implementations;
using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace FAQ.Tests.ApiTests.ServicesTests
{
    public class QuestionServiceTests
    {
        public string _defaultLanguage = "en_US";
        public readonly Mock<IFacade> _mockFacade;
        public readonly QuestionService _questionService;

        public QuestionServiceTests()
        {
            _mockFacade = new Mock<IFacade>();
            _questionService = new QuestionService(_mockFacade.Object);
        }

        #region Get all questions
        [Fact]
        public void GetAllQuestions_OK_English()
        {
            string language = "en_US";

            _mockFacade.Setup(x => x.GetQuestions(language)).Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish);

            var result = _questionService.GetQuestions(language);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListEnglish);
        }

        [Fact]
        public void GetAllQuestions_OK_French()
        {
            string language = "fr_FR";

            _mockFacade.Setup(x => x.GetQuestions(language)).Returns(DataExamples.QuestionsDataExamples.QuestionsListFrench);

            var result = _questionService.GetQuestions(language);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListFrench);
        }

        [Fact]
        public void GetAllQuestions_NO_BadLanguageCodeReturnEnglish()
        {
            string language = "gr_GR";

            _mockFacade.Setup(x => x.GetQuestions(_defaultLanguage)).Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish);

            var result = _questionService.GetQuestions(language);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListEnglish);
        }
        #endregion

        #region Get question
        [Fact]
        public void GetQuestion_OK_English()
        {
            int questionId = 2;
            string language = "en_US";

            _mockFacade.Setup(x => x.GetQuestion(language, questionId)).Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));

            var result = _questionService.GetQuestion(language, questionId);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_OK_French()
        {
            int questionId = 1;
            string language = "fr_FR";

            _mockFacade.Setup(x => x.GetQuestion(language, questionId)).Returns(DataExamples.QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1));

            var result = _questionService.GetQuestion(language, questionId);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_NO_BadLanguageCodeReturnEnglish()
        {
            int questionId = 2;
            string language = "gr_GR";

            _mockFacade.Setup(x => x.GetQuestion(_defaultLanguage, questionId)).Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));

            var result = _questionService.GetQuestion(language, questionId);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));
        }
        #endregion

        #region Create question
        [Fact]
        public void CreateQuestion_OK_English()
        {
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                     DataExamples.QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0)
                },
                Answers = new List<AnswerModel>
                {
                    DataExamples.QuestionsDataExamples.NewAnswerEnglish
                }
            };

            _mockFacade.Setup(x => x.CreateQuestion(newQuestion));

            _questionService.CreateQuestion(newQuestion);

            _mockFacade.Verify(x => x.CreateQuestion(newQuestion), Times.Once);
        }

        [Fact]
        public void createQuestion_OK_French()
        {
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                     DataExamples.QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0)
                },
                Answers = new List<AnswerModel>
                {
                    DataExamples.QuestionsDataExamples.NewAnswerFrench
                }
            };

            _mockFacade.Setup(x => x.CreateQuestion(newQuestion));

            _questionService.CreateQuestion(newQuestion);

            _mockFacade.Verify(x => x.CreateQuestion(newQuestion), Times.Once);
        }

        [Fact]
        public void createQuestion_NO_BadLanguageQuestion()
        {
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                     DataExamples.QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0)
                },
                Answers = new List<AnswerModel>
                {
                    DataExamples.QuestionsDataExamples.NewAnswerEnglish
                }
            };

            // Put a bad language in the static dataset test
            newQuestion.QuestionTranslates.ElementAt(0).Language = "gr_GR";

            _mockFacade.Setup(x => x.CreateQuestion(newQuestion));

            Assert.Throws<Exception>(() => _questionService.CreateQuestion(newQuestion));

            _mockFacade.Verify(x => x.CreateQuestion(newQuestion), Times.Never);

            // Put the original value in the static dataset test
            newQuestion.QuestionTranslates.ElementAt(0).Language = "en_US";
        }

        [Fact]
        public void createQuestion_NO_BadLanguageAnswer()
        {
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                     DataExamples.QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0)
                },
                Answers = new List<AnswerModel>
                {
                    DataExamples.QuestionsDataExamples.NewAnswerEnglish
                }
            };

            // Put a bad language in the static dataset test
            newQuestion.QuestionTranslates.ElementAt(0).Language = "gr_GR";

            _mockFacade.Setup(x => x.CreateQuestion(newQuestion));

            Assert.Throws<Exception>(() => _questionService.CreateQuestion(newQuestion));

            _mockFacade.Verify(x => x.CreateQuestion(newQuestion), Times.Never);

            // Put the original value in the static dataset test
            newQuestion.QuestionTranslates.ElementAt(0).Language = "en_US";
        }
        #endregion
    }
}
