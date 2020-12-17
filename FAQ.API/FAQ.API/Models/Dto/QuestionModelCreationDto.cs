using System.Collections.Generic;

namespace FAQ.API.Models.Dto
{
    public class QuestionModelCreationDto
    {
        /// <summary>
        /// Language of the question
        /// </summary>
        /// <example>en_US</example>
        public string Language { get; set; }

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
