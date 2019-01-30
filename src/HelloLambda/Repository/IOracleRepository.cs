namespace HelloLambda.Repository
{
    public interface IOracleRepository
    {
        void UpdateRecord(int id, string message);
        void InsertRecord(string message);

    }
}
