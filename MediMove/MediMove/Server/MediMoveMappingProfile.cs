using AutoMapper;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server
{
    public class MediMoveMappingProfile : Profile
    {
        public MediMoveMappingProfile()
        {

            CreateMap<CreateTeamDTO, Team>();

            CreateMap<Team, TeamDTO>();

            CreateMap<Availability, AvailabilityDTO>()
                .ForMember(m => m.ParamedicFirstName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.FirstName))
                .ForMember(m => m.ParamedicLastName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.LastName));

            CreateMap<CreateAvailabilitiesDTO, IEnumerable<Availability>>()
                .ConvertUsing<AvailabilityListConverter>();

            CreateMap<Transport, TransportDTO>()
                .ForMember(m => m.PatientFirstName, c => c.MapFrom(s => s.Patient.PersonalInformation.FirstName))
                .ForMember(m => m.PatientLastName, c => c.MapFrom(s => s.Patient.PersonalInformation.LastName))
                .ForMember(m => m.PatientPhoneNumber, c => c.MapFrom(s => s.Patient.PersonalInformation.PhoneNumber))
                .ForMember(m => m.PatientStreetAddress, c => c.MapFrom(s => s.Patient.PersonalInformation.StreetAddress))
                .ForMember(m => m.PatientHouseNumber, c => c.MapFrom(s => s.Patient.PersonalInformation.HouseNumber))
                .ForMember(m => m.PatientApartmentNumber, c => c.MapFrom(s => s.Patient.PersonalInformation.ApartmentNumber))
                .ForMember(m => m.PatientPostalCode, c => c.MapFrom(s => s.Patient.PersonalInformation.PostalCode))
                .ForMember(m => m.PatientCity, c => c.MapFrom(s => s.Patient.PersonalInformation.City))
                .ForMember(m => m.PatientWeight, c => c.MapFrom(s => s.Patient.Weight));

            CreateMap<CreateTransportDTO, Transport>();

            CreateMap<Patient, PatientDTO>()
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName))
                .ForMember(m => m.StreetAddress, c => c.MapFrom(s => s.PersonalInformation.StreetAddress))
                .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.PersonalInformation.HouseNumber))
                .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.PersonalInformation.ApartmentNumber))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.PersonalInformation.PostalCode))
                .ForMember(m => m.City, c => c.MapFrom(s => s.PersonalInformation.City))
                .ForMember(m => m.StateProvince, c => c.MapFrom(s => s.PersonalInformation.StateProvince))
                .ForMember(m => m.Country, c => c.MapFrom(s => s.PersonalInformation.Country));

            CreateMap<CreatePatientDTO, Patient>()
                .ConvertUsing<PatientConverter>();

            //CreateMap<CreatePatientDTO, Patient>()
            //    .ForMember(m => m.PersonalInformation.FirstName, c => c.MapFrom(s => s.FirstName))
            //    .ForMember(m => m.PersonalInformation.LastName, c => c.MapFrom(s => s.LastName))
            //    .ForMember(m => m.PersonalInformation.StreetAddress, c => c.MapFrom(s => s.StreetAddress))
            //    .ForMember(m => m.PersonalInformation.HouseNumber, c => c.MapFrom(s => s.HouseNumber))
            //    .ForMember(m => m.PersonalInformation.ApartmentNumber, c => c.MapFrom(s => s.ApartmentNumber))
            //    .ForMember(m => m.PersonalInformation.PostalCode, c => c.MapFrom(s => s.PostalCode))
            //    .ForMember(m => m.PersonalInformation.StateProvince, c => c.MapFrom(s => s.StateProvince))
            //    .ForMember(m => m.PersonalInformation.City, c => c.MapFrom(s => s.City))
            //    .ForMember(m => m.PersonalInformation.Country, c => c.MapFrom(s => s.Country))
            //    .ForMember(m => m.PersonalInformation.PhoneNumber, c => c.MapFrom(s => s.PhoneNumber));











            //CreateMap<Patient, SelectPatientDTO>()
            //    //.ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
            //    .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
            //    .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName))
            //    .ForMember(m => m.StreetAddress, c => c.MapFrom(s => s.PersonalInformation.StreetAddress))
            //    .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.PersonalInformation.HouseNumber))
            //    .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.PersonalInformation.ApartmentNumber))
            //    .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.PersonalInformation.PostalCode))
            //    .ForMember(m => m.StateProvince, c => c.MapFrom(s => s.PersonalInformation.StateProvince))
            //    .ForMember(m => m.City, c => c.MapFrom(s => s.PersonalInformation.City))
            //    .ForMember(m => m.PhoneNumber, c => c.MapFrom(s => s.PersonalInformation.PhoneNumber));

            //CreateMap<Paramedic, ParamedicsForShiftDTO>()
            //.ForMember(m => m.Paramedics, c => c.MapFrom(s => new List<(int, string, string, bool)>
            //{

            //    (s.Id, s.PersonalInformation.FirstName, s.PersonalInformation.LastName, s.IsDriver)
            //}));

            //CreateMap<Paramedic, ParamedicDTO>()
            //    //.ForMember(m => m.PayPerHour, c => c.MapFrom(s => s.Rates.First())) // Jak ma działac data w rate?
            //    .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
            //    .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName))
            //    .ForMember(m => m.StreetAddress, c => c.MapFrom(s => s.PersonalInformation.StreetAddress))
            //    .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.PersonalInformation.HouseNumber))
            //    .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.PersonalInformation.ApartmentNumber))
            //    .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.PersonalInformation.PostalCode))
            //    .ForMember(m => m.StateProvince, c => c.MapFrom(s => s.PersonalInformation.StateProvince))
            //    .ForMember(m => m.City, c => c.MapFrom(s => s.PersonalInformation.City))
            //    .ForMember(m => m.Country, c => c.MapFrom(s => s.PersonalInformation.Country));

            //CreateMap<Dispatcher,DispatcherDTO>()
            //    //Dodać mapowanie Salary
            //    .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
            //    .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName))
            //    .ForMember(m => m.StreetAddress, c => c.MapFrom(s => s.PersonalInformation.StreetAddress))
            //    .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.PersonalInformation.HouseNumber))
            //    .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.PersonalInformation.ApartmentNumber))
            //    .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.PersonalInformation.PostalCode))
            //    .ForMember(m => m.StateProvince, c => c.MapFrom(s => s.PersonalInformation.StateProvince))
            //    .ForMember(m => m.City, c => c.MapFrom(s => s.PersonalInformation.City))
            //    .ForMember(m => m.Country, c => c.MapFrom(s => s.PersonalInformation.Country));





            

            CreateMap<RegisterUserDTO, User>();



        }
    }

    public class AvailabilityListConverter : ITypeConverter<CreateAvailabilitiesDTO, IEnumerable<Availability>>
    {
        public IEnumerable<Availability> Convert(CreateAvailabilitiesDTO source, IEnumerable<Availability> destination, ResolutionContext context)
        {
            var availabilities = new List<Availability>();

            for (int i = 0; i < source.Days.Count(); i++)
            {
                var availability = new Availability
                {
                    Day = source.Days.ElementAt(i),
                    ShiftType = source.ShiftTypes.ElementAt(i),
                    ParamedicId = source.ParamedicId
                };

                availabilities.Add(availability);
            }

            return availabilities;
        }
    }

    public class PatientConverter : ITypeConverter<CreatePatientDTO, Patient>
    {
        public Patient Convert(CreatePatientDTO source, Patient destination, ResolutionContext context)
        {
            var patient = new Patient();

            var personalInformation = new PersonalInformation()
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                StreetAddress = source.StreetAddress,
                HouseNumber = source.HouseNumber,
                ApartmentNumber = source.ApartmentNumber,
                City = source.City,
                PostalCode = source.PostalCode,
                StateProvince = source.StateProvince,
                Country = source.Country,
                PhoneNumber = source.PhoneNumber
            };
            patient.Weight = source.Weight;
            patient.PersonalInformation = personalInformation;
            return patient;

        }
    }
}
