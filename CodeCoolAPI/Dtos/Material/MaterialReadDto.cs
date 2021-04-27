using System;

namespace CodeCoolAPI.Dtos
{
    public class MaterialReadDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime PublishTime { get; set; }
        public string MaterialTypeName { get; set; }
        public int MaterialTypeId { get; set; }
    }
}