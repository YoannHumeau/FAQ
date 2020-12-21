using System.Collections.Generic;

namespace FAQ.API.Models.Dto
{
    /// <summary>
    /// AnswerModelCreationDto class
    /// </summary>
    public class AnswerModelCreationDto
    {
        /// <summary>
        /// Code language
        /// </summary>
        /// <example>en_US</example>
        public string Language { get; set; }

        /// <summary>
        /// Text of the answer
        /// </summary>
        /// <example>Text of the answer</example>
        public string Text { get; set; }

        /// <summary>
        /// Id of the question model
        /// </summary>
        /// <example>123</example>
        public int QuestionModelId { get; set; }
    }
}
