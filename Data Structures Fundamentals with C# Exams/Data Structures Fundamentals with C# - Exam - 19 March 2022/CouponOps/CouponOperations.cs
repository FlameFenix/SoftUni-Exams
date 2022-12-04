namespace CouponOps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CouponOps.Models;
    using Interfaces;

    public class CouponOperations : ICouponOperations
    {
        private Dictionary<string, Website> websites = new Dictionary<string, Website>();

        private Dictionary<string, Coupon> coupons = new Dictionary<string,Coupon>();

        public void AddCoupon(Website website, Coupon coupon)
        {
            if (!Exist(website) || coupons.ContainsKey(coupon.Code))
            {
                throw new ArgumentException();
            }

            coupons.Add(coupon.Code, coupon);
            coupons[coupon.Code].WebSite = website;
            websites[website.Domain].Coupons.Add(coupon);  
        }

        public bool Exist(Website website) => websites.ContainsKey(website.Domain);

        public bool Exist(Coupon coupon) => coupons.ContainsKey(coupon.Code);

        public IEnumerable<Coupon> GetCouponsForWebsite(Website website)
        {
            if (!Exist(website))
            {
                throw new ArgumentException();
            }

            return coupons.Values.Where(x => x.WebSite.Domain == website.Domain).ToArray();
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
        => coupons.Values.OrderByDescending(x => x.Validity).ThenByDescending(x => x.DiscountPercentage);

        public IEnumerable<Website> GetSites() => websites.Values.ToArray();

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
        => websites.Values.OrderBy(x => x.UsersCount).ThenByDescending(x => x.Coupons.Count);

        public void RegisterSite(Website website)
        {
            if (Exist(website))
            {
                throw new ArgumentException();
            }

            websites.Add(website.Domain, website);
        }

        public Coupon RemoveCoupon(string code)
        {
            if (!coupons.ContainsKey(code))
            {
                throw new ArgumentException();
            }

            var oldCoupon = coupons[code];

            coupons.Remove(code);

            return oldCoupon;
        }

        public Website RemoveWebsite(string domain)
        {
            if(!websites.ContainsKey(domain))
            {
                throw new ArgumentException();
            }

            var currentWebSite = websites[domain];
            var currentCoupons = websites[domain].Coupons;

            foreach (var coupon in currentCoupons)
            {
                RemoveCoupon(coupon.Code);
            }

            websites.Remove(currentWebSite.Domain);

            return currentWebSite;
        }

        public void UseCoupon(Website website, Coupon coupon)
        {
            if(!Exist(website) || !Exist(coupon) || coupon.WebSite.Domain != website.Domain)
            {
                throw new ArgumentException();
            }

            RemoveCoupon(coupon.Code);
        }
    }
}
