using System;
using System.Collections.Generic;
using System.Linq;
using FAQ.API.Services;
using FAQ.API.Services.Implementations;
using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using FAQ.Datas.Resources;
using FluentAssertions;
using Moq;
using Xunit;

namespace FAQ.Tests.ApiTests.ServicesTests
{
    public class QuestionServiceTests
    {
        public string _defaultLanguage = "en_US";
        public readonly Mock<IFacade> _mockFacade;
        public readonly IQuestionService _questionService;

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
            int questionCreatedId = 4;

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

            _mockFacade.Setup(x => x.CreateQuestion(newQuestion)).Returns(questionCreatedId);

            var result = _questionService.CreateQuestion(newQuestion);

            result.Should().Be(questionCreatedId);

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

            Assert.Throws<ArgumentException>(() => _questionService.CreateQuestion(newQuestion))
                .Message.Should().Be(API.Resources.En_resource.TranslateBadLanguageQuestion);

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
            newQuestion.Answers.ElementAt(0).Language = "gr_GR";

            _mockFacade.Setup(x => x.CreateQuestion(newQuestion));

            Assert.Throws<ArgumentException>(() => _questionService.CreateQuestion(newQuestion))
                .Message.Should().Be(API.Resources.En_resource.TranslateBadLanguageAnswer);

            _mockFacade.Verify(x => x.CreateQuestion(newQuestion), Times.Never);

            // Put the original value in the static dataset test
            newQuestion.Answers.ElementAt(0).Language = "en_US";
        }
        #endregion

        #region remove question

        [Fact]
        public void RemoveQuestion_OK()
        {
            int questionIdToRemove = 2;

            _mockFacade.Setup(x => x.RemoveQuestion(questionIdToRemove)).Returns(true);

            var result = _questionService.RemoveQuestion(questionIdToRemove);

            result.Should().BeTrue();

            _mockFacade.Verify(x => x.RemoveQuestion(questionIdToRemove), Times.Once);
        }

        [Fact]
        public void RemoveQuestion_NO_BadQuestionId()
        {
            int questionIdToRemove = 777;

            _mockFacade.Setup(x => x.RemoveQuestion(questionIdToRemove)).Throws(new ArgumentException(En_resources.QuestionDoesNotExists));

            Assert.Throws<ArgumentException>(() => _questionService.RemoveQuestion(questionIdToRemove))
                .Message.Should().Be(En_resources.QuestionDoesNotExists);

            _mockFacade.Verify(x => x.RemoveQuestion(questionIdToRemove), Times.Once);
        }
        #endregion
    }
}
