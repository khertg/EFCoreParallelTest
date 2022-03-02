using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IAssetRepository
    {
        IEnumerable<AssetCollection> GetAll();
        void Update(AssetCollection updatedAsset);
        AssetCollection GetById(object id);
    }
}
