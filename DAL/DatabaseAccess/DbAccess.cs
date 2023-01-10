using DAL.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DatabaseAccess
{
    public static class DbAccess
    {
        private static string DefaultConnectionString = 
            $"Server={System.Environment.MachineName}; Database=RwaApartmani; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True";

        public static IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(DefaultConnectionString, nameof(LoadApartments)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Address = row[nameof(Apartment.Address)].ToString(),
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    CreatedAt = row[nameof(Apartment.CreatedAt)].ToString(),
                    DeletedAt = row[nameof(Apartment.DeletedAt)].ToString(),
                    Price = (decimal)row[nameof(Apartment.Price)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Owner = row["OwnerName"].ToString(),
                    Status = row["StatusName"].ToString(),
                    City = row["CityName"].ToString()
                });
            }

            return apartments;
        }
    }
}
