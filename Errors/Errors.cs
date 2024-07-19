using ErrorOr;

namespace HouseInv.Errors
{
    public static class DataErrors
    {
        public static Error DataNotFound => Error.NotFound(
            code: "Data.NotFound",
            description: "The data requested was not found please check the ID referenced");
    }
}