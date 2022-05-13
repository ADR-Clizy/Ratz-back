using DatabaseConnection;

namespace Ratz_API.QrCodeAggregate.Database
{
    public interface IQrCodeRepository
    {
        QrCode GetQrCodeById(int iQrCodeId);
        QrCode NewQrCode(string data, int userId);
        List<QrCode> GetAll(int iUserId);
        QrCode DeleteQrCode(int iQrCodeId);
        QrCode UpdateQrCode(QrCode ioQrCodeChanges);
    }
}
