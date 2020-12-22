using System.ComponentModel.DataAnnotations;

namespace FAQ.API.Models.Dto
{

    /// <summary>
    /// QuestionTranslateDto class
    /// </summary>
    public class QuestionTranslateModelCreationDto
    {
        /// <summary>
        /// Text of the question
        /// </summary>
        public string QuestionText { get; set; }
    }
}
