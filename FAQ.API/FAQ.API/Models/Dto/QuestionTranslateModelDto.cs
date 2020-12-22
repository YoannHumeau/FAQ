using System.ComponentModel.DataAnnotations;

namespace FAQ.API.Models.Dto
{

    /// <summary>
    /// QuestionTranslateDto class
    /// </summary>
    public class QuestionTranslateModelDto
    {
        /// <summary>
        /// Code language
        /// </summary>
        /// <example>en_US</example>
        [MaxLength(5)]
        public string Language { get; set; }

        /// <summary>
        /// Text of the question
        /// </summary>
        public string QuestionText { get; set; }
    }
}
