using Flunt.Validations;

namespace PayRight.Shared.Utils.Extentions;

public static class FluntExtentions
{
    public static Contract<T> IsNotContainSpace<T>(this Contract<T> contract, string? val, string key, string message)
    {
        if (val != null && val.Any(char.IsWhiteSpace))
            contract.AddNotification(key, message);
        return contract;
    }
    
    public static Contract<T> PasswordStrength<T>(this Contract<T> contract, string? val, string key, string message)
    {
        if (val != null && 
            ( !val.Any(char.IsDigit)
            || !val.Any(char.IsUpper)
            || !val.Any(char.IsLower)
            || val.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) == -1 )
            )
            contract.AddNotification(key, message);
        return contract;
    }

    public static Contract<T> LengthInBetween<T>(this Contract<T> contract, string? val, int start, int end, string key,
        string message)
    {
        if (val != null && ( val.Length < start || val.Length > end))
            contract.AddNotification(key, message);

        return contract;
    }

    public static Contract<T> IsGuidNotEmpty<T>(this Contract<T> contract, Guid guid, string key, string message)
    {
        if (guid == Guid.Empty)
            contract.AddNotification(key, message);

        return contract;
    }
    
    public static Contract<T> IsGreaterOrEqualsThan<T>(this Contract<T> contract, DateOnly val, DateOnly comparer, string key, string message)
    {
        if (val < comparer)
            contract.AddNotification(key, message);
        
        return contract;
    }
}