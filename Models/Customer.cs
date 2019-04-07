using System;

namespace StoreTransferKit.Models {
    public class Customer {
    // public int id { get; set; }
    public string lastName { get; set; }
    public string code { get; set; }
    public string address	    {get; set;}
    public string nationalCode	{get; set;}
    public DateTime birthDate	    {get; set;}
    public DateTime marriedDate	    {get; set;}
    public string mobile	    {get; set;}
    public string firstName	    {get; set;}
    public string email	        {get; set;}
    public string telephone	    {get; set;}
    public bool married	        {get; set;}
    }
}