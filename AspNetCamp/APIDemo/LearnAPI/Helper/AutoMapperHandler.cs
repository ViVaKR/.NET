using AutoMapper;
using LearnAPI.Modal;
using LearnAPI.Repos.Models;

namespace LearnAPI.Helper
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<PaymentDetail, CustomerModal>(); 
        }
    }
}
