namespace ApprovalUtilities.Utilities;

public static partial class CollectionUtilities
{
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key) =>
        map.ContainsKey(key) ? map[key] : default(TValue);
}