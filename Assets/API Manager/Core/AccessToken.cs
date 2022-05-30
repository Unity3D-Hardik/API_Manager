using System;

namespace Com.ApiManager
{
    public class AccessToken 
    {
        public string access_token;
        public string token_type;
        public int expires_in;
        private DateTime createDate;

        public AccessToken()
        {
            this.createDate = DateTime.Now;
        }
        public bool IsExpired()
        {
            DateTime expireDate = this.createDate.Add(TimeSpan.FromSeconds(this.expires_in));
            return DateTime.Now.CompareTo(expireDate) > 0;
        }
    }
}