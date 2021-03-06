using AgriProductTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business.Interfaces
{
    public interface ICoreDataService
    {
        List<DropDownViewModel> GetProductCategories();
        List<DropDownViewModel> GetDeliveryServices();
        List<DropDownViewModel> GetPaymentType();
    }
}
