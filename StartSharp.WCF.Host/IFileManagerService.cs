using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace StartSharp.WCF.Host
{
    [ServiceContract]
    public interface IFileManagerService
    {
        [OperationContract]
        [WebGet(UriTemplate = "RetrieveFile?Path={path}")]
        Stream RetrieveFile(string path);

        [OperationContract]
        [WebInvoke(UriTemplate = "UploadFile?Path={path}")]
        void UploadFile(string path, Stream stream);  
        //UploadFileResponse UploadFile(UploadFileRequest input);
    }

    [MessageContract]
    public class UploadFileRequest
    {
        [MessageHeader]
        public string fileName;
        [MessageBodyMember]
        public Stream content;
    }
    [MessageContract]
    public class UploadFileResponse
    {
        [MessageBodyMember]
        public string UploadFileResult;
    }
}
