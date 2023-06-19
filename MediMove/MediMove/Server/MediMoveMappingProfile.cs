using AutoMapper;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Shared.Models.DTOs;
using System;

namespace MediMove.Server
{
    public class MediMoveMappingProfile : Profile
    {
        public MediMoveMappingProfile()
        {
            CreateMap<Team, TeamDTO>();

            CreateMap<CreateAvailabilitiesCommand, Availability[]>()
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
                .ForMember(m => m.PatientWeight, c => c.MapFrom(s => s.Patient.Weight))
                .ForMember(m => m.StartLocation, c => c.MapFrom(s => s.StartLocation))
                .ForMember(m => m.ReturnLocation, c => c.MapFrom(s => s.ReturnLocation))
                .ForMember(m => m.Note, c => c.MapFrom(s => s.Note));

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

            CreateMap<CreatePatientRequest, Patient>()
                .ConvertUsing<PatientConverter>();

            CreateMap<CreateTransportWithBillingDTO, Transport>()
                .ConvertUsing<TransportWithBillingConverter>();


            CreateMap<RegisterUserCommand, User>();

            CreateMap<Team, GetTeamsByDateAndShiftResponse.TeamInfo>()
                .ForMember(m => m.DriverFirstName, c => c.MapFrom(s => s.Driver.PersonalInformation.FirstName))
                .ForMember(m => m.DriverLastName, c => c.MapFrom(s => s.Driver.PersonalInformation.LastName))
                .ForMember(m => m.ParamedicFirstName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.FirstName))
                .ForMember(m => m.ParamedicLastName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.LastName));

            CreateMap<CreateTeamsCommand, Team[]>()
                .ConvertUsing<CreateTeamsCommandToTeamIEnumerableConverter>();

            CreateMap<ParamedicDTO, Paramedic>()
                .ForPath(m => m.PersonalInformation.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForPath(m => m.PersonalInformation.LastName, c => c.MapFrom(s => s.LastName))
                .ForPath(m => m.PersonalInformation.StreetAddress, c => c.MapFrom(s => s.StreetAddress))
                .ForPath(m => m.PersonalInformation.HouseNumber, c => c.MapFrom(s => s.HouseNumber))
                .ForPath(m => m.PersonalInformation.ApartmentNumber, c => c.MapFrom(s => s.ApartmentNumber))
                .ForPath(m => m.PersonalInformation.PostalCode, c => c.MapFrom(s => s.PostalCode))
                .ForPath(m => m.PersonalInformation.City, c => c.MapFrom(s => s.City))
                .ForPath(m => m.PersonalInformation.StateProvince, c => c.MapFrom(s => s.StateProvince))
                .ForPath(m => m.PersonalInformation.Country, c => c.MapFrom(s => s.Country))
                .ForPath(m => m.PersonalInformation.PhoneNumber, c => c.MapFrom(s => s.PhoneNumber));

            CreateMap<DispatcherDTO, Dispatcher>()
                .ForPath(m => m.PersonalInformation.FirstName, c => c.MapFrom(s => s.FirstName))
                .ForPath(m => m.PersonalInformation.LastName, c => c.MapFrom(s => s.LastName))
                .ForPath(m => m.PersonalInformation.StreetAddress, c => c.MapFrom(s => s.StreetAddress))
                .ForPath(m => m.PersonalInformation.HouseNumber, c => c.MapFrom(s => s.HouseNumber))
                .ForPath(m => m.PersonalInformation.ApartmentNumber, c => c.MapFrom(s => s.ApartmentNumber))
                .ForPath(m => m.PersonalInformation.PostalCode, c => c.MapFrom(s => s.PostalCode))
                .ForPath(m => m.PersonalInformation.City, c => c.MapFrom(s => s.City))
                .ForPath(m => m.PersonalInformation.StateProvince, c => c.MapFrom(s => s.StateProvince))
                .ForPath(m => m.PersonalInformation.Country, c => c.MapFrom(s => s.Country))
                .ForPath(m => m.PersonalInformation.PhoneNumber, c => c.MapFrom(s => s.PhoneNumber));

            CreateMap<RegisterParamedicRequest, Paramedic>();
            CreateMap<RegisterDispatcherRequest, Dispatcher>();

            CreateMap<RegisterParamedicRequest, PersonalInformation>();
            CreateMap<RegisterDispatcherRequest, PersonalInformation>();
        }
    }


    /// <summary>
    /// Converter from CreateTeamsCommand to array of teams
    /// </summary>
    public class CreateTeamsCommandToTeamIEnumerableConverter : ITypeConverter<CreateTeamsCommand, Team[]>
    {
        /// <summary>
        /// Method that converts CreateTeamsCommand to array of teams
        /// </summary>
        /// <param name="source">CreateTeamsCommand</param>
        /// <param name="destination">array of teams</param>
        /// <param name="context">ResolutionContext</param>
        /// <returns>array of teams</returns>
        public Team[] Convert(CreateTeamsCommand source, Team[] destination, ResolutionContext context)
        {
            var teams = new Team[source.Request.Teams.Count];
            int i = 0;
            foreach (var team in source.Request.Teams)
            {
                teams[i] = new Team
                {
                    DriverId = team.DriverId,
                    ParamedicId = team.ParamedicId,
                    Day = source.Request.Date.Date,
                    ShiftType = source.Request.Shift

                };
                i++;
            }
                
            return teams;
        }
    }

    /// <summary>
    /// Converter for CreateAvailabilitiesCommand to availabilities array
    /// </summary>
    public class CreateAvailabilitiesCommandToAvailabilityListConverter : ITypeConverter<CreateAvailabilitiesCommand, Availability[]>
    {
        /// <summary>
        /// Method that converts CreateAvailabilitiesCommand to availabilities array
        /// </summary>
        /// <param name="source">CreateAvailabilitiesCommand object</param>
        /// <param name="destination">array of availabilities</param>
        /// <param name="context">ResolutionContext object</param>
        /// <returns>array of availabilities</returns>
        public Availability[] Convert(CreateAvailabilitiesCommand source, Availability[] destination, ResolutionContext context)
        {
            var availabilities = new Availability[source.Request.Availabilities.Count];
            int i = 0;
            foreach (var declaration in source.Request.Availabilities)
            {
                availabilities[i] = new Availability
                {
                    ParamedicId = source.ParamedicId,
                    Day = declaration.Key,
                    ShiftType = declaration.Value
                };
                i++;
            }

            return availabilities;
        }
    }

    public class PatientConverter : ITypeConverter<CreatePatientRequest, Patient>
    {
        public Patient Convert(CreatePatientRequest source, Patient destination, ResolutionContext context)
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
                StartLocation = source.StartLocation,
                ReturnLocation = source.ReturnLocation,
                Note = source.Note
            };




            return transport;

        }
    }
}
