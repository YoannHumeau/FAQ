using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FAQ.API.Models.Dto;
using System.ComponentModel.DataAnnotations;
using FAQ.API.Services;
using System.Collections.Generic;
using FAQ.Datas.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using FAQ.API.Resources;

namespace FAQ.API.Controllers
{
    /// <summary>
    /// Question controller class
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="questionService">Question service</param>
        public QuestionController(ILogger<QuestionController> logger, IMapper mapper, IQuestionService questionService)
        {
            _logger = logger;
            _mapper = mapper;
            _questionService = questionService;
        }

        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="questionDto"><see cref="QuestionModelCreationDto"/> Question to create</param>
        [HttpPost]
        [Consumes("application/json")]
        public void CreateQuestion([FromBody] QuestionModelCreationDto questionDto)
        {
            var newQuestion = new QuestionModel
            {
                QuestionTranslates = new List<QuestionTranslateModel>
                {
                    new QuestionTranslateModel
                    {
                        Language = questionDto.Language,
                        QuestionText = questionDto.TextContent,
                    }
                },
                Answers = new List<AnswerModel>()
            };

            foreach (var answer in questionDto.Answers)
            {
                newQuestion.Answers.Add(new AnswerModel
                {
                    Language = answer.Language,
                    Text = answer.Text
                });
            }

            _questionService.CreateQuestion(newQuestion);
        }

        /// <summary>
        /// Get all questions in a specific language
        /// </summary>
        /// <param name="lang">local language code (en_US)</param>
        /// <returns><see cref="IEnumerable{QuestionModelDto}"/></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QuestionModelDto>), StatusCodes.Status200OK)]
        public IActionResult GetQuestions([FromQuery] string lang)
        {
            var questions = _questionService.GetQuestions(lang);

            return Ok(_mapper.Map<IEnumerable<QuestionModelDto>>(questions));
        }

        /// <summary>
        /// Get a question in a specific language
        /// </summary>
        /// <param name="lang">local language code (en_US)</param>
        /// <param name="id">question's id</param>
        /// <returns><see cref="QuestionModelDto"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuestionModelDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetQuestion([FromQuery] string lang, [FromRoute][Required] int id)
        {
            var question = _questionService.GetQuestion(lang, id);

            if (question == null)
                return NotFound(En_resource.QuestionNotFound);

            return Ok(_mapper.Map<QuestionModelDto>(question));
        }
    }
}
