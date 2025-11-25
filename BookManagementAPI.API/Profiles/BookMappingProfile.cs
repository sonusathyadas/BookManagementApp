using AutoMapper;
using BookManagementAPI.API.DTOs;
using BookManagementAPI.Core.Models;

namespace BookManagementAPI.API.Profiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}