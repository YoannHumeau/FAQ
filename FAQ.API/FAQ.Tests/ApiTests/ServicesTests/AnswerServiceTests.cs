using FAQ.API.Services;
using FAQ.API.Services.Implementations;
using FAQ.Datas.Facades;
using FAQ.Datas.Models;
using FluentAssertions;
using Moq;
using System;
using Xunit;
using FAQ.API.Resources;

namespace FAQ.Tests.ApiTests.ServicesTests
{
    public class AnswerServiceTests
    {
        public string _defaultLanguage = "en_US";
        public readonly Mock<IFacade> _mockFacade;
        public readonly IAnswerService _answerService;

        public AnswerServiceTests()
        {
            _mockFacade = new Mock<IFacade>();
            _answerService = new AnswerService(_mockFacade.Object);
        }

        #region CreateAnswer
        [Fact]
        public void CreateAnswer_OK_English()
        {
            int idNewAnswer = 8;

            var newAnswer = new AnswerModel
            {
                Language = "en_US",
                Text = "Another english answer"
            };

            _mockFacade.Setup(x => x.CreateAnswer(newAnswer)).Returns(idNewAnswer);

            var result = _answerService.CreateAnswer(newAnswer);

            result.Should().Be(idNewAnswer);

            _mockFacade.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void CreateAnswer_OK_French()
        {
            int idNewAnswer = 8;

            var newAnswer = new AnswerModel
            {
                Language = "fr_FR",
                Text = "Une nouvelle réponse française"
            };

            _mockFacade.Setup(x => x.CreateAnswer(newAnswer)).Returns(idNewAnswer);

            var result = _answerService.CreateAnswer(newAnswer);

            result.Should().Be(idNewAnswer);

            _mockFacade.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void CreateAnswer_NO_BadLanguagueCode()
        {
            var newAnswer = new AnswerModel()
            {
                Language = "gr_GR",
                Text = "That doesn't matter :)"
            };

            _mockFacade.Setup(x => x.CreateAnswer(It.IsAny<AnswerModel>()))
                .Throws(new ArgumentException(En_resource.TranslateBadLanguageAnswer));

            Assert.Throws<ArgumentException>(() => _answerService.CreateAnswer(newAnswer))
                .Message.Should().Be(En_resource.TranslateBadLanguageAnswer);

            _mockFacade.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Never);
        }
        #endregion
    }
}
    