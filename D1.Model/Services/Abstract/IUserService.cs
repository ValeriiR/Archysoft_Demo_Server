using System.Collections.Generic;
using D1.Model.Common;
using D1.Model.Users;


namespace D1.Model.Services.Abstract
{
    public interface IUserService
    {
       SearchResult<UserGridModel> Get(BaseFilter filter);
    }
}
