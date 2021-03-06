﻿using AutoMapper;
using FAQ.API.Controllers;
using FAQ.API.Models.Dto;
using FAQ.API.Services;
using FAQ.Datas.Models;
using FAQ.Datas.Resources;
using FAQ.Tests.DataExamples;
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
    public class AnswerControllerTests
    {
        public readonly AnswerController _answerController;

        public readonly Mock<IAnswerService> _mockAnswerService;
        private readonly Mock<ILogger<AnswerController>> _mockLogger;
        private readonly IMapper _mapper;

        public AnswerControllerTests()
        {
            _mockLogger = new Mock<ILogger<AnswerController>>();
            _mockAnswerService = new Mock<IAnswerService>();

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapping()));
            _mapper = mapperConfig.CreateMapper();

            _answerController = new AnswerController(_mockLogger.Object, _mapper, _mockAnswerService.Object);
        }

        #region Create answer
        [Fact]
        public void CreateAnswer_OK_English()
        {
            int answerCreatedId = 4;
            string language = "en_US";

            var newAnswerCreationDto = new AnswerModelCreationDto
            {
                Language = language,
                Text = QuestionsDataExamples.NewAnswerEnglish.Text
            };

            _mockAnswerService.Setup(x => x.CreateAnswer(It.IsAny<AnswerModel>())).Returns(answerCreatedId);

            var result = _answerController.CreateAnswer(newAnswerCreationDto);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().Be(answerCreatedId);

            _mockAnswerService.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void CreateAnswer_OK_french()
        {
            int answerCreatedId = 4;
            string language = "fr_FR";

            var newAnswerCreationDto = new AnswerModelCreationDto
            {
                Language = language,
                Text = DataExamples.QuestionsDataExamples.NewAnswerFrench.Text
            };

            _mockAnswerService.Setup(x => x.CreateAnswer(It.IsAny<AnswerModel>())).Returns(answerCreatedId);

            var result = _answerController.CreateAnswer(newAnswerCreationDto);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().Be(answerCreatedId);

            _mockAnswerService.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void CreateAnswer_NO_BadLanguageCode()
        {
            string badLanguage = "gr_GR";

            var newAnswerCreationDto = new AnswerModelCreationDto
            {
                Language = badLanguage,
                Text = QuestionsDataExamples.NewAnswerFrench.Text
            };

            _mockAnswerService.Setup(x => x.CreateAnswer(It.IsAny<AnswerModel>())).Throws(new ArgumentException(Datas.Resources.En_resources.Need_enUS_Language));

            var result = _answerController.CreateAnswer(newAnswerCreationDto);
            var okObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(400);

            okObjectResult.Value.Should().Be(Datas.Resources.En_resources.Need_enUS_Language);

            _mockAnswerService.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void CreateAnswer_NO_WithError500Returned()
        {
            string language = "en_US";

            var newAnswerCreationDto = new AnswerModelCreationDto
            {
                Language = language,
                Text = QuestionsDataExamples.NewAnswerFrench.Text
            };

            _mockAnswerService.Setup(x => x.CreateAnswer(It.IsAny<AnswerModel>())).Throws(new Exception());

            var result = _answerController.CreateAnswer(newAnswerCreationDto);
            var statusCodeResult = result as StatusCodeResult;

            statusCodeResult.StatusCode.Should().Be(500);

            _mockAnswerService.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }
        #endregion

        #region Update answer
        [Fact]
        public void UpdateAnswer_OK_English()
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
                    QuestionsDataExamples.UpdateAnswerEnglish
                }
            };

            var answerUpdate = new AnswerModelUpdateDto
            {
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text
            };

            var answerReturned = new AnswerModel
            {
                Id = answerUpdateId,
                Language = QuestionsDataExamples.UpdateAnswerEnglish.Language,
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text,
                QuestionModelId = questionid
            };

            _mockAnswerService.Setup(x => x.UpdateAnswer(It.IsAny<AnswerModel>())).Returns(answerReturned);

            var result = _answerController.UpdateAnswer(answerUpdate, answerUpdateId);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().Be(answerReturned);

            _mockAnswerService.Verify(x => x.UpdateAnswer(It.IsAny<AnswerModel>()), Times.Once);
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

            var answerUpdate = new AnswerModelUpdateDto
            {
                Text = QuestionsDataExamples.UpdateAnswerFrench.Text
            };

            var answerReturned = new AnswerModel
            {
                Id = answerUpdateId,
                Language = QuestionsDataExamples.UpdateAnswerFrench.Language,
                Text = QuestionsDataExamples.UpdateAnswerFrench.Text,
                QuestionModelId = questionid
            };

            _mockAnswerService.Setup(x => x.UpdateAnswer(It.IsAny<AnswerModel>())).Returns(answerReturned);

            var result = _answerController.UpdateAnswer(answerUpdate, answerUpdateId);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().Be(answerReturned);

            _mockAnswerService.Verify(x => x.UpdateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void UpdateAnswer_NO_BadAnswerId()
        {
            int questionid = 2;
            int answerUpdateId = 999;

            var updateQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.QuestionsListFrench.ElementAt(questionid-1).QuestionTranslates.ElementAt(0),
                },
                Answers = new List<AnswerModel>
                {
                    QuestionsDataExamples.UpdateAnswerEnglish
                }
            };

            var answerUpdate = new AnswerModelUpdateDto
            {
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text
            };

            var answerReturned = new AnswerModel
            {
                Id = answerUpdateId,
                Language = QuestionsDataExamples.UpdateAnswerEnglish.Language,
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text,
                QuestionModelId = questionid
            };

            _mockAnswerService.Setup(x => x.UpdateAnswer(It.IsAny<AnswerModel>())).Throws(new ArgumentException(En_resources.AnswerDoesNotExists));

            var result = _answerController.UpdateAnswer(answerUpdate, answerUpdateId);
            var objectResult = result as BadRequestObjectResult;

            Assert.NotNull(objectResult);
            objectResult.StatusCode.Should().Be(400);

            objectResult.Value.Should().Be(En_resources.AnswerDoesNotExists);

            _mockAnswerService.Verify(x => x.UpdateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }

        [Fact]
        public void UpdateAnswer_NO_WithError500Returned()
        {
            int questionid = 2;
            int answerUpdateId = 999;

            var updateQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    QuestionsDataExamples.QuestionsListFrench.ElementAt(questionid-1).QuestionTranslates.ElementAt(0),
                },
                Answers = new List<AnswerModel>
                {
                    QuestionsDataExamples.UpdateAnswerEnglish
                }
            };

            var answerUpdate = new AnswerModelUpdateDto
            {
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text
            };

            var answerReturned = new AnswerModel
            {
                Id = answerUpdateId,
                Language = QuestionsDataExamples.UpdateAnswerEnglish.Language,
                Text = QuestionsDataExamples.UpdateAnswerEnglish.Text,
                QuestionModelId = questionid
            };

            _mockAnswerService.Setup(x => x.UpdateAnswer(It.IsAny<AnswerModel>())).Throws(new Exception());

            var result = _answerController.UpdateAnswer(answerUpdate, answerUpdateId);
            var statusCodeResult = result as StatusCodeResult;

            statusCodeResult.StatusCode.Should().Be(500);

            _mockAnswerService.Verify(x => x.UpdateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }
        #endregion

        #region Remove answer
        [Fact]
        public void RemoveAnswer_OK()
        {
            int answerId = 1;

            _mockAnswerService.Setup(x => x.RemoveAnswer(answerId)).Returns(true);

            var result = _answerController.RemoveAnswer(answerId);
            var okObjectresult = result as OkObjectResult;

            Assert.NotNull(okObjectresult);

            okObjectresult.StatusCode.Should().Be(200);

            _mockAnswerService.Verify((x => x.RemoveAnswer(answerId)), Times.Once);
        }

        [Fact]
        public void RemoveAnswer_NO_BadId()
        {
            int answerId = 777;

            _mockAnswerService.Setup(x => x.RemoveAnswer(answerId))
                .Throws(new ArgumentException(En_resources.AnswerDoesNotExists));
            
            var result = _answerController.RemoveAnswer(answerId);
            var badObjectResult = result as BadRequestObjectResult;

            Assert.NotNull(badObjectResult);

            badObjectResult.StatusCode.Should().Be(400);
            badObjectResult.Value.Should().Be(En_resources.AnswerDoesNotExists);

            _mockAnswerService.Verify(x => x.RemoveAnswer(answerId), Times.Once);
        }

        [Fact]
        public void RemoveAnswer_NO_WithError500Returned()
        {
            int answerId = 777;

            _mockAnswerService.Setup(x => x.RemoveAnswer(answerId)).Throws(new Exception());

            var result = _answerController.RemoveAnswer(answerId);
            var statusCodeResult = result as StatusCodeResult;

            statusCodeResult.StatusCode.Should().Be(500);

            _mockAnswerService.Verify(x => x.RemoveAnswer(answerId), Times.Once);
        }
        #endregion
    }
}
