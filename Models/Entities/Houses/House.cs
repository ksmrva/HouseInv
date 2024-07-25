using System.ComponentModel.DataAnnotations.Schema;
using ErrorOr;
using HouseInv.Models.Dtos.Houses;
using HouseInv.Models.Entities.Persons;

namespace HouseInv.Models.Entities.Houses
{
    [Table("house")]
    public class House
    {
        public const int MinZipLength = 5;

        public const int MinStateLength = 2;

        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("address1")]
        public required string Address1 { get; set; }

        [Column("address2")]
        public string? Address2 { get; set; }

        [Column("city")]
        public required string City { get; set; }

        [Column("state")]
        public required string State { get; set; }

        [Column("zip")]
        public required string Zip { get; set; }

        [Column("owner_id")]
        [ForeignKey("Owner")]
        public required long OwnerId { get; set; }
        public virtual Person Owner { get; set; }

        [Column("created_date")]
        public required DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public required DateTime ModifiedDate { get; set; }

        [Column("created_user")]
        public required string CreatedUser { get; set; }

        [Column("modified_user")]
        public required string ModifiedUser { get; set; }

        private House()
        {

        }

        private House(long? id,
                     string name,
                     string address1,
                     string address2,
                     string city,
                     string state,
                     string zip,
                     long ownerId,
                     DateTime createdDate,
                     DateTime modifiedDate,
                     string createdUser,
                     string modifiedUser)
        {
            Id = (long)id;
            Name = name;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Zip = zip;
            OwnerId = ownerId;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            CreatedUser = createdUser;
            ModifiedUser = modifiedUser;
        }

        public static ErrorOr<House> Create(long? id,
                                            string name,
                                            string address1,
                                            string address2,
                                            string city,
                                            string state,
                                            string zip,
                                            long ownerId,
                                            DateTime createdDate,
                                            DateTime modifiedDate,
                                            string createdUser,
                                            string modifiedUser)
        {
            ErrorOr<House> createHouseResult;
            List<Error> errors = new();
            if (state.Length < MinStateLength)
            {
                errors.Add(Errors.DataErrors.StateTooShort);
            }
            if (zip.Length < MinZipLength)
            {
                errors.Add(Errors.DataErrors.ZipTooShort);
            }

            if (errors.Count > 0)
            {
                createHouseResult = errors;
            }
            else
            {
                House house = new()
                {
                    Id = (long)id,
                    Name = name,
                    Address1 = address1,
                    Address2 = address2,
                    City = city,
                    State = state,
                    Zip = zip,
                    OwnerId = ownerId,
                    CreatedDate = createdDate,
                    ModifiedDate = modifiedDate,
                    CreatedUser = createdUser,
                    ModifiedUser = modifiedUser
                };
                createHouseResult = house;
            }
            return createHouseResult;
        }

        public static ErrorOr<House> FromCreateDto(CreateHouseDto createHouseDto)
        {
            var utcNowValue = DateTime.UtcNow;
            return Create(
                null,
                createHouseDto.Name,
                createHouseDto.Address1,
                createHouseDto.Address2,
                createHouseDto.City,
                createHouseDto.State,
                createHouseDto.Zip,
                createHouseDto.OwnerId,
                utcNowValue,
                utcNowValue,
                createHouseDto.UserId,
                createHouseDto.UserId
            );
        }
    }
}