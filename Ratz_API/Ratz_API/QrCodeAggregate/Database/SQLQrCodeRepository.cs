using DatabaseConnection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ratz_API.QrCodeAggregate.UseCases;

namespace Ratz_API.QrCodeAggregate.Database
{
    public class SqlQrCodeRepository : IQrCodeRepository
    {
        private readonly RatzDbContext _context;
        private readonly CreateQrCodeUseCase _createQrCodeUseCase = new ();
        public SqlQrCodeRepository(RatzDbContext iContext) => _context = iContext;

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

        public List<QrCode> GetAll(int iUserId)
        {
            return _context.QrCodes.Where((aQrCode) => iUserId == aQrCode.UserId).ToList();
        }

        public QrCode GetQrCodeById(int iQrCodeId) => _context.QrCodes.Find(iQrCodeId);

        public QrCode NewQrCode(string data, int userId)
        {
            QrCode qrCode = _createQrCodeUseCase.Handle(data, userId);
            Console.WriteLine(qrCode);
            _context.QrCodes.Add(qrCode);
            _context.SaveChanges();
            
            return qrCode;
        }

        public QrCode UpdateQrCode(QrCode ioQrCodeChanges)
        {
            EntityEntry<QrCode> aQrCode = _context.QrCodes.Attach(ioQrCodeChanges);
            aQrCode.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return ioQrCodeChanges;
        }
    }
}
