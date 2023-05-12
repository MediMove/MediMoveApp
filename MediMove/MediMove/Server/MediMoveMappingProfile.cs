using AutoMapper;
using MediMove.Server.Models;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server
{
    public class MediMoveMappingProfile : Profile
    {
        public MediMoveMappingProfile()
        {
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

            



                /*CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                    c => c.MapFrom(dto => new Address() 
                        { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));
                 */
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



            CreateMap<Patient, PatientNameDTO>()
                //.ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName));

            CreateMap<CreatePatientDTO, Patient>()
                .ForMember(p => p.PersonalInformation, c => c.MapFrom(dto => new PersonalInformation
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    StreetAddress = dto.StreetAddress,
                    HouseNumber = dto.HouseNumber,
                    ApartmentNumber = dto.ApartmentNumber,
                    PostalCode = dto.PostalCode,
                    StateProvince = dto.StateProvince,
                    City = dto.City,
                    Country = dto.Country,
                    PhoneNumber = dto.PhoneNumber
                }));

            CreateMap<Paramedic, ParamedicDTO>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName))
                .ForMember(m => m.StreetAddress, c => c.MapFrom(s => s.PersonalInformation.StreetAddress))
                .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.PersonalInformation.HouseNumber))
                .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.PersonalInformation.ApartmentNumber))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.PersonalInformation.PostalCode))
                .ForMember(m => m.City, c => c.MapFrom(s => s.PersonalInformation.City))
                .ForMember(m => m.StateProvince, c => c.MapFrom(s => s.PersonalInformation.StateProvince))
                .ForMember(m => m.Country, c => c.MapFrom(s => s.PersonalInformation.Country))
                .ForMember(m => m.IsDriver, c => c.MapFrom(s => s.IsDriver))
                .ForMember(m => m.BankAccountNumber, c => c.MapFrom(s => s.BankAccountNumber))
                .ForMember(m => m.PayPerHour, c => c.MapFrom(s => s.Rates.OrderByDescending(r => r.Date).First().PayPerHour));

            /*
            //CreateMap<Paramedic, ParamedicsForShiftDTO>()
            //.ForMember(m => m.Paramedics, c => c.MapFrom(s => new List<(int, string, string, bool)>
            //{

            //    (s.Id, s.PersonalInformation.FirstName, s.PersonalInformation.LastName, s.IsDriver)
            //}));

            

            CreateMap<Dispatcher,DispatcherDTO>()
                //Dodać mapowanie Salary
                .ForMember(m => m.FirstName, c => c.MapFrom(s => s.PersonalInformation.FirstName))
                .ForMember(m => m.LastName, c => c.MapFrom(s => s.PersonalInformation.LastName))
                .ForMember(m => m.StreetAddress, c => c.MapFrom(s => s.PersonalInformation.StreetAddress))
                .ForMember(m => m.HouseNumber, c => c.MapFrom(s => s.PersonalInformation.HouseNumber))
                .ForMember(m => m.ApartmentNumber, c => c.MapFrom(s => s.PersonalInformation.ApartmentNumber))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.PersonalInformation.PostalCode))
                .ForMember(m => m.StateProvince, c => c.MapFrom(s => s.PersonalInformation.StateProvince))
                .ForMember(m => m.City, c => c.MapFrom(s => s.PersonalInformation.City))
                .ForMember(m => m.Country, c => c.MapFrom(s => s.PersonalInformation.Country));*/

            // TeamController
            CreateMap<Team, TeamDTO>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Day, c => c.MapFrom(s => DateOnly.FromDateTime(s.Day)))
                .ForMember(m => m.DriverId, c => c.MapFrom(s => s.DriverId))
                .ForMember(m => m.ParamedicId, c => c.MapFrom(s => s.ParamedicId));

            CreateMap<Team, SelectTeamDTO>()
                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.DriverFullName, c => c.MapFrom(s => s.Driver.PersonalInformation.FirstName + s.Driver.PersonalInformation.LastName))
                .ForMember(m => m.ParamedicFullName, c => c.MapFrom(s => s.Paramedic.PersonalInformation.FirstName + s.Paramedic.PersonalInformation.LastName));

            CreateMap<CreateTeamDTO, Team>()
                .ForMember(m => m.DriverId, c => c.MapFrom(s => s.DriverId))
                .ForMember(m => m.ParamedicId, c => c.MapFrom(s => s.ParamedicId))
                .ForMember(m => m.Day, c => c.MapFrom(s => s.Day.ToDateTime(new TimeOnly())));

            CreateMap<Availability, AvailabilityDTO>()
                .ForMember(m => m.Day, c => c.MapFrom(s => s.Day.ToDateOnly()));

            CreateMap<RegisterUserDTO, User>();
            //.ForMember(m => m.Email, c => c.MapFrom(s => s.Email))
            //.ForMember(m => m.RoleId, c => c.MapFrom(s => s.RoleId))
            //.ForMember(m => m.AccountId, c => c.MapFrom(s => s.AccountId))
            //;

        }
    }
}
