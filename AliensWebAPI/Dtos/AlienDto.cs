using AliensWebAPI.Models;

namespace AliensWebAPI.Dtos;

public class AlienDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime ContactDate { get; set; }
    public string CategoryName { get; set; } = "";
}