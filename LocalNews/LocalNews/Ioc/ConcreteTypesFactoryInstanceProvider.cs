using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LocalNews.Helpers;
using Ninject.Extensions.Factory;
using Ninject.Parameters;
using Xamarin.Forms.Internals;

namespace LocalNews.Ioc
{
    public sealed class ConcreteTypesFactoryInstanceProvider<T> : StandardInstanceProvider
    {
        private readonly List<string> _constructorArgumentNames;

        public ConcreteTypesFactoryInstanceProvider()
        {
            var methods = typeof(T).GetRuntimeMethods().Materialize();
            if (methods.Count != 1)
            {
                throw new ArgumentException("Supplied factory interface must contain exactly one method returning concrete class", "<T>");
            }

            var methodInfo = methods[0];
            var returnType = methodInfo.ReturnType;
            var constructors = returnType.GetTypeInfo().DeclaredConstructors.Materialize();
            if (constructors.Count != 1)
            {
                throw new ArgumentException("The only method of supplied factory interface must return a type with exactly one public constructor", "<T>");
            }

            var constructorInfo = constructors[0];
            var constructorParams = constructorInfo.GetParameters().ToList();
            _constructorArgumentNames = new List<string>(constructorParams.Count);
            var methodParams = methodInfo.GetParameters();

            foreach (var methodParam in methodParams)
            {
                var matchedCtorParam = FindMatchedCtorParameter(constructorParams, methodParam, constructorInfo);
                constructorParams.Remove(matchedCtorParam);
                _constructorArgumentNames.Add(matchedCtorParam.Name);
            }
        }

        private static ParameterInfo FindMatchedCtorParameter(
            IEnumerable<ParameterInfo> constructorParams,
            ParameterInfo methodParam,
            ConstructorInfo constructorInfo)
        {
            var matchedCtorParameters = constructorParams.Where(
                ctorParam => ReflectionExtensions.IsAssignableFrom(ctorParam.ParameterType, methodParam.ParameterType)).ToList();

            if (!matchedCtorParameters.Any())
            {
                throw new ArgumentException(
                    string.Format("Could not match constructor parameter of '{0}' for parameter '{1}'", constructorInfo,
                        methodParam), "<T>");
            }

            if (matchedCtorParameters.Count > 1)
            {
                throw new ArgumentException(
                    string.Format("Too many matched constructor parameters of '{0}' for parameter '{1}'",
                        constructorInfo, methodParam), "<T>");
            }

            return matchedCtorParameters.Single();
        }

        private IConstructorArgument[] GetConstructorArguments(MethodInfo methodInfo, object[] arguments)
        {
            return _constructorArgumentNames.Select((name, i) => new ConstructorArgument(name, arguments[i])).ToArray();
        }
    }
}