using AutoMapper;
using FAQ.API.Controllers;
using FAQ.API.Models.Dto;
using FAQ.API.Services;
using FAQ.Datas.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void CreateQuestion_OK_English()
        {
            int answerCreatedId = 4;
            string language = "en_US";

            var newAnswerCreationDto = new AnswerModelCreationDto
            {
                Language = language,
                Text = DataExamples.QuestionsDataExamples.NewAnswerEnglish.Text
            };

            _mockAnswerService.Setup(x => x.CreateAnswer(It.IsAny<AnswerModel>())).Returns(answerCreatedId);

            var result = _answerController.CreateAnswer(newAnswerCreationDto);
            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            okObjectResult.StatusCode.Should().Be(200);

            okObjectResult.Value.Should().Be(answerCreatedId);

            _mockAnswerService.Verify(x => x.CreateAnswer(It.IsAny<AnswerModel>()), Times.Once);
        }
        #endregion
    }
}
