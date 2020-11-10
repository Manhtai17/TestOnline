using ApplicationCore.Entitty;
using AutoMapper;

namespace ApplicationCore.Entity
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
           
            CreateMap<Exam, ExamDTO>();
            //CreateMap<DeclarationDetail, Employee>();

        }
    }
}
