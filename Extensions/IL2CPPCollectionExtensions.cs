using Il2CppSystem.Linq;
using Il2CppCollections = Il2CppSystem.Collections.Generic;
using ValueType = Il2CppSystem.ValueType;

namespace BuffBar.Extensions;

public static class IL2CPPCollectionExtensions
{
    public static T Il2CppFirst<T>(this Il2CppCollections.IEnumerable<T> collection) =>
        collection.First();

    public static T Il2CppFirst<T>(this Il2CppCollections.List<T> collection) where T : ValueType =>
        (T)collection[(Index)0];

    public static T? Il2CppFirstOrDefault<T>(this Il2CppCollections.List<T> collection, Func<T, bool> predicate)
        where T : ValueType
    {
        foreach (var item in collection)
            if (predicate(item))
                return item;

        return default;
    }

    public static T Il2CppFirstOrDefault<T>(this Il2CppCollections.IEnumerable<T> collection) =>
        collection.FirstOrDefault();

    public static T Il2CppFirstOrDefault<T>(this Il2CppCollections.IEnumerable<T> collection,
        Func<T, bool> predicate) =>
        collection.FirstOrDefault(predicate);

    public static Il2CppCollections.IEnumerable<T> Il2CppWhere<T>(this Il2CppCollections.IEnumerable<T> collection,
        Func<T, bool> predicate) =>
        collection.Where(predicate);
}