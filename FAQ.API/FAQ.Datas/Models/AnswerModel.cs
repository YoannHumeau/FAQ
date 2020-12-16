using System.ComponentModel.DataAnnotations;

namespace FAQ.Datas.Models
{
    /// <summary>
    /// Answer Class
    /// </summary>
    public class AnswerModel
    {
        /// <summary>
        /// Technical ID
        /// </summary>
        /// <example>123</example>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Code language
        /// </summary>
        /// <example>en_US</example>
        [MaxLength(5)]
        public string Language { get; set; }

        /// <summary>
        /// Text of the answer
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Id of the question model
        /// </summary>
        public int QuestionModelId { get; set; }
    }
}
