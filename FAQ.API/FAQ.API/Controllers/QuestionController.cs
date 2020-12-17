using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FAQ.API.Models.Dto;
using System.ComponentModel.DataAnnotations;
using FAQ.API.Services;
using System.Collections.Generic;

namespace FAQ.API.Controllers
{
    [ApiController]
    [Route("question")]
    public class QuestionController
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;

        public QuestionController(ILogger<QuestionController> logger, IMapper mapper, IQuestionService questionService)
        {
            _logger = logger;
            _mapper = mapper;
            _questionService = questionService;
        }

        [HttpGet]
        public IEnumerable<QuestionModelDto> GetQuestions([FromQuery] string lang)
        {
            var questions = _questionService.GetQuestions(lang);

            return _mapper.Map<IEnumerable<QuestionModelDto>>(questions);
        }

        [HttpGet("{id}")]
        public QuestionModelDto GetQuestion([FromQuery] string lang, [FromRoute][Required] int id)
        {
            var question = _questionService.GetQuestion(lang, id);

            return _mapper.Map<QuestionModelDto>(question);
        }
    }
}
