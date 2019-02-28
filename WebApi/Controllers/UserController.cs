
using D1.Model.Common;
using D1.Model.Services.Abstract;
using D1.Model.Users;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

     
        [HttpGet]
        [Route(Routes.Users)]
        public ApiResponse<SearchResult<UserGridModel>> GetUsers(BaseFilter filter)
        {
            SearchResult<UserGridModel> users = _userService.Get(filter);
            return new ApiResponse<SearchResult<UserGridModel>>(users);
        }
    }
}
