using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace societymanagement.Entity
{
  public class Event
  {
    public string EventName { get; set; }
    public string Description { get; set; }
    public DateOnly? EventDate { get; set; }
    public string Place { get; set; }
    public string EventTime { get; set; }
  }
}
