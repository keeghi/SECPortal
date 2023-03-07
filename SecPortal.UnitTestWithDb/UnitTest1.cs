using SecPortal.Entities.Data;

namespace SecPortal.UnitTestWithDb
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            try
            {
                using (var db = new ApplicationDbContext(DbHelper.PrepareCleanDb()))
                {
                    db.Database.EnsureCreated();

                    Assert.True(true);
                }
            }
            catch (Exception ex)
            {
                Assert.False(true);
            }
        }
    }
}