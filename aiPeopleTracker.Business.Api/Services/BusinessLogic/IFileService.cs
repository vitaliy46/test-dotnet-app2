namespace aiPeopleTracker.Business.Api.Services.BusinessLogic
{
    public interface IFileService
    {
        byte[] ReadFile(string uri);
        void SaveFile(byte[] data);
    }
}