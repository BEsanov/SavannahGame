
using SavannahGames.Game.AnimalBehavior;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SavannahGames.Game.Main
{
    public static class PluginLoader
    {
        public static ICollection<IAnimal> LoadPlugins(string path)
        {
            string[] dllFileNames = null;

            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "Savannah.Animal.*.dll");

                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);


                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(IAnimal);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                ICollection<IAnimal> plugins = new List<IAnimal>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    IAnimal plugin = (IAnimal)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = Builder.GetInstance();
            var gameManager = builder.GetGameManager();
            gameManager.Setup(60, 40);


            gameManager.StartGame();
        }
    }
}
