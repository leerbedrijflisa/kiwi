using Lisa.Kiwi.WebApi;

namespace Lisa.Kiwi.Web
{
    public class StatusChangeViewModel
    {
        public int Id { get; set; }
        public StatusEnum Status { get; set; }
        public bool IsVisible { get; set; }
    }
}