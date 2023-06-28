//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace Caliburn.Micro
//{
//    /// <summary>A simple IoC container.</summary>
//    public class SimpleContainer
//    {
//        private static readonly Type delegateType = typeof(Delegate);
//        private static readonly Type enumerableType = typeof(IEnumerable);
//        private static readonly TypeInfo enumerableTypeInfo = SimpleContainer.enumerableType.GetTypeInfo();
//        private static readonly TypeInfo delegateTypeInfo = SimpleContainer.delegateType.GetTypeInfo();
//        private Type simpleContainerType = typeof(SimpleContainer);
//        private readonly List<SimpleContainer.ContainerEntry> entries;

//        /// <summary>
//        ///   Initializes a new instance of the <see cref="T:Caliburn.Micro.SimpleContainer" /> class.
//        /// </summary>
//        public SimpleContainer() => this.entries = new List<SimpleContainer.ContainerEntry>();

//        private SimpleContainer(
//          IEnumerable<SimpleContainer.ContainerEntry> entries)
//        {
//            this.entries = new List<SimpleContainer.ContainerEntry>(entries);
//        }

//        /// <summary>
//        /// Whether to enable recursive property injection for all resolutions.
//        /// </summary>
//        public bool EnablePropertyInjection { get; set; }

//        /// <summary>Registers the instance.</summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="implementation">The implementation.</param>
//        public void RegisterInstance(Type service, string key, object implementation) => this.RegisterHandler(service, key, (Func<SimpleContainer, object>)(container => implementation));

//        /// <summary>
//        ///   Registers the class so that a new instance is created on every request.
//        /// </summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="implementation">The implementation.</param>
//        public void RegisterPerRequest(Type service, string key, Type implementation) => this.RegisterHandler(service, key, (Func<SimpleContainer, object>)(container => container.BuildInstance(implementation)));

//        /// <summary>
//        ///   Registers the class so that it is created once, on first request, and the same instance is returned to all requestors thereafter.
//        /// </summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="implementation">The implementation.</param>
//        public void RegisterSingleton(Type service, string key, Type implementation)
//        {
//            object singleton = (object)null;
//            this.RegisterHandler(service, key, (Func<SimpleContainer, object>)(container => singleton ?? (singleton = container.BuildInstance(implementation))));
//        }

//        /// <summary>
//        ///   Registers a custom handler for serving requests from the container.
//        /// </summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        /// <param name="handler">The handler.</param>
//        public void RegisterHandler(Type service, string key, Func<SimpleContainer, object> handler) => this.GetOrCreateEntry(service, key).Add(handler);

//        /// <summary>
//        ///   Unregisters any handlers for the service/key that have previously been registered.
//        /// </summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        public void UnregisterHandler(Type service, string key)
//        {
//            SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
//            if (entry == null)
//                return;
//            this.entries.Remove(entry);
//        }

//        /// <summary>Requests an instance.</summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>The instance, or null if a handler is not found.</returns>
//        public object GetInstance(Type service, string key)
//        {
//            SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
//            if (entry != null)
//            {
//                object instance = entry.Single<Func<SimpleContainer, object>>()(this);
//                if (this.EnablePropertyInjection && instance != null)
//                    this.BuildUp(instance);
//                return instance;
//            }
//            if (service == (Type)null)
//                return (object)null;
//            TypeInfo typeInfo = service.GetTypeInfo();
//            if (SimpleContainer.delegateTypeInfo.IsAssignableFrom(typeInfo))
//            {
//                Type type = typeof(SimpleContainer.FactoryFactory<>).MakeGenericType(typeInfo.GenericTypeArguments[0]);
//                object instance = Activator.CreateInstance(type);
//                return type.GetRuntimeMethod("Create", new Type[1]
//                {
//          this.simpleContainerType
//                }).Invoke(instance, new object[1] { (object)this });
//            }
//            if (!SimpleContainer.enumerableTypeInfo.IsAssignableFrom(typeInfo) || !typeInfo.IsGenericType)
//                return (object)null;
//            Type genericTypeArgument = typeInfo.GenericTypeArguments[0];
//            List<object> list = this.GetAllInstances(genericTypeArgument).ToList<object>();
//            Array instance1 = Array.CreateInstance(genericTypeArgument, list.Count);
//            for (int index = 0; index < instance1.Length; ++index)
//            {
//                if (this.EnablePropertyInjection)
//                    this.BuildUp(list[index]);
//                instance1.SetValue(list[index], index);
//            }
//            return (object)instance1;
//        }

//        /// <summary>
//        /// Determines if a handler for the service/key has previously been registered.
//        /// </summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key.</param>
//        /// <returns>True if a handler is registere; false otherwise.</returns>
//        public bool HasHandler(Type service, string key) => this.GetEntry(service, key) != null;

