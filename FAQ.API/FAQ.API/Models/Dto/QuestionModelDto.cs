using System.Collections.Generic;

namespace FAQ.API.Models.Dto
{
    /// <summary>
    /// QuestionModelDto class
    /// </summary>
    public class QuestionModelDto
    {
        /// <summary>
        /// Technical ID
        /// </summary>
        /// <example>123</example>
        public int Id { get; set; }

        /// <summary>
        /// Content of the question
        /// </summary>
        /// <example>This is a question ?</example>
        public string TextContent { get; set; }

        /// <summary>
        /// Answers for the question
        /// </summary>
        /// <example><see cref="List{AnswerModel}"/></example>
        public ICollection<AnswerModelDto> Answers { get; set; }
    }
}
