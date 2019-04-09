using System.Collections.Generic;
using RestSharp;
using StoreTransferKit.Models;
using StoreTransferKit.Repository;
using StoreTransferKit.Callers;
using System;
using StoreTransferKit.Helper;

namespace StoreTransferKit.Transfer
{
    public class FactorTransfer : IFactorTransfer
    {
        private FactorCaller factorCaller;
        private FactorRespository factorRespository;

        public FactorTransfer()
        {
            factorCaller = new FactorCaller(ConfigurationHelper.ReadSetting("serverUrl"));
            factorRespository = new FactorRespository();
        }

        public void SendItems()
        {
            int serverMaxRefID = Int32.Parse(factorCaller.GetMaxRefID());
            
            List<Factor> newFactors = factorRespository.GetItems(serverMaxRefID);
            Console.WriteLine("\n The factors sending process is starting...");
            if (newFactors.Count > 0)
            {
                Console.WriteLine("\t {0} new factors will be sending ...", newFactors.Count);
                foreach (Factor factor in newFactors)
                {
                    IRestResponse response = factorCaller.Create(factor);
                    Console.WriteLine("Factor id :{0} \n\t Successful {1} , Code {2} , Status {3}.", factor.refID,
                        response.IsSuccessful, factor.code, response.StatusCode);
                }
            }
            else
            {
                Console.WriteLine("\t There is not any factor to send.");
            }
        }

        public void Verify()
        {
            Console.WriteLine("\n Starting the simple verification process...");

            int serverMaxRefID = Int32.Parse(factorCaller.GetMaxRefID());
            int serverFactorCount = Int32.Parse(factorCaller.Count());
            
            int clientFactorCount = factorRespository.Count(0);
            int clientFactorCountLess = factorRespository.Count(serverMaxRefID);
            
            if (serverFactorCount == clientFactorCount)
            {
                Console.WriteLine("\t Verification is Successful. Server factors count is equal with client. Both of them : {0}",serverFactorCount.ToString());
            } 
            else if(clientFactorCountLess != serverFactorCount)
            {
                Console.WriteLine("\t *** Error : Error in verification.*** \n Server factors count {0} , but client factor count {1}", serverFactorCount.ToString(),clientFactorCountLess.ToString());
            }
            else
            {
                Console.WriteLine("\t *** Warning : Failure in verification.*** \n Server factors count {0} , but client factor count {1}", serverFactorCount.ToString(),clientFactorCount.ToString());
            }
        }
        
        /// <summary>
        /// Sample method to get factors.
        /// </summary>
        public void GetItems()
        {
            var factors = factorCaller.GetItems();

            Console.WriteLine("Get factors ... \n Found {0} factors: ", factors.Count);
            if (factors.Count > 0)
            {
                foreach (var factor in factors)
                {
                    Console.WriteLine("\n Factor id :{0} \n\t Code {1} , Customer lastname {2} , Amount Paid {3}.", factor.refID , factor.code, factor.customer.lastName , factor.amountPaid );
                    
                }
            }
        }
    }
}