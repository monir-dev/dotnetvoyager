﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voyager.Core.Models;

namespace Voyager.Core.Utility
{
    public class ObjectInstance
    {
        /// <summary>
        /// Get Instance of a class as an object. Need to cast to actual Class.
        /// </summary>
        /// <param name="FullyQualifiedNameOfClass"></param>
        /// <returns></returns>
        public static object GetObjectInstanceOfSameAssembly(string FullyQualifiedNameOfClass)
        {
            Type t = Type.GetType(FullyQualifiedNameOfClass);
            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// Get Instance of a class as an object. Need to cast to actual Class.
        /// </summary>
        /// <param name="FullyQualifiedNameOfClass"></param>
        /// <returns></returns>
        public static object GetObjectInstanceOfDifferentAssembly(string FullyQualifiedNameOfClass)
        {
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
        public static IVoyagerModel GetVoyagerModelInstanceOfDifferentAssembly(string FullyQualifiedNameOfClass)
        {
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
        public static T GetGenericInstanceOfDifferentAssembly<T>(string FullyQualifiedNameOfClass)
        {
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
