using System.Reflection;

namespace Sample.Domain.Primitives;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> 
    where TEnum : Enumeration<TEnum>
{
    private static readonly Lazy<Dictionary<int, TEnum>> EnumerationsDictionary = new(() => CreateEnumerationDictionary(typeof(TEnum)));

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; protected init; }

    public string Name { get; protected set; }

    public static bool operator ==(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
    {
        if (left is null && right is null)
        {
            return false;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
    {
        return !(left == right);
    }

    public static IReadOnlyCollection<TEnum> GetValues()
    {
        return EnumerationsDictionary.Value.Values.ToList();
    }

    public static TEnum? FromId(int id)
    {
        return EnumerationsDictionary.Value.TryGetValue(id, out TEnum? enumeration) ? enumeration : null;
    }

    public static TEnum? FromName(string name)
    {
        return EnumerationsDictionary.Value.Values.FirstOrDefault(x => x.Name == name);
    }

    public static bool Contains(int id)
    {
        return EnumerationsDictionary.Value.ContainsKey(id);
    }
    
    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && other.Id.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        return obj is Enumeration<TEnum> otherValue && otherValue.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 34;
    }

    private static Dictionary<int, TEnum> CreateEnumerationDictionary(Type enumType)
    {
        return GetFieldsForType(enumType).ToDictionary(tEnum => tEnum.Id);
    }

    private static IEnumerable<TEnum> GetFieldsForType(Type enumType)
    {
        return enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
    }
}