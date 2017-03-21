namespace Kandanda.Dal.DataTransferObjects
{
    public interface IEntry
    {
        int Id { get; set; }
        byte[] RowVersion { get; set; }
    }
}
