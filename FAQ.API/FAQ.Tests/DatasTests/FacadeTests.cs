using FAQ.Datas.Facades;
using FAQ.Datas.Facades.Implementations;
using FAQ.Datas.Models;
using FAQ.Tests.DataExamples;
using FluentAssertions;
using System.Collections.Generic;
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
        //private readonly string _dbTestsModel = "..\\FAQ.Datas\\FAQ.db";
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
        [Fact]
        public void GetAllQuestions_OK_English()
        {
            var result = _facade.GetQuestions("en_US");

            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListEnglish,
                    options => options.Excluding(q => q.QuestionTranslates)
                );
        }

        [Fact]
        public void GetAllQuestions_OK_French()
        {
            var result = _facade.GetQuestions("fr_FR");

            result.Should().BeEquivalentTo(
                QuestionsDataExamples.QuestionsListFrench,
                options => options.Excluding(o => o.QuestionTranslates)
            );
        }

        // TODO : Implement me
        //[Fact]
        //public void GetAllQuestions_KO_French_Miss_One_Translate()
        //{
        //    var result = _facade.GetQuestions("fr_FR");

        //    var listWithOneMissing = QuestionsDataExamples.QuestionsListFrench;
        //    listWithOneMissing.ElementAt(1).QuestionTranslates = QuestionsDataExamples.QuestionsListEnglish.ElementAt(1).QuestionTranslates;

        //    result.Should().BeEquivalentTo(
        //        QuestionsDataExamples.QuestionsListFrench,
        //        options => options.Excluding(o => o.QuestionTranslates)
        //    );
        //}

        [Fact]
        public void GetQuestion_OK_English()
        {
            int questionId = 2;

            var result = _facade.GetQuestion("en_US", questionId);

            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId - 1),
                    options => options.Excluding(q => q.QuestionTranslates)
                );
        }

        [Fact]
        public void GetQuestion_OK_French()
        {
            int questionId = 2;

            var result = _facade.GetQuestion("fr_FR", questionId);

            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1),
                    options => options.Excluding(q => q.QuestionTranslates)
                );
        }

        [Fact]
        public void GetQuestion_KO_QuestionNotFoundEnglish()
        {
            int questionId = 99999;

            var result = _facade.GetQuestion("en_US", questionId);

            result.Should().BeNull();
        }

        [Fact]
        public void GetQuestion_KO_QuestionNotFoundFrench()
        {
            int questionId = 99998;

            var result = _facade.GetQuestion("fr_FR", questionId);

            result.Should().BeNull();
        }

        #endregion

        #region Create/Remove questions
        [Fact]
        public void CreateQuestion_OK_All()
        {
            // Create a newquestion for insert properly
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0),
                    QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0),
                }
            };

            // Add translates to the statics questions (English)
            var questionsListEnglishAddedOne = QuestionsDataExamples.QuestionsListEnglish;
            questionsListEnglishAddedOne.Add(QuestionsDataExamples.NewQuestionEnglish);

            // Add translates to the statics questions (French)
            var questionsListFrenchAddedOne = QuestionsDataExamples.QuestionsListFrench;
            questionsListFrenchAddedOne.Add(QuestionsDataExamples.NewQuestionFrench);

            // Create the new question in DB
            _facade.CreateQuestion(newQuestion);

            // Check that is good in english
            var resultEnglish = _facade.GetQuestions("en_US");
            resultEnglish.Should().BeEquivalentTo(questionsListEnglishAddedOne,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Check that is good in french
            var facade = new Facade(_dbPath + _dbTests);
            var resultFrench = facade.GetQuestions("fr_FR");
            resultFrench.Should().BeEquivalentTo(questionsListFrenchAddedOne,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Clean the statics questions added (statics keeps in memory for other tests)
            questionsListEnglishAddedOne.Remove(questionsListEnglishAddedOne.Last());
            questionsListFrenchAddedOne.Remove(questionsListFrenchAddedOne.Last());
        }

        [Fact]
        public void CreateQuestion_KO_NoFrenchTranslate()
        {
            // Create a newquestion for insert properly
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0),
                }
            };

            // Add translates to the statics questions (English)
            var questionsListEnglishAddedOne = QuestionsDataExamples.QuestionsListEnglish;
            questionsListEnglishAddedOne.Add(QuestionsDataExamples.NewQuestionEnglish);

            // Add translates to the statics questions (French) but with in english for the last
            var questionsListFrenchAddedOneWithError = QuestionsDataExamples.QuestionsListFrench;
            questionsListFrenchAddedOneWithError.Add(QuestionsDataExamples.NewQuestionEnglish);

            // Create the new question in DB
            _facade.CreateQuestion(newQuestion);

            // Check that is good in english
            var resultEnglish = _facade.GetQuestions("en_US");
            resultEnglish.Should().BeEquivalentTo(questionsListEnglishAddedOne,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Check that is the no translate in french return english translate
            var facade = new Facade(_dbPath + _dbTests);
            var resultFrench = facade.GetQuestions("fr_FR");
            resultFrench.Should().BeEquivalentTo(questionsListFrenchAddedOneWithError,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Clean the statics questions added (statics keeps in memory for other tests)
            questionsListEnglishAddedOne.Remove(questionsListEnglishAddedOne.Last());
            questionsListFrenchAddedOneWithError.Remove(questionsListFrenchAddedOneWithError.Last());
        }

        //[Fact, Order(11)]
        //public void Remove_OK()
        //{
        //    int questionId = 2;

        //    var result = _facade.RemoveQuestion(questionId);
        //    result.Should().BeTrue();

        //    var questions = _facade.GetQuestions();
        //    questions.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListEnglishRemovedOne);
        //}

        //[Fact, Order(12)]
        //public void Remove_KO_BadQuestionId()
        //{
        //    int questionId = 999999;

        //    var result = _facade.RemoveQuestion(questionId);
        //    result.Should().BeFalse();

        //    var questions = _facade.GetQuestions();
        //    questions.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsList);
        //}
        #endregion
    }
}
