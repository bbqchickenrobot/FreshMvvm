using System;
using System.Collections;
//using FreshTinyIoC;
using SimpleInjector;
using System.Collections.Generic;

namespace FreshMvvm
{
    public class FreshIOC : IDisposable
    {
        //public static FreshTinyIoCContainer Container { 
        //    get {
        //        return FreshTinyIoCContainer.Current;
        //    }
        //}
        protected static readonly Container _container = new Container();

        public static T Resolve<T>() where T :  class
        {
           return  _container.GetInstance<T>();
        }

        public static object Resolve(Type t)
        {
           return  _container.GetInstance(t);
        }

        public static IEnumerable<T> GetAllInstances<T>() where T: class
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

        public object GetContainer()
        {
            return _container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}

