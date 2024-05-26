using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MapService:GenericSvc<MapRepository ,Location>
    {
        private MapRepository _mapRepository; 
        public MapService() {
            _mapRepository = new MapRepository();
        }
        public IEnumerable<Location> GetAll()
        {
            return _mapRepository.All;
        }
    }
}
