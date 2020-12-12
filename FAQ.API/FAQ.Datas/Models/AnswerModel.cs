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
        /// Content text of the answer
        /// </summary>
        /// <example>You can this, using like that !</example>
        public string ContentText { get; set; }

        /// <summary>
        /// Question foreign Key
        /// </summary>
        public int QuestionModelId { get; set; }

        /// <summary>
        /// Question linked to the answer
        /// </summary>
        public QuestionModel Question { get; set; } 
    }
}
