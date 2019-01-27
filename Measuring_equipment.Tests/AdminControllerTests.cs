using System.Collections.Generic;
using System.Linq;
using Moq;
using Measuring_equipment.Controllers;
using Measuring_equipment.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Measuring_equipment.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Devices()
        {
            //Przygotowanie - tworzenie imitacji repozytorium
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            mock.Setup(m => m.Devices).Returns(new Device[]
            {
                new Device {DeviceId = 1, RegistrationNo = 1},
                new Device {DeviceId = 2, RegistrationNo = 2},
                new Device {DeviceId = 3, RegistrationNo = 3},
            }.AsQueryable<Device>());

            //Przygotowanie - utworzenie kontrolera
            AdminController target = new AdminController(mock.Object);

            //Działanie
            Device[] result = GetViewModel<IQueryable<Device>>(target.Index())?.ToArray();

            //Asercje
            Assert.Equal(3, result.Length);
            Assert.Equal(1, result[0].RegistrationNo);
            Assert.Equal(2, result[1].RegistrationNo);
            Assert.Equal(3, result[2].RegistrationNo);
        }
        
        [Fact]
        public void Can_Edit_Device()
        {
            //Przygotowanie - tworzenie imitacji repozytorium
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            mock.Setup(m => m.Devices).Returns(new Device[]
            {
                new Device {DeviceId = 1, RegistrationNo = 1, TypeId = 1},
                new Device {DeviceId = 2, RegistrationNo = 2, TypeId = 1},
                new Device {DeviceId = 3, RegistrationNo = 3, TypeId = 1},
            }.AsQueryable<Device>());

            //Przygotowanie - utworzenie kontrolera
            AdminController target = new AdminController(mock.Object);

            //Działanie
            Device p1 = GetViewModel<Device>(target.Edit(1));
            Device p2 = GetViewModel<Device>(target.Edit(2));
            Device p3 = GetViewModel<Device>(target.Edit(3));

            //Asercje
            Assert.Equal(1, p1.DeviceId);
            Assert.Equal(2, p2.DeviceId);
            Assert.Equal(3, p3.DeviceId);
        }

        [Fact]
        public void Cannot_Edit_Nonexisting_Device()
        {
            //Przygotowanie - tworzenie imitacji repozytorium
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            mock.Setup(m => m.Devices).Returns(new Device[]
            {
                new Device {DeviceId = 1, RegistrationNo = 1},
                new Device {DeviceId = 2, RegistrationNo = 2},
                new Device {DeviceId = 3, RegistrationNo = 3},
            }.AsQueryable<Device>());

            //Przygotowanie - utworzenie kontrolera
            AdminController target = new AdminController(mock.Object);

            //Działanie
            Device result = GetViewModel<Device>(target.Edit(4));

            //Asercje
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            //Przygotowanie - tworzenie imitacji repozytorium
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            //Przygotowanie - tworzenie kontrolera TempData
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            //Przygotowanie - utworzenie kontrolera
            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            //Przygotowanie - tworzenie produktu
            Device Device = new Device { RegistrationNo = 2000 };

            //Działanie
            IActionResult result = target.Edit(Device);

            //Asercje - sprawdzanie czy zostało wywołane repozytorium
            mock.Verify(m => m.SaveDevice(Device));
            //Asercje - sprawdzanie typu zwracanego z metody
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            //Przygotowanie - tworzenie imitacji repozytorium
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            //Przygotowanie - utworzenie kontrolera
            AdminController target = new AdminController(mock.Object);

            //Przygotowanie - tworzenie produktu
            Device Device = new Device { RegistrationNo = 2000 };

            //Przygotowanie - dodanie błędu do stanu modelu
            target.ModelState.AddModelError("error", "error");

            //Działanie - próba zapisania produktu
            IActionResult result = target.Edit(Device);

            //Asercje - sprawdzanie czy nie zostało wywołane repozytorium
            mock.Verify(m => m.SaveDevice(It.IsAny<Device>()), Times.Never);
            //Asercje - sprawdzanie typu zwracanego z metody
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Devices()
        {
            //Przygotowanie - tworzenie produktu
            Device prod = new Device { DeviceId = 2, RegistrationNo = 2000 };


            //Przygotowanie - tworzenie imitacji repozytorium
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();
            mock.Setup(m => m.Devices).Returns(new Device[]
            {
                new Device {DeviceId = 1, RegistrationNo = 1},
                prod,
                new Device {DeviceId = 3, RegistrationNo = 3},
            }.AsQueryable<Device>());

            //Przygotowanie - utworzenie kontrolera
            AdminController target = new AdminController(mock.Object);

            //Działanie - usunięcie produku
            target.Delete(prod.DeviceId);

            //Asercje
            mock.Verify(m => m.DeleteDevice(prod.DeviceId));
        }
   
        private T GetViewModel<T>(IActionResult result) where T : class { return (result as ViewResult)?.ViewData.Model as T; }
    }
}
