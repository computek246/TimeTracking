using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeTracking
{
    public class Program
    {

        private static async Task Main(string[] args)
        {

            Console.Title = "Time Tracking";
            Console.WriteLine("Time Tracking");

            try
            {
                var now = DateTime.Now;

                var arg = Regex.Replace(string.Join(" ", args), @"\s+", " ");

                Console.WriteLine($"{arg} at: {now:dd/MM/yyyy hh:mm:ss tt}");

                var setting = new Setting();

                if (!string.IsNullOrEmpty(arg))
                {
                    await using var context = new TimeTrackingDbContext();

                    await ResilientTransaction.New(context)
                        .ExecuteAsync(async () =>
                        {
                            var log = new ActionsLog
                            {
                                ActionId = null,
                                ActionName = arg,
                                ActionDate = now
                            };

                            await context.SingleInsertAsync(log);
                            await context.BulkSaveChangesAsync();
                        });

                    setting = await context.Settings.FirstOrDefaultAsync(x =>
                        x.Name == SettingValues.GeneralSetting.LegalCopyright);
                }


                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(setting?.Value);
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
            }
            finally
            {

                Console.ResetColor();
                Console.WriteLine("Press any key to exit . . .");
                Console.ReadLine();
            }
        }
    }
}
