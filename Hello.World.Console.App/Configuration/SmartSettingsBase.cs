using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Hello.World.ConsoleApp.Interfaces;

namespace Hello.World.ConsoleApp.Configuration
{
    /// <summary>
    /// Encapsulates reading values in a strongly-typed manner from a configuration store.
    /// </summary>
    public abstract class SmartSettingsBase
    {
        /// <summary>
        /// If true, an exception will be thrown if a key is not found. Otherwise, getter methods will return the default for the given data type.
        /// </summary>
        public bool ThrowExceptionOnKeyNotFound { get; set; } = true;

        /// <summary>
        /// Used to cache values for any given key.
        /// </summary>
        private readonly ConcurrentDictionary<string, object> _valueDictionary = new ConcurrentDictionary<string, object>();

        private IConfigurationLookup _configurationLookup;

        /// <summary>
        /// The configuration store facade interface that is used to get configuration values.
        /// </summary>
        public IConfigurationLookup ConfigurationLookup => _configurationLookup ?? (_configurationLookup = DefaultConfigurationLookup.Default);

        /// <summary>
        /// Registers a custom <see cref="TypeConverter"/>.
        /// </summary>
        /// <typeparam name="T">The type which will be converted.</typeparam>
        /// <typeparam name="TTypeConverter">Type of the custom type converter.</typeparam>
        public static void RegisterTypeConverter<T, TTypeConverter>() where TTypeConverter : TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TTypeConverter)));
        }

        /// <summary>
        /// Creates a new <see cref="SmartSettingsBase"/> instance, using the default configuration store which reads values from the App.Config or Web.Config file for the solution.
        /// </summary>
        protected SmartSettingsBase()
        { }

        /// <summary>
        /// Creates a new <see cref="SmartSettingsBase"/> instance, using the specified configuration store. 
        /// </summary>
        /// <param name="configurationLookup">Configuration store to read values.</param>
        protected SmartSettingsBase(IConfigurationLookup configurationLookup)
            : this()
        {
            _configurationLookup = configurationLookup ?? throw new ArgumentNullException(nameof(configurationLookup));
        }

        /// <summary>
        /// Gets a value using a strongly-typed lambda expression. The key for the value, as passed to the underlying configuration store
        /// is of form "(truncated interface name)_(property name)". For example: "Caching_CacheExpiration".
        /// The first time this method is invoked for a given interface/property the value is retrieved from the underlying configuration store
        /// and then cached. Subsequent calls for that property are retrieved from cache.
        /// </summary>
        /// <typeparam name="TInterface">Type of the settings interface for the property.</typeparam>
        /// <typeparam name="TProperty">Type of the property for the value to be retrieved from the underlying configuration store.</typeparam>
        /// <param name="propertyLambda">Lambda expression that specifies the property whose value should be retrieved from the configuration store.</param>
        /// <returns>Property value from the underlying configuration store.</returns>
        protected internal TProperty GetValueWithCaching<TInterface, TProperty>(Expression<Func<TInterface, TProperty>> propertyLambda)
        {
            var interfaceType = typeof(TInterface);
            var propertyInfo = GetPropertyInfoFromLambda(propertyLambda);
            // For ease of maintenance, use a string key with format "MyNamespace.MyInterface.MyProperty".
            var key = $"{interfaceType.FullName}.{propertyInfo.Name}";
            return (TProperty)_valueDictionary.GetOrAdd(key, po => GetValue<TProperty>(interfaceType, propertyInfo));
        }

        /// <summary>
        /// Gets a value from the underlying configuration store using a strongly-typed lambda expression. The key for the value, as passed to the underlying configuration store
        /// is of form "(truncated interface name)_(property name)". For example: "Caching_CacheExpiration".
        /// </summary>
        /// <typeparam name="TInterface">Type of the settings interface for the property.</typeparam>
        /// <typeparam name="TProperty">Type of the property for the value to be retrieved from the underlying configuration store.</typeparam>
        /// <param name="propertyLambda">Lambda expression that specifies the property whose value should be retrieved from the configuration store.</param>
        /// <returns>Property value from the underlying configuration store.</returns>
        protected internal TProperty GetValue<TInterface, TProperty>(Expression<Func<TInterface, TProperty>> propertyLambda)       // Marked as internal so we can unit test this method.
        {
            var interfaceType = typeof(TInterface);
            var propertyInfo = GetPropertyInfoFromLambda(propertyLambda);
            return GetValue<TProperty>(interfaceType, propertyInfo);
        }

        private TProperty GetValue<TProperty>(Type interfaceType, PropertyInfo propertyInfo)
        {
            // Remove the leading "I" if the interface type is an actual interface.
            var interfaceName = (interfaceType.IsInterface && interfaceType.Name.StartsWith("I")) ? interfaceType.Name.Substring(1) : interfaceType.Name;
            // Remove the word "Settings" from the end of the name as well.
            if (interfaceName.EndsWith("Settings"))
            {
                interfaceName = interfaceName.Substring(0, interfaceName.Length - 8);
            }
            var propertyName = propertyInfo.Name;
            var propertyType = propertyInfo.PropertyType;
            if (propertyType != typeof(TProperty))
            {
                throw new InvalidOperationException();
            }
            var keyName = $"{interfaceName}_{propertyName}";
            var setting = ConfigurationLookup[keyName];
            if (ThrowExceptionOnKeyNotFound && ReferenceEquals(null, setting))
            {
                throw new InvalidOperationException($"Value with key '{keyName}' was not found in the config or is otherwise invalid.");
            }
            if (propertyType == typeof(string))
            {
                return (TProperty)((object)setting);
            }

            if (string.IsNullOrWhiteSpace(setting))
            {
                return default(TProperty);
            }
            var parseMethod = propertyType.GetMethod("Parse", new[] { typeof(string) });

            if (parseMethod != null)
            {
                return (TProperty)parseMethod.Invoke(null, new object[] { setting });
            }

            return (TProperty)ChangeType(propertyType, setting);
        }

        private PropertyInfo GetPropertyInfoFromLambda<TInterface, TProperty>(Expression<Func<TInterface, TProperty>> propertyLambda)
        {
            var interfaceType = typeof(TInterface);

            if (!(propertyLambda.Body is MemberExpression member))
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
            }

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            }

            if (propertyInfo.ReflectedType != null && interfaceType != propertyInfo.ReflectedType && !propertyInfo.ReflectedType.IsAssignableFrom(interfaceType))
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a property that is not from type {interfaceType}.");
            }
            return propertyInfo;
        }

        private static T ChangeType<T>(object value) => (T)ChangeType(typeof(T), value);

        private static object ChangeType(Type t, object value)
        {
            var tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }
    }
}