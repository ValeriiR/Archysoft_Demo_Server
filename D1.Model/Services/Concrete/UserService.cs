using AutoMapper;
using D1.Data.Repositories.Abstract;
using D1.Model.Common;
using D1.Model.Services.Abstract;
using D1.Model.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using D1.Model.Extentions;

namespace D1.Model.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public SearchResult<UserGridModel> Get(BaseFilter filter)
        {
            
            var users = _userRepository.GetReadonly().Include(x => x.Profile)
                .FilterUsers(filter).Select(x => _mapper.Map<UserGridModel>(x));

            var searchResult = users.BaseFilter(filter);
            return searchResult;
        }
    }
}
