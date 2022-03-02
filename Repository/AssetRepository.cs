using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AssetRepository: IAssetRepository
    {
        private readonly MyContext _context;
        public AssetRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<AssetCollection> GetAll()
        {
            return _context.Assets.ToList();
        }

        public void Update(AssetCollection updatedAsset)
        {
            _context.Assets.Update(updatedAsset);
            _context.SaveChanges();
        }

        public AssetCollection GetById(object id)
        {
            return _context.Assets.Find(id);
        }
    }
}
