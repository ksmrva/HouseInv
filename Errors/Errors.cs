using ErrorOr;
using HouseInv.Models.Entities.Houses;

namespace HouseInv.Errors
{
    public static class DataErrors
    {
        public static Error StateTooShort => Error.Validation(
            code: "Data.StateTooShort",
            description: $"The State must be no less than {House.MinStateLength} characters");

        public static Error ZipTooShort => Error.Validation(
            code: "Data.ZipTooShort",
            description: $"The Zip Code must be no less than {House.MinZipLength} characters");

        public static Error DataNotFound => Error.NotFound(
            code: "Data.NotFound",
            description: "The data requested was not found please check the ID referenced");
    }
}