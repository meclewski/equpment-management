using Microsoft.AspNetCore.Mvc;
using Measuring_equipment.Models;
using Measuring_equipment.Models.ViewModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Type = Measuring_equipment.Models.Type;
using Microsoft.AspNetCore.Authorization;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Measuring_equipment.Controllers
{
    


    [Authorize (Roles ="Administratorzy")]
    public class AdminController : Controller
    {
        
        private IDeviceRepository repository;
        private readonly UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        

        public AdminController(IDeviceRepository repo,
            UserManager<AppUser> userMgr,
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passValid,
            IPasswordHasher<AppUser> passwordHash)
        {
            repository = repo;
            userManager = userMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        public ViewResult Index()
        {
            ViewBag.Breadcrumb = "Index";
            return View();
        }

        // Load data for DataTable (server side proccessing)
        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            // DataTable Server side processing

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

            // Skip number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();

            // Paging Length   
            var length = Request.Form["length"].FirstOrDefault();

            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            // Paging Size  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;

           
            // Getting all Device and Type data  
            var deviceData = (from tempDevice in repository.DevicesDT
                                             select tempDevice);

            IQueryable<Tuple<Device, Type>> GetDeviceWithType()
            {
                var dd = (from tempDevice in deviceData
                                   join tempType in repository.Types on tempDevice.TypeId equals tempType.TypeId
                                   select new Tuple<Device, Type>(tempDevice, tempType));
                return dd;

            }

            //Search 
            IQueryable<Tuple<Device,Type>> MatchesKeyword(IQueryable<Tuple<Device,Type>> list, string keyword)
            {
                var predicate = PredicateBuilder.New<Tuple<Device, Type>>();
                               
                predicate = predicate.Or(
                         item =>  item.Item1.RegistrationNo.ToString().Contains(keyword)
                         || item.Item1.InventoryNo.Contains(keyword)
                         || item.Item1.SerialNo.Contains(keyword)
                         || item.Item1.VerificationDate.ToString().Contains(keyword)
                         || item.Item1.TimeToVerification.ToString().Contains(keyword)
                         || item.Item1.VerificationResult.Contains(keyword)
                         || item.Item1.ProductionDate.ToString().Contains(keyword)
                         || item.Item1.DeviceDesc.Contains(keyword)
                         || item.Item2.TypeName.Contains(keyword)
                         || item.Item2.DeviceName.Contains(keyword)
                      
                );

                return list.AsQueryable().Where(predicate);
            }

            IQueryable<Tuple<Device, Type>> deviceType = GetDeviceWithType();

            if (!string.IsNullOrEmpty(searchValue))
            {
                
                deviceType = MatchesKeyword(deviceType, searchValue);
            }

            //Choose useful columns
            var deviceData2 = (from tempDevice in deviceType
                               select new {
                                   tempDevice.Item1.DeviceId,
                                   tempDevice.Item1.RegistrationNo,
                                   tempDevice.Item1.InventoryNo,
                                   tempDevice.Item1.SerialNo,
                                   tempDevice.Item1.VerificationDate,
                                   tempDevice.Item1.TimeToVerification,
                                   tempDevice.Item1.VerificationResult,
                                   tempDevice.Item1.ProductionDate,
                                   tempDevice.Item1.DeviceDesc,
                                   tempDevice.Item2.TypeName,
                                   tempDevice.Item2.DeviceName });
            

            //Sorting 
            if (!(string.IsNullOrEmpty(sortColumn)) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                if (sortColumnDirection == "asc")
                {
                    deviceData2 = deviceData2.OrderBy(sortColumn + " ASC");
                }
                else
                {
                    deviceData2 = deviceData2.OrderBy(sortColumn + " DESC");
                }
            }
            //Total number of rows counts   
            recordsTotal = deviceData2.Count();
            int recordsFiltered = recordsTotal;

            //Paging
            var data = await deviceData2.Skip(skip).Take(pageSize).ToListAsync();
            
            //Returning Json Data 
            return Json(new { draw, recordsFiltered, recordsTotal, data });
        }

        public async Task<ViewResult> Edit(int deviceId)
        {
            ViewBag.Breadcrumb = "Edycja";
            ViewBag.CreateMode = false;

            Device device = await repository.Devices
                .FirstOrDefaultAsync(d => d.DeviceId == deviceId);

            
            // Types value for select option 
            List<SelectListItem> typeList = await TypeList();

            //User name
            ViewBag.userName = userManager.GetUserName(HttpContext.User);


            //Device picture
            string imgSrc = Url.Content("~/images/no_pic.jpg");
            if (device.Type.Image != null)
            {
                var base64 = Convert.ToBase64String(device.Type.Image);
                imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            }

            AdminEditViewModel model = new AdminEditViewModel
            {
                DeviceId = device.DeviceId,
                RegistrationNo = device.RegistrationNo,
                InventoryNo = device.InventoryNo,
                SerialNo = device.SerialNo,
                VerificationDate = device.VerificationDate,
                TimeToVerification = device.TimeToVerification,
                VerificationResult = device.VerificationResult,
                ProductionDate = device.ProductionDate,
                DeviceDesc = device.DeviceDesc,
                CurrentlyInUse = device.CurrentlyInUse,
                TypeId = device.TypeId,
                DeviceName = device.Type.DeviceName,
                ProducerName = device.Type.Producer.ProducerName,
                VerificationName = device.Type.Verification.VerificationName,
                ImageStr = imgSrc,
                TypeListVm = typeList
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminEditViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                Device device = new Device
                {
                    DeviceId = model.DeviceId,
                    RegistrationNo = model.RegistrationNo,
                    InventoryNo = model.InventoryNo,
                    SerialNo = model.SerialNo,
                    VerificationDate = model.VerificationDate,
                    TimeToVerification = model.TimeToVerification,
                    VerificationResult = model.VerificationResult,
                    ProductionDate = model.ProductionDate,
                    DeviceDesc = model.DeviceDesc,
                    CurrentlyInUse = model.CurrentlyInUse,
                    TypeId = model.TypeId,
                };

                repository.SaveDevice(device);
                TempData["message"] = $"Zapisano {String.Format("{0:D5}",model.RegistrationNo)}.";
                return RedirectToAction("Index");
            }
            else
            {
                
                TempData["error"] = $"Uzupełnij wszystkie wymagane dane";

                ViewBag.CreateMode = false;

                //User name
                ViewBag.userName = userManager.GetUserName(HttpContext.User);

                return await Edit(model.DeviceId);
            }
        }

        
        public async Task<ViewResult> Create()

        {
            ViewBag.Breadcrumb = "Nowe urządzenie";
            ViewBag.CreateMode = true;
            
            //User name
            ViewBag.userName = userManager.GetUserName(HttpContext.User);
            List<SelectListItem> typeList = await TypeList();
            //Get last Device for next RegistrationNo count
            Device device = await repository.Devices.OrderBy(d => d.RegistrationNo).LastAsync();
           
            return View("Edit", new AdminEditViewModel {
                RegistrationNo = device.RegistrationNo +1,
                TypeListVm = typeList});
        }   
        [HttpPost]
        public async Task<IActionResult> Create (AdminEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Device device = new Device
                {
                    DeviceId = model.DeviceId,
                    RegistrationNo = model.RegistrationNo,
                    InventoryNo = model.InventoryNo,
                    SerialNo = model.SerialNo,
                    VerificationDate = model.VerificationDate,
                    TimeToVerification = model.TimeToVerification,
                    VerificationResult = model.VerificationResult,
                    ProductionDate = model.ProductionDate,
                    DeviceDesc = model.DeviceDesc,
                    CurrentlyInUse = model.CurrentlyInUse,
                    TypeId = model.TypeId,
                };

                repository.SaveDevice(device);
                TempData["message"] = $"Zapisano {String.Format("{0:D5}", model.RegistrationNo)}.";
                return RedirectToAction("Index");
            }
            else
            {

                TempData["error"] = $"Uzupełnij wszystkie wymagane dane";

                ViewBag.CreateMode = false;

                //User name
                ViewBag.userName = userManager.GetUserName(HttpContext.User);

                return await Edit(model.DeviceId);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int deviceId)
        {
            Device deletedDevice = await repository.DeleteDeviceAsync(deviceId);
            if (deletedDevice != null)
            {
                TempData["message"] = $"Usunięto {deletedDevice.InventoryNo}.";
            }
            return RedirectToAction("Index");
        }

        private async Task<List<SelectListItem>> TypeList()
        {
            return await repository.Types.Select(t => new SelectListItem()
            {
                Value = t.TypeId.ToString(),
                Text = t.TypeName
            }).Distinct().ToListAsync();
        }

        public async Task<IActionResult> GetData(int typeId)
        {
            
            Device device = await repository.Devices.FirstOrDefaultAsync(d => d.TypeId == typeId);

            Type type = await repository.Types.FirstAsync(t => t.TypeId == typeId);
            
            Type typeresult = new Type()
            {
                TypeId = type.TypeId,
                TypeName = type.TypeName,
                DeviceName = type.DeviceName,
                TypeDesc = type.TypeDesc,
                ValidityPierod = type.ValidityPierod,
                Price = type.Price,
                ProducerId = type.ProducerId
            };
            
            return new JsonResult(typeresult);
        }

        public async Task<IActionResult> GetDataProd(int typeId)
        {
            Type type = await repository.Types.FirstAsync(t => t.TypeId == typeId);
            int producerId = type.ProducerId;
            Producer producerSelected = await repository.Producers.FirstAsync(p => p.ProducerId == producerId);

            Producer producerresult = new Producer()
            {
                ProducerId = producerSelected.ProducerId,
                ProducerName = producerSelected.ProducerName
            };
            
            return new JsonResult(producerresult);
        }

        public async Task<IActionResult> GetDataVer(int typeId)
        {
            Type type = await repository.Types.FirstAsync(t => t.TypeId == typeId);
            int verificationId = type.VerificationId;
            Verification verificationSelected = await repository.Verifications.FirstAsync(p => p.VerificationId == verificationId);

            Verification verificationresult = new Verification()
            {
                VerificationId = verificationSelected.VerificationId,
                VerificationName = verificationSelected.VerificationName
            };
            
            return new JsonResult(verificationresult);
        }

        [HttpGet]
        public IActionResult GetAllData(int intiger)
        {
            IQueryable<Device> All = repository.Devices.OrderBy(d => d.DeviceId);
            return new JsonResult(All);
        }


        /*
          -------------------------------------------------------------
          ------------------------IDENTITY-----------------------------
          -------------------------------------------------------------
        */


        public ViewResult UsersIndex()
        {
            ViewBag.Breadcrumb = "Użytkownicy";
            return View(userManager.Users);
        }

        public ViewResult UsersCreate()
        {
            ViewBag.Breadcrumb = "Dodawanie użytkowników";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsersCreate(UserViewModel.CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["message"] = $"Dodano {user.NormalizedUserName}.";
                    return RedirectToAction("UsersIndex");
                }
                else
                {
                    
                    foreach (IdentityError error in result.Errors)
                    {
                        TempData["error"] = error.Description.ToString();
                        //ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UsersDelete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["message"] = $"Usunięto poprawnie {user.NormalizedUserName}.";
                    return RedirectToAction("UsersIndex"); 
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                //ModelState.AddModelError("", "Nie znaleziono użytkownika");
                TempData["error"] = "Nie znaleziono użytkownika";
            }
            return View("UsersIndex", userManager.Users);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                TempData["error"] = error.Description.ToString();
                //ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> UsersEdit(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("UsersIndex");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UsersEdit(string id,  string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
               if ((validEmail.Succeeded && validPass == null) 
                    || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"Wyedytowano poprawnie {user.NormalizedUserName}.";
                        return RedirectToAction("UsersIndex");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }     
            }
            else
            {
                //ModelState.AddModelError("", "Nie znaleziono użytkownika");
                TempData["error"] = "Nie znaleziono użytkownika";
            }
            return View(user);
        }
    }
}