namespace DatabaseConnection
{
    public class SQLQrCodeRepository : IQrCodeRepository
    {
        private readonly RatzDbContext _context;

        public SQLQrCodeRepository(RatzDbContext iContext)
        {
            _context = iContext;
        }

        public QrCode DeleteQrCode(int iQrCodeId)
        {
            QrCode aQrCode = _context.QrCodes.Find(iQrCodeId);
            if(aQrCode != null)
            {
                _context.QrCodes.Remove(aQrCode);
                _context.SaveChanges();
            }
            return aQrCode;
        }

        public QrCode GetQrCodeById(int iQrCodeId)
        {
            
            return _context.QrCodes.Find(iQrCodeId);
        }

        public QrCode NewQrCode(QrCode ioQrCode)
        {
            _context.QrCodes.Add(ioQrCode);
            _context.SaveChanges();
            return ioQrCode;
        }

        public QrCode UpdateQrCode(QrCode ioQrCodeChanges)
        {
            var aQrCode = _context.QrCodes.Attach(ioQrCodeChanges);
            aQrCode.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return ioQrCodeChanges;
        }
    }
}
