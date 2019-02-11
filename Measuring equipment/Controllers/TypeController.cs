using Microsoft.AspNetCore.Mvc;
using Measuring_equipment.Models;
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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Measuring_equipment.Models.ViewModels;


namespace Measuring_equipment.Controllers
{
    [Authorize(Roles = "Administratorzy")]
    public class TypeController : Controller
    {
        private ITypeRepository repository;
        private readonly UserManager<AppUser> userManager;
        public TypeController(ITypeRepository repo, UserManager<AppUser> userMgr, IHostingEnvironment e)
        {
            repository = repo;
            userManager = userMgr;
        }

        public IActionResult Index()
        {
            ViewBag.Breadcrumb = "Index";
            return View();
        }


        public async Task<IActionResult> Edit(int? typeId)
        {
            if (typeId == null)
            {
                TempData["error"] = $"Nie odnaleziono.";
                return RedirectToAction("Index");
            }

            Type type = repository.Types.FirstOrDefault(t => t.TypeId == typeId);

            if (type == null)
            {
                TempData["error"] = $"Nie odnaleziono.";
                return RedirectToAction("Index");
            }

            List<SelectListItem> prodList = await ProducerList();
            List<SelectListItem> labList = await LaboratoryList();
            List<SelectListItem> verList = await VerificationList();

            //Type picture
            string imgSrc = Url.Content("~/images/no_pic.jpg");
            if (type.Image != null)
            {
                var base64 = Convert.ToBase64String(type.Image);
                imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            }

            TypeEditViewModel model = new TypeEditViewModel
            {
                TypeId = type.TypeId,
                TypeName = type.TypeName,
                DeviceName = type.DeviceName,
                ValidityPierod = type.ValidityPierod,
                Price = type.Price,
                Image = type.Image,
                TypeDesc = type.TypeDesc,
                ProducerId = type.ProducerId,
                LaboratoryId = type.LaboratoryId,
                VerificationId = type.VerificationId,
                ImageStr = imgSrc,
                ProducerListVm = prodList,
                LaboratoryListVm = labList,
                VerificationListVm = verList,
                ReturnUrl = HttpContext.Request.Headers["Referer"]
        };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TypeEditViewModel model)
        {
            if (ModelState.IsValid)
                try
                {
                        Type type = new Type
                        {
                            TypeId = model.TypeId,
                            TypeName = model.TypeName,
                            DeviceName = model.DeviceName,
                            ValidityPierod = model.ValidityPierod,
                            Price = model.Price,
                            Image = model.Image,
                            TypeDesc = model.TypeDesc,
                            ProducerId = model.ProducerId,
                            LaboratoryId = model.LaboratoryId,
                            VerificationId = model.VerificationId,
                        };

                        if (HttpContext.Request.Form.Files.Count != 0)
                        {
                            var file = HttpContext.Request.Form.Files[0];
                            using (var stream = new MemoryStream())
                            {
                                await file.CopyToAsync(stream);
                                type.Image = stream.ToArray();
                            }
                        }

                        else
                        {
                            if (type.TypeId != 0)
                            {
                                Type tmp = await repository.Types.FirstAsync(t => t.TypeId == type.TypeId);
                                type.Image = tmp.Image;
                            }
                        }

                        repository.SaveType(type);
                        TempData["message"] = $"Zapisano {type.TypeName}.";
                        return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Nie można zapisać zmian", e.Message);
                    return await Edit(model.TypeId);
                }
            else
            {
                TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";

                List<SelectListItem> prodList = await ProducerList();
                List<SelectListItem> labList = await LaboratoryList();
                List<SelectListItem> verList = await VerificationList();

                return await Edit(model.TypeId);
            }
        }

