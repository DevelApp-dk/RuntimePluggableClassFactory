﻿using DevelApp.RuntimePluggableClassFactory;
using DevelApp.RuntimePluggableClassFactory.Interface;
using PluginImplementations;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginClassFactory<ISpecificInterface> pluginClassFactory = new PluginClassFactory<ISpecificInterface>(retainOldVersions: 10);
            Uri pluginDirectory = new Uri("file:///E:/Projects/RuntimePluggableClassFactory/PluginFolder", UriKind.Absolute);
            if (!Directory.Exists(pluginDirectory.AbsolutePath))
            {
                Console.WriteLine("Directory NOT ok");
            }
            pluginClassFactory.LoadFromDirectory(pluginDirectory);

            var allInstanceNames = pluginClassFactory.GetAllInstanceNamesDescriptionsAndVersions();

            Console.WriteLine("All stored instances are:");
            foreach(var var in allInstanceNames)
            {
                Console.WriteLine("Instance {0} Description {1} Versions [{2}]", var.Name, var.Description, ListToString(var.Versions));
            }
            Console.WriteLine("End of instances");

            ISpecificInterface instance = pluginClassFactory.GetInstance("PluginImplementations.SpecificClassImpl", 1);

            if (!instance.Execute("Mønster"))
            {
                Console.WriteLine("Result is Nay");
            }
            if (instance.Execute("Monster"))
            {
                Console.WriteLine("Result is Yay");
            }

            Console.WriteLine("Finished. Press Any key to continue (Only [Enter] works though)");
            Console.ReadLine();
       }

        private static string ListToString(List<int> versions)
        {
            string list = string.Empty;
            foreach(int version in versions)
            {
                if(!list.Equals(string.Empty))
                {
                    list = list + "," + version.ToString();
                }
                else
                {
                    list = version.ToString();
                }
            }
            return list;
        }
    }
}