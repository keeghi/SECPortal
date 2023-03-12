using SecPortal.Entities.Data;
using SecPortal.UnitTestWithDb.Fixtures;

namespace SecPortal.UnitTestWithDb
{
    public class UnitTest1 : IClassFixture <DatabaseFixture>
    {
        DatabaseFixture _fixture;
        public UnitTest1(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Test1()
        {
            try
            {   
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(true);
            }
        }
    }
}