using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Measuring_equipment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Measuring_equipment.Controllers
{
    public class DeviceController : Controller
    {
        private IDeviceRepository repository;

        public DeviceController(IDeviceRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string str) => View();
        

        public ViewResult List => View(repository.Devices.OrderBy(d => d.InventoryNo));


    }
}