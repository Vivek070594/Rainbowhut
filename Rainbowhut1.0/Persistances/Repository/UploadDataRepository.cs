using Microsoft.EntityFrameworkCore;
using Rainbowhut1._0.Model;
using Rainbowhut1._0.Persistances.Filters;
using Rainbowhut1._0.Persistances.Repositories.Common;
using System;
using System.Linq.Expressions;

namespace Rainbowhut1._0.Persistances.Repository
{
    public class UploadDataRepository: IUploadDataRepository
    {
        private readonly IRepository<ProfileModel> _prepository;
        private readonly IRepository<GalleryModel> _grepository;
        private readonly IRepository<SlideShowModel> _srepository;
        private readonly IRepository<QrCodeModel> _qrepository;
        private readonly IConfiguration configuration;
        public UploadDataRepository(IRepository<ProfileModel> prepository, IRepository<GalleryModel> grepository, IRepository<SlideShowModel> srepository, IRepository<QrCodeModel> qrepository, IConfiguration configuration)
        {
            _prepository = prepository;
            _grepository = grepository;
            _srepository = srepository;
            _qrepository = qrepository;
            this.configuration = configuration;
        }
        public async Task<int> ProfileImageUpdate(ProfileModel model)
        {
            string isAdd = configuration.GetSection("Value").GetSection("isAdd").Value;
            if (isAdd == "true")
            {
                _prepository.Add(model);
            }
            else
            {
                _prepository.Update(model);
            }

            int id = await _prepository.SaveAsyncWithReturn();
            return id;
        }
        public async Task<int> GalleryImageAdd(GalleryModel model)
        {
            _grepository.Add(model);
            int id = await _grepository.SaveAsyncWithReturn();
            return id;
        }
        public async Task<int> SlideImageAdd(SlideShowModel model)
        {
            _srepository.Add(model);
            int id = await _srepository.SaveAsyncWithReturn();
            return id;
        }
        public async Task<int> QrCodeFileAdd(QrCodeModel model)
        {
            _qrepository.Add(model);
            int id=await _qrepository.SaveAsyncWithReturn();
            return id;
        }
        public async Task<GalleryModel> GetGalleryByIdAsync(int Id)
        {
            var expressions = new List<Expression<Func<GalleryModel, bool>>>
            {
                x=>Id == x.ID
            };

            return await _grepository.FirstOrDefaultAsync(expressions.ToArray());
        }
        public async Task<SlideShowModel> GetSlideShowByIdAsync(int Id)
        {
            var expressions = new List<Expression<Func<SlideShowModel, bool>>>
            {
                x=>Id == x.ID
            };

            return await _srepository.FirstOrDefaultAsync(expressions.ToArray());
        }
        public async Task<int> DeleteGalleryAsync(GalleryModel model)
        {
            _grepository.Delete(model);
            int id = await _grepository.SaveAsyncWithReturn();
            return id;
        }
        public async Task<int> DeleteSlideshowAsync(SlideShowModel model)
        {
            _srepository.Delete(model);
            int id = await _srepository.SaveAsyncWithReturn();
            return id;
        }
        public async Task<ProfileModel> GetProfileImage(UploadFilter filter)
        {
            var filters = GetAllUploadProfileFilter(filter);

            var image = await _prepository.GetItemsAsync(filters.ToArray(), null, filter);
            return image.FirstOrDefault();

        }
        public async Task<List<SlideShowModel>> GetSlideShowImage(UploadFilter filter)
        {
            var filters = GetAllUploadSlideShowFilter(filter);

            var image = await _srepository.GetItemsAsync(filters.ToArray(), null, filter);
            return image;

        }
        public async Task<int> GetSlideshowCount()
        {
            int count = await _srepository.CountAsync();
            return count;
        }
        public async Task<int> GetVideoCount()
        {
            var expressions = new List<Expression<Func<GalleryModel, bool>>>
            {
                x=>"Video" == x.GalleryType,
            };
            int count = await _grepository.CountAsync(expressions.ToArray());
            return count;
        }
        public async Task<int> GetGalleryCount()
        {
            var expressions = new List<Expression<Func<GalleryModel, bool>>>
            {
                x=>"Video" != x.GalleryType,
            };
            int count = await _grepository.CountAsync(expressions.ToArray());
            return count;
        }
        public async Task<List<GalleryModel>> GetGalleryImage(UploadFilter filter)
        {
            var filters = GetAllUploadGalleryFilter(filter);

            var image = await _grepository.GetItemsAsync(filters.ToArray(), null, filter);
            return image;

        }
        public async Task<int> GetLatestQrID(UploadFilter filter)
        {
            var filters = GetAllUploadQrCodeFilter(filter);

            var image = await _qrepository.GetItemsAsync(filters.ToArray(), null, filter);
            int id = image.Select(x => x.ID).LastOrDefault();
            return id;

        }
        public async Task<QrCodeModel> GetQrCodeFiles(int id, Guid guid)
        {
            var expressions = new List<Expression<Func<QrCodeModel, bool>>>
            {
                x=>id == x.ID,
                x=>guid==x.KeyID
            };

            return await _qrepository.FirstOrDefaultAsync(expressions.ToArray());
        }

        private static List<Expression<Func<SlideShowModel, bool>>> GetAllUploadSlideShowFilter(UploadFilter filter)
        {
            var filters = new List<Expression<Func<SlideShowModel, bool>>>();
            return filters;
        }
        private static List<Expression<Func<ProfileModel, bool>>> GetAllUploadProfileFilter(UploadFilter filter)
        {
            var filters = new List<Expression<Func<ProfileModel, bool>>>();
            return filters;
        }
        private static List<Expression<Func<GalleryModel, bool>>> GetAllUploadGalleryFilter(UploadFilter filter)
        {
            var filters = new List<Expression<Func<GalleryModel, bool>>>();
            return filters;
        }
        private static List<Expression<Func<QrCodeModel, bool>>> GetAllUploadQrCodeFilter(UploadFilter filter)
        {
            var filters = new List<Expression<Func<QrCodeModel, bool>>>();
            return filters;
        }


    }
    public interface IUploadDataRepository
    {
        Task<int> ProfileImageUpdate(ProfileModel model);
        Task<int> GalleryImageAdd(GalleryModel model);

        Task<int> SlideImageAdd(SlideShowModel model);

        Task<int> QrCodeFileAdd(QrCodeModel model);

        Task<GalleryModel> GetGalleryByIdAsync(int id);
        Task<SlideShowModel> GetSlideShowByIdAsync(int id);
        Task<int> DeleteGalleryAsync(GalleryModel model);
        Task<int> DeleteSlideshowAsync(SlideShowModel model);

        Task<ProfileModel> GetProfileImage(UploadFilter filter);
        Task<List<SlideShowModel>> GetSlideShowImage(UploadFilter filter);
        Task<List<GalleryModel>> GetGalleryImage(UploadFilter filter);
        Task<int> GetLatestQrID(UploadFilter filter);
        Task<QrCodeModel> GetQrCodeFiles(int id, Guid guid);
        Task<int> GetSlideshowCount();

        Task<int> GetVideoCount();

        Task<int> GetGalleryCount();
    }
    }
