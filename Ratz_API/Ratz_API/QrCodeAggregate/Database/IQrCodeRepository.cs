using DatabaseConnection;

namespace Ratz_API.QrCodeAggregate.Database
{
    public interface IQrCodeRepository
    {
        QrCode GetQrCodeById(int iQrCodeId);
        QrCode NewQrCode(string dataToEncode);
        QrCode DeleteQrCode(int iQrCodeId);
        QrCode UpdateQrCode(QrCode ioQrCodeChanges);
    }
}
