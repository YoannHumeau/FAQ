using FAQ.API.Services;
using FAQ.API.Services.Implementations;
using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FAQ.Tests.ApiTests.ServicesTests
{
    public class QuestionTranslateServiceTests
    {
        public readonly Mock<IFacade> _mockFacade;
        public readonly IQuestionTranslateService _questionTranslateService;

        public QuestionTranslateServiceTests()
        {
            _mockFacade = new Mock<IFacade>();
            _questionTranslateService = new QuestionTranslateService(_mockFacade.Object);
        }

        [Fact]
        public void UpdateQuestionTranslate_OK()
        {
            int questionId = 1;

            var newQuestionTranslate = new QuestionTranslateModel
            {
                Id = 2,
                Language = DataExamples.QuestionsDataExamples.ListQuestionsTranslatesEnglish.ElementAt(questionId-1).First().Language,
                QuestionText = DataExamples.QuestionsDataExamples.ListQuestionsTranslatesEnglish.ElementAt(questionId-1).First().QuestionText,
                QuestionModelId = questionId
            };

            _mockFacade.Setup(x => x.UpdateQuestionTranslate(newQuestionTranslate.Id, newQuestionTranslate.QuestionText))
                .Returns(newQuestionTranslate);

            var result = _questionTranslateService.UpdateQuestionTranslate(newQuestionTranslate.Id, newQuestionTranslate.QuestionText);

            result.Should().BeEquivalentTo(newQuestionTranslate);

            _mockFacade.Verify(x => x.UpdateQuestionTranslate(newQuestionTranslate.Id, newQuestionTranslate.QuestionText), Times.Once);
        }

        [Fact]
        public void UpdateQuestionTranslate_NO_ThrowException()
        {
            int questionTranslateId = 123;
            string questionTranlateText = "That doesn't matter";

            _mockFacade.Setup(x => x.UpdateQuestionTranslate(questionTranslateId, questionTranlateText)).Throws(new Exception());

            Assert.Throws<Exception>(() => _questionTranslateService.UpdateQuestionTranslate(questionTranslateId, questionTranlateText));

            _mockFacade.Verify(x => x.UpdateQuestionTranslate(questionTranslateId, questionTranlateText), Times.Once);
        }
    }
}
