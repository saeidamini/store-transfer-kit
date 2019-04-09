using System;
using StoreTransferKit.Transfer;

namespace StoreTransferKit
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
               FactorTransfer factorTransfer=new FactorTransfer();
               factorTransfer.SendItems();
               factorTransfer.Verify();
               
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error occured :\n {0}", ex.Message);
            }
        }

    }
}