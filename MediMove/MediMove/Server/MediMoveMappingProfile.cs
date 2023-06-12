using AutoMapper;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server
{
    public class MediMoveMappingProfile : Profile
    {
        public MediMoveMappingProfile()
        {

            


            CreateMap<Team, TeamDTO>();

            CreateMap<CreateAvailabilitiesCommand, IEnumerable<Availability>>()
                .ConvertUsing<CreateAvailabilitiesCommandToAvailabilityListConverter>();

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

            CreateMap<CreateTransportWithBillingDTO, Transport>()
                .ConvertUsing<TransportWithBillingConverter>();

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

            CreateMap<Team, GetTeamsByDayAndShiftResponse.TeamInfo>()
                .ForMember(m => m.DriverFirstName, c => c.MapFrom(s => s.Driver.PersonalInformation.FirstName))
                .ForMember(m => m.DriverLastName, c => c.MapFrom(s => s.Driver.PersonalInformation.LastName))
                .ForMember(m => m.ParamedicFirstName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.FirstName))
                .ForMember(m => m.ParamedicLastName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.LastName));

            CreateMap<CreateTeamsCommand, IEnumerable<Team>>()
                .ConvertUsing<CreateTeamsCommandToTeamIEnumerableConverter>();
        }
    }


    /// <summary>
    /// Converter from CreateTeamsCommand to IEnumerable of Teams
    /// </summary>
    public class CreateTeamsCommandToTeamIEnumerableConverter : ITypeConverter<CreateTeamsCommand, IEnumerable<Team>>
    {
        /// <summary>
        /// Method that converts CreateTeamsCommand to IEnumerable of Teams
        /// </summary>
        /// <param name="source">CreateTeamsCommand</param>
        /// <param name="destination">IEnumerable of Teams</param>
        /// <param name="context">ResolutionContext</param>
        /// <returns></returns>
        public IEnumerable<Team> Convert(CreateTeamsCommand source, IEnumerable<Team> destination, ResolutionContext context)
        {
            var teams = new List<Team>();

            foreach (var team in source.Request.Teams)
                teams.Add(new Team
                {
                    DriverId = team.DriverId,
                    ParamedicId = team.ParamedicId,
                    Day = source.Request.Day.Date
                });

            return teams;
        }
    }

    /// <summary>
    /// Converter for CreateAvailabilitiesCommand to IEnumerable of Availabilities
    /// </summary>
    public class CreateAvailabilitiesCommandToAvailabilityListConverter : ITypeConverter<CreateAvailabilitiesCommand, IEnumerable<Availability>>
    {
        /// <summary>
        /// Method that converts CreateAvailabilitiesCommand to IEnumerable of Availabilities
        /// </summary>
        /// <param name="source">CreateAvailabilitiesCommand object</param>
        /// <param name="destination">IEnumerable of Availabilities</param>
        /// <param name="context">ResolutionContext object</param>
        /// <returns>IEnumerable of Availabilities</returns>
        public IEnumerable<Availability> Convert(CreateAvailabilitiesCommand source, IEnumerable<Availability> destination, ResolutionContext context)
        {
            var availabilities = new List<Availability>();

            foreach (var declaration in source.Request.Availabilities)
            {
                availabilities.Add(new Availability
                {
                    ParamedicId = source.ParamedicId,
                    Day = declaration.Day,
                    ShiftType = declaration.Shift
                });
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

    public class TransportWithBillingConverter : ITypeConverter<CreateTransportWithBillingDTO, Transport>
    {
        public Transport Convert(CreateTransportWithBillingDTO source, Transport destination, ResolutionContext context)
        {
            

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

            var billing = new Billing()
            {
                BankAccountNumber = source.BankAccountNumber,
                InvoiceDate = source.InvoiceDate,
                Cost = source.Cost,
                InvoiceNumber = source.InvoiceNumber,
                PersonalInformation = personalInformation
            };

            var transport = new Transport
            {
                Billing = billing,
                Destination = source.Destination,
                TransportType = source.TransportType,
                Financing = source.Financing,
                PatientId = source.PatientId,
                PatientPosition = source.PatientPosition,
                StartTime = source.StartTime,

            };




            return transport;

        }
    }
}
