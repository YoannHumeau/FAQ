using AutoMapper;
using FAQ.Datas.Models;


namespace FAQ.API.Models.Dto
{
    /// <summary>
    /// AutoMapping class
    /// </summary>
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Default contructor, all mapping needed here
        /// </summary>
        public AutoMapping()
        {
            // Tip : CreateMap < TSource, TDestination >

            CreateMap<AnswerModel, AnswerModelDto>();
            CreateMap<AnswerModelCreationDto, AnswerModel>();

            CreateMap<QuestionModel, QuestionModelDto>();
        }
    }
}
