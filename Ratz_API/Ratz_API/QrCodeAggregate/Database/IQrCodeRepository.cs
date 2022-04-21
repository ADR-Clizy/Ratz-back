namespace DatabaseConnection
{
    public interface IQrCodeRepository
    {
        QrCode GetQrCodeById(int iQrCodeId);
        QrCode NewQrCode(QrCode ioQrCode);
        QrCode DeleteQrCode(int iQrCodeId);
        QrCode UpdateQrCode(QrCode ioQrCodeChanges);
    }
}
