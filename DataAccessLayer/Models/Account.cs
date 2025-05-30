﻿using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Frequency { get; set; } = null!;

    public DateOnly Created { get; set; }

    public decimal Balance { get; set; }

    public virtual ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<PermanentOrder> PermanentOrders { get; set; } = new List<PermanentOrder>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
