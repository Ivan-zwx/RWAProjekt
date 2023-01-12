using DAL.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DatabaseAccess
{
    public static class DbAccess
    {
        //private static string APARTMENTS_CS = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;
        private static string ConnectionString =
            $"Server={System.Environment.MachineName}; Database=RwaApartmani; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True";

        /********************************************************************************************************************************************/

        public static void SoftDeleteApartment(int id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(SoftDeleteApartment), id);
        }

        public static int QueryApartmentDeletedStatus(int id)
        {
            SqlParameter[] procedureParameters = new SqlParameter[2];
            procedureParameters[0] = new SqlParameter($"@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = id
            };
            procedureParameters[1] = new SqlParameter($"@DeletedStatus", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, nameof(QueryApartmentDeletedStatus), procedureParameters);

            return (int)procedureParameters[1].Value;
        }

        public static void RemoveAllTaggedApartmentsById(int apartmentId)
        {
            SqlParameter[] procedureParameters = new SqlParameter[1];
            procedureParameters[0] = new SqlParameter($"@ApartmentId", SqlDbType.Int)
            { Direction = ParameterDirection.Input, Value = apartmentId };

            SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, nameof(RemoveAllTaggedApartmentsById), procedureParameters);
        }

        public static void AddTaggedApartmentById(int apartmentId, string tagName)
        {
            SqlParameter[] procedureParameters = new SqlParameter[2];
            procedureParameters[0] = new SqlParameter($"@ApartmentId", SqlDbType.Int)
            { Direction = ParameterDirection.Input,Value = apartmentId };
            procedureParameters[1] = new SqlParameter($"@TagName", SqlDbType.NVarChar)
            { Direction = ParameterDirection.Input, Value = tagName };

            SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, nameof(AddTaggedApartmentById), procedureParameters);
        }

        /********************************************************************************************************************************************/

        public static void AddUser(User u)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(AddUser), u.Email, u.UserName, Cryptography.Cryptography.HashPassword(u.PasswordHash), u.PhoneNumber, u.Address);
        }
        public static IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();

            var tblUsers = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadUsers)).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(
                    new User
                    {
                        Id = (int)row[nameof(User.Id)],
                        Email = row[nameof(User.Email)].ToString(),
                        UserName = row[nameof(User.UserName)].ToString(),
                        PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                        CreatedAt = row[nameof(User.CreatedAt)].ToString(),
                        DeletedAt = row[nameof(User.DeletedAt)].ToString(),
                        PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                        Address = row[nameof(User.Address)].ToString()
                    }
                );
            }

            return users;
        }

        public static User AuthUser(string email, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(ConnectionString, nameof(AuthUser), email, Cryptography.Cryptography.HashPassword(password)).Tables[0];
            if (tblAuth.Rows.Count == 0) return null;

            DataRow row = tblAuth.Rows[0];
            return new User()
            {
                Id = (int)row[nameof(User.Id)],
                Email = row[nameof(User.Email)].ToString(),
                UserName = row[nameof(User.UserName)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                CreatedAt = row[nameof(User.CreatedAt)].ToString(),
                DeletedAt = row[nameof(User.DeletedAt)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Address = row[nameof(User.Address)].ToString()
            };
        }
        public static void DeleteUser(int id)
        {
            SqlHelper.ExecuteDataset(ConnectionString, nameof(DeleteUser), id);
        }
        public static User AuthUserWithoutHash(string email, string password)
        {
            var tblAuth = SqlHelper.ExecuteDataset(ConnectionString, nameof(AuthUser), email, password).Tables[0];
            if (tblAuth.Rows.Count == 0) return null;

            DataRow row = tblAuth.Rows[0];
            return new User()
            {
                Id = (int)row[nameof(User.Id)],
                Email = row[nameof(User.Email)].ToString(),
                UserName = row[nameof(User.UserName)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                CreatedAt = row[nameof(User.CreatedAt)].ToString(),
                DeletedAt = row[nameof(User.DeletedAt)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Address = row[nameof(User.Address)].ToString()
            };
        }

        public static void SaveUser(User u)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(SaveUser), u.Id, u.Email, u.UserName, u.PasswordHash, u.PhoneNumber, u.Address);
        }

        public static Apartment GetApartmentById(int selectedId)
        {
            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(GetApartmentById), selectedId).Tables[0];
            var row = tblApartments.Rows[0];
            if (tblApartments != null)
            {
                return new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    Address = row[nameof(Apartment.Address)].ToString(),
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    Price = (decimal)row[nameof(Apartment.Price)],
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                    BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                    Owner = row["OwnerName"].ToString(),
                    Status = row["StatusName"].ToString(),
                    City = row["CityName"].ToString()
                };
            }
            else return null;
        }

        public static IList<Apartment> LoadApartmentsByTagID(int id)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadApartmentsByTagID), id).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Address = row[nameof(Apartment.Address)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Price = (decimal)row[nameof(Apartment.Price)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        Owner = row["OwnerName"].ToString(),
                        Status = row["StatusName"].ToString(),
                        City = row["CityName"].ToString()
                    }
                );
            }

            return apartments;
        }
        public static IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadApartments)).Tables[0];

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
        public static void SaveApartment(Apartment a)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(SaveApartment), a.Id, a.Name, a.NameEng, a.City, a.Owner, a.Status, a.MaxAdults, a.MaxChildren, a.Price, a.BeachDistance, a.TotalRooms, a.Address);
        }
        public static void DeleteApartment(int id)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(DeleteApartment), id);
        }
        public static void AddApartment(Apartment a)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(AddApartment), a.Name, a.NameEng, a.City, a.Owner, a.Status, a.MaxAdults, a.MaxChildren, a.Price, a.BeachDistance, a.TotalRooms, a.Address);
        }

        public static IList<City> GetCities()
        {
            IList<City> cities = new List<City>();
            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(GetCities)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                cities.Add(new City
                {
                    Id = (int)row[nameof(City.Id)],
                    Name = row[nameof(City.Name)].ToString()
                });
            }

            return cities;
        }

        public static IList<Owner> GetOwners()
        {
            IList<Owner> owners = new List<Owner>();
            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(GetOwners)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                owners.Add(new Owner
                {
                    Id = (int)row[nameof(Owner.Id)],
                    Name = row[nameof(Owner.Name)].ToString()
                });
            }

            return owners;
        }

        public static IList<Status> GetStatus()
        {
            IList<Status> statuses = new List<Status>();
            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(GetStatus)).Tables[0];

            foreach (DataRow row in tblApartments.Rows)
            {
                statuses.Add(new Status
                {
                    Id = (int)row[nameof(Status.Id)],
                    Name = row[nameof(Status.Name)].ToString(),
                    NameEng = row[nameof(Status.NameEng)].ToString()
                });
            }

            return statuses;
        }

        public static IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadTags)).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                Tag tag = new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    CreatedAt = row[nameof(Tag.CreatedAt)].ToString(),
                    Type = row["Type"].ToString()
                };
                tag.NumberOfApartments = LoadApartmentsByTagID(tag.Id).Count;
                tags.Add(tag);
            }

            return tags;
        }

        public static IList<Tag> LoadTagsForApartment(int id)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadTagsForApartment), id).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                Tag tag = new Tag
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString(),
                    CreatedAt = row[nameof(Tag.CreatedAt)].ToString(),
                    Type = row["Type"].ToString()
                };
                tag.NumberOfApartments = LoadApartmentsByTagID(tag.Id).Count;
                tags.Add(tag);
            }

            return tags;
        }

        public static void DeleteTag(int tagID)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(DeleteTag), tagID);
        }

        public static void AddTag(Tag t)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(AddTag), t.Name, t.NameEng, t.Type);
        }

        public static IList<TagType> LoadTagTypes()
        {
            IList<TagType> tt = new List<TagType>();

            var tblTagTypes = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadTagTypes)).Tables[0];
            foreach (DataRow row in tblTagTypes.Rows)
            {
                TagType type = new TagType
                {
                    Id = (int)row[nameof(Tag.Id)],
                    Name = row[nameof(Tag.Name)].ToString(),
                    NameEng = row[nameof(Tag.NameEng)].ToString()
                };
                tt.Add(type);
            }

            return tt;
        }

        public static void AddTaggedApartment(string aName, string tName)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(AddTaggedApartment), aName, tName);
        }

        public static void DeleteTaggedApartment(string aName, string tName)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(DeleteTaggedApartment), aName, tName);
        }

        public static IList<Apartment> LoadApartmentsByCityAndStatus(string status, string city)
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(ConnectionString, nameof(LoadApartmentsByCityAndStatus), status, city).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                apartments.Add(
                    new Apartment
                    {
                        Id = (int)row[nameof(Apartment.Id)],
                        Address = row[nameof(Apartment.Address)].ToString(),
                        Name = row[nameof(Apartment.Name)].ToString(),
                        NameEng = row[nameof(Apartment.NameEng)].ToString(),
                        Price = (decimal)row[nameof(Apartment.Price)],
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        Owner = row["OwnerName"].ToString(),
                        Status = row["StatusName"].ToString(),
                        City = row["CityName"].ToString()
                    }
                );
            }

            return apartments;
        }

        public static void AddReservationForNonExistingUser(int apartmentId, string details, string username, string email, string phone)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(AddReservationForNonExistingUser), apartmentId, details, username, email, phone);
        }

        public static void AddReservationForExistingUser(int userId, int apartmentId, string details)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, nameof(AddReservationForExistingUser), userId, apartmentId, details);
        }
    }
}
