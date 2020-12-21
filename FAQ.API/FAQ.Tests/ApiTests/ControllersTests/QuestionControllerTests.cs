﻿using AutoMapper;
using FAQ.API.Controllers;
using FAQ.API.Models.Dto;
using FAQ.API.Resources;
using FAQ.API.Services;
using FAQ.Datas.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FAQ.Tests.ApiTests.ControllersTests
{
    public class QuestionControllerTests
    {
        public readonly QuestionController _questionController;

        public readonly Mock<IQuestionService> _mockQuestionService;
        private readonly Mock<ILogger<QuestionController>> _mockLogger;
        private readonly IMapper _mapper;

        public QuestionControllerTests()
        {
            _mockLogger = new Mock<ILogger<QuestionController>>();
            _mockQuestionService = new Mock<IQuestionService>();

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapping()));
            _mapper = mapperConfig.CreateMapper();

            _questionController = new QuestionController(_mockLogger.Object, _mapper, _mockQuestionService.Object);
        }

        #region Get all questions
        [Fact]
        public void GetQuestions_OK_English()
        {
            string language = "en_US";

            _mockQuestionService.Setup(x => x.GetQuestions(language))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish);

            var result = _questionController.GetQuestions(language);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListEnglish);
        }

        [Fact]
        public void GetQuestions_OK_French()
        {
            string language = "fr_FR";

            _mockQuestionService.Setup(x => x.GetQuestions(language))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListFrench);

            var result = _questionController.GetQuestions(language);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListFrench);
        }

        [Fact]
        public void GetQuestions_NO_BadLanguageCodeReturnEnglish()
        {
            string language = "gr_GR";

            _mockQuestionService.Setup(x => x.GetQuestions(language))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish);

            var result = _questionController.GetQuestions(language);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListEnglish);
        }
        #endregion

        #region Get question
        [Fact]
        public void GetQuestion_OK_English()
        {
            int questionId = 2;
            string language = "en_US";

            _mockQuestionService.Setup(x => x.GetQuestion(language, questionId))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));

            var result = _questionController.GetQuestion(language, questionId);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListEnglish.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_OK_French()
        {
            int questionId = 2;
            string language = "fr_FR";

            _mockQuestionService.Setup(x => x.GetQuestion(language, questionId))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListFrench.ElementAt(questionId-1));

            var result = _questionController.GetQuestion(language, questionId);
            var okObjectResult = result as OkObjectResult;
            
            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListFrench.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_NO_BadLanguageCodeReturnEnglish()
        {
            int questionId = 2;
            string language = "gr_GR";

            _mockQuestionService.Setup(x => x.GetQuestion(language, questionId))
                .Returns(DataExamples.QuestionsDataExamples.QuestionsListEnglish.ElementAt(questionId-1));

            var result = _questionController.GetQuestion(language, questionId);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().BeEquivalentTo(DataExamples.QuestionsDataExamples.QuestionsDtoListEnglish.ElementAt(questionId-1));
        }

        [Fact]
        public void GetQuestion_NO_BadQuestionId_English()
        {
            int questionId = 999;
            string language = "en_US";

            _mockQuestionService.Setup(x => x.GetQuestion(language, questionId)).Returns(() => null);

            var result = _questionController.GetQuestion(language, questionId);
            var okObjectResult = result as NotFoundObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(404);

            okObjectResult.Value.Should().Be(En_resource.QuestionNotFound);
        }

        [Fact]
        public void GetQuestion_NO_BadQuestionId_French()
        {
            int questionId = 999;
            string language = "en_US";

            _mockQuestionService.Setup(x => x.GetQuestion(language, questionId)).Returns(() => null);

            var result = _questionController.GetQuestion(language, questionId);
            var okObjectResult = result as NotFoundObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(404);

            okObjectResult.Value.Should().Be(En_resource.QuestionNotFound);
        }

        [Fact]
        public void GetQuestion_NO_BadQuestionId_BadLanguageCode()
        {
            int questionId = 999;
            string language = "gr_GR";

            _mockQuestionService.Setup(x => x.GetQuestion(language, questionId)).Returns(() => null);

            var result = _questionController.GetQuestion(language, questionId);
            var okObjectResult = result as NotFoundObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(404);

            okObjectResult.Value.Should().Be(En_resource.QuestionNotFound);
        }

        #endregion

        #region Create question
        [Fact]
        public void CreateQuestion_OK_English()
        {
            int questionCreatedId = 4;
            string language = "en_US";

            var newQuestionCreationDto = new QuestionModelCreationDto
            {
                Language = language,
                TextContent = DataExamples.QuestionsDataExamples.NewQuestionEnglish.QuestionTranslates.ElementAt(0).QuestionText,
                Answers = new List<AnswerModelDto>
                {
                    new AnswerModelDto
                    {
                        Language = language,
                        Text = DataExamples.QuestionsDataExamples.NewAnswerEnglish.Text
                    }
                }
            };

            _mockQuestionService.Setup(x => x.CreateQuestion(It.IsAny<QuestionModel>())).Returns(questionCreatedId);

            var result =_questionController.CreateQuestion(newQuestionCreationDto);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().Be(questionCreatedId);

            _mockQuestionService.Verify(x => x.CreateQuestion(It.IsAny<QuestionModel>()), Times.Once);
        }

        [Fact]
        public void CreateQuestion_NO_French()
        {
            string language = "fr_FR";

            var newQuestionCreationDto = new QuestionModelCreationDto
            {
                Language = language,
                TextContent = DataExamples.QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0).QuestionText,
                Answers = new List<AnswerModelDto>
                {
                    new AnswerModelDto
                    {
                        Language = language,
                        Text = DataExamples.QuestionsDataExamples.NewAnswerFrench.Text
                    }
                }
            };

            _mockQuestionService.Setup(x => x.CreateQuestion(It.IsAny<QuestionModel>())).Throws(new ArgumentException(Datas.Resources.En_resources.Need_enUS_Language));

            var result = _questionController.CreateQuestion(newQuestionCreationDto);
            var okObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(400);

            okObjectResult.Value.Should().Be(Datas.Resources.En_resources.Need_enUS_Language);

            _mockQuestionService.Verify(x => x.CreateQuestion(It.IsAny<QuestionModel>()), Times.Once);
        }

        [Fact]
        public void CreateQuestion_NO_BadLanguageCodeQuestion()
        {
            string language = "en_US";
            string badLanguage = "gr_GR";

            var newQuestionCreationDto = new QuestionModelCreationDto
            {
                Language = badLanguage,
                TextContent = DataExamples.QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0).QuestionText,
                Answers = new List<AnswerModelDto>
                {
                    new AnswerModelDto
                    {
                        Language = language,
                        Text = DataExamples.QuestionsDataExamples.NewAnswerFrench.Text
                    }
                }
            };

            _mockQuestionService.Setup(x => x.CreateQuestion(It.IsAny<QuestionModel>())).Throws(new ArgumentException(Datas.Resources.En_resources.Need_enUS_Language));

            var result = _questionController.CreateQuestion(newQuestionCreationDto);
            var okObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(400);

            okObjectResult.Value.Should().Be(Datas.Resources.En_resources.Need_enUS_Language);

            _mockQuestionService.Verify(x => x.CreateQuestion(It.IsAny<QuestionModel>()), Times.Once);
        }
        
        [Fact]
        public void CreateQuestion_NO_BadLanguageCodeQuestionAndAnswer()
        {
            string badLanguage = "gr_GR";

            var newQuestionCreationDto = new QuestionModelCreationDto
            {
                Language = badLanguage,
                TextContent = DataExamples.QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0).QuestionText,
                Answers = new List<AnswerModelDto>
                {
                    new AnswerModelDto
                    {
                        Language = badLanguage,
                        Text = DataExamples.QuestionsDataExamples.NewAnswerFrench.Text
                    }
                }
            };

            _mockQuestionService.Setup(x => x.CreateQuestion(It.IsAny<QuestionModel>())).Throws(new ArgumentException(Datas.Resources.En_resources.Need_enUS_Language));

            var result = _questionController.CreateQuestion(newQuestionCreationDto);
            var okObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(400);

            okObjectResult.Value.Should().Be(Datas.Resources.En_resources.Need_enUS_Language);

            _mockQuestionService.Verify(x => x.CreateQuestion(It.IsAny<QuestionModel>()), Times.Once);
        }

        [Fact]
        public void CreateQuestion_NO_WithError500Returned()
        {
            string language = "en_US";

            var newQuestionCreationDto = new QuestionModelCreationDto
            {
                Language = language,
                TextContent = DataExamples.QuestionsDataExamples.NewQuestionFrench.QuestionTranslates.ElementAt(0).QuestionText,
                Answers = new List<AnswerModelDto>
                {
                    new AnswerModelDto
                    {
                        Language = language,
                        Text = DataExamples.QuestionsDataExamples.NewAnswerFrench.Text
                    }
                }
            };

            _mockQuestionService.Setup(x => x.CreateQuestion(It.IsAny<QuestionModel>())).Throws(new Exception());

            var result = _questionController.CreateQuestion(newQuestionCreationDto);
            var statusCodeResult = result as StatusCodeResult;

            statusCodeResult.StatusCode.Should().Be(500);
        }
        #endregion
    }
}
