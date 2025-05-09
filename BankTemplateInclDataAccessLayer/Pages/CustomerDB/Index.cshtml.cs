using Azure;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Services.Infrastructure.Paging;
using Services.Services;
using BankApp.ViewModels;

namespace BankApp.Pages.CustomerDB;

[Authorize(Roles = "Cashier, Admin")]

public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;
    public IndexModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    public List<CustomerViewModel> Customers { get; set; }


    public int PageNo { get; set; }
    public string SortColumn { get; set; }
    public string SortOrder { get; set; }
    public string Q { get; set; }

    public string SearchSubject { get; set; }

    public int PageCount { get; set; }
    public int PageSize { get; set; } = 50;


    public void OnGet(
            string sortColumn,
            string sortOrder,
            int pageNo,
            int pageSize,
            string q,
            string searchSubject
        )
    {
        Q = q; // Söktext
        SearchSubject = searchSubject; // Sökämne

        SortColumn = sortColumn;
        SortOrder = sortOrder;

        if (pageNo == 0)
            pageNo = 1;
        PageNo = pageNo;

        if (pageSize == 0)
            pageSize = 50;
        PageSize = pageSize;

        var result = _customerService.GetCustomers(PageNo, PageSize, SortColumn, SortOrder, Q, SearchSubject);

        PageCount = result.PageCount;

        Customers = result.Results
            .Select(c => new CustomerViewModel
            {
                ViewId = c.CustomerId,
                Givenname = c.Givenname,
                Surname = c.Surname,
                City = c.City,
                Streetaddress = c.Streetaddress,
                NationalId = c.NationalId,
            })
            .ToList();
    }
}
