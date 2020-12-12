using FAQ.Datas.Facades;
using FAQ.Datas.Facades.Implementations;
using FAQ.Datas.Models;
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

        #region Get questions
        [Fact, Order(1)]
        public void GetAllQuestions_OK()
        {
            var result = _facade.GetQuestions().AsQueryable().ToList();

            result.Count().Should().Be(3);
            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsList);
        }

        [Fact, Order(2)]
        public void GetQuestion_OK()
        {
            int questionId = 2;

            var result = _facade.GetQuestion(questionId);

            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsList.ElementAt(1));
        }

        [Fact, Order(3)]
        public void GetQuestion_KO_QuestionNotFound()
        {
            int questionId = 99999;

            var result = _facade.GetQuestion(questionId);

            result.Should().BeNull();
        }
        #endregion

        #region Create/Remove questions
        [Fact, Order(10)]
        public void CreateQuestion_OK()
        {
            var question = QuestionsDataExamples.NewQuestion;

            _facade.CreateQuestion(question);
  
            var result = _facade.GetQuestions().AsQueryable().ToList();
            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListAddedOne);
        }

        [Fact, Order(11)]
        public void Remove_OK()
        {
            int questionId = 2;

            var result = _facade.RemoveQuestion(questionId);
            result.Should().BeTrue();

            var questions = _facade.GetQuestions();
            questions.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListRemovedThird);
        }

        [Fact, Order(12)]
        public void Remove_KO_BadQuestionId()
        {
            int questionId = 999999;

            var result = _facade.RemoveQuestion(questionId);
            result.Should().BeFalse();

            var questions = _facade.GetQuestions();
            questions.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsList);
        }
        #endregion
    }
}
