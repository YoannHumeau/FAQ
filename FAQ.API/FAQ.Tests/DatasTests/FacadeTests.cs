using FAQ.Datas.Facades;
using FAQ.Datas.Facades.Implementations;
using FAQ.Datas.Models;
using FAQ.Datas.Resources;
using FAQ.Tests.DataExamples;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace FAQ.Tests.DatasTests
{
    /// <summary>
    /// Facade class test
    /// </summary>
    public class FacadeTests
    {
        private readonly IFacade _facade;

        // Path to have the Dtabase prepared for tests
        private readonly string _dbTestsModel = Path.Combine("..", "..", "..", "FAQ-Tests-Model.db");
        private readonly string _dbTests = Path.Combine("..", "..", "..", "FAQ-Tests.db");

        /// <summary>
        /// Default constructor
        /// </summary>
        public FacadeTests()
        {
            // Copy the database prepared for the tests (ovveride an existing db test)
            System.IO.File.Copy(_dbTestsModel, _dbTests, true);

            _facade = new Facade(_dbTests);
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

        [Fact]
        public void GetAllQuestions_NO_French_Miss_One_Translate()
        {
            int questionId = 2;

            // Get the sentence in memory for recovering sentence
            var backupFrenchTranslate = QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1).QuestionTranslates.ElementAt(0).QuestionText;
            
            // Put the english translate in the french list to compare for the test
            QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1).QuestionTranslates.ElementAt(0).QuestionText =
                QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1).QuestionTranslates.ElementAt(0).QuestionText;

            // Remove a translate from database
            var resultRemove = _facade.RemoveQuestionTranslate("fr_FR", questionId);
            Assert.True(resultRemove);

            // Get all the questions
            var result = _facade.GetQuestions("fr_FR");

            // Test if the result is OK
            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListFrench,
                    options => options.Excluding(q => q.QuestionTranslates)
                );

            // Put back the original sentence in french
            QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId - 1).QuestionTranslates.ElementAt(0).QuestionText = backupFrenchTranslate;
        }

        [Fact]
        public void GetQuestion_OK_English()
        {
            int questionId = 2;

            var result = _facade.GetQuestion("en_US", questionId);

            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1),
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
        public void GetQuestion_No_NoFrenchQuestionTranslateButAnswerYes_LoadDefaultQuestionEnglish()
        {
            int questionId = 2;

            var resultRemove = _facade.RemoveQuestionTranslate("fr_FR", questionId);

            Assert.True(resultRemove);

            var result = _facade.GetQuestion("fr_FR", questionId);

            var questionEnAnswerFr = new QuestionModel
            {
                Id = questionId,
                QuestionTranslates = QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1).QuestionTranslates,
                Answers = new List<AnswerModel> { QuestionsDataExamples.AnswersListFrench.ElementAt(questionId-1) }
            };

            result.Should().BeEquivalentTo(questionEnAnswerFr,
                    options => options.Excluding(q => q.QuestionTranslates)
                );
        }

        [Fact]
        public void GetQuestion_NO_QuestionNotFoundEnglish()
        {
            int questionId = 99999;

            var result = _facade.GetQuestion("en_US", questionId);

            result.Should().BeNull();
        }

        [Fact]
        public void GetQuestion_NO_QuestionNotFoundFrench()
        {
            int questionId = 99998;

            var result = _facade.GetQuestion("fr_FR", questionId);

            result.Should().BeNull();
        }
        #endregion

        #region Create questions
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
            var questionid = _facade.CreateQuestion(newQuestion);
            
            // Check the id return
            questionid.Should().Be(4);

            // Check that is good in english
            var resultEnglish = _facade.GetQuestions("en_US");
            resultEnglish.Should().BeEquivalentTo(questionsListEnglishAddedOne,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Check that is good in french
            var facade = new Facade(_dbTests);
            var resultFrench = facade.GetQuestions("fr_FR");
            resultFrench.Should().BeEquivalentTo(questionsListFrenchAddedOne,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Clean the statics questions added (statics keeps in memory for other tests)
            questionsListEnglishAddedOne.Remove(questionsListEnglishAddedOne.Last());
            questionsListFrenchAddedOne.Remove(questionsListFrenchAddedOne.Last());
        }

        [Fact]
        public void CreateQuestion_NO_NoFrenchTranslate_LoadDefaultEnglish()
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
            var questionId = _facade.CreateQuestion(newQuestion);

            questionId.Should().Be(4);

            // Check that is good in english
            var resultEnglish = _facade.GetQuestions("en_US");
            resultEnglish.Should().BeEquivalentTo(questionsListEnglishAddedOne,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Check that is the no translate in french return english translate
            var facade = new Facade(_dbTests);
            var resultFrench = facade.GetQuestions("fr_FR");
            resultFrench.Should().BeEquivalentTo(questionsListFrenchAddedOneWithError,
                options => options.Excluding(q => q.QuestionTranslates).Excluding(q => q.Id)
                );

            // Clean the statics questions added (statics keeps in memory for other tests)
            questionsListEnglishAddedOne.Remove(questionsListEnglishAddedOne.Last());
            questionsListFrenchAddedOneWithError.Remove(questionsListFrenchAddedOneWithError.Last());
        }

        [Fact]
        public void CreateQuestion_NO_NoEnglish_Translate()
        {
            // Create a newquestion for insert not properly
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0),
                }
            };

            Assert.Throws<ArgumentException>(() => _facade.CreateQuestion(newQuestion))
                .Message.Should().Be(Datas.Resources.En_resources.Need_enUS_Language);
        }
        #endregion

        #region Remove question
        [Fact]
        public void RemoveQuestion_OK()
        {
            var questionId = _facade.GetQuestions("en_US").Last().Id;

            var result = _facade.RemoveQuestion(questionId);

            result.Should().BeTrue();

            var questions = _facade.GetQuestion("en_US", questionId);
            questions.Should().BeNull();
        }

        [Fact]
        public void RemoveQuestion_NO()
        {
            var questionId = 99999;

            var result = _facade.RemoveQuestion(questionId);

            result.Should().BeFalse();
        }
        #endregion

        #region Update question translat
        [Fact]
        public void UpdateQuestionTranslate_OK()
        {
            int questionId = 3;
            int questionTranslateId = 6;
            string questionTranslateText = "A question translate updated succesfully";

            var updatedQuestionTranslate = new QuestionTranslateModel
            {
                Id = questionTranslateId,
                Language = "en_US",
                QuestionText = questionTranslateText,
                QuestionModelId = questionId
            };

            var result = _facade.UpdateQuestionTranslate(questionTranslateId, questionTranslateText);

            result.Should().BeEquivalentTo(updatedQuestionTranslate);
        }

        [Fact]
        public void UpdateQuestiontranslate_NO_BadQuestionTranslateId()
        {
            int questionTranslateId = 777 ;
            string questionTranslateText = "That doesn't matter";

            Assert.Throws<ArgumentException>(() => _facade.UpdateQuestionTranslate(questionTranslateId, questionTranslateText))
                .Message.Should().Be(En_resources.QuestionTranslateDoesNotExists);
        }
        #endregion

        #region Remove question translate
        [Fact]
        public void RemoveQuestionTranslate_OK_French()
        {
            // Get the id of the french question translate in the last question (Linq used to check that we have a french translate)
            var questionId = _facade.GetQuestions("fr_FR").Last().Id;

            var resultRemove = _facade.RemoveQuestionTranslate("fr_FR", questionId);

            Assert.True(resultRemove);

            var resultGet = _facade.GetQuestion("fr_FR", questionId);
            Assert.True(resultGet.TextContent == QuestionsDataExamples.QuestionsListEnglish.Last().TextContent);
        }

        [Fact]
        public void RemoveQuestionTranslate_OK_English()
        {
            var questionId = _facade.GetQuestions("en_US").Last().Id;

            var resultRemove = _facade.RemoveQuestionTranslate("en_US", questionId);

            Assert.False(resultRemove);

            var resultGet = _facade.GetQuestion("en_US", questionId);
            Assert.True(resultGet.TextContent == QuestionsDataExamples.QuestionsListEnglish.Last().TextContent);
        }
        #endregion

        #region Update answers
        [Fact]
        public void UpdateAnswer_OK_English()
        {
            int questionid = 2;
            int answerUpdateId = 3;

            var updateQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionid-1).QuestionTranslates.ElementAt(0),
                },
                Answers = new List<AnswerModel>
                {
                    QuestionsDataExamples.UpdateAnswerEnglish
                }
            };

            var answerUpdate = new AnswerModel
            {
                Id = answerUpdateId,
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text
            };

            // Adapt the data test for the Db
            updateQuestion.Answers.ElementAt(0).QuestionModelId = questionid;

            var answerUpdating = _facade.UpdateAnswer(answerUpdate);

            var questionWithAnswerUpdated = _facade.GetQuestion("en_US", questionid);

            questionWithAnswerUpdated.Answers.ElementAt(0).Should().BeEquivalentTo(updateQuestion.Answers.ElementAt(0),
                options => options.Excluding(a => a.Id)
                );

            // Put the data test in original vlaue
            updateQuestion.Answers.ElementAt(0).QuestionModelId = 0;
        }

        [Fact]
        public void UpdateAnswer_OK_French()
        {
            int questionid = 2;
            int answerUpdateId = 4;

            var updateQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.QuestionsListFrench.ElementAt(questionid-1).QuestionTranslates.ElementAt(0),
                },
                Answers = new List<AnswerModel>
                {
                    QuestionsDataExamples.UpdateAnswerFrench
                }
            };

            var answerUpdate = new AnswerModel
            {
                Id = answerUpdateId,
                Text = QuestionsDataExamples.UpdateAnswerFrench.Text
            };

            // Adapt the data test for the Db
            updateQuestion.Answers.ElementAt(0).QuestionModelId = questionid;

            var answerUpdating = _facade.UpdateAnswer(answerUpdate);

            var questionWithAnswerUpdated = _facade.GetQuestion("fr_FR", questionid);

            questionWithAnswerUpdated.Answers.ElementAt(0).Should().BeEquivalentTo(updateQuestion.Answers.ElementAt(0),
                options => options.Excluding(a => a.Id)
                );

            // Put the data test in original vlaue
            updateQuestion.Answers.ElementAt(0).QuestionModelId = 0;
        }
        #endregion

        #region Create answer
        [Fact]
        public void CreateAnswer_OK_English()
        {
            int questionId = 2;
            string language = "en_US";

            // Add the new answer for the test
            QuestionsDataExamples.NewAnswerEnglish.QuestionModelId = questionId;
            QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1).Answers.Add(QuestionsDataExamples.NewAnswerEnglish);

            var newQuestionId = _facade.CreateAnswer(QuestionsDataExamples.NewAnswerEnglish);

            var facade = new Facade(_dbTests);
            var result = facade.GetQuestion(language, questionId);
            result.Should().BeEquivalentTo(QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1), 
                option => option.Excluding(q => q.QuestionTranslates)
                );

            // Clean the static data tests values
            QuestionsDataExamples.NewAnswerEnglish.QuestionModelId = 0;
            QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1).Answers
                .Remove(QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1).Answers.Last());
        }

        [Fact]
        public void CreateAnswer_OK_French()
        {
            // Create a newquestion for insert properly
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0),
                    QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0),
                },
                Answers = new List<AnswerModel>
                {
                    QuestionsDataExamples.NewAnswerEnglish
                }
            };

            // Create the new question in DB, facade used for creation (fix bug of cache)
            var facade = new Facade(_dbTests);
            facade.CreateQuestion(newQuestion);
            var newQuestionId = facade.GetQuestions("fr_FR").Last().Id;

            // Prepare the data test to check with
            newQuestion.QuestionTranslates.Remove(newQuestion.QuestionTranslates.Where(qt => qt.Language == "en_US").FirstOrDefault());
            newQuestion.Answers.Remove(newQuestion.Answers.Where(a => a.Language == "en_US").FirstOrDefault());
            QuestionsDataExamples.NewAnswerFrench.QuestionModelId = newQuestionId;

            // Insert the answer in database
            facade = new Facade(_dbTests);
            var result = facade.CreateAnswer(QuestionsDataExamples.NewAnswerFrench);

            result.Should().Be(8);

            // Check the question with answer inserted is OK
            newQuestion.Answers.Add(QuestionsDataExamples.NewAnswerFrench);
            facade = new Facade(_dbTests);
            var questionResult = facade.GetQuestion("fr_FR", newQuestionId);
            questionResult.Should().BeEquivalentTo(newQuestion);

            // Clean the static data test
            QuestionsDataExamples.NewAnswerFrench.QuestionModelId = 0;
            QuestionsDataExamples.NewAnswerFrench.Id = 0;
        }

        [Fact]
        public void CreateAnswer_NO_BadLanguageCode()
        {
            string language = "gr_GR";

            var newAnswer = new AnswerModel
            {
                Id = 2,
                Language = language,
                Text = "That doesn't matter"
            };

            Assert.Throws<ArgumentException>(() => _facade.CreateAnswer(newAnswer))
                .Message.Should().Contain(En_resources.QuestionDoesNotExists, En_resources.QuestionDoesNotExists);
        }
        #endregion
    }
}
