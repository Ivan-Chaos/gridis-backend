using AutoMapper;
using GridisBackend.DTOs;
using GridisBackend.DTOs.Address;
using GridisBackend.DTOs.Bill;
using GridisBackend.DTOs.CallOperator;
using GridisBackend.DTOs.District;
using GridisBackend.DTOs.InstalledMeter;
using GridisBackend.DTOs.Manufacturer;
using GridisBackend.DTOs.MeterModel;
using GridisBackend.DTOs.OperatorReadings;
using GridisBackend.DTOs.Person;
using GridisBackend.DTOs.Readings;
using GridisBackend.DTOs.Residence;
using GridisBackend.DTOs.Street;
using GridisBackend.DTOs.Tarrif;
using GridisBackend.Models;

namespace GridisBackend
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<City, City_GET_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<District, District_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<District, District_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            
            CreateMap<Street, Street_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Street, Street_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Address, Address_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Address, Address_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Manufacturer, Manufacturer_GET_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<MeterModel, MeterModel_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<MeterModel, MeterModel_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<InstalledMeter, InstalledMeter_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<InstalledMeter, InstalledMeter_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Person, Person_GET_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Reading, Reading_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Reading, Reading_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Tarrif, Tarrif_GET_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Bill, Bill_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Bill, Bill_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Residence, Residence_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Residence, Residence_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CallOperator, CallOperator_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<CallOperator, CallOperator_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<OperatorReading, OperatorReading_GET_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<OperatorReading, OperatorReading_POST_DTO>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());

        }
    }
}
