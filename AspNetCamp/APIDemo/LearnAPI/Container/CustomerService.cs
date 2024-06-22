using AutoMapper;
using LearnAPI.Modal;
using LearnAPI.Repos;
using LearnAPI.Repos.Models;
using LearnAPI.Service;

namespace LearnAPI.Container
{
    public class CustomerService : ICustomerService
    {
        private readonly LearnDataContext _context;
        private readonly IMapper _mapper;

        public CustomerService(LearnDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<CustomerModal> GetAll()
        {
            List<CustomerModal> _res = new List<CustomerModal>();
            var data = _context.PaymentDetails.ToList();

            if (data != null)
            {
                _res = _mapper.Map<List<PaymentDetail>, List<CustomerModal>>(data);
            }

            return _res;
        }
    }
}
