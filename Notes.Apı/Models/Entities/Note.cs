namespace Notes.Apı.Models.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }//Notların başlıkları
        public string Description { get; set; }//Notların içerikleri
        public bool IsVisible { get; set; }//Kullanıcılara gözüküp gözükmeyeceği
    }
}
