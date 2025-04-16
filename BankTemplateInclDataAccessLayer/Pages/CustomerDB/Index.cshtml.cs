using Azure;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Services.Infrastructure.Paging;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB;

[Authorize(Roles = "Cashier, Admin")]

public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;
    public IndexModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
    }
    public List<CustomerViewModel> Customers { get; set; }


    public int PageNo { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string Q { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; } = 5;


    public void OnGet(
            string sortColumn,
            string sortOrder,
            int pageNo,
            int pageSize,
            string q
        )
    {
        Q = q; // Söktext

        SortColumn = sortColumn;
        SortOrder = sortOrder;

        if (pageNo == 0)
            pageNo = 1;
        PageNo = pageNo;

        if (pageSize == 0)
            pageSize = 5;
        PageSize = pageSize;

        var result = _customerService.GetCustomers(PageNo, PageSize, SortColumn, SortOrder, Q);

        PageCount = result.PageCount;

        Customers = result.Results
            .Select(c => new CustomerViewModel
            {
                CustomerId = c.CustomerId,
            })
            .ToList();
    }
}
