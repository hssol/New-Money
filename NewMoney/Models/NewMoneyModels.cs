using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewMoney.Models
{
    public class BitAccount
    {
        public int BitAccountId { get; set; }
        public int BitBalance { get; set; }
        
        public static BitAccount ConvertToMoney()
        {
            float conversionAmount = BitBalance / 100;
            return conversionAmount;
        }
        public static BitAccount ConvertToBits(int amountToConvert)
        {
            int conversionAmount = amountToConvert * 100;
            return conversionAmount;
        }
        public static BitAccount Remove(int amountToSend)
        {
            BitBalance -= amountToSend;
            return BitBalance;
        }
        public static BitAccount Add(int amountBought)
        {
            BitBalance += amountBought;
            return BitBalance;
        }
    }



    
}
