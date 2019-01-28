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

namespace Measuring_equipment.Controllers
{
    [Authorize]
    public class TypeController : Controller
    {
        private ITypeRepository repository;
        private readonly UserManager<AppUser> userManager;
        //private IHostingEnvironment he; 
        public TypeController(ITypeRepository repo, UserManager<AppUser> userMgr, IHostingEnvironment e)
        {
            repository = repo;
            userManager = userMgr;
           // he = e;
        }

        public IActionResult Index()
        {
            ViewBag.Breadcrumb = "Index";
            return View();
        }
        
        public ViewResult Edit(int typeId)
        {
            Type type = repository.Types.FirstOrDefault(t => t.TypeId == typeId);

            ViewBag.Breadcrumb = "Edycja";
            ViewBag.CreateMode = false;

            ViewBag.Producers = (repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Producer.ProducerId.ToString(),
                Text = t.Producer.ProducerName
            }).Distinct().ToList());

            ViewBag.Labs = (repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Laboratory.LaboratoryId.ToString(),
                Text = t.Laboratory.LaboratoryName
            }).Distinct().ToList());

            ViewBag.Verifications = (repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Verification.VerificationId.ToString(),
                Text = t.Verification.VerificationName
            }).Distinct().ToList());

            //Type picture
            if (type.Image != null)
            {
                var base64 = Convert.ToBase64String(type.Image);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ViewBag.Image = imgSrc;
            }
            else
                ViewBag.Image = Url.Content("~/images/no_pic.jpg");

            return View(type);
        }

        [HttpPost]
        
        public IActionResult Edit(int typeId, Type type)
        {
           if (ModelState.IsValid)
            {
                

                if (HttpContext.Request.Form.Files.Count != 0)
                {
                    var file = HttpContext.Request.Form.Files[0];
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        type.Image = stream.ToArray();
                    }
                }
                else
                {
                    if (type.TypeId != 0)
                    {
                        Type tmp = repository.Types.First(t => t.TypeId == typeId);
                        type.Image = tmp.Image;
                    }
                }
                
                
                
                repository.SaveType(type);
                TempData["message"] = $"Zapisano {type.TypeName}.";
                return RedirectToAction("Index");
                
            
              }
            else
            {
                TempData["error"] = $"Uzupełnij wszystkie wymagane dane";

                ViewBag.Breadcrumb = "Edycja";
                ViewBag.CreateMode = false;

                ViewBag.Producers = (repository.Types.Select(t => new SelectListItem()
                {
                    Value = t.Producer.ProducerId.ToString(),
                    Text = t.Producer.ProducerName
                }).Distinct().ToList());

                ViewBag.Labs = (repository.Types.Select(t => new SelectListItem()
                {
                    Value = t.Laboratory.LaboratoryId.ToString(),
                    Text = t.Laboratory.LaboratoryName
                }).Distinct().ToList());

                ViewBag.Verifications = (repository.Types.Select(t => new SelectListItem()
                {
                    Value = t.Verification.VerificationId.ToString(),
                    Text = t.Verification.VerificationName
                }).Distinct().ToList());

                return View(type);
            }
        }

        [HttpPost]
        public IActionResult LoadData()
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
            var data = typeData.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data  
            return Json(new { draw, recordsFiltered, recordsTotal, data });
        }

        public ViewResult Create()
        {
            ViewBag.Breadcrumb = "Nowe urządzenie";
            ViewBag.CreateMode = true;

            //  Producers, Labs and Veryfications values for select options
            ViewBag.Producers = (repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Producer.ProducerId.ToString(),
                Text = t.Producer.ProducerName
            }).Distinct().ToList());

            ViewBag.Labs = (repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Laboratory.LaboratoryId.ToString(),
                Text = t.Laboratory.LaboratoryName
            }).Distinct().ToList());

            ViewBag.Verifications = (repository.Types.Select(t => new SelectListItem()
            {
                Value = t.Verification.VerificationId.ToString(),
                Text = t.Verification.VerificationName
            }).Distinct().ToList());

            

            return View("Edit", new Type());
        }

        [HttpPost]
        public IActionResult Delete(int typeId)
        {
            Type deletedType = repository.DeleteType(typeId);
            if (deletedType != null)
            {
                TempData["message"] = $"Usunięto {deletedType.TypeName}.";
            }
            return RedirectToAction("Index");
        }
    }
}