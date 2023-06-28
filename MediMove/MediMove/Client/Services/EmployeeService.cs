using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Validators;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MediMove.Client.Services
{
    public class EmployeeService : BaseService
    {
        public EmployeeService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        private static string? ValidateEmployee(EmployeeDTO employee)
        {
            if (!employee.FirstName.IsValidFirstName())
                return "Invalid first name";

            if (!employee.LastName.IsValidLastName())
                return "Invalid last name";

            if (!employee.Email.IsValidEmail())
                return "Invalid email";

            if (!employee.StreetAddress.IsValidStreetAddress())
                return "Invalid street address";

            if (!employee.HouseNumber.IsValidHouseNumber())
                return "Invalid house number";

            if (!employee.ApartmentNumber.IsValidApartmentNumber())
                return "Invalid apartment number";

            if (!employee.PostalCode.IsValidPostalCode())
                return "Invalid postal code";

            if (!employee.City.IsValidCity())
                return "Invalid city";

            if (!employee.StateProvince.IsValidStateProvince())
                return "Invalid state/province";

            if (!employee.Country.IsValidCountry())
                return "Invalid country";

            if (!employee.PhoneNumber.IsValidPhoneNumber())
                return "Invalid phone number";

            if (!employee.BankAccountNumber.IsValidBankAccountNumber())
                return "Invalid bank account number";

            if (employee is ParamedicDTO paramedic && !paramedic.PayPerHour.IsValidPayPerHour())
                return "Invalid pay per hour";

            if (employee is DispatcherDTO dispatcher && !dispatcher.Salary.IsValidSalary())
                return "Invalid salary";
            
            return null;
        }


        public async Task<ErrorOr<EmployeeDTO[]>> GetAllEmployees()
        {
            var result = await HandleRequestAsync<GetAllEmployeesResponse>("api/v1/Employee", HttpMethod.Get);

            if (result.IsError)
                return result.Errors;

            var paramedics = result.Value.Paramedics;
            var dispatchers = result.Value.Dispatchers;

            // merge paramedics and dispatchers
            var toReturn = new EmployeeDTO[paramedics.Length + dispatchers.Length];
            paramedics.CopyTo(toReturn, 0);
            dispatchers.CopyTo(toReturn, paramedics.Length);

            return toReturn;
        }

        public async Task<ErrorOr<GetEmployeesInMonthByHoursAndSalaryDTO>> GetEmployeesReport(DateTime start, DateTime end, decimal startAmount, decimal endAmount, decimal startHours, decimal endHours)
        {
            var content = new GetEmployeesInMonthByHoursAndSalaryRequest(start, end, startAmount, endAmount, startHours, endHours);
            return await HandleRequestAsync<GetEmployeesInMonthByHoursAndSalaryRequest, GetEmployeesInMonthByHoursAndSalaryDTO>("api/v1/Employee/Report/Multiple", HttpMethod.Post, content);
        }
        public async Task<ErrorOr<GetEmployeeRatesByIdAndDatesDTO>> GetParamedicReport(int id, DateTime start, DateTime end)
        {

            var uriBuilder = GenerateUriBuilder("api/v1/Employee/Report/Single");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["id"] = id.ToString();
            query["startTime"] = start.ToString("yyyy-MM-dd");
            query["endTime"] = end.ToString("yyyy-MM-dd");
           
            uriBuilder.Query = query.ToString();

            return await HandleQueryAsync<GetEmployeeRatesByIdAndDatesDTO>(uriBuilder, HttpMethod.Get);
        }

        public async Task<ErrorOr<Unit>> UpdateEmployees(List<EmployeeDTO> employees)
        {
            var paramedics = new List<ParamedicDTO>();
            var dispatchers = new List<DispatcherDTO>();
            foreach (var employee in employees)
            {
                var errorMessage = ValidateEmployee(employee);
                if (errorMessage is not null) return Error.Failure(errorMessage);
                if (employee is ParamedicDTO paramedic)
                    paramedics.Add(paramedic);
                else if (employee is DispatcherDTO dispatcher)
                    dispatchers.Add(dispatcher);
            }

            return await HandleRequestAsync<PutEmployeesRequest, Unit>("api/v1/Employee", HttpMethod.Put,
                new PutEmployeesRequest(paramedics.ToArray(), dispatchers.ToArray()));
        }

        public async Task<ErrorOr<Unit>> Register(RegisterAdminRequest content) =>
            await HandleRequestAsync<RegisterAdminRequest, Unit>("api/v1/Accounts/Register/Admin", HttpMethod.Post, content);

        public async Task<ErrorOr<Unit>> Register(RegisterParamedicRequest content) =>
            await HandleRequestAsync<RegisterParamedicRequest, Unit>("api/v1/Accounts/Register/Paramedic", HttpMethod.Post, content);

        public async Task<ErrorOr<Unit>> Register(RegisterDispatcherRequest content) =>
            await HandleRequestAsync<RegisterDispatcherRequest, Unit>("api/v1/Accounts/Register/Dispatcher", HttpMethod.Post, content);
    }
}
