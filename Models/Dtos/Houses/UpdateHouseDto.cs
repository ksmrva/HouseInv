namespace HouseInv.Models.Dtos.Houses
{
    public record UpdateHouseDto
    {
        public required string Name { get; set; }
        public required string Address1 { get; set; }
        public string? Address2 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
        public required string OwnerId { get; set; }
        public required string UserId { get; set; }
    }
}