namespace WooneasyManagement.Application.Interfaces.Storage;

public interface IStorageService : IStorage
{
    public string StorageName { get; }
}