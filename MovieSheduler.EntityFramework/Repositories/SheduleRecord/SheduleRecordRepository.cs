﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MovieSheduler.Domain.Cinema;
using MovieSheduler.Domain.Movie;
using MovieSheduler.Domain.SheduleRecord;

namespace MovieSheduler.EntityFramework.Repositories.SheduleRecord
{
    public class SheduleRecordRepository : MovieShedulerRepositoryBase<Domain.SheduleRecord.SheduleRecord, int>, ISheduleRecordRepository
    {
        public SheduleRecordRepository(MovieShedulerContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<DateTime?> GetFirstAvailableDateAsync()
        {
            return (await Table.OrderBy(r => r.Date).FirstOrDefaultAsync())?.Date;
        }

        public async Task<IReadOnlyCollection<Domain.SheduleRecord.SheduleRecord>> GetSheduleByDateAsync(DateTime date)
        {
            ReadOnlyCollection<Domain.SheduleRecord.SheduleRecord> sheduleRecords = await
                Task.Run(() => Context.Database.SqlQuery<SheduleRecordByDateOutput>("GetSheduleByDate @date", new SqlParameter("date", date))
                    .Select(r => new Domain.SheduleRecord.SheduleRecord
                    {
                        Id = r.SheduleRecordId,
                        Cinema = new Cinema
                        {
                            Id = r.CinemaId,
                            Name = r.Cinema
                        },
                        CinemaId = r.CinemaId,
                        Movie = new Movie
                        {
                            Name = r.Movie,
                            Id = r.MovieId
                        },
                        MovieId = r.MovieId,
                        Date = r.Date
                    }).ToList().AsReadOnly());

            return sheduleRecords;
        }

        public void AddRecords(IEnumerable<Domain.SheduleRecord.SheduleRecord> record)
        {
            Table.AddRange(record);
        }

        public bool ExistByProperties(Domain.SheduleRecord.SheduleRecord record)
        {
            return Table.Any(r => r.CinemaId == record.CinemaId && r.MovieId == record.MovieId && r.Date == record.Date);
        }
    }
}
