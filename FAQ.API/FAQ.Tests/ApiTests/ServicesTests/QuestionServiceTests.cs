using System.Linq;
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

        public QuestionServiceTests()
        {

        }

        [Fact]
        public void GetQuestion_OK_English()
        {
            int questionId = 2;
            string language = "en_US";

            var mockFacade = new Mock<IFacade>();
            mockFacade.Setup(x => x.GetQuestion(language, questionId)).Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));

            var questionService = new QuestionService(mockFacade.Object);

            var result = questionService.GetQuestion(language, questionId);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_OK_French()
        {
            int questionId = 1;
            string language = "fr_FR";

            var mockFacade = new Mock<IFacade>();
            mockFacade.Setup(x => x.GetQuestion(language, questionId)).Returns(DataExamples.QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1));

            var questionService = new QuestionService(mockFacade.Object);

            var result = questionService.GetQuestion(language, questionId);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_KO_BadLanguageCodeReturnEnglish()
        {
            int questionId = 2;
            string language = "gr_GR";

            var mockFacade = new Mock<IFacade>();
            mockFacade.Setup(x => x.GetQuestion(_defaultLanguage, questionId)).Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));

            var questionService = new QuestionService(mockFacade.Object);

            var result = questionService.GetQuestion(language, questionId);

            result.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));
        }
    }
}
