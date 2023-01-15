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

namespace Rainbowhut1._0.Domain
{
    public class UploadDataDomain
    {
        private readonly IUploadDataRepository _uploadRepository;
        private readonly IConfiguration configuration;
        public UploadDataDomain(IUploadDataRepository uploadRepository, IConfiguration configuration)
        {
            _uploadRepository = uploadRepository;
            this.configuration = configuration;
        }
        public async Task ProfileImageUpdate(IFormFile files)
        {
            ProfileModel fileModel = new ProfileModel();
           
            try
            {
                if (files.Length > 0)
                {
                    fileModel.ContentType = files.ContentType;
                    using (var ms = new MemoryStream())
                    {
                        files.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64 = Convert.ToBase64String((byte[])fileBytes);
                        fileModel.Data = "data:" + files.ContentType + "; base64," + base64;
                    }
                }
                  await _uploadRepository.ProfileImageUpdate(fileModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public async Task GalleryImageAdd(IFormFile files)
        {
            
            GalleryModel galleryModel = new GalleryModel();
            try
            {
                if (files.Length > 0)
                {
                    galleryModel.ContentType = files.ContentType;
                    using (var ms = new MemoryStream())
                    {
                        files.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64 = Convert.ToBase64String((byte[])fileBytes);
                        galleryModel.Data = "data:" + files.ContentType + "; base64," + base64;
                    }
                }
                galleryModel.GalleryType = files.Name;
                await _uploadRepository.GalleryImageAdd(galleryModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task SlideImageAdd(IFormFile files)
        {
           
            SlideShowModel fileModel = new SlideShowModel();
            try
            {
                if (files.Length > 0)
                {
                    fileModel.ContentType = files.ContentType;
                    using (var ms = new MemoryStream())
                    {
                        files.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64 = Convert.ToBase64String((byte[])fileBytes);
                        fileModel.Data = "data:" + files.ContentType + "; base64," + base64;

                    }
                }
                 await _uploadRepository.SlideImageAdd(fileModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
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
                    fileModel.ContentType = files.ContentType;
                    fileModel.FileName = files.FileName;
                    using (var ms = new MemoryStream())
                    {
                        files.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64pdf = Convert.ToBase64String((byte[])fileBytes);
                        fileModel.Data = "data:" + files.ContentType + "; base64," + base64pdf;
                    }
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

                    base64 = "data:image / jpeg; base64," + Convert.ToBase64String((byte[])byteArray);

                    qrview.Data = base64;
                    qrview.FileName = Path.GetFileNameWithoutExtension(files.FileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return qrview;

        }
        public async Task GalleryImageDelete(int id)
        {
           
            try
            {
                var Data = await _uploadRepository.GetGalleryByIdAsync(id);
                await _uploadRepository.DeleteGalleryAsync(Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task SlideImageDelete(int id)
        {
            
            try
            {
                var Data = await _uploadRepository.GetSlideShowByIdAsync(id);
                await _uploadRepository.DeleteSlideshowAsync(Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
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
                if (result != null)
                {
                    string base64 = result.Data.Split("data:" + result.ContentType + "; base64,")[1];
                    result.Data = base64;
                }
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
