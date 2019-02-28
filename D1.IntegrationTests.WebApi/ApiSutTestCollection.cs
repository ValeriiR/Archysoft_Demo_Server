using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace D1.IntegrationTests.WebApi
{
    [CollectionDefinition("Web Api Test Collection")]
   public class ApiSutTestCollection:ICollectionFixture<ApiSut>
    {
    }
}
