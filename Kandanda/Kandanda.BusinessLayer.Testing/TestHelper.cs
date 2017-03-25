using Kandanda.Dal;

namespace Kandanda.BusinessLayer.Testing
{
    public static class TestHelper
    {
        public static void ResetDatabase()
        {
            using (KandandaDbContext db = new KandandaDbContext())
            {
                db.Database.Initialize(true);
            }
        }
    }
}
