using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class BannerService : IBannerServices
    {
        private GenericRepository<BannerEntity> BannerEntityGenericRepository = new GenericRepository<BannerEntity>();
        public List<BannerEntity> GetBannerList()
        {
            try
            {

                var bannerEntity = BannerEntityGenericRepository.ExecuteSQL<BannerEntity>("GetBannerList").ToList<BannerEntity>().ToList();
                return bannerEntity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //public List<BannerEntity> GetBannerList()
        //{
        //    List<BannerEntity> Banner = new List<BannerEntity>();
        //    Banner.Add(new BannerEntity() { BannerId = 1, BannerName = "MAKE A DIFFERENCE THANK" ,StatusId = 1,StatusName ="Enabled"  });
        //    Banner.Add(new BannerEntity() { BannerId = 2, BannerName = "Annodata Rewards", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 3, BannerName = "arkle finance", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 4, BannerName = "Asset Advantage", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 5, BannerName = "blizzardrewards", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 6, BannerName = "BTLB Rewards", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 7, BannerName = "clearwinnersclub", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 8, BannerName = "Cordoba Slider", StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 9, BannerName = "CVD Rewards" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 10, BannerName = "D and D Leasing Rewards",StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 11, BannerName = "Default Banner" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 12, BannerName = "Econocom Rewards" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 13, BannerName = "fr.pbmakeadifferencethankyou",StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 14, BannerName = "GO SKIPPY" ,StatusId = 1, StatusName = "Enabled" });
        //    Banner.Add(new BannerEntity() { BannerId = 15, BannerName = "Grenke Rewards" ,StatusId = 1, StatusName = "Enabled" });
        //    return Banner;
        //}

        public BannerEntity GetBannerById(int bannerId)
        {
            BannerEntity bannerEntity = new BannerEntity();
            List<BannerEntity> bannerDetail = new List<BannerEntity>();
            try
            {
                var paramBannerId = new SqlParameter
                {
                    ParameterName = "bannerId",
                    DbType = DbType.Int32,
                    Value = bannerId
                };
                bannerEntity = BannerEntityGenericRepository.ExecuteSQL<BannerEntity>("GetBannerById", paramBannerId).ToList<BannerEntity>().FirstOrDefault();
                return bannerEntity;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public List<BannerEntity> GetBannerImageById(int bannerId)
        {
            //BannerEntity bannerImageList = new BannerEntity();
            List<BannerEntity> bannerImageList = new List<BannerEntity>();
            //List<BannerEntity> bannerImageList = new List<BannerEntity>();
            try
            {
                var paramBannerId = new SqlParameter
                {
                    ParameterName = "bannerId",
                    DbType = DbType.Int32,
                    Value = bannerId
                };
                bannerImageList = BannerEntityGenericRepository.ExecuteSQL<BannerEntity>("GetBannerImageList", paramBannerId).ToList<BannerEntity>().ToList();
                return bannerImageList;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        //public bool UploadBannerImage(int banner_id , int banner_image_id , string link , int sort_order , string ImageName , int ImageKey)
        //{
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[] { 
        //            new SqlParameter("banner_id", banner_id),
        //            new SqlParameter("banner_image_id",banner_image_id),
        //            new SqlParameter("link",!string.IsNullOrWhiteSpace(link)?(object)link:(object)DBNull.Value),
        //            new SqlParameter("sort_order" ,sort_order),
        //            new SqlParameter("ImageName", !string.IsNullOrWhiteSpace(ImageName)?(object)ImageName:(object)DBNull.Value)
        //        };
        //        int result = BannerEntityGenericRepository.ExecuteSQL<int>("UploadBannerImage", sqlParameter).FirstOrDefault();
        //        if (result > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public long UploadBannerImage(int banner_id, int banner_image_id, string link, int sort_order, string ImageName, string ImageFileName, int ImageKey)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("banner_id", banner_id),
                    new SqlParameter("banner_image_id",banner_image_id),
                    new SqlParameter("link",!string.IsNullOrWhiteSpace(link)?(object)link:(object)DBNull.Value),
                    new SqlParameter("sort_order" ,sort_order),
                    new SqlParameter("ImageName", !string.IsNullOrWhiteSpace(ImageName)?(object)ImageName:(object)DBNull.Value),
                    new SqlParameter("ImageFileName",!string.IsNullOrWhiteSpace(ImageFileName)?(object)ImageFileName:(object)DBNull.Value)
                };
                int result = BannerEntityGenericRepository.ExecuteSQL<int>("UploadBannerImage", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteBannerImage(int banner_image_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("bannerImageId", banner_image_id)                                                 
                                                   
                                               };
            var result = BannerEntityGenericRepository.ExecuteSQL<int>("deleteBannerImage", sqlParameter).FirstOrDefault();
            return result;
        }

        public int InsertUpdateBanner(int banner_id, string name, int status)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("banner_id", banner_id),
                    new SqlParameter("banner_image_id",name), 
                    new SqlParameter("sort_order" ,status),
                   
                };
                var result = BannerEntityGenericRepository.ExecuteSQL<int>("InsertOrUpdateBanner", sqlParameter).FirstOrDefault();
                return result;
            }
            catch(Exception e)
            {
                return 0;
            }
        }

        public int DeleteBanner(int banner_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("banner_id", banner_id)                   
                };
                var result = BannerEntityGenericRepository.ExecuteSQL<int>("DeleteBanner", sqlParameter).FirstOrDefault();
                return result;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
        //public BannerEntity GetBannerById(int BannerId, int StoreId, int LoggedInUserId)
        //{
        //    BannerEntity bannerEntity = new BannerEntity();
        //    if (BannerId > 0)
        //    {
        //        bannerEntity = (from t in GetBannerList()
        //                        where t.BannerId == BannerId
        //                        select t).FirstOrDefault();
        //    }
        //    else
        //    {
        //        bannerEntity = new BannerEntity();
        //        bannerEntity.StatusId = 1;
        //    }
        //    return bannerEntity;
        //}
    }
}
