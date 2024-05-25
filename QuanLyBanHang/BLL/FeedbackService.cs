using BLL.DTOs;
using Common.Req.FeedbackReq;
using Common.Req.OrderReq;
using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FeedbackService : GenericSvc<FeedbackRepository, Feedback>
    {
        private readonly FeedbackRepository _feedbackRepository;
        public FeedbackService()
        {
            _feedbackRepository = new FeedbackRepository();
        }

        public async Task<Feedback> CreateFeedbackAsync(long userId, FeedbackRequest feedback)
        {
            try
            {
                var f = await _feedbackRepository.CreateFeedbackAsync(userId, feedback);
                return f;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
