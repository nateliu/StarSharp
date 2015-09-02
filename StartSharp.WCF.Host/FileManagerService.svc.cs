using System;
using System.IO;
using System.ServiceModel.Web;

namespace StartSharp.WCF.Host
{
    //http://www.c-sharpcorner.com/UploadFile/6a5c37/downloading-and-uploading-file-using-wcf-rest-services-with/
    public class FileManagerService : IFileManagerService
    {
        public Stream RetrieveFile(string path)
        {
            if (WebOperationContext.Current == null) throw new Exception("WebOperationContext not set");

            // As the current service is being used by a windows client, there is no browser interactivity.  
            // In case you are using the code Web, please use the appropriate content type.  
            var fileName = Path.GetFileName(path);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
            WebOperationContext.Current.OutgoingResponse.Headers.Add("content-disposition", "inline; filename=" + fileName);

            return File.OpenRead(path);  
        }

        public void UploadFile(string path, System.IO.Stream stream)
        {
            CreateDirectoryIfNotExists(path);
            using (var file = File.Create(path))
            {
                stream.CopyTo(file);
            }
        }

        private void CreateDirectoryIfNotExists(string filePath)
        {
            var directory = new FileInfo(filePath).Directory;
            if (directory == null) throw new Exception("Directory could not be determined for the filePath");

            Directory.CreateDirectory(directory.FullName);
        }


        //public UploadFileResponse UploadFile(UploadFileRequest input)
        //{
        //    CreateDirectoryIfNotExists(input.fileName);
        //    using (var file = File.Create(input.fileName))
        //    {
        //        input.content.CopyTo(file);
        //    }
        //    return null;
        //}
    }
}
