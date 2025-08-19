using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.Entity.Concrete
{
    public class Notification : BaseEntity<int>
    {
        // Bildirimi kime gönderiyoruz
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

        // Kim gönderiyor
        public int? SenderId { get; set; }
        public AppUser? Sender { get; set; }

        // İlgili kitap varsa
        public int? BookId { get; set; }
        public Book? Book { get; set; }

        // Tür ve durum
        public NotificationType Type { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.Unread;

        // Mesaj içeriği
        public string Message { get; set; }
    }

}
