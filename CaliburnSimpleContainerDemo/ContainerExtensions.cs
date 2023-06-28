//// Decompiled with JetBrains decompiler
//// Type: Caliburn.Micro.ContainerExtensions
//// Assembly: Caliburn.Micro.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
//// MVID: B5EA9E01-9CB8-488B-BFF3-CC30DF7E0DA4
//// Assembly location: C:\Users\Administrator\.nuget\packages\caliburn.micro.core\4.0.212\lib\netstandard2.0\Caliburn.Micro.Core.dll
//// XML documentation location: C:\Users\Administrator\.nuget\packages\caliburn.micro.core\4.0.212\lib\netstandard2.0\Caliburn.Micro.Core.xml

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace Caliburn.Micro
//{
//    /// <summary>
//    /// Extension methods for the <see cref="T:Caliburn.Micro.SimpleContainer" />.
//    /// </summary>
//    public static class ContainerExtensions
//    {
//        /// <summary>Registers a singleton.</summary>
//        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer Singleton<TImplementation>(
//          this SimpleContainer container,
//          string key = null)
//        {
//            return container.Singleton<TImplementation, TImplementation>(key);
//        }

//        /// <summary>Registers a singleton.</summary>
//        /// <typeparam name="TService">The type of the service.</typeparam>
//        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer Singleton<TService, TImplementation>(
//          this SimpleContainer container,
//          string key = null)
//          where TImplementation : TService
//        {
//            container.RegisterSingleton(typeof(TService), key, typeof(TImplementation));
//            return container;
//        }

//        /// <summary>Registers an service to be created on each request.</summary>
//        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer PerRequest<TImplementation>(
//          this SimpleContainer container,
//          string key = null)
//        {
//            return container.PerRequest<TImplementation, TImplementation>(key);
//        }

//        /// <summary>Registers an service to be created on each request.</summary>
//        /// <typeparam name="TService">The type of the service.</typeparam>
//        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer PerRequest<TService, TImplementation>(
//          this SimpleContainer container,
//          string key = null)
//          where TImplementation : TService
//        {
//            container.RegisterPerRequest(typeof(TService), key, typeof(TImplementation));
//            return container;
//        }

//        /// <summary>Registers an instance with the container.</summary>
//        /// <typeparam name="TService">The type of the service.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="instance">The instance.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer Instance<TService>(
//          this SimpleContainer container,
//          TService instance)
//        {
//            container.RegisterInstance(typeof(TService), (string)null, (object)instance);
//            return container;
//        }

//        /// <summary>
//        /// Registers a custom service handler with the container.
//        /// </summary>
//        /// <typeparam name="TService">The type of the service.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="handler">The handler.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer Handler<TService>(
//          this SimpleContainer container,
//          Func<SimpleContainer, object> handler)
//        {
//            container.RegisterHandler(typeof(TService), (string)null, handler);
//            return container;
//        }

//        /// <summary>
//        /// Registers all specified types in an assembly as singleton in the container.
//        /// </summary>
//        /// <typeparam name="TService">The type of the service.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="assembly">The assembly.</param>
//        /// <param name="filter">The type filter.</param>
//        /// <returns>The container.</returns>
//        public static SimpleContainer AllTypesOf<TService>(
//          this SimpleContainer container,
//          Assembly assembly,
//          Func<Type, bool> filter = null)
//        {
//            if (filter == null)
//                filter = (Func<Type, bool>)(type => true);
//            Type serviceType = typeof(TService);
//            foreach (TypeInfo typeInfo in assembly.DefinedTypes.Where<TypeInfo>((Func<TypeInfo, bool>)(type => serviceType.GetTypeInfo().IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface && filter(type.AsType()))))
//                container.RegisterSingleton(typeof(TService), (string)null, typeInfo.AsType());
//            return container;
//        }

//        /// <summary>Requests an instance.</summary>
//        /// <typeparam name="TService">The type of the service.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>The instance.</returns>
//        public static TService GetInstance<TService>(this SimpleContainer container, string key = null) => (TService)container.GetInstance(typeof(TService), key);

//        /// <summary>
//        /// Gets all instances of a particular type and the given key (default null).
//        /// </summary>
//        /// <typeparam name="TService">The type to resolve.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key shared by those instances</param>
//        /// <returns>The resolved instances.</returns>
//        public static IEnumerable<TService> GetAllInstances<TService>(
//          this SimpleContainer container,
//          string key = null)
//        {
//            return container.GetAllInstances(typeof(TService), key).Cast<TService>();
//        }

//        /// <summary>
//        /// Determines if a handler for the service/key has previously been registered.
//        /// </summary>
//        /// <typeparam name="TService">The service type.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>True if a handler is registere; false otherwise.</returns>
//        public static bool HasHandler<TService>(this SimpleContainer container, string key = null) => container.HasHandler(typeof(TService), key);

//        /// <summary>
//        ///   Unregisters any handlers for the service/key that have previously been registered.
//        /// </summary>
//        /// <typeparam name="TService">The service type.</typeparam>
//        /// <param name="container">The container.</param>
//        /// <param name="key">The key.</param>
//        public static void UnregisterHandler<TService>(this SimpleContainer container, string key = null) => container.UnregisterHandler(typeof(TService), key);
//    }
//}
