using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;

namespace BankOfMikaila.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig() { 
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();

            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, AccountCreateDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();
            CreateMap<AccountDTO, AccountCreateDTO>().ReverseMap();

            CreateMap<Bill, BillDTO>().ReverseMap();
            CreateMap<Bill, BillCreateDTO>().ReverseMap();
            CreateMap<Bill, BillUpdateDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();

            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<Transaction, TransactionCreateDTO>().ReverseMap();
            CreateMap<Transaction, TransactionUpdateDTO>().ReverseMap();

            CreateMap<Withdrawal, WithdrawalDTO>().ReverseMap();
            CreateMap<Withdrawal, WithdrawalCreateDTO>().ReverseMap();
            CreateMap<Withdrawal, WithdrawalUpdateDTO>().ReverseMap();

            CreateMap<Deposit, DepositDTO>().ReverseMap();
            CreateMap<Deposit, DepositCreateDTO>().ReverseMap();
            CreateMap<Deposit, DepositUpdateDTO>().ReverseMap();

            CreateMap<P2P, P2PDTO>().ReverseMap();
            CreateMap<P2P, P2PCreateDTO>().ReverseMap();
            CreateMap<P2P, P2PUpdateDTO>().ReverseMap();
        }
    }
}
