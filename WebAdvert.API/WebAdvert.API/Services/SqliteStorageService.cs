using AdvertAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.API.Services
{
    public class SqliteStorageService : IAdvertStorageService
    {
        public readonly IMapper _mapper;

        public SqliteStorageService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDBModel>(model);
            dbModel.Id = Guid.NewGuid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.Pending;

            
            using (var db = new SQLiteDBContext())
            {
                db.AdvertDBModel.Add(dbModel);
                db.SaveChanges();
            }
            
            return dbModel.Id;
        }

        public Task Confirm(ConfirmAdvertModel model)
        {
            throw new NotImplementedException();
        }

        public List<AdvertModel> Read(string Id)
        {
            using (var db = new SQLiteDBContext())
            {
                return (from a in db.AdvertDBModel where (string.IsNullOrEmpty(Id) || a.Id == Id) select new AdvertModel {Title = a.Title, Description = a.Description, Price = a.Price }).ToList();
            }
        }
    }
}
