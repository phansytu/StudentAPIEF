using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentAPIw6.Context;
using StudentAPIw6.Model;
using static StudentAPIw6.API.Exceptions.BoMonException;

namespace StudentAPIw6.API.Validators.BusinessValidators
{
    public class BoMonBusinessValidator
    {
        private readonly AppDbContext _appDbContext;

        public BoMonBusinessValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<BoMon> CheckId(int id)
        {
            var byId = await _appDbContext.BoMons
               .FindAsync(id);
            if (byId == null)
            {
                throw new BoMonNotFoundException($"Bộ môn có Id {id} không tồn tại.");
            }
            return byId;
        }
        public async Task<BoMon> CheckMaBM(string mabm)
        {
            var ByMa = await _appDbContext.BoMons
               .FirstOrDefaultAsync(x => x.maBM == mabm);
            if (ByMa == null)
            {
                throw new BoMonNotFoundException(
                    $"Mã bộ môn {mabm} không tồn tại"
                );
            }
            return ByMa;
        }
        public async Task CheckTenBoMon(string tenmon)
        {
            var ByTen = await _appDbContext.BoMons
               .AnyAsync(x => x.tenBM == tenmon);
            if (ByTen)
            {
                throw new BoMonBadRequestException(
                    $"Ten bộ môn {tenmon} đã tồn tại"
                );
            }

        }
    }
}