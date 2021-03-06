﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace FluentInstallation
{
  
    public class InstallerFactory : IInstallerFactory
    {
        private  Func<Assembly> GetAssembly { get; set; }

        public InstallerFactory(Func<Assembly> getAssembly)
        {
            if (getAssembly == null)
            {
                throw new ArgumentNullException("getAssembly");
            }

            GetAssembly = getAssembly;
        }

        public IInstaller Create()
        {
            var installer = GetAssembly()
                        .FindInstallerTypesMarkedAsDefault()
                        .Select(Activator.CreateInstance)
                        .Cast<IInstaller>()
                        .FirstOrDefault();

            if (installer == null)
            {
                throw new InvalidOperationException("Unable to find any installer in assembly marked with the DefaultInstaller attribute.");
            }

            return installer;
        }

        public IInstaller Create(string typeName)
        {

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException("typeName");
            }

            return GetAssembly()
                       .FindInstallerTypes()
                       .Where(type => type.Name.Equals(typeName))
                       .Select(Activator.CreateInstance)
                       .Cast<IInstaller>()
                       .FirstOrDefault();
        }
    }
}