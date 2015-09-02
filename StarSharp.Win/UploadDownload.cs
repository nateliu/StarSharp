using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarSharp.Win
{
    public partial class UploadDownload : Form
    {
        public UploadDownload()
        {
            InitializeComponent();
        }

        private const string FileManagerServiceUrl = "http://localhost/StartSharp.WCF.Host/FileManagerService.svc";
        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFileToRemoteLocation(this.tbxLocalLocation.Text.Trim(), this.tbxRemoteLocation.Text.Trim());
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownLoadFileFromRemoteLocation(this.tbxRemoteLocation.Text.Trim(), this.tbxLocalLocation.Text.Trim());
        }

        private void btnRemoteLocation_Click(object sender, EventArgs e)
        {
            this.tbxRemoteLocation.Text = GetFileName();   
        }

        private void btnLocalLocation_Click(object sender, EventArgs e)
        {
            this.tbxLocalLocation.Text = GetFileName();
        }

        private void UploadFileToRemoteLocation(string filePath, string destinationFilePath)
        {
            var serviceUrl = string.Format("{0}/UploadFile?Path={1}", FileManagerServiceUrl, destinationFilePath);
            var request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.Method = "POST";
            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (var requestStream = request.GetRequestStream())
            {
                using (var file = File.OpenRead(filePath))
                {
                    file.CopyTo(requestStream);
                }
            }

            using (var response =(HttpWebResponse) request.GetResponse())
            {
                
            }

        }

        private void DownLoadFileFromRemoteLocation(string downloadFileLocation, string downloadedFileSaveLocation)
        {
            string serviceUrl = string.Format("{0}/RetrieveFile?Path={1}", FileManagerServiceUrl, downloadFileLocation);
            var request = WebRequest.Create(serviceUrl);
            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var fileStream = response.GetResponseStream())
                    {
                        if (fileStream == null)
                        {
                            MessageBox.Show("File not recieved");
                            return;
                        }

                        CreateDirectoryForSaveLocation(downloadedFileSaveLocation);
                        SaveFile(downloadedFileSaveLocation, fileStream);
                    }
                }

                MessageBox.Show("File downloaded and copied");

            }
            catch (Exception ex)
            {
                MessageBox.Show("File could not be downloaded or saved. Message :" + ex.Message);
            }

        }

        private static void SaveFile(string downloadedFileSaveLocation, Stream fileStream)
        {
            using (var file = File.Create(downloadedFileSaveLocation))
            {
                fileStream.CopyTo(file);
            }
        }

        private void CreateDirectoryForSaveLocation(string downloadedFileSaveLocation)
        {
            var fileInfo = new FileInfo(downloadedFileSaveLocation);
            if (fileInfo.DirectoryName == null) throw new Exception("Save location directory could not be determined");
            Directory.CreateDirectory(fileInfo.DirectoryName);
        }

        private string GetFileName()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return string.Empty;
        }
    }
}
