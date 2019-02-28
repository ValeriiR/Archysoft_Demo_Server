


using System;
using System.Linq;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using D1.Data.Entities;
using D1.Model.Common;
using D1.Model.Users;
using Xunit;

namespace D1.UnitTests.WebApi.UserService
{
   public class GetMethodShould
    {
        public UserServiceSut UserServiceSut { get; set; }

        public GetMethodShould()
        {
            UserServiceSut=new UserServiceSut();
        }

        [Fact]
        public void ReturnsSearchResultIfBaseFilterIsEmpty()
        {         
            //Arrange      
            BaseFilter filter = new BaseFilter();
            SearchResult<UserGridModel> expectedResult=new SearchResult<UserGridModel>();

            //Action     
            var actualResult = UserServiceSut.Instance.Get(filter);
            // Assert
           // Assert.IsType<SearchResult<UserGridModel>>(actualResult);
            Assert.Equal(expectedResult.Data,actualResult.Data);
        }

        [Fact]
        public void ReturnsSearchResultIfBaseFilterIsNull()
        {
            //Arrange                   
            //Action     
            var actualResult = UserServiceSut.Instance.Get(null);
            // Assert
            Assert.IsType<SearchResult<UserGridModel>>(actualResult);
        }

        //[Fact]
        //public void ReturnsSearchResultIfBaseFilterIsNotEmpty()
        //{
        //    //Arrange   
        //    BaseFilter filter = new BaseFilter
        //    {
        //        Search = null,
        //        OrderBy = "UserName",
        //        Skip = null,
        //        Take = null
        //    };

        //    this.UserServiceSut.UserRepository.Setup(a => a.GetReadonly()).Returns(new Func<IQueryable<User>>());
        //    //Action     
        //    var actualResult = UserServiceSut.Instance.Get(null);
        //    // Assert
        //    Assert.IsType<SearchResult<UserGridModel>>(actualResult);
        //}
    }
}
