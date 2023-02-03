using Microsoft.Extensions.Configuration;
using Rainbowhut1._0.Model;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using System.Drawing.Drawing2D;
using System.Drawing;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using ZXing.QrCode;
using System;
using Rainbowhut1._0.Persistances.Repository;
using Microsoft.AspNetCore.Hosting.Server;

namespace Rainbowhut1._0.Domain
{
    public class UploadDataDomain
    {
        private readonly IUploadDataRepository _uploadRepository;
        private readonly IConfiguration configuration;
        private IWebHostEnvironment _hostingEnvironment;
        public UploadDataDomain(IUploadDataRepository uploadRepository, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _uploadRepository = uploadRepository;
            this.configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<int> ProfileImageUpdate(IFormFile files)
        {
            int result = 0;
            ProfileModel fileModel = new ProfileModel();

            try
            {
                if (files.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, configuration.GetSection("Path").GetSection("profilepath").Value);
                    System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
                    foreach (FileInfo file in folderInfo.GetFiles())
                    {
                        file.Delete();
                    }
                    string Name = Guid.NewGuid().ToString() + "-" + files.FileName;
                    string fullpath = folderPath + Name;
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                    fileModel.Path = fullpath;
                    fileModel.ViewPath = configuration.GetSection("Path").GetSection("viewprofilepath").Value + Name;
                }
                result = await _uploadRepository.ProfileImageUpdate(fileModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        public async Task<int> GalleryImageAdd(IFormFile files)
        {
            int result = 0;
            GalleryModel galleryModel = new GalleryModel();
            try
            {
                if (files.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, configuration.GetSection("Path").GetSection("gallerypath").Value);
                    string Name = Guid.NewGuid().ToString() + "-" + files.FileName;
                    string fullpath = folderPath + Name;
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                    galleryModel.Path = fullpath;
                    galleryModel.ViewPath = configuration.GetSection("Path").GetSection("viewgallerypath").Value + Name;
                }
                galleryModel.GalleryType = files.Name;
                result = await _uploadRepository.GalleryImageAdd(galleryModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<int> GalleryVideoAdd(string instaurl)
        {
            int result = 0;
            GalleryModel galleryModel = new GalleryModel();
            try
            {
                string url = string.Empty;
                if (instaurl != null)
                {
                    if (instaurl.Contains("youtube.com"))
                    {
                        if (instaurl.Contains("watch"))
                        {
                            string codeurl = instaurl.Split("watch?v=")[1];
                            if(codeurl.Contains("&"))
                            {
                                codeurl=codeurl.Split("&")[0];
                            }
                            url =   codeurl + "?autoplay=1&amp;controls=1&amp;showinfo=0&amp;rel=0&amp;loop=0&amp;modestbranding=1&amp;wmode=transparent&amp;mute=1";
                        }
                        else if (instaurl.Contains("shorts"))
                        {
                            string codeurl = instaurl.Split("shorts/")[1];
                            if (codeurl.Contains("&"))
                            {
                                codeurl = codeurl.Split("&")[0];
                            }
                            url =  codeurl + "?autoplay=1&amp;controls=1&amp;showinfo=0&amp;rel=0&amp;loop=0&amp;modestbranding=1&amp;wmode=transparent&amp;mute=1";
                        }

                        galleryModel.Path = url;
                        galleryModel.ViewPath = url;
                        galleryModel.GalleryType = "Video";
                        result = await _uploadRepository.GalleryImageAdd(galleryModel);
                    }
                    else
                    {
                        result = -6000;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<int> SlideImageAdd(IFormFile files)
        {
            int result = 0;
            SlideShowModel fileModel = new SlideShowModel();
            try
            {
                if (files.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, configuration.GetSection("Path").GetSection("slidepath").Value);
                    string Name = Guid.NewGuid().ToString() + "-" + files.FileName;
                    string fullpath = folderPath + Name;
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                    fileModel.Path = fullpath;
                    fileModel.ViewPath = configuration.GetSection("Path").GetSection("viewslidepath").Value + Name;
                }
                result = await _uploadRepository.SlideImageAdd(fileModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<QrCodeViewModel> QrCodeFileAdd(IFormFile files)
        {
            string base64 = string.Empty;
            QrCodeModel fileModel = new QrCodeModel();
            QrCodeViewModel qrview = new QrCodeViewModel();
            try
            {
                int latestid = await _uploadRepository.GetLatestQrID(null);
                if (latestid > 0)
                {
                    latestid = latestid + 1;
                }
                else if (latestid == 0)
                {
                    latestid = 1;
                }
                if (files.Length > 0)
                {
                    string folderPath = Path.Combine(_hostingEnvironment.ContentRootPath, configuration.GetSection("Path").GetSection("qrpath").Value);
                    string Name = Guid.NewGuid().ToString() + "-" + files.FileName;
                    string fullpath = folderPath + Name;
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                    fileModel.Path = fullpath;
                    fileModel.ID = latestid;
                }
                Guid _key = Guid.NewGuid();
                fileModel.KeyID = _key;
                int result = await _uploadRepository.QrCodeFileAdd(fileModel);
                if (result == 1)
                {
                    string url = configuration.GetSection("QrSettings").GetSection("url").Value + "?" + "id=" + latestid + "&key=" + _key;
                    Byte[] byteArray;
                    var width = 400;
                    var height = 400;
                    var margin = 0;
                    var qrCodeWriter = new ZXing.BarcodeWriterPixelData
                    {
                        Format = ZXing.BarcodeFormat.QR_CODE,
                        Options = new QrCodeEncodingOptions
                        {
                            Height = height,
                            Width = width,
                            Margin = margin
                        }
                    };
                    var pixelData = qrCodeWriter.Write(url);

                    using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                    {
                        using (var ms = new MemoryStream())
                        {
                            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                            try
                            {
                                System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                            }
                            finally
                            {
                                bitmap.UnlockBits(bitmapData);
                            }
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byteArray = ms.ToArray();
                        }
                    }

                    //base64 = "data:image / jpeg; base64," + Convert.ToBase64String((byte[])byteArray);

                    qrview.Data = byteArray;
                    qrview.FileName = Path.GetFileNameWithoutExtension(files.FileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return qrview;

        }
        public async Task<int> GalleryImageDelete(int id)
        {
            int result = 0;
            try
            {
                var Data = await _uploadRepository.GetGalleryByIdAsync(id);
                if (Data.GalleryType != "Video")
                {
                    FileInfo file = new FileInfo(Data.Path);
                    if (file.Exists)
                    {
                        file.Delete();
                        result = await _uploadRepository.DeleteGalleryAsync(Data);
                    }
                }
                else
                {
                    result = await _uploadRepository.DeleteGalleryAsync(Data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<int> SlideImageDelete(int id)
        {
            int result = 0;
            try
            {
                var Data = await _uploadRepository.GetSlideShowByIdAsync(id);
                FileInfo file = new FileInfo(Data.Path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                    result = await _uploadRepository.DeleteSlideshowAsync(Data);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<ProfileModel> GetProfileImage()
        {
            ProfileModel result = new ProfileModel();
            try
            {
                result = await _uploadRepository.GetProfileImage(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<List<SlideShowModel>> GetSlideShowImage()
        {
            List<SlideShowModel> result = new List<SlideShowModel>();
            try
            {
                result = await _uploadRepository.GetSlideShowImage(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<List<GalleryModel>> GetGalleryImage()
        {
            List<GalleryModel> result = new List<GalleryModel>();
            try
            {
                result = await _uploadRepository.GetGalleryImage(null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<QrCodeModel> GetQrCodeFiles(int id, Guid guid)
        {
            QrCodeModel result = new QrCodeModel();
            try
            {
                result = await _uploadRepository.GetQrCodeFiles(id, guid);
                //if (result != null)
                //{
                //    string base64 = result.Data.Split("data:" + result.ContentType + "; base64,")[1];
                //    result.Data = base64;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<FileModel> GetAllImage()
        {
            FileModel result = new FileModel();
            try
            {
                result.profilemodel = await _uploadRepository.GetProfileImage(null);
                result.slideshowmodel = await _uploadRepository.GetSlideShowImage(null);
                result.gallerymodel = await _uploadRepository.GetGalleryImage(null);
                if (result.profilemodel == null && result.slideshowmodel.Count == 0 && result.gallerymodel.Count == 0)
                {
                    result = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<int> GetSlideshowCount()
        {
            int result = 0;
            try
            {
                result = await _uploadRepository.GetSlideshowCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<int> GetVideoCount()
        {
            int result = 0;
            try
            {
                result = await _uploadRepository.GetVideoCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<int> GetGalleryCount()
        {
            int result = 0;
            try
            {
                result = await _uploadRepository.GetGalleryCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
