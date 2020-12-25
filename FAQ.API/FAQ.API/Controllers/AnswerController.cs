using AutoMapper;
using FAQ.API.Models.Dto;
using FAQ.API.Services;
using FAQ.Datas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FAQ.API.Controllers
{
    /// <summary>
    /// Question controller class
    /// </summary>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly IMapper _mapper;
        private readonly IAnswerService _answerService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="answerService">Answer service</param>
        public AnswerController(ILogger<AnswerController> logger, IMapper mapper, IAnswerService answerService)
        {
            _logger = logger;
            _mapper = mapper;
            _answerService = answerService;
        }

        /// <summary>
        /// Create an answer
        /// </summary>
        /// <param name="answerDto"><see cref="AnswerModelCreationDto"/> Answer to create</param>
        /// <returns>Id of the new answer</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAnswer([FromBody] AnswerModelCreationDto answerDto)
        {
            var newAnswer = _mapper.Map<AnswerModel>(answerDto);

            try
            {
                var result = _answerService.CreateAnswer(newAnswer);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e?.Message);
            }
            catch (Exception e)
            {
                // Return 500 because of another unknow error
                _logger.LogError(
                    $"Creating question Error with question : [{newAnswer}]" +
                    $"ExceptionMessage: {e?.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an answer
        /// </summary>
        /// <param name="answerDto"><see cref="AnswerModelUpdateDto"/> Answwer to update</param>
        /// <param name="id">Id of the answer</param>
        /// <returns><see cref="AnswerModelDto"/> Answer updated</returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAnswer([FromBody] AnswerModelUpdateDto answerDto, int id)
        {
            var updateAnswer = new AnswerModel
            {
                Id = id,
                Text = answerDto.Text
            };

            try
            {
                var result = _answerService.UpdateAnswer(updateAnswer);
                return Ok(result);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e?.Message);
            }
            catch(Exception e)
            {
                // Return 500 because of another unknow error
                _logger.LogError(
                    $"Creating question Error with update question : [{updateAnswer}] on Id : [{id}]" +
                    $"ExceptionMessage: {e?.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Remove a question
        /// </summary>
        /// <param name="id">Id of the answer</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveAnswer(int id)
        {
            try
            {
                _answerService.RemoveAnswer(id);
                return Ok("");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
