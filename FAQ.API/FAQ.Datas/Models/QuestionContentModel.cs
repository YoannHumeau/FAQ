using System.ComponentModel.DataAnnotations;

namespace FAQ.Datas.Models
{
    /// <summary>
    /// Content text of question class
    /// </summary>
    public class QuestionContentModel
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
        /// Text of the question
        /// </summary>
        public string Text { get; set; }
    }
}
