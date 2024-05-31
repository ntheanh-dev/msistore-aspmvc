using Common.Req.ExchangeReq;
using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;

namespace BLL
{
    public class ExchangeService : GenericSvc<ExchangeRepository, Exchange>
    {
        private ExchangeRepository _exchangeRepository;
        public ExchangeService()
        {
            _exchangeRepository = new ExchangeRepository();
        }
        
        public async Task<Exchange> CreateExchange(ExchangeReq req , long userId)
        {
            try
            {
                var exchange = await _exchangeRepository.createExchange(req, userId);
                return exchange;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SingleRsp> updateExchange(long orderItemId,string newReason,long userId)
        {
            var res = new SingleRsp();
            try
            {
                var exchange = await _exchangeRepository.UpdateExchangeReason(orderItemId,newReason,userId);
                res.Resutls = exchange;
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 


        public async Task<SingleRsp> DeletedExchange(long exchangeId, long userId)
        {
            var res = new SingleRsp();
            try
            {
                var deleted = await _exchangeRepository.DeleteExchange(exchangeId, userId);
                res.Resutls = deleted;
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
