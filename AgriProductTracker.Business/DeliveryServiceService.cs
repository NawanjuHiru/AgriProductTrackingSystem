using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.DeliveryService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public  class DeliveryServiceService : IDeliveryServiceService
    {
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public DeliveryServiceService(AgriProductTrackerDbContext _db, IConfiguration _configuration, ICurrentUserService _currentUserService)
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        }

        #region Business Services Methods
        public async Task<ResponseViewModel> DeliveryServiceDelete(long id)
        {
            var response = new ResponseViewModel();

            try
            {
                var deliveryservice = _db.DeliveryServices.FirstOrDefault(x => x.Id == id && x.IsActive == true);

                if (deliveryservice != null)
                {
                    deliveryservice.IsActive = false;

                    _db.DeliveryServices.Update(deliveryservice);

                    response.IsSuccess = true;
                    response.Message = "Delete Delivery Service  Successfull";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Delivery service Not Found";
                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

            }


            return response;
        }

        public async Task<ResponseViewModel> DeliveryServiceSave(DeliveryServiceViewModel vm, string userName)
        {
            var response = new ResponseViewModel();
            var loggedInUser = _currentUserService.GetUserByUsername(userName);
            try
            {

                var deliveryservice = _db.DeliveryServices.FirstOrDefault(p => p.Id == vm.Id);


                if (deliveryservice == null)
                {
                    deliveryservice =  new DeliveryService()
                    {
                        Name = vm.Name,
                        Address = vm.Address,
                        Email = vm.Email,
                        TelePhoneNumber = vm.TelePhoneNumber,
                        DeliveryDetails = vm.DiliveryDetails,
                        IsActive = true,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedById = loggedInUser.Id,
                        CreatedOn = DateTime.UtcNow,
                        CreatedById = loggedInUser.Id

                };

                _db.DeliveryServices.Add(deliveryservice);

                    response.IsSuccess = true;
                    response.Message = "Delivery service has been save Successfully.";

                }
                else
                {
                    deliveryservice.Name = vm.Name;
                    deliveryservice.Address = vm.Address;
                    deliveryservice.Email = vm.Email;
                    deliveryservice.TelePhoneNumber = vm.TelePhoneNumber;
                    deliveryservice.DeliveryDetails = vm.DiliveryDetails;
                    deliveryservice.IsActive = true;
                    deliveryservice.UpdatedOn = DateTime.UtcNow;
                    deliveryservice.UpdatedById = loggedInUser.Id;
                    _db.DeliveryServices.Update(deliveryservice);

                    response.IsSuccess = true;
                    response.Message = "Delivery Service has been update Successfully";

                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public List<DeliveryServiceViewModel> GetAllDeliveryServiceList()
        {
            var response = new List<DeliveryServiceViewModel>();
            var query = _db.DeliveryServices.Where(u => u.Id != null);
            
            var DeliveryServiceList = query.ToList();

            foreach (var item in DeliveryServiceList)
            {
                var vm = new DeliveryServiceViewModel
                {

                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Email = item.Email,
                    TelePhoneNumber = item.TelePhoneNumber,
                    DiliveryDetails = item.DeliveryDetails,
                    UpdatedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow
            };

                response.Add(vm);
            }

            return response;
        }

        public List<DropDownViewModel> GetAllDeliveryServices()
        {
            var deliveryservices = _db.DeliveryServices
            .Where(x => x.IsActive == true)
            .Select(t => new DropDownViewModel() { Id = t.Id, Name = string.Format("{0}", t.Name) })
            .Distinct().ToList();

            return deliveryservices;
        }


        public DeliveryServiceViewModel GetDeliveryServicebyId(int id)
        {
            var response = new DeliveryServiceViewModel();

            var query = _db.DeliveryServices.FirstOrDefault(x => x.Id == id);

            response.Id = query.Id;
            response.Name = query.Name;
            response.Address = query.Address;   
            response.Email= query.Email;
            response.TelePhoneNumber = query.TelePhoneNumber;   
            response.DiliveryDetails = query.DeliveryDetails;               

           

        
            return response;
        }

        public PaginatedItemsViewModel<BasicDeliveryServiceViewModel> GetDeliveryServiceList(DeliveryServiceFilterViewModel filter)
        {
            int totalRecordCount = 0;
            double totalPages = 0;
            int totalPageCount = 0;

            var vmu = new List<BasicDeliveryServiceViewModel>();

            var deliveryservices = _db.DeliveryServices.Where(x => x.IsActive == true).OrderBy(u => u.Name);

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                deliveryservices = deliveryservices.Where(x => x.Name.Contains(filter.SearchText)).OrderBy(u => u.Name);
            }


            totalRecordCount = deliveryservices.Count();
            
            totalPageCount = (int)Math.Ceiling((Convert.ToDecimal(totalPages) / filter.PageSize));

            var deliveryServiceList = deliveryservices.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            foreach (var deliveryservice in deliveryServiceList)
            {
                var vm = new BasicDeliveryServiceViewModel()
                {
                    Id = deliveryservice.Id,
                    Name = deliveryservice.Name,
                    Address = deliveryservice.Address,
                    Email = deliveryservice.Email,
                    TelePhoneNumber = deliveryservice.TelePhoneNumber,
                    DiliveryDetails = deliveryservice.DeliveryDetails,
                    CreatedByName = deliveryservice.CreatedBy.FullName,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedByName = deliveryservice.UpdatedBy.FullName,
                    UpdatedOn = DateTime.UtcNow,

                    
                };
                vmu.Add(vm);
            };

            var container = new PaginatedItemsViewModel<BasicDeliveryServiceViewModel>(filter.CurrentPage, filter.PageSize, totalPageCount, totalRecordCount, vmu);

            return container;
        }
    }
}
#endregion