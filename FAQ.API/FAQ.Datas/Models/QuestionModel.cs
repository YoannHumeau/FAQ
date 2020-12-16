using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        /// <example>This is a question ?</example>
        [NotMapped]
        public string TextContent 
        {
            get
            {
                return QuestionTranslates.ElementAt(0).QuestionText;
            }
        }

        /// <summary>
        /// List used by EFcore for One-To-Many relation
        /// </summary>
        public ICollection<QuestionTranslateModel> QuestionTranslates { get; set; }

        /// <summary>
        /// Answers for the question
        /// </summary>
        /// <example><see cref="List{AnswerModel}"/></example>
        public ICollection<AnswerModel> Answers { get; set; }
    }
}
