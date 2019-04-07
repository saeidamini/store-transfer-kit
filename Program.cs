using System;
using System.Collections.Generic;
using RestSharp;
using StoreTransferKit.Callers;
using StoreTransferKit.Helper;
using StoreTransferKit.Models;
using StoreTransferKit.Repository;

namespace StoreTransferKit
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FactorCaller caller = new FactorCaller(ConfigurationHelper.ReadSetting("serverUrl"));

                var factorRespository = new FactorRespository();
              
                int serverMaxRefID = Int32.Parse(caller.GetMaxRefID());
                int clientFactorCount = factorRespository.Count();
                
                List<Factor> newFactors = factorRespository.GetItems(serverMaxRefID);
                Console.WriteLine("\n The factors sending process is starting...");
                if (newFactors.Count > 0)
                {
                    Console.WriteLine("\t {0} new factors will be sending ...", newFactors.Count);
                    foreach (Factor factor in newFactors)
                    {
                        IRestResponse response= caller.Create(factor);
                        Console.WriteLine("Factor id :{0} \n\t Successful {1} , Code {2} , Status {3}.", factor.refID , response.IsSuccessful ,factor.code ,  response.StatusCode);
                    }
                }
                else
                {
                    Console.WriteLine("\t There is not any factor to send.");
                }
                
                Console.WriteLine("\n Starting the simple verification process...");
                int serverFactorCount = 0;
                Int32.TryParse(caller.Count(),out serverFactorCount );
                if (serverFactorCount == clientFactorCount)
                {
                    Console.WriteLine("\t Verification is Successful. Server factors count is equal with client. Both of them : {0}",serverFactorCount.ToString());
                }
                else
                {
                    Console.WriteLine("\t *** Failure in verification.*** \n Server factors count {0} , but client factor count {1}", serverFactorCount.ToString(),clientFactorCount.ToString());
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error occured :\n {0}", ex.Message);
            }
        }

        /// <summary>
        /// Sample method to get factors.
        /// </summary>
        static void getFactors()
        {
            var caller = new FactorCaller(ConfigurationHelper.ReadSetting("serverUrl"));

            var factors = caller.GetItems();

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