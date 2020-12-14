using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAQ.Datas.Models
{
    /// <summary>
    /// Content text of question class
    /// </summary>
    public class QuestionTranslateModel
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
        public string QuestionText { get; set; }

        /// <summary>
        /// Id of the question model
        /// </summary>
        public int QuestionModelId { get; set; }
    }
}
