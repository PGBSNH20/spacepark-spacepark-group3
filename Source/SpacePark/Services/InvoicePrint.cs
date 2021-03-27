using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark.Services
{ 

        public static void InvoicePrint(Invoice invoice, int i = 0)
        {
            if (i % 1 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.WriteLine($"InvoiceID: {invoice.InvoiceID}\n " +
            $"Ship: {invoice.ShipName}\n " +
            $"StarteTime: {invoice.StarteTime}\n " +
            $"EndTime: {invoice.EndTime}\n " +
            $"TotalTime:{invoice.TotalTime}\n" +
            $"HourlyPrice:{invoice.HourlyPrice}\n" +
            $"TotalPrice:{invoice.TotalPrice}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

