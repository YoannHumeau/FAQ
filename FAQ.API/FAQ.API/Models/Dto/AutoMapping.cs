using AutoMapper;
using FAQ.Datas.Models;


namespace FAQ.API.Models.Dto
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // Tip : CreateMap < TSource, TDestination >

            CreateMap<AnswerModel, AnswerModelDto>();
            CreateMap<QuestionModel, QuestionModelDto>();
        }
    }
}
