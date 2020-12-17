using AutoMapper;
using FAQ.API.Controllers;
using FAQ.API.Models.Dto;
using FAQ.API.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FAQ.Tests.ApiTests.ControllersTests
{
    public class QuestionControllerTests
    {
        public readonly QuestionController _questionController;

        public readonly Mock<IQuestionService> _mockFacade;
        private readonly Mock<ILogger<QuestionController>> _mockLogger;
        private readonly IMapper _mapper;

        public QuestionControllerTests()
        {
            _mockLogger = new Mock<ILogger<QuestionController>>();
            _mockFacade = new Mock<IQuestionService>();

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapping()));
            _mapper = mapperConfig.CreateMapper();

            _questionController = new QuestionController(_mockLogger.Object, _mapper, _mockFacade.Object);
        }

        [Fact]
        public void GetQuestions_OK_English()
        {
            string language = "en_US";

            _mockFacade.Setup(x => x.GetQuestions(language))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish);

            var result = _questionController.GetQuestions(language);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListEnglish);
        }

        [Fact]
        public void GetQuestions_OK_French()
        {
            string language = "fr_FR";

            _mockFacade.Setup(x => x.GetQuestions(language))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListFrench);

            var result = _questionController.GetQuestions(language);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListFrench);
        }

        [Fact]
        public void GetQuestions_NO_BadLanguageCodeReturnEnglish()
        {
            string language = "gr_GR";

            _mockFacade.Setup(x => x.GetQuestions(language))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish);

            var result = _questionController.GetQuestions(language);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListEnglish);
        }
    }
}
