using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    //Pagenation işlemleri için tasarlanan base bir class

    public abstract class RequestParameters //Bu bir base class olacak
    {
        const int maxPageSize = 50;

        //Auto-iplemented property
        public int PageNumber { get; set; }

        //Full-property
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

    }
}
