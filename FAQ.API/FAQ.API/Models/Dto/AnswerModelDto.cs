namespace FAQ.API.Models.Dto
{
    public class AnswerModelDto
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
    }
}
