﻿using System.Linq;
using FAQ.API.Services.Implementations;
using FAQ.Datas.Facades;
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

        #region Get question 
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
    }
}