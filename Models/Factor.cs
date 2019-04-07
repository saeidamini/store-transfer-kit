namespace StoreTransferKit.Models {
    public class Factor {
        public int ID { get; set; }
        public int refID { get; set; }
        public string code { get; set; }
        public decimal totalPrice { get; set; }
        public decimal totalDiscountPrice { get; set; } 
        public decimal totalTaxAddedValue { get; set; }
        public decimal amountPaid { get; set; } 
        public bool recycled { get; set; }
        public int numberItems { get; set; }
        public string salesDate { get; set; }
        // public DateTime createDate { get; set; }
        // public int customer_id {get; set;}
        public Customer customer {get;set;}

    }
}