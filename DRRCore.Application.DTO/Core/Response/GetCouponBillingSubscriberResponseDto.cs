﻿namespace DRRCore.Application.DTO.Core.Response
{
    public class GetCouponBillingSubscriberResponseDto
    {
        public int Id { get; set; }

        public int? IdSubscriber { get; set; }

        public decimal? NumCoupon { get; set; }

        public decimal? PriceT1 { get; set; }

        public decimal? PriceT2 { get; set; }

        public decimal? PriceT3 { get; set; }

        public decimal? PriceT0 { get; set; }
    }
}
