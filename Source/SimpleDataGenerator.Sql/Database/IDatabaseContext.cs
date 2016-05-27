namespace SimpleDataGenerator.Sql.Database
{
    public interface IDatabaseContext
    {
        PetaPoco.Database GetSession();
    }
}
