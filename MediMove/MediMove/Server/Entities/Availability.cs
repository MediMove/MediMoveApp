﻿namespace MediMove.Server.Entities
{
    public class Availability
    {
        public int Id { get; set; }
        public int PersonalInfoId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BankAccountNumber { get; set; }
        public decimal Cost { get; set; }

    }
}
