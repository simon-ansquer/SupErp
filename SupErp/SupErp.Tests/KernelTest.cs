using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SupErp.Kernel;
using System.Collections.Generic;
using System.Linq;
using SupErp.Entities;
using SupErp.Shared;
using SupErp.Kernel.Exceptions;

namespace SupErp.Tests
{
    [TestClass]
    public class KernelTest
    {
        [TestInitialize]
        public void Initialize()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            CopyDll("SupErpModuleUser.dll");
        }

        private void CopyDll(string dllName)
        {
            string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
            string dllPath = Path.Combine(destinationPath, dllName);

            if (!File.Exists(dllPath))
            {
                string sourcePath = Path.Combine(destinationPath, @"..\..\..\..\SupErp.Kernel\ModulesDlls", dllName);
                File.Copy(sourcePath, dllPath);
            }
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
            Assert.AreEqual("Administration", mainMenus[0].MenuName);
            Assert.IsTrue(mainMenus[0].SubMenus.Count > 0);
        }

        [TestMethod]
        public void TestGetMainMenuNotImplemented()
        {
            CopyDll("MainMenuNotImplemented.dll");

            DllManager dllManager = new DllManager();
            Role role = new Role()
            {
                RoleModules = new List<RoleModule>()
                {
                    new RoleModule()
                    {
                        Module = new Module()
                        {
                            Name = "MainMenuNotImplemented"
                        }
                    }
                }
            };

            try
            {
                List<IMainMenu> mainMenus = dllManager.GetMainMenus(role);
                Assert.Fail();
            }
            catch (MainMenuNotImplementedException e1)
            {
                Assert.IsNotNull(e1);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestGetMainMenuBadImplementation()
        {
            CopyDll("MainMenuBadImplementation.dll");

            DllManager dllManager = new DllManager();
            Role role = new Role()
            {
                RoleModules = new List<RoleModule>()
                {
                    new RoleModule()
                    {
                        Module = new Module()
                        {
                            Name = "MainMenuBadImplementation"
                        }
                    }
                }
            };

            try
            {
                List<IMainMenu> mainMenus = dllManager.GetMainMenus(role);
                Assert.Fail();
            }
            catch (MainMenuImplementationException e1)
            {
                Assert.IsNotNull(e1);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
