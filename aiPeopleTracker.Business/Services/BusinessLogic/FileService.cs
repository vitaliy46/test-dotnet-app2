using System;
using System.IO;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Services.Crud;

namespace aiPeopleTracker.Business.Services.BusinessLogic
{
    public class FileService : ServiceBase, IFileService
    {
        public byte[] ReadFile(string uri)
        {
            byte[] buff = null;

            FileStream fs = new FileStream(uri, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            long numBytes = new FileInfo(uri).Length;

            buff = br.ReadBytes((int)numBytes);

            return buff;
        }

        public void SaveFile(byte[] data)
        {
            var fileNameToSave = Guid.NewGuid() + ".png";

            File.WriteAllBytes(fileNameToSave, data);
        }

       
    }
}
