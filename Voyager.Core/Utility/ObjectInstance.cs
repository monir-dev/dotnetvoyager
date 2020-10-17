using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voyager.Core.Models;

namespace Voyager.Core.Utility
{
    public class ObjectInstance
    {

        public List<string> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IVoyagerModel).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Get Type of class from string class name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        public static Type GetClassType<T>(string className)
        {
            Type type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && x.Name == className)
                 .First();
            return type;
        }

        /// <summary>
        /// Get Instance of a class as an object. Need to cast to actual Class.
        /// </summary>
        /// <param name="FullyQualifiedNameOfClass"></param>
        /// <returns></returns>
        public static object GetObjectInstanceOfSameAssembly(string className)
        {
            var objectType = ObjectInstance.GetClassType<IVoyagerModel>(className);
            string FullyQualifiedNameOfClass = objectType.AssemblyQualifiedName;

            Type t = Type.GetType(FullyQualifiedNameOfClass);
            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// Get Instance of a class as an object. Need to cast to actual Class.
        /// </summary>
        /// <param name="FullyQualifiedNameOfClass"></param>
        /// <returns></returns>
        public static object GetObjectInstanceOfDifferentAssembly(string className)
        {
            var objectType = ObjectInstance.GetClassType<IVoyagerModel>(className);
            string FullyQualifiedNameOfClass = objectType.AssemblyQualifiedName;

            Type type = Type.GetType(FullyQualifiedNameOfClass);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(FullyQualifiedNameOfClass);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }


        /// <summary>
        /// Get IVoyager Model Instance of a class.
        /// </summary>
        /// <param name="FullyQualifiedNameOfClass"></param>
        /// <returns></returns>
        public static IVoyagerModel GetVoyagerModelInstanceOfDifferentAssembly(string className)
        {
            var objectType = ObjectInstance.GetClassType<IVoyagerModel>(className);
            string FullyQualifiedNameOfClass = objectType.AssemblyQualifiedName;

            Type type = Type.GetType(FullyQualifiedNameOfClass);
            if (type != null)
                return (IVoyagerModel)Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(FullyQualifiedNameOfClass);
                if (type != null)
                    return (IVoyagerModel)Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// Get Generic Model Instance of a class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FullyQualifiedNameOfClass"></param>
        /// <returns></returns>
        public static T GetGenericInstanceOfDifferentAssembly<T>(string className)
        {
            var objectType = ObjectInstance.GetClassType<IVoyagerModel>(className);

            string FullyQualifiedNameOfClass = objectType.AssemblyQualifiedName;

            Type type = Type.GetType(FullyQualifiedNameOfClass);
            if (type != null)
                return (T)Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(FullyQualifiedNameOfClass);
                if (type != null)
                    return (T)Activator.CreateInstance(type);
            }
            return default(T);
        }

    }
}