//        /// <summary>
//        ///   Requests all instances of a given type and the given key (default null).
//        /// </summary>
//        /// <param name="service">The service.</param>
//        /// <param name="key">The key shared by those instances</param>
//        /// <returns>All the instances or an empty enumerable if none are found.</returns>
//        public IEnumerable<object> GetAllInstances(Type service, string key = null)
//        {
//            SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
//            if (entry == null)
//                return (IEnumerable<object>)new object[0];
//            IEnumerable<object> allInstances = entry.Select<Func<SimpleContainer, object>, object>((Func<Func<SimpleContainer, object>, object>)(e => e(this)));
//            foreach (object instance in allInstances)
//            {
//                if (this.EnablePropertyInjection && instance != null)
//                    this.BuildUp(instance);
//            }
//            return allInstances;
//        }

//        /// <summary>
//        ///   Pushes dependencies into an existing instance based on interface properties with setters.
//        /// </summary>
//        /// <param name="instance">The instance.</param>
//        public void BuildUp(object instance)
//        {
//            foreach (PropertyInfo propertyInfo in instance.GetType().GetRuntimeProperties().Where<PropertyInfo>((Func<PropertyInfo, bool>)(p => p.CanRead && p.CanWrite && p.PropertyType.GetTypeInfo().IsInterface)))
//            {
//                object instance1 = this.GetInstance(propertyInfo.PropertyType, (string)null);
//                if (instance1 != null)
//                    propertyInfo.SetValue(instance, instance1, (object[])null);
//            }
//        }

//        /// <summary>Creates a child container.</summary>
//        /// <returns>A new container.</returns>
//        public SimpleContainer CreateChildContainer() => new SimpleContainer((IEnumerable<SimpleContainer.ContainerEntry>)this.entries);

//        private SimpleContainer.ContainerEntry GetOrCreateEntry(Type service, string key)
//        {
//            SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
//            if (entry == null)
//            {
//                entry = new SimpleContainer.ContainerEntry()
//                {
//                    Service = service,
//                    Key = key
//                };
//                this.entries.Add(entry);
//            }
//            return entry;
//        }

//        private SimpleContainer.ContainerEntry GetEntry(Type service, string key)
//        {
//            if (service == (Type)null)
//                return this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>)(x => x.Key == key));
//            return key == null ? this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>)(x => x.Service == service && string.IsNullOrEmpty(x.Key))) ?? this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>)(x => x.Service == service)) : this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>)(x => x.Service == service && x.Key == key));
//        }

//        /// <summary>
//        ///   Actually does the work of creating the instance and satisfying it's constructor dependencies.
//        /// </summary>
//        /// <param name="type">The type.</param>
//        /// <returns></returns>
//        protected object BuildInstance(Type type)
//        {
//            object[] constructorArgs = this.DetermineConstructorArgs(type);
//            return this.ActivateInstance(type, constructorArgs);
//        }

//        /// <summary>
//        ///   Creates an instance of the type with the specified constructor arguments.
//        /// </summary>
//        /// <param name="type">The type.</param>
//        /// <param name="args">The constructor args.</param>
//        /// <returns>The created instance.</returns>
//        protected virtual object ActivateInstance(Type type, object[] args)
//        {
//            object obj = args.Length != 0 ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
//            this.Activated(obj);
//            return obj;
//        }

//        /// <summary>Occurs when a new instance is created.</summary>
//        public event Action<object> Activated = _param1 => { };

//        private object[] DetermineConstructorArgs(Type implementation)
//        {
//            List<object> objectList = new List<object>();
//            ConstructorInfo constructorInfo = this.SelectEligibleConstructor(implementation);
//            if (constructorInfo != (ConstructorInfo)null)
//                objectList.AddRange(((IEnumerable<ParameterInfo>)constructorInfo.GetParameters()).Select<ParameterInfo, object>((Func<ParameterInfo, object>)(info => this.GetInstance(info.ParameterType, (string)null))));
//            return objectList.ToArray();
//        }

//        private ConstructorInfo SelectEligibleConstructor(Type type) => type.GetTypeInfo().DeclaredConstructors.Where<ConstructorInfo>((Func<ConstructorInfo, bool>)(c => c.IsPublic)).Select(c => new
//        {
//            Constructor = c,
//            HandledParamters = ((IEnumerable<ParameterInfo>)c.GetParameters()).Count<ParameterInfo>((Func<ParameterInfo, bool>)(p => this.HasHandler(p.ParameterType, (string)null)))
//        }).OrderByDescending(c => c.HandledParamters).Select(c => c.Constructor).FirstOrDefault<ConstructorInfo>();

//        private class ContainerEntry : List<Func<SimpleContainer, object>>
//        {
//            public string Key;
//            public Type Service;
//        }

//        private class FactoryFactory<T>
//        {
//            public Func<T> Create(SimpleContainer container) => (Func<T>)(() => (T)container.GetInstance(typeof(T), (string)null));
//        }
//    }
//}