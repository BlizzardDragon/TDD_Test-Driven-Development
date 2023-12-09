using System.Linq;
using VContainer;

namespace FrameworkUnity.OOP.VContainer.Extensions
{
    public static class ObjectResolverExtensions
    {
        public static T CreateInstance<T>(this IObjectResolver objectResolver)
        {
            var type = typeof(T);
            var constructor = type.GetConstructors().FirstOrDefault();

            if (constructor is null)
            {
                throw new VContainerException(type, "Failed to find suitable constructor!");
            }

            var parametrInfos = constructor.GetParameters();
            var parameters = new object[parametrInfos.Length];

            for (int i = 0; i < parametrInfos.Length; i++)
            {
                parameters[i] = objectResolver.Resolve(parametrInfos[i].ParameterType);
            }

            var instance = (T)constructor.Invoke(parameters);
            objectResolver.Inject(instance);

            return instance;
        }
    }
}
