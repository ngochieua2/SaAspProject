using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class ReservoirsController : ApiController
    {
        private ReservoirVolumesSAEntities db = new ReservoirVolumesSAEntities();

        // GET: /api/Reservoirs
        public object GetReservoirs()
        {
            var reservoirs = db.Reservoirs.Select(r => new
            {
                dateRange = db.ReservoirVolumes.GroupBy(rv => rv.reservoirID)
                .Where(rv => rv.Key == r.reservoirID)
                .Select(rv => new
                {
                    from = rv.Min(d => d.recordDate).Year,
                    to = rv.Max(d => d.recordDate).Year,
                }).FirstOrDefault(),
                records = r.ReservoirVolumes.Count(),
                reservoirDetail = r
            }).ToList();

            return reservoirs;
        }

        // GET: /api/Reservoirs/5
        public object GetReservoir(int id)
        {
            var reservoirDetails = db.ReservoirVolumes
                .Where(rv => rv.reservoirID == id)
                .GroupBy(rv => rv.recordDate.Year)
                .Select(rv => new ReservoirRecords
                {
                    year = rv.Key,
                    records = rv.Count(),
                    avgVolume = rv.Average(v => v.averageVolume),
                    firstMonthNo= rv.Min(v => v.recordDate.Month),
                    lastMonthNo = rv.Max(v => v.recordDate.Month)
                })
                .ToList();

            reservoirDetails = reservoirDetails.Select(rv =>
            {
                rv.firstMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(rv.firstMonthNo);
                rv.lastMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(rv.lastMonthNo);
                return rv;
            }).ToList();


            return (reservoirDetails);
        }


        // GET: /api/Reservoirs/5?year=2018
        public object GetReservoir(int id, int year)
        {
            var reservoirDetails = db.ReservoirVolumes
                .Where(rv => rv.reservoirID == id && rv.recordDate.Year == year)
                .OrderBy(rv => rv.recordDate.Month)
                .ThenBy(rv => rv.recordDate.Day)
                .Select(rv => new ReservoirRecordDetail
                {
                    dayNo = rv.recordDate.Day,
                    year = year,
                    monthNo = rv.recordDate.Month,
                    monthName = "",
                    avgVolume = rv.averageVolume,
                    volumeToday = rv.volumeToday,
                    volumeLastYear = rv.volumeLastYear,
                    recordDate = rv.recordDate
                })
                .ToList();

            reservoirDetails = reservoirDetails.Select(rv =>
            {
                rv.monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(rv.monthNo);
                return rv;
            }).ToList();


            return (reservoirDetails);
        }


    }
}