using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FAQ.Datas.Models
{
    /// <summary>
    /// Questions class
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// Technical ID
        /// </summary>
        /// <example>123</example>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Content of the question
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Answer foreign Key
        /// </summary>
        public int AnswerModelId { get; set; }

        /// <summary>
        /// Answers for the question
        /// </summary>
        /// <example><see cref="List{AnswerModel}"/></example>
        public ICollection<AnswerModel> Answers { get; set; }
    }
}
