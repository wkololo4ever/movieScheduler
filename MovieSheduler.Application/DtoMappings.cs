﻿using AutoMapper;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Movie.Dtos;
using MovieSheduler.Application.SheduleRecord.Dtos;

namespace MovieSheduler.Application
{
    public static class DtoMappings //: Profile
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<Domain.SheduleRecord.SheduleRecord, SheduleRecordDto>()
                .ForMember(d => d.Movie, m => m.MapFrom(s => s.Movie.Name))
                .ForMember(d => d.Cinema, d => d.MapFrom(s => s.Cinema.Name));
            Mapper.CreateMap<Domain.Movie.Movie, MovieDto>();
            Mapper.CreateMap<Domain.Cinema.Cinema, CinemaDto>();
        }

        //protected override void Configure()
        //{
        //    //TODO сделать общий мапинг
        //    CreateMap<IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>, IReadOnlyCollection<SheduleRecordDto>>();
        //}
    }
}