        public async Task<ViewResult> Create()
        {
            //  Producers, Labs and Veryfications values for select options
            List<SelectListItem> prodList = await ProducerList();
            List<SelectListItem> labList = await LaboratoryList();
            List<SelectListItem> verList = await VerificationList();

            return View("Create", new TypeEditViewModel {
                ProducerListVm = prodList,
                LaboratoryListVm = labList,
                VerificationListVm = verList,
                ReturnUrl = HttpContext.Request.Headers["Referer"]
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try {
                    Type type = new Type
                    {
                        TypeId = model.TypeId,
                        TypeName = model.TypeName,
                        DeviceName = model.DeviceName,
                        ValidityPierod = model.ValidityPierod,
                        Price = model.Price,
                        Image = model.Image,
                        TypeDesc = model.TypeDesc,
                        ProducerId = model.ProducerId,
                        LaboratoryId = model.LaboratoryId,
                        VerificationId = model.VerificationId,
                    };

                    if (HttpContext.Request.Form.Files.Count != 0)
                    {
                        var file = HttpContext.Request.Form.Files[0];
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            type.Image = stream.ToArray();
                        }
                    }
                    else
                    {
                        if (type.TypeId != 0)
                        {
                            Type tmp = await repository.Types.FirstAsync(t => t.TypeId == type.TypeId);
                            type.Image = tmp.Image;
                        }
                    }

                    repository.SaveType(type);
                    TempData["message"] = $"Zapisano {type.TypeName}.";
                    return Redirect(model.ReturnUrl);

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Nie można utworzyć typu", e.Message);
                    return await Create();
                }
            }
            else
            {
                TempData["error"] = $"Uzupełnij poprawnie wszystkie wymagane dane";
                return await Create();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int typeId)
        {
            try
            {
                Type deletedType = await repository.DeleteTypeAsync(typeId);
                if (deletedType != null)
                {
                    TempData["message"] = $"Usunięto {deletedType.TypeName}.";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Nie można usunąć, istnieją urządzenia tego typu", e.Message);
                TempData["error"] = $"Nie można usunąć, istnieją urządzenia tego typu";
            }
            return RedirectToAction("Index");
        }

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

            //Paging Size  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;



            // getting all Device data  
            var typeData = (from tempType in repository.TypesDT
                            select tempType);



            //Sorting 
            if (!(string.IsNullOrEmpty(sortColumn)) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                if (sortColumnDirection == "asc")
                {
                    typeData = typeData.OrderBy(sortColumn + " ASC");
                }
                else
                {
                    typeData = typeData.OrderBy(sortColumn + " DESC");
                }
            }

            //Search 
            IQueryable<Type> MatchesKeyword(IQueryable<Type> list, string keyword)
            {
                var predicate = PredicateBuilder.New<Type>();

                predicate = predicate.Or(
                         item => item.DeviceName.Contains(keyword)
                        || item.TypeName.Contains(keyword)
                        || item.ValidityPierod.ToString().Contains(keyword)
                        || item.TypeDesc.Contains(keyword)
                );

                return list.AsQueryable().Where(predicate);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                typeData = MatchesKeyword(typeData, searchValue);
            }

            //total number of rows counts   
            recordsTotal = typeData.Count();
            int recordsFiltered = recordsTotal;

            //Paging  
            var data = await typeData.Skip(skip).Take(pageSize).ToListAsync();

            //Returning Json Data  
            return Json(new { draw, recordsFiltered, recordsTotal, data });
        }

        private async Task<List<SelectListItem>> ProducerList()
        {
            return await repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Producer.ProducerId.ToString(),
                Text = t.Producer.ProducerName
            }).Distinct().ToListAsync();
        }

        private async Task<List<SelectListItem>> LaboratoryList()
        {
            return await repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Laboratory.LaboratoryId.ToString(),
                Text = t.Laboratory.LaboratoryName
            }).Distinct().ToListAsync();
        }

        private async Task<List<SelectListItem>> VerificationList()
        {
            return await repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Verification.VerificationId.ToString(),
                Text = t.Verification.VerificationName
            }).Distinct().ToListAsync();
        }
    }
}