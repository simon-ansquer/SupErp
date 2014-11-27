using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SupErp.Kernel;
using System.Collections.Generic;
using System.Linq;
using SupErp.Entities;
using SupErp.Shared;

namespace SupErp.Tests
{
    [TestClass]
    public class KernelTest
    {
        [TestMethod]
        public void TestGetDllsPaths()
        {
            DllManager dllManager = new DllManager();
            IEnumerable<string> paths = dllManager.GetDllsPaths();
            Assert.IsTrue(paths.Count() > 0);
        }

        [TestMethod]
        public void TestGetMainMenus()
        {
            DllManager dllManager = new DllManager();
            Role role = new Role()
            {
                RoleModules = new List<RoleModule>()
                {
                    new RoleModule()
                    {
                        Module = new Module()
                        {
                            Name = "SupErpModuleUser"
                        }
                    }
                }
            };

            List<IMainMenu> mainMenus = dllManager.GetMainMenus(role);
            Assert.IsNotNull(mainMenus);
            Assert.AreEqual(1, mainMenus.Count);
        }
    }
}
