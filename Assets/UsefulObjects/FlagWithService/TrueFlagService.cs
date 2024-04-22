
using System.Collections.Generic;


namespace UsefulObjects
{
    public class TrueFlagService
    {
        public bool Flag => _requestsAmount != 0;

        private int _requestsAmount;

        public void AddTrueRequest()
        {
            _requestsAmount += 1;
        }

        public void RemoveTrueRequest()
        {
            _requestsAmount -= _requestsAmount != 0 ? 1 : 0;
        }

        public void DeleteRequestInfo()
        {
            _requestsAmount = 0;
        }

        public TrueFlagService()
        {
            _requestsAmount = 0;
        }
    }
}