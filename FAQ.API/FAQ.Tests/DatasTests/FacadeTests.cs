using FAQ.Datas.Facades;
using FAQ.Datas.Facades.Implementations;
using FAQ.Tests.DataExamples;
using FluentAssertions;
using System.Linq;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FAQ.Tests.DatasTests
{
    /// <summary>
    /// Facade class test
    /// </summary>
    public class FacadeTests
    {
        private readonly IFacade _facade;

        private readonly string _dbPath = "..\\..\\..\\";
        private readonly string _dbTestsModel = "FAQ-Tests-Model.db";
        private readonly string _dbTests = "FAQ-Tests.db";

        /// <summary>
        /// Default constructor
        /// </summary>
        public FacadeTests()
        {
            // Copy the database prepared for the tests (ovveride an existing db test)
            System.IO.File.Copy(_dbPath + _dbTestsModel, _dbPath + _dbTests, true);

            _facade = new Facade(_dbPath + _dbTests);
        }

        [Fact, Order(1)]
        public void GetAllQuestions_OK()
        {
            var result = _facade.GetQuestions().AsQueryable().ToList();

            result.Count().Should().Be(3);
            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsList);
        }

        [Fact, Order(2)]
        public void CreateQuestion_OK()
        {
            var question = QuestionsDataExamples.NewQuestion;

            // Insert the new question
            _facade.CreateQuestion(question);
            
            // Add the question to the existant list
            var questionList = QuestionsDataExamples.QuestionsList;
            question.Id = 4;
            questionList.Add(question);

            // Test the insert
            var result = _facade.GetQuestions().AsQueryable().ToList();
            result.Should().BeEquivalentTo(questionList);
        }
    }
}
