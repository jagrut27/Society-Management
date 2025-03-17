using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;

namespace Admin.Module
{
  public class Events
  {
    [Key]
    public int event_id { get; set; }

    public string  event_name {get;set;}

    public string Description { get; set; }
    public string place { get; set; }
    public DateOnly event_date { get; set; }

    [JsonConverter(typeof(TimeSpanToStringConverter))]
    public TimeSpan event_time { get; set; }

  }
}
