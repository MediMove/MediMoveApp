namespace MediMove.Server.Application.Shared
{
    /// <summary>
    /// Converter for PersonalInformation to Location.
    /// </summary>
    public static class PersonalInformationToLocationConverter
    {
        public static string Convert(string houseNumber, int? apartmentNumber, string streetAddress, string postalCode, string city) =>
            $"{streetAddress} {houseNumber}{(apartmentNumber.HasValue ? $"/{apartmentNumber}" : "" )}, {postalCode}, {city}";
    }
}
