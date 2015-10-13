using System;
using System.Collections;
using SimpleInjector;
using System.Collections.Generic;
using Paxie.Core;

namespace Xamarui.Forms.Mvvm
{
    public static class FreshIoC
    {
        static Container _container;

        public static Container Container { get { return _container; } set { _container = value; } }

        public static T Resolve<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public static object Resolve(Type t)
        {
            return _container.GetInstance(t);
        }

        public static IEnumerable<T> GetAllInstances<T>() where T : class
        {
            return _container.GetAllInstances<T>();
        }

        public static IEnumerable<object> GetAllInstances(Type t)
        {
            return _container.GetAllInstances(t);
        }

        public static void Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
        }

        public static void Register<T>(T type)
        {
            _container.Register(type.GetType());
        }

        public static Container GetContainer()
        {
            return _container;
        }

        public static void SetContainer(Container container)
        {
            Ensure.IsNotNull(container);
            _container = container;
        }

        public static void Test()
        {

        }
    }
}

