using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CardService : ICardService
    {
        private readonly BankAppDataContext _dbContext;
        public CardService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Card> GetCardsByAccountId(int accountId)
        {
            return _dbContext.Cards.Where(c => c.Disposition.AccountId == accountId).ToList();
        }
    }
}
